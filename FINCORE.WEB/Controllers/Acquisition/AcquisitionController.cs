using FINCORE.Models.Helpers;
using FINCORE.Models.Models;
using FINCORE.Models.Models.Acquisition.Paginations;
using FINCORE.Models.Models.Acquisition.PO;
using FINCORE.Models.Models.Acquisition.Reports;
using FINCORE.Models.Models.LDAP;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.Acquisition;
using FINCORE.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using static FINCORE.Services.Helpers.Response.Collection;
using static FINCORE.WEB.Helpers.Commons;

namespace FINCORE.WEB.Controllers.Acquisition
{
    public class AcquisitionController : Controller
    {
        private AcquisitionServices services = new AcquisitionServices();
        private TrCasServices casServices = new TrCasServices();
        private IWebHostEnvironment environment;
        private readonly string _FOLDER_TARGET = "\\Docs\\Temp\\";

        public AcquisitionController(IWebHostEnvironment environment)
                    => this.environment = environment;

        public IActionResult IndexMobil(int pageIndex, string searchTerm)
        {
            ViewBag.StateSend = HttpContext.Session.GetString("SuccessCM");
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);

            CreateReportSchema();

            var dataPaging = new PaginationModels<PaginationAcquisitionMobilModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPaginationAcquisitionMobil(sessBranchId, string.Empty, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationAcquisitionMobilModels>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationAcquisitionMobil(sessBranchId, searchTerm, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationAcquisitionMobilModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;

            return View("~/Views/Acquisition/Index.cshtml", dataPaging);
        }

        public IActionResult IndexMotor(int pageIndex, string searchTerm, string statusRO)
        {
            InitBindAsync();
            ViewBag.StateSend = HttpContext.Session.GetString("SuccessCM");
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);

            CreateReportSchema();

            var dataPaging = new PaginationModels<PaginationAcquisitionMotorModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || statusRO == null)
            {
                var data = services.GetPaginationAcquisitionMotor(sessBranchId, string.Empty, string.Empty, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationAcquisitionMotorModels>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationAcquisitionMotor(sessBranchId, searchTerm, statusRO, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationAcquisitionMotorModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;

            return View("~/Views/Acquisition/IndexMotor.cshtml", dataPaging);
        }

        [HttpPost]
        public IActionResult OpenCM(string poNo, string creditId, string itemId)
        {
            //var actionToUrl = "";
            object dataActionToUrl = new { actionUrl = "", parameters = new { creditId = "", isEdit = false, item = "" } };
            var res = services.OpenCM(poNo, creditId).Result;
            if (res.status == Status.SUCCESS)
            {
                //redirect to cas page edit
                //actionToUrl = Url.Action("GetTrCasByCreditId", "CAS"
                //        , new { creditId = creditId, isEdit = true, item = itemId });

                dataActionToUrl = new { actionUrl = Url.Action("GetTrCasByCreditId", "CAS"), parameters = new { creditId = creditId, isEdit = true, item = itemId } };
            }
            return Ok(dataActionToUrl);
        }

        /// <summary>
        /// Init fucntion for Load data function such as Dropdown, etc.
        /// </summary>
        private async void InitBindAsync()
        {
            var dataCreditSource = await casServices.GetCreditSource();

            ViewBag.CASSource = new SelectList((List<MsCreditSourceModels>)dataCreditSource.data, "credit_source_id", "credit_source_desc");
        }

        #region FTP Services

        public void UploadFileToFTP(string localTempPath, byte[] fileByte, string poNo, string fileName)
        {
            try
            {
                string year = DateTime.Now.Year.ToString();
                string period = DateTime.Now.ToString("yyyyMM");

                FTPService _ftpServices;
                _ftpServices = new($"ftp://{FTP.AcquisitionDoc.FTP_HOST}:{FTP.AcquisitionDoc.FTP_PORT}", FTP.AcquisitionDoc.FTP_USER_NAME, FTP.AcquisitionDoc.FTP_PASSWORD);
                _ftpServices.CreateFTPDirectory($"{FTP.AcquisitionDoc.FTP_PATH_PO_DOCUMENT}/{year}/");
                _ftpServices.CreateFTPDirectory($"{FTP.AcquisitionDoc.FTP_PATH_PO_DOCUMENT}/{year}/{period}/");
                _ftpServices.CreateFTPDirectory($"{FTP.AcquisitionDoc.FTP_PATH_PO_DOCUMENT}/{year}/{period}/{poNo}/");
                _ftpServices.CreateFileToFolder($"{FTP.AcquisitionDoc.FTP_PATH_PO_DOCUMENT}/{year}/{period}/{poNo}/{fileName}", fileByte);

                if (System.IO.File.Exists(Path.Combine(localTempPath)))
                {
                    System.IO.File.Delete(Path.Combine(localTempPath));
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        #endregion FTP Services

        #region RDLC Print Reports

        public void CreateReportSchema()
        {
            try
            {
                var types = new[] { typeof(PrintPOMobilModels), typeof(PrintPOMotorModels) };//put model in here
                var xri = new System.Xml.Serialization.XmlReflectionImporter();
                var xss = new System.Xml.Serialization.XmlSchemas();
                var xse = new System.Xml.Serialization.XmlSchemaExporter(xss);
                foreach (var type in types)
                {
                    var xtm = xri.ImportTypeMapping(type);
                    xse.ExportTypeMapping(xtm);
                }
                using var sw = new StreamWriter("ReportPOSchemas.xsd", false, Encoding.UTF8);
                for (int i = 0; i < xss.Count; i++)
                {
                    var xs = xss[i];
                    xs.Id = "ReportItemSchemas";
                    xs.Write(sw);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        public IActionResult OnPrintPOMobil(string poNo)
        {
            string year = DateTime.Now.Year.ToString();
            string period = DateTime.Now.ToString("yyyyMM");
            var userSession = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);

            var branchId = sessBranchId;
            var printId = userSession.EmployeeId;
            var targetRptName = "FINCORE.WEB.Views.Acquisition.CAS.Reports.PrintPOMobil.rdlc";
            byte[] bytePdf;

            var pofileName = poNo.Contains('/') ? poNo.Replace("/", "_") : poNo;
            var fileName = $"{pofileName}.{FileExtensions.PDF}";
            var wwwrootPath = $"{this.environment.WebRootPath}{_FOLDER_TARGET}";

            var filePath = Path.Combine(wwwrootPath, fileName);
            try
            {
                var dataSource = (List<PrintPOMobilModels>)services.PrintPOMobil(poNo, branchId, printId).Result.data;

                using var localReport = new Microsoft.Reporting.NETCore.LocalReport();
                Report.Load(localReport, dataSource.First(), targetRptName);
                bytePdf = localReport.Render(FileExtensions.PDF);

                CreateFolderOnDir(this.environment.WebRootPath, _FOLDER_TARGET);
                System.IO.File.WriteAllBytesAsync(filePath, bytePdf);

                UploadFileToFTP(filePath, bytePdf, poNo.Replace("/", ""), fileName);
                var outputFile = $"{FTP.AcquisitionDoc.FTP_PATH_PO_DOCUMENT}/{year}/{period}/{poNo}/";

                var isSend = InsertTrPoSendEmail(poNo, dataSource.First().dealer_code, fileName, outputFile);
                if (isSend.status == Status.SUCCESS)
                {
                    ViewBag.StateRoute = Status.SUCCESS;
                    ViewBag.Message = isSend.message;
                    ViewBag.UrlRoute = "/Acquisition/Index";
                }
                else
                {
                    ViewBag.StateRoute = Status.FAILED;
                    ViewBag.Message = isSend.message;
                    ViewBag.UrlRoute = "/Acquisition/Index";
                }
                //return File(bytePdf, MimeTypes.PDF, $"Copy_{fileName}");
                return IndexMobil(1, "");
            }
            catch (Exception ex)
            {
                var err = $"Failed to PrintPO Mobil. {ex.Message}";
                ViewBag.StateRoute = Status.FAILED;
                ViewBag.Message = err;
                ViewBag.UrlRoute = "/Acquisition/Index";
                return IndexMobil(1, "");
            }
        }

        public IActionResult OnPrintPOMotor(string poNo)
        {
            string year = DateTime.Now.Year.ToString();
            string period = DateTime.Now.ToString("yyyyMM");
            var userSession = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);

            var branchId = sessBranchId; //untuk test pake 00115
            var printId = userSession.DisplayName;
            var targetRptName = "FINCORE.WEB.Views.Acquisition.CAS.Reports.PrintPOMotor.rdlc";
            byte[] bytePdf;

            var pofileName = poNo.Contains('/') ? poNo.Replace("/", "_") : poNo;
            var fileName = $"{pofileName}.{FileExtensions.PDF}";
            var wwwrootPath = $"{this.environment.WebRootPath}{_FOLDER_TARGET}";

            var filePath = Path.Combine(wwwrootPath, fileName);

            try
            {
                var dataSource = (List<PrintPOMotorModels>)services.PrintPOMotor(poNo, branchId, printId).Result.data;

                using var localReport = new Microsoft.Reporting.NETCore.LocalReport();
                Report.Load(localReport, dataSource.First(), targetRptName);
                bytePdf = localReport.Render(FileExtensions.PDF);

                CreateFolderOnDir(this.environment.WebRootPath, _FOLDER_TARGET);
                System.IO.File.WriteAllBytesAsync(filePath, bytePdf);

                UploadFileToFTP(filePath, bytePdf, poNo.Replace("/", ""), fileName);
                var outputFile = $"{FTP.AcquisitionDoc.FTP_PATH_PO_DOCUMENT}/{year}/{period}/{poNo.Replace("/", "")}/";

                var isSend = InsertTrPoSendEmail(poNo, dataSource.First().dealer_code, fileName, outputFile);
                if (isSend.status == Status.SUCCESS)
                {
                    ViewBag.StateRoute = Status.SUCCESS;
                    ViewBag.Message = isSend.message;
                    ViewBag.UrlRoute = "/Acquisition/IndexMotor";
                }
                else
                {
                    ViewBag.StateRoute = Status.FAILED;
                    ViewBag.Message = isSend.message;
                    ViewBag.UrlRoute = "/Acquisition/IndexMotor";
                }
                return IndexMotor(1, "", "");
                //return File(bytePdf, MimeTypes.PDF, $"Copy_{fileName}");
            }
            catch (Exception ex)
            {
                var err = $"Failed to PrintPO Motor. {ex.Message} {ex.InnerException}";
                ViewBag.StateRoute = Status.FAILED;
                ViewBag.Message = err;
                ViewBag.UrlRoute = "/Acquisition/IndexMotor";

                return IndexMotor(1, "", "");
            }
        }

        #endregion RDLC Print Reports

        #region Custom Function Helpers

        private TrPoModels FindPoByCreditId(string creditId)
        {
            var data = (List<TrPoModels>)services.GetPoNoByCreditId(creditId).Result.data;
            return data.First();
        }

        /// <summary>
        /// Insert into table TrPoSendToEmail and exec Send Email, Update SumOfPrint TrPO.
        /// </summary>
        /// <param name="poNo"></param>
        /// <param name="dealerCode"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <exception cref="Exception"></exception>

        private APIResponse InsertTrPoSendEmail(string poNo, string dealerCode
                        , string fileName, string filePath)
        {
            var sendRepospone = new APIResponse();
            var userSession = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
            TrPoSendToEmailModels itemsPoEmail = new()
            {
                PoNo = poNo,
                CreatedOn = DateTime.Now,
                CreatedBy = userSession.EmployeeId, //from session login
                BranchId = sessBranchId, //from session login
                IsSend = false,
                DealerCode = dealerCode,
                FileName = fileName,
                FilePath = filePath,
                LastUpdatedBy = "",
                LastUpdatedOn = Commons.GetDefaultDatetime()
            };
            try
            {
                var res = services.InsertTrPoSendEmail(itemsPoEmail);
                if (res.Result.status == Status.SUCCESS)
                    if (res.Result.status == Status.SUCCESS)
                    {
                        //send email with execute sp
                        var sendMailRespons = (List<SendEmailPrintPOModels>)services.SendEmailPrintPO(poNo).Result.data;
                        var isSended = sendMailRespons.First();
                        if (isSended.SendStatus == SendingStatus.STATUS_SENDED)
                        {
                            var sumRes = services.UpdateSumPrintPO(poNo, userSession.EmployeeId);
                            sendRepospone = new APIResponse(Codes.CREATED, Status.SUCCESS, $"File PO: {poNo} berhasil terkirim. Filename: {fileName}", "");
                        }
                        else
                        {
                            sendRepospone = new APIResponse(Codes.INTERNAL_SERVER_ERROR
                                    , Status.FAILED, $"Failed to Send email. Send Status: {isSended.SendStatus}", "");
                            var sumRes = services.UpdateSumPrintPO(poNo, userSession.EmployeeId);
                            sendRepospone = new APIResponse(Codes.CREATED, Status.SUCCESS, $"File PO: {poNo} berhasil terkirim. Filename: {fileName}", "");
                        }
                    }
                    else
                    {
                        sendRepospone = new APIResponse(Codes.INTERNAL_SERVER_ERROR
                                    , Status.FAILED, $"Failed Insert into table TrPOSendEmail. Error: {res.Result.message}", "");
                    }
            }
            catch (Exception ex)
            {
                var d = ex.Message;
                sendRepospone = new APIResponse(Codes.INTERNAL_SERVER_ERROR
                                , Status.FAILED, ex.Message, "");
            }
            return sendRepospone;
        }

        private bool IsUserPrintPO()
        {
            //Position Id allowed to Print PO: 901, 902

            var isPrintPO = false;
            var sessUser = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

            var data = (List<PositionUserPrintPOModels>)services.CheckUserPositionPrintPO(sessUser.EmployeeId).Result.data;
            if (data is not null)
            {
                isPrintPO = true;
            }
            return isPrintPO;
        }

        #endregion Custom Function Helpers

        public IActionResult ApproveMobil(string trans_id)
        {
            return RedirectToAction("ApproveTrCas", "CAS", new { creditId = trans_id, item = "002" });
        }

        public IActionResult ApproveMotor(string trans_id)
        {
            return RedirectToAction("ApproveTrCas", "CAS", new { creditId = trans_id, item = "001" });
        }

        public IActionResult ViewMobil(string trans_id)
        {
            return RedirectToAction("ViewTrCas", "CAS", new { creditId = trans_id, item = "002" });
        }

        public IActionResult ViewMotor(string trans_id)
        {
            return RedirectToAction("ViewTrCas", "CAS", new { creditId = trans_id, item = "001" });
        }

        public IActionResult EditMobil(string trans_id)
        {
            return RedirectToAction("GetTrCasByCreditId", "CAS", new { creditId = trans_id, isEdit = true, item = "002" });
        }

        public IActionResult EditMotor(string trans_id)
        {
            return RedirectToAction("GetTrCasByCreditId", "CAS", new { creditId = trans_id, isEdit = true, item = "001" });
        }

    }
}