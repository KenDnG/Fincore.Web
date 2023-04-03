using FINCORE.Models.Models.Acquisition.CAS.ModelHelper;
using FINCORE.Models.Models.Masters;
using FINCORE.Services.Services.Acquisition;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace FINCORE.WEB.Helpers
{
    public class Commons
    {
        public static readonly int SESSION_EXPIRED_TIME = 1; //in Minutes

        public struct Paginations
        {
            public static readonly int MaxPerPage = 10;
            public static readonly int MaxPerPageLookup = 5;
            public static readonly int ScrollSpeed = 50;
            public static readonly int MaxRecords = 1000000;
        }

        public struct SessionKey
        {
            public static readonly string Branchid = "branch_id";
            public static readonly string User = "SessionUser";
        }

        public struct PAYMENT_TYPE
        {
            public static readonly int RENCANA_PEMBAYARAN = 1;
            public static readonly int LOKASI_PEMBAYARAN = 2;
        }

        /// <summary>
        /// Format nominal dengan pemisah titik(.)
        /// Contoh: 10.000.000
        /// </summary>
        /// <param name="nominal"></param>
        /// <returns></returns>
        public static string ConvertToNominal(int nominal)
        {
            return Convert.ToDecimal(nominal).ToString("N0");
        }

        /// <summary>
        /// 01/01/1900
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDefaultDatetime()
        {
            return Convert.ToDateTime("1/1/1900");
        }

        /// <summary>
        /// Upload Multiple physical file to Share Folder Network, Local Folder.
        /// </summary>
        /// <param name="destinationPath">Destination path location</param>
        /// <param name="formFile">Form File as type file</param>
        public static void UploadFileTo(string destinationPath, List<IFormFile> formFile)
        {
            for (int i = 0; i < formFile.ToList().Count; i++)
            {
                string filePath = Path.Combine(destinationPath, formFile[i].FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile[i].CopyTo(fileStream);
                }
            }
        }

        /// <summary>
        /// Upload Single physical file to Share Folder Network, Local Folder.
        /// </summary>
        /// <param name="destinationPath">Destination path location</param>
        /// <param name="formFile">Form File as type file</param>
        public static void UploadFileTo(string destinationPath, IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                string filePath = Path.Combine(destinationPath, formFile.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
            }
        }

        /// <summary>
        /// Upload Single physical file to FTP Server.
        /// </summary>
        /// <param name="ftpDestinationUrl">FTP Url</param>
        /// <param name="formFile">From file type</param>
        /// <param name="ftpCredentialUsername">FTP Credential Username</param>
        /// <param name="ftpCredentialPassword">FTP Credential Password</param>
        public static void UploadFileToFTP(string ftpDestinationUrl, IFormFile formFile, string ftpCredentialUsername, string ftpCredentialPassword)
        {
            if (formFile.Length > 0)
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpDestinationUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpCredentialUsername, ftpCredentialPassword);

                using (Stream fileStream = request.GetRequestStream())
                {
                    formFile.CopyTo(fileStream);
                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        Console.WriteLine($"Upload File Status: {response.StatusDescription}");
                    }
                }
            }
        }

        public static MsLocationDetailModels GetLoctionById(int locationId)
        {
            var casServices = new TrCasServices();
            var data = (List<MsLocationDetailModels>)casServices.GetMsLocationById(locationId).Result.data;
            return data.FirstOrDefault();
        }

        public static MsBankMasterModels GetBankMasterById(string bankId)
        {
            var casServices = new TrCasServices();
            var data = (List<MsBankMasterModels>)casServices.GetBankMaster().Result.data;
            var dt = data.ToList().Where(m => m.bank_id.Equals(bankId)).FirstOrDefault();
            if (dt == null)
            {
                return new MsBankMasterModels
                {
                    bank_id = "",
                    bank_name = ""
                };
            }
            else
            {
                return dt;
            }
        }

        public static string CombineLocationName(string villageName, string districtName, string regencyName, string provinceName, string zipCode)
        {
            return $"{villageName} {districtName} {regencyName} {provinceName} {zipCode}";
        }

        public static string GetGenderByNIK(string nik)
        {
            //pattern: 123456-110694-0004
            //var nik_laki = "1234561106940004";
            //var nik_perempuan = "1234566306940004";
            var gender = "";
            var x = nik.Remove(nik.Length - 8);
            var getBirthDate = x.Substring(x.Length - 2);

            if (Convert.ToInt32(getBirthDate) >= 40)
            {
                gender = "P"; //Female
            }
            else if (Convert.ToInt32(getBirthDate) >= 1 && Convert.ToInt32(getBirthDate) < 40)
            {
                gender = "L"; //Male
            }
            return gender;
        }

        #region Validate Email

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsEmail(string email)
        {
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        #endregion Validate Email

        /// <summary>
        /// Removing File in base on target path.
        /// </summary>
        /// <param name="fileName">filename with extension</param>
        /// <param name="filePath">target location path</param>
        public static void RemovingFile(string fileName, string filePath)
        {
            var mFile = Path.Combine(filePath, fileName);
            try
            {
                if (File.Exists(mFile))
                {
                    File.Delete(mFile);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        /// <summary>
        /// Check and Create Directory Folder if not exist.
        /// </summary>
        /// <param name="targetPath">path tagrget location</param>
        /// <param name="folderName">folder name will create</param>
        public static void CreateFolderOnDir(string targetPath, string folderName)
        {
            try
            {
                var path = $"{targetPath}{folderName}";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        public static string GenerateCreditID(string branchId)
        {
            TrCasServices services = new();
            var results = (List<CreditIDResultsModels>)services.GenerateCreditId(branchId).Result.data;
            return results.ToList().FirstOrDefault().Result;
        }

        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        public static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}