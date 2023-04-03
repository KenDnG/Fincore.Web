using FINCORE.Models.Helpers;
using FINCORE.Models.Models.Acquisition.Vertel;
using FINCORE.Models.Models.Acquisition.Vertel.Report;
using FINCORE.Models.Models.LDAP;
using FINCORE.Models.Models.Vertel;
using FINCORE.Models.Models.Vertel.Report;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Models.ViewModels;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.Acquisition;
using FINCORE.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using ServiceStack;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using static FINCORE.Services.Helpers.Response.Collection;
using static FINCORE.WEB.Helpers.Commons;

namespace FINCORE.WEB.Controllers.Acquisition
{
    public class VertelController : Controller
    {
        private readonly VertelServices services = new VertelServices();
        private readonly IWebHostEnvironment environment;

        public VertelController(IWebHostEnvironment environment)
                    => this.environment = environment;

        public IActionResult Index()
        {
            return View("~/Views/Acquisition/Vertel/ListedIndex.cshtml");
        }

        public IActionResult Add()
        {
            try
            {
                string CMNo = "0";
                var DocField = services.GetGetDocField(CMNo).Result;
                ViewBag.DocField = DocField.data;

                return View("~/Views/Acquisition/Vertel/Add.cshtml", new VertelModels());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;
                return RedirectToAction("List");
            }
        }

        public IActionResult Edit(string CMNo)
        {
            try
            {
                LDAPModels lDAPModels = HttpContext.Session.GetSessionFromJson<LDAPModels>("SessionUser");
                var employeeId = lDAPModels.EmployeeId;

                var dataVerifikasiKonsumen = services.GetDataVertifikasiKonsumen(CMNo, employeeId).Result;
                ViewBag.VerifikasiKonsumen = dataVerifikasiKonsumen.data;

                var DocField = services.GetGetDocField(CMNo).Result;
                ViewBag.DocField = DocField.data;
                return View("~/Views/Acquisition/Vertel/Edit.cshtml", new VertelModels());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;
                return RedirectToAction("List");
            }
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult ViewVertel(string CMNo, string trans_id)
        {
            try
            {
                CMNo = trans_id == null ? CMNo : trans_id;

                LDAPModels lDAPModels = HttpContext.Session.GetSessionFromJson<LDAPModels>("SessionUser");
                var employeeId = lDAPModels.EmployeeId;

                var dataVerifikasiKonsumen = services.GetDataVertifikasiKonsumen(CMNo, employeeId).Result;
                ViewBag.VerifikasiKonsumen = dataVerifikasiKonsumen.data;

                var DocField = services.GetGetDocField(CMNo).Result;
                ViewBag.DocField = DocField.data;
                ViewBag.Title = "VIEW";

                return View("~/Views/Acquisition/Vertel/View.cshtml", new VertelModels());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;
                return View("~/Views/Acquisition/Vertel/ListedIndex.cshtml");
            }
        }

        [HttpGet]
        public IActionResult GetData()
        {
            var data = services.GetDataVertel().Result.data;

            return Ok(data);
        }

        public IActionResult GetDocField(string CMNo)
        {
            APIResponse DocField = services.GetGetDocField(CMNo).Result;
            ViewBag.DocField = DocField.data;
            try
            {
                var data = (List<DocFieldModels>)DocField.data;
                var newdata = new VertelModels
                {
                    DocFieldModels = data
                };

                return PartialView("~/Views/Acquisition/Vertel/_DocForm.cshtml", newdata);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;
                return RedirectToAction("List");
            }
        }

        public IActionResult List(int pageIndex, string searchTerm)
        {
            string BranchId = GetSession<string>("branch_id");
            try
            {
                //CreateReportSchema(); //Uncomment when you on development
                _ = new PaginationModels<PagingVertelModels>();
                var page = pageIndex == 0 ? 1 : pageIndex;
                if (searchTerm == null)
                {
                    searchTerm = Request.Query["searchTerm"].ToString();
                }

                PaginationModels<PagingVertelModels>? dataPaging;
                if (searchTerm == null || searchTerm == string.Empty)
                {
                    var data = services.GetPagingList(string.Empty, BranchId, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PagingVertelModels>)data.Result.data;
                }
                else
                {
                    page = 1;
                    var data = services.GetPagingList(searchTerm, BranchId, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PagingVertelModels>)data.Result.data;
                }
                ViewBag.CurrentPage = page;
                ViewBag.PagingVertel = dataPaging;
                ViewBag.VertelModel = dataPaging.Model;

                return View("~/Views/Acquisition/Vertel/ListedIndex.cshtml");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        public void CreateReportSchema()
        {
            try
            {
                var types = new[] { typeof(PrintVertelHeader) };//put model in here
                var xri = new System.Xml.Serialization.XmlReflectionImporter();
                var xss = new System.Xml.Serialization.XmlSchemas();
                var xse = new System.Xml.Serialization.XmlSchemaExporter(xss);
                foreach (var type in types)
                {
                    var xtm = xri.ImportTypeMapping(type);
                    xse.ExportTypeMapping(xtm);
                }
                using var sw = new StreamWriter("ReportVertelSchemas.xsd", false, Encoding.UTF8);
                for (int i = 0; i < xss.Count; i++)
                {
                    var xs = xss[i];
                    xs.Id = "ReportVertelSchemas";
                    xs.Write(sw);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;
            }
        }

        [HttpPost]
        public IActionResult VertelLookUp(int pageIndex, string searchTerm)
        {
            string branchId = GetSession<string>("branch_id");
            try
            {
                ModelState.Clear();
                ViewModelVertelLookup VM = new ViewModelVertelLookup();
                var dataPaging = new PaginationModels<PagingVertelLookupModels>();
                var page = pageIndex == 0 ? 1 : pageIndex;
                if (searchTerm == null)
                {
                    searchTerm = Request.Query["searchTerm"].ToString();
                }
                if (searchTerm == null || searchTerm == string.Empty)
                {
                    var data = services.GetVertelLookupNPP(string.Empty, page, Paginations.MaxPerPageLookup, branchId);
                    dataPaging = (PaginationModels<PagingVertelLookupModels>)data.Result.data;
                }
                else
                {
                    page = 1;
                    var data = services.GetVertelLookupNPP(searchTerm, page, Paginations.MaxPerPage, branchId);
                    dataPaging = (PaginationModels<PagingVertelLookupModels>)data.Result.data;
                }

                VM.page = page;
                VM.paging = dataPaging;
                VM.data = dataPaging.Model;

                return PartialView("~/Views/Acquisition/Vertel/Lookup/PartialViewVertel.cshtml", VM);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public IActionResult PriceAwal(string creditId)
        {
            var dataPriceAwal = services.GetPriceAwal(creditId);
            ViewBag.PriceAwal = dataPriceAwal.Result.data;

            return PartialView("~/Views/Acquisition/Vertel/Lookup/PriceAwalView.cshtml", new ViewPriceAwal());
        }

        [HttpPost]
        public IActionResult Save([Bind] VertelModels item, FTPUpload fTPUpload)
        {
            LDAPModels lDAPModels = HttpContext.Session.GetSessionFromJson<LDAPModels>("SessionUser");
            item.CreatedBy = lDAPModels.EmployeeId;

            int count = 0;
            for (int i = item.DocFieldModels.Count - 1; i >= 0; i--)
            {
                if (item.DocFieldModels[i].isAvailable != true)
                {
                    ++count;
                    item.DocFieldModels.RemoveAt(i);
                }
            }

            item = ConvertDateRequestModel(item);

            try
            {
                string state;
                string msg;

                if (ModelState.IsValid)
                {
                    #region Upload

                    var pathFolderFtp = "";
                    if (item.DokumenIn != null)
                    {
                        for (int j = 0; j < item.DokumenIn.Count; j++)
                        {
                            for (int i = 0; i < item.DocFieldModels.Count; i++)
                            {
                                if (item.DocFieldModels[i].isAvailable != null && item.DocFieldModels[i].isAvailable != false && item.DocFieldModels[i].CreditId == null)
                                {
                                    string uniqueFileName = "Dokumen_" + item.Cmno + "_" + item.DocFieldModels[i].FieldName + Path.GetExtension(item.DokumenIn[j].FileName);
                                    string regex = "[!@#$%^&*-+=|:;\"'(),~`;]";
                                    uniqueFileName = uniqueFileName.Replace(">", "Lebih Dari ");
                                    uniqueFileName = uniqueFileName.Replace("<", "Kurang Dari ");
                                    uniqueFileName = Regex.Replace(Regex.Replace(Regex.Replace(uniqueFileName, "/", "_"), " ", "_"), regex, "_");

                                    pathFolderFtp = fTPUpload.UploadFile(environment, item.DokumenIn[j], FTP.AcquisitionDoc.FTP_PORT.ToString(),
                                                       FTP.AcquisitionDoc.FTP_HOST, FTP.AcquisitionDoc.FTP_USER_NAME, FTP.AcquisitionDoc.FTP_PASSWORD,
                                                       FTP.AcquisitionDoc.FTP_PATH_ACQUISITION_DOCUMENT, item.Cmno, uniqueFileName
                                                       );

                                    item.DocFieldModels[i].Path = pathFolderFtp;
                                    item.DocFieldModels[i].FileName = uniqueFileName;
                                    item.DocFieldModels[i].CreditId = item.Cmno;
                                }
                            }
                        }
                    }

                    #endregion Upload

                    #region Save or Edit

                    if (item.SaveType == "D" || item.SaveType == "0")
                    {
                        (state, msg) = InsertRazor(item);
                    }
                    else
                    {
                        (state, msg) = UpdateRazor(item);
                    }

                    if (state == Status.SUCCESS)
                    {
                        TempData["Success"] = msg;
                    }
                    else
                    {
                        if (msg.IsNullOrEmpty())
                        {
                            TempData["Error"] = "Something wrong, please try again!";
                        }
                        TempData["Error"] = msg;
                    }

                    #endregion Save or Edit

                    return RedirectToAction("List");
                }
                else
                {
                    var errorsCount = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                    var errorsMessage = string.Join(" | ", ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage));
                    string message;

                    if (errorsCount.Any())
                    {
                        message = errorsMessage;
                    }
                    else
                    {
                        message = "Sorry, one of the inputs data is invalid!";
                    }

                    TempData["Error"] = message;
                    return RedirectToAction("List");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;
                return RedirectToAction("List");
            }
        }

        private static VertelModels ConvertDateRequestModel(VertelModels item)
        {
            if (!item.strTglTerimaTagihan.IsNullOrEmpty())
                item.TglTerimaTagihan = DateTime.Parse(item.strTglTerimaTagihan
                                            , CultureInfo.CreateSpecificCulture("fr-FR")
                                            , DateTimeStyles.NoCurrentDateDefault);

            if (!item.strTglTerimaMotorSebenarnya.IsNullOrEmpty())
                item.OptTglTerimaMotorSebenarnya = DateTime.Parse(item.strTglTerimaMotorSebenarnya
                                            , CultureInfo.CreateSpecificCulture("fr-FR")
                                            , DateTimeStyles.NoCurrentDateDefault);

            if (!item.strTglKonfirmasi.IsNullOrEmpty())
                item.TglKonfirmasi = DateTime.Parse(DateTime.Now.ToString());
                //item.TglKonfirmasi = DateTime.Parse(item.strTglKonfirmasi
                //                            , CultureInfo.CreateSpecificCulture("fr-FR")
                //                            , DateTimeStyles.NoCurrentDateDefault);
            return item;
        }

        public (string state, string message) InsertRazor([Bind] VertelModels item)
        {
            string message = "";
            string state = "";
            item.CreatedOn = DateTime.Now;
            item.StatusVerifikasi = "";
            var data = services.InsertVertel(item);
            if (data.Result.status == Status.SUCCESS)
            {
                state = Status.SUCCESS;
            }
            else
            {
                state = Status.FAILED;
                message = data.Result.message;
            }
            return (state, message);
        }

        public (string state, string message) UpdateRazor([Bind] VertelModels item)
        {
            string message = "";
            string state = "";
            item.CreatedOn = DateTime.Now;

            var data = services.UpdateVertel(item);
            if (data.Result.status == Status.SUCCESS)
            {
                state = Status.SUCCESS;
                message = Status.SUCCESS;
            }
            else
            {
                state = Status.FAILED;
                message = data.Result.message;
            }

            return (state, message);
        }

        #region DownloadFile

        public async Task<IActionResult> DownloadFile(string path, string FileName, int IsNewZoom)
        {
            var wwwrootPath = Path.Combine(this.environment.WebRootPath, "FileUpload\\AcquisitionDocument\\");
            if (!Directory.Exists(wwwrootPath))
            {
                Directory.CreateDirectory(wwwrootPath);
            }

            try
            {
                string localPath = Path.Combine(wwwrootPath, FileName);
                string remoteFtpPath = path;

                if (System.IO.File.Exists(localPath))
                {
                    System.IO.File.Delete(localPath);
                }

                WebClient client = new WebClient();
                if (!remoteFtpPath.Contains(FTP.AcquisitionDoc_NewZoom.FTP_HOST_NEW_ZOOM) && IsNewZoom == 1)
                {
                    client.Credentials = new NetworkCredential(FTP.AcquisitionDoc_NewZoom.FTP_USER_NAME_NEW_ZOOM, FTP.AcquisitionDoc_NewZoom.FTP_PASSWORD_NEW_ZOOM);
                    remoteFtpPath = $"ftp://{FTP.AcquisitionDoc_NewZoom.FTP_HOST_NEW_ZOOM}:{FTP.AcquisitionDoc_NewZoom.FTP_PORT_NEW_ZOOM}{remoteFtpPath}";
                    client.DownloadFile(remoteFtpPath, localPath);
                }
                else if (!remoteFtpPath.Contains(FTP.AcquisitionDoc.FTP_HOST) && IsNewZoom != 1)
                {
                    client.Credentials = new NetworkCredential(FTP.AcquisitionDoc.FTP_USER_NAME, FTP.AcquisitionDoc.FTP_PASSWORD);
                    remoteFtpPath = $"ftp://{FTP.AcquisitionDoc.FTP_HOST}:{FTP.AcquisitionDoc.FTP_PORT}{remoteFtpPath}";
                    client.DownloadFile(remoteFtpPath, localPath);
                }
                else
                {
                    client.Credentials = new NetworkCredential(FTP.AcquisitionDoc.FTP_USER_NAME, FTP.AcquisitionDoc.FTP_PASSWORD);
                    client.DownloadFile(remoteFtpPath, localPath);
                }

                byte[] bytes = System.IO.File.ReadAllBytes(localPath);

                return File(bytes, GetContentType(path), Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;
                return RedirectToAction("List");
            }
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
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

        #endregion DownloadFile

        #region LDAP

        private T GetSession<T>(string sesseionKey)
        {
            return HttpContext.Session.GetSessionFromJson<T>(sesseionKey);
        }

        #endregion LDAP

        #region Approval

        [AcceptVerbs("Get", "Post")]
        public IActionResult ViewVertelApproval(string trans_id)
        {
            try
            {
                LDAPModels lDAPModels = HttpContext.Session.GetSessionFromJson<LDAPModels>("SessionUser");
                var employeeId = lDAPModels.EmployeeId;
                var dataVerifikasiKonsumen = services.GetDataVertifikasiKonsumen(trans_id, employeeId).Result;
                ViewBag.VerifikasiKonsumen = dataVerifikasiKonsumen.data;

                var DocField = services.GetGetDocField(trans_id).Result;
                ViewBag.DocField = DocField.data;
                ViewBag.Title = "APPROVAL";

                return View("~/Views/Acquisition/Vertel/View.cshtml", new VertelModels());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        public IActionResult GetApprovalReason(string typeApproval)
        {
            var dataApproval = services.GetDataApproverType(typeApproval).Result;
            ViewBag.DataApproval = dataApproval.data;

            return PartialView("~/Views/Acquisition/Vertel/Lookup/ApprovalVetrtelView.cshtml", new VertelApprovalModels());
        }

        [HttpPost]
        public IActionResult ApprovalProcess(VertelApprovalModels data)
        {
            LDAPModels lDAPModels = HttpContext.Session.GetSessionFromJson<LDAPModels>("SessionUser");
            data.EmployeeId = lDAPModels.EmployeeId;

            try
            {
                var dataApproval = services.ApprovalProccess(data);
                if (dataApproval.Result.status == Status.SUCCESS)
                {
                    TempData["Success"] = Status.SUCCESS;
                }
                else
                {
                    TempData["Error"] = dataApproval.Result.message;
                }

                return RedirectToAction("Inbox", "Approval");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;
                return RedirectToAction("Inbox", "Approval");
            }
        }

        #endregion Approval

        #region Print

        public IActionResult PrinVerfikasiKonsumen(string transId) => PrepareReport("PDF", "pdf", "application/pdf", "PrintVerifikasiKonsumen", transId);

        public IActionResult PrepareReport(string renderFormat, string extension, string mimeType, string PrintType, string transId)
        {
            try
            {
                using var report = new LocalReport();
                var userSession = HttpContext.Session.GetSessionFromJson<LDAPModels>(SessionIdentity.LDAP_KEY_NAME);
                var sessBranchId = HttpContext.Session.GetSessionFromJson<string>(SessionIdentity.BRANCH_ID);
                var employeeId = userSession.EmployeeId;
                byte[] bytePdf;
                string reportName = "FINCORE.WEB.Views.Acquisition.Vertel.Reports." + PrintType + ".rdlc";

                #region List Dokumen

                PrintVertelHeader reportData = new PrintVertelHeader();
                List<PrintVertelModels> printVertelField = (List<PrintVertelModels>)services.PrintVerificationCustomer(transId, employeeId, sessBranchId).Result.data;
                List<PrintVertelDokumen> dataPrintVertelDokumens = (List<PrintVertelDokumen>)services.PrintVerificationCustomerDokumen(transId).Result.data;

                reportData.PrintVertelField = printVertelField;
                reportData.PrintVertelDokumens = dataPrintVertelDokumens;
                LoadDokumen(report, reportData, reportName);

                #endregion List Dokumen

                bytePdf = report.Render(renderFormat);

                return File(bytePdf, "application/pdf", "Report Verification Customer_" + transId + "." + extension);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something wrong, please try again later! " + ex.Message + ". " + ex.InnerException?.Message;
                return RedirectToAction("List");
            }
        }

        public void LoadDokumen(LocalReport localReport, PrintVertelHeader data, string reportName)
        {
            using var rs = Assembly.GetExecutingAssembly().GetManifestResourceStream(reportName);
            localReport.LoadReportDefinition(rs);

            localReport.DataSources.Add(new ReportDataSource("Items", data.PrintVertelField));
            localReport.DataSources.Add(new ReportDataSource("DokumenIn", data.PrintVertelDokumens));
        }

        #endregion Print
    }
}