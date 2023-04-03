using System.Data;
using System.Net;

namespace FINCORE.WEB.Helpers
{
    public class FTPService
    {
        public WebResponse resp;

        private char csvDelimiter = '|'; //ConfigurationManager.AppSettings["CsvDelimiter"].ToCharArray()[0];

        private readonly string _url;
        private readonly string _ftpUserId;
        private readonly string _ftpPassword;

        private FtpWebRequest _ftpReq;

        /// <summary>
        ///  Ftp credential configuration
        /// </summary>
        /// <param name="url">address ftp</param>
        /// <param name="ftpUserId">User login</param>
        /// <param name="ftpPassword">password</param>
        public FTPService(string url, string ftpUserId, string ftpPassword)
        {
            _url = url;
            _ftpUserId = ftpUserId;
            _ftpPassword = ftpPassword;

            _ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(url));
            _ftpReq.UseBinary = true;
            _ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
            _ftpReq.Credentials = new NetworkCredential(ftpUserId, ftpPassword);
            _ftpReq.Proxy = null;
            _ftpReq.EnableSsl = true;
        }

        /// <summary>
        /// Ftp credential configuration
        /// </summary>
        /// <param name="url">address ftp</param>
        /// <param name="fileName">File name</param>
        /// <param name="ftpUserId">User login</param>
        /// <param name="ftpPassword">Password</param>
        public FTPService(string url, string fileName, string ftpUserId, string ftpPassword)
        {
            _ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(url + fileName));
            _ftpReq.UseBinary = true;
            _ftpReq.Credentials = new NetworkCredential(ftpUserId, ftpPassword);
            _ftpReq.Proxy = null;
        }

        /// <summary>
        /// Init for create, copy, move
        /// </summary>
        /// <param name="fileName">File name</param>
        public void InitFile(string fileName)
        {
            _ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(_url + fileName));
            _ftpReq.UseBinary = true;
            _ftpReq.Credentials = new NetworkCredential(_ftpUserId, _ftpPassword);
            _ftpReq.Proxy = null;
        }

        /// <summary>
        /// Get all file in folder
        /// </summary>
        /// <returns>File list in folder</returns>
        public List<string> GetAllFile()
        {
            _ftpReq.Method = WebRequestMethods.Ftp.ListDirectory;
            resp = _ftpReq.GetResponse();
            var fileList = new List<string>();

            // ReSharper disable once AssignNullToNotNullAttribute
            var reader = new StreamReader(resp.GetResponseStream());

            string line = reader.ReadLine();

            while (line != null)
            {
                string[] lines = line.Split('/');
                string fileLine = lines[lines.Length - 1];
                fileList.Add(fileLine);
                line = reader.ReadLine();
            }

            reader.Close();
            resp.Close();

            return fileList;
        }

        /// <summary>
        /// Get contents of file
        /// </summary>
        /// <param name="fileName">filename</param>
        /// <returns>Contents of file</returns>
        public List<string> GetFileContents(string fileName)
        {
            InitFile(fileName);

            _ftpReq.Method = WebRequestMethods.Ftp.DownloadFile;
            WebResponse response = _ftpReq.GetResponse();

            var fileContents = new List<string>();

            // ReSharper disable once AssignNullToNotNullAttribute
            var reader = new StreamReader(response.GetResponseStream());

            string content = reader.ReadLine();
            while (content != null)
            {
                fileContents.Add(content);
                try
                {
                    content = reader.ReadLine();
                }
                catch
                {
                    content = null;
                }
            }

            // Close reader and response
            reader.Close();
            reader.Dispose();
            response.Close();
            response.Dispose();

            return fileContents;
        }

        /// <summary>
        /// Read file contents to dataset
        /// </summary>
        /// <param name="filePath">fileName for read</param>
        /// <returns>dataset contents of file</returns>
        public object ReadFileContents(string filePath, bool hasColumnName)
        {
            DataTable dtDataSource = new DataTable();
            var fileContents = GetFileContents(filePath).ToArray();

            if (fileContents.Length > 0)
            {
                // varable for handle duplicate column
                bool isDuplicated = false;
                int duplicateIndex = fileContents.Length;
                List<string> columnsAdded = new List<string>();

                //Create data table columns
                string[] columns = fileContents[0].Split(csvDelimiter);
                for (int i = 0; i < columns.Length; i++)
                {
                    string columnName = hasColumnName ? columns[i] : ("Column" + i);

                    if (columnsAdded.Contains(columnName))
                    {
                        duplicateIndex = i;
                        isDuplicated = true;
                        break;
                    }

                    dtDataSource.Columns.Add(columnName);
                    columnsAdded.Add(columnName);
                }

                //Add row data
                for (int i = hasColumnName ? 1 : 0; i < fileContents.Length; i++)
                {
                    string[] rowData = fileContents[i].Split(csvDelimiter);
                    if (isDuplicated)
                        rowData = rowData.Where((val, idx) => idx != duplicateIndex).ToArray();
                    dtDataSource.Rows.Add(rowData);
                }
            }
            return dtDataSource;
        }

        /// <summary>
        /// Convert file of ftp to streamreader
        /// </summary>
        /// <param name="fileName">fileName on FTP</param>
        /// <returns>file stream</returns>
        public Stream GetStream(string fileName)
        {
            try
            {
                InitFile(fileName);

                _ftpReq.Method = WebRequestMethods.Ftp.DownloadFile;
                resp = _ftpReq.GetResponse();
            }
            catch (Exception)
            {
                return null;
            }

            //// ReSharper disable once AssignNullToNotNullAttribute
            //var reader = new StreamReader(response.GetResponseStream());

            return resp.GetResponseStream();
        }

        /// <summary>
        /// Create Ftp Directory
        /// </summary>
        /// <param name="directory">directory will be created</param>
        /// <returns>success if create directory to ftp success</returns>
        public bool CreateFTPDirectory(string directory)
        {
            try
            {
                //create the directory
                InitFile(directory);
                _ftpReq.Method = WebRequestMethods.Ftp.MakeDirectory;
                _ftpReq.UseBinary = true;
                _ftpReq.KeepAlive = false;
                resp = (FtpWebResponse)_ftpReq.GetResponse();
                Stream ftpStream = resp.GetResponseStream();

                ftpStream.Close();
                ftpStream.Dispose();
                resp.Close();
                resp.Dispose();

                return true;
            }
            catch (WebException ex)
            {
                resp = (FtpWebResponse)ex.Response;
                if (((FtpWebResponse)resp).StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    resp.Close();
                    return true;
                }
                else
                {
                    resp.Close();
                    return false;
                }
            }
        }

        /// <summary>
        /// Create file on ftp
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <param name="fileContent">Content of file</param>
        //public void CreateFileToFolder(string fileName, string fileContent)
        //{
        //    InitFile(fileName);

        //    _ftpReq.KeepAlive = false;
        //    _ftpReq.ContentLength = fileContent.Length;
        //    _ftpReq.Method = WebRequestMethods.Ftp.UploadFile;

        //    using (var tempStream = _ftpReq.GetRequestStream())
        //    {
        //        var tempEncoding = new ASCIIEncoding();
        //        var tempBytes = tempEncoding.GetBytes(fileContent);
        //        tempStream.Write(tempBytes, 0, tempBytes.Length);
        //    }

        //    resp = _ftpReq.GetResponse();
        //}
        public void CreateFileToFolder(string fileName, byte[] fileContent)
        {
            try
            {
                InitFile(fileName);

                _ftpReq.KeepAlive = false;
                _ftpReq.ContentLength = fileContent.Length;
                _ftpReq.Method = WebRequestMethods.Ftp.UploadFile;

                using (Stream request_stream = _ftpReq.GetRequestStream())
                {
                    request_stream.Write(fileContent, 0, fileContent.Length);
                    request_stream.Close();
                }

                //using (var tempStream = _ftpReq.GetRequestStream())
                //{
                //    var tempEncoding = new ASCIIEncoding();
                //    var tempBytes = tempEncoding.GetBytes(fileContent);
                //    tempStream.Write(tempBytes, 0, tempBytes.Length);
                //}
            }
            catch (WebException ex)
            {
                String status = ((FtpWebResponse)ex.Response).StatusDescription;
            }

            resp = _ftpReq.GetResponse();
        }

        /// <summary>
        /// Move file path
        /// </summary>
        /// <param name="source">source path</param>
        /// <param name="destination">folder destination</param>
        public void MoveFile(string source, string destination)
        {
            InitFile(source);
            string fileName = GetFileName(source);

            _ftpReq.Method = WebRequestMethods.Ftp.Rename;
            _ftpReq.RenameTo = destination + fileName;
            resp = _ftpReq.GetResponse();
            Close();
        }

        /// <summary>
        /// Get file name from directory/filename.extension
        /// </summary>
        /// <param name="source">path</param>
        /// <returns>filename</returns>
        private string GetFileName(string source)
        {
            var sources = source.Split('/');

            return sources[sources.Count() - 1];
        }

        /// <summary>
        /// Close ftpresp
        /// </summary>
        public void Close()
        {
            if (resp != null)
            {
                resp.Close();
                resp.Dispose();
                resp = null;
            }
        }

        //public void UploadSFTPFile(string host, string username, string password, int port, string sourcefile, string destinationpath, string destinationpath_1, string destinationpath_2)
        //{
        //    using (SftpClient client = new SftpClient(host, port, username, password))
        //    {
        //        client.Connect();

        //        if (client.Exists(destinationpath_1) == false)
        //        {
        //            client.CreateDirectory(destinationpath_1);
        //        }

        //        if (client.Exists(destinationpath_2) == false)
        //        {
        //            client.CreateDirectory(destinationpath_2);
        //        }

        //        client.ChangeDirectory(destinationpath);

        //        using (FileStream fs = new FileStream(sourcefile, FileMode.Open))
        //        {
        //            client.BufferSize = 4 * 1024;
        //            client.UploadFile(fs, Path.GetFileName(sourcefile));
        //        }
        //    }
        //}
    }
}