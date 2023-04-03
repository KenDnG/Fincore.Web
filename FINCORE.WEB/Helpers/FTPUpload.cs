namespace FINCORE.WEB.Helpers
{
    public class FTPUpload
    {
        public string UploadFile(IWebHostEnvironment environment, IFormFile fileUpload
            , string port, string host, string username, string password, string ftp_path
            , string credit_id, string unique_file_name)
        {
            long length = fileUpload.Length;
            using var fileStream = fileUpload.OpenReadStream();
            byte[] bytesFile = new byte[length];
            fileStream.Read(bytesFile, 0, (int)fileUpload.Length);
            string year = DateTime.Now.Year.ToString();
            string period = DateTime.Now.ToString("yyyyMM");

            FTPService _ftpService;
            _ftpService = new FTPService("ftp://" + host + ":" + port, username, password);
            _ftpService.CreateFTPDirectory(ftp_path + "/" + year + "/");
            _ftpService.CreateFTPDirectory(ftp_path + "/" + year + "/" + period + "/");
            _ftpService.CreateFTPDirectory(ftp_path + "/" + year + "/" + period + "/" + credit_id + "/");
            _ftpService.CreateFileToFolder(ftp_path + "/" + year + "/" + period + "/" + credit_id + "/" + unique_file_name, bytesFile);

            var pathFolderFtp = "ftp://" + host + ":" + port + "/" + ftp_path + "/" + year + "/" + period + "/" + credit_id + "/";

            return pathFolderFtp;
        }
    }
}