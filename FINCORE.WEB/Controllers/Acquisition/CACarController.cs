using FINCORE.Models.Models.Acquisition.CA;
using FINCORE.Models.Models.LDAP;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.Acquisition;
using FINCORE.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using System.Net;
using static FINCORE.Services.Helpers.Response.Collection;
using FTP = FINCORE.Models.Helpers.FTP;

namespace FINCORE.WEB.Controllers.Acquisition
{
    public class CACarController : Controller
    {
        private CACarServices services = new CACarServices();

        private IWebHostEnvironment environment;

        private static string host = FTP.AcquisitionDoc.FTP_HOST;
        private static string username = FTP.AcquisitionDoc.FTP_USER_NAME;
        private static string password = FTP.AcquisitionDoc.FTP_PASSWORD;
        private static int port = FTP.AcquisitionDoc.FTP_PORT;
        private static string ftp_path = FTP.AcquisitionDoc.FTP_PATH_ACQUISITION_DOCUMENT;

        private static string host_NewZoom = FTP.AcquisitionDoc_NewZoom.FTP_HOST_NEW_ZOOM;
        private static string username_NewZoom = FTP.AcquisitionDoc_NewZoom.FTP_USER_NAME_NEW_ZOOM;
        private static string password_NewZoom = FTP.AcquisitionDoc_NewZoom.FTP_PASSWORD_NEW_ZOOM;
        private static int port_NewZoom = FTP.AcquisitionDoc_NewZoom.FTP_PORT_NEW_ZOOM;
        private static string ftp_path_NewZoom = FTP.AcquisitionDoc_NewZoom.FTP_PATH_ACQUISITION_DOCUMENT_NEW_ZOOM;

        public CACarController(IWebHostEnvironment environment)
                   => this.environment = environment;

        [HttpGet]
        public IActionResult CACar()
        {
            HttpContext.Session.SetString("SuccessCM", "");

            ViewBag.State = TempData["Success"];
            ViewBag.Message = TempData["Message"];

            var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            var Flag = HttpContext.Session.GetString("Flag");
            LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

            if (Request.Query["credit_id"].ToString() != "")
            {
                credit_id = Request.Query["credit_id"].ToString();
                Flag = Request.Query["Flag"].ToString();
                HttpContext.Session.SetString("credit_id", credit_id);
                HttpContext.Session.SetString("Flag", Flag);
            }

            if (Flag == "Edit" || Flag == "Correction")
            {
                Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.ISEDIT_KEY_NAME, true);
            }
            else
            {
                Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.ISEDIT_KEY_NAME, false);
            }

            CACarModels CACar = new CACarModels();

            if (Flag != "Add")
            {
                var CACardata = services.Get_ca_car_detail(credit_id, lDAPModels.EmployeeId).Result;
                ViewBag.CACarModels = CACardata.data;
            }

            var ms_photo_type = services.Get_ms_photo_type(credit_id).Result;
            ViewBag.ms_photo_type = ms_photo_type.data;

            if (Flag == "Approval")
            {
                var ApprReason = services.Get_ApprovalReason(lDAPModels.EmployeeId, "-").Result;
                ViewBag.ApprReason = ApprReason.data;

                var HistoryApproval = services.Get_History_Approval(credit_id).Result;
                ViewBag.HistoryApproval = HistoryApproval.data;
            }

            return View("~/Views/Acquisition/CA/CACar.cshtml", CACar);
        }

        [HttpGet]
        public IActionResult Get_Tr_CM_photo_detail(string credit_id, string photo_type_id, string photo_id)
        {
            var data = services.Get_Tr_CM_photo_detail(credit_id, photo_type_id, photo_id).Result;
            return Ok(data.data);
        }

        [HttpPost]
        public IActionResult SaveCMMotor_PhotoDetail(string photo_type_id, IFormFile fileUpload, string photo_id, string flagAction)
        {
            TempData["Success"] = "";
            TempData["Message"] = "";

            ViewBag.State = Collection.Status.SUCCESS;
            HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);

            if (photo_type_id != null)
            {
                try
                {
                    var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);

                    //upload FTP
                    string uploadRoot = Path.Combine(this.environment.WebRootPath, "/FileUpload/AcquisitionDocument/");

                    string uniqueFileName = "Foto_Detail_" + credit_id + "_" + photo_type_id + "_" + photo_id + Path.GetExtension(fileUpload.FileName);
                    string filePath = System.IO.Path.Combine(this.environment.WebRootPath + "\\FileUpload\\AcquisitionDocument\\", uniqueFileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        fileUpload.CopyToAsync(stream);
                    }

                    byte[] bytesFile = System.IO.File.ReadAllBytes(filePath);
                    string year = DateTime.Now.Year.ToString();
                    string period = DateTime.Now.ToString("yyyyMM");

                    FTPService _ftpService;
                    _ftpService = new FTPService("ftp://" + host + ":" + port, username, password);
                    _ftpService.CreateFTPDirectory(ftp_path + "/" + year + "/");
                    _ftpService.CreateFTPDirectory(ftp_path + "/" + year + "/" + period + "/");
                    _ftpService.CreateFTPDirectory(ftp_path + "/" + year + "/" + period + "/" + credit_id + "/");
                    _ftpService.CreateFileToFolder(ftp_path + "/" + year + "/" + period + "/" + credit_id + "/" + uniqueFileName, bytesFile);

                    if (System.IO.File.Exists(Path.Combine(filePath)))
                    {
                        System.IO.File.Delete(Path.Combine(filePath));
                    }
                    //end upload FTP

                    LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

                    ViewModelCAPhotoType data = new ViewModelCAPhotoType();
                    data.credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
                    data.CreatedBy = lDAPModels.EmployeeId;
                    data.LastUpdatedBy = lDAPModels.EmployeeId;

                    data.photo_type_id = photo_type_id;
                    data.photo_id = photo_id;
                    data.filename = uniqueFileName;
                    data.filePath = ftp_path + "/" + year + "/" + period + "/" + credit_id + "/";

                    var save = services.InsertCMPhotoDetail(data);

                    if (save.Result.status == Collection.Status.SUCCESS)
                    {
                        TempData["Success"] = Collection.Status.SUCCESS;
                        HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);

                        ViewBag.State = Collection.Status.SUCCESS;
                    }
                    else
                    {
                        TempData["Success"] = Collection.Status.FAILED;
                        TempData["Message"] = save.Result.message;

                        ViewBag.State = Collection.Status.FAILED;
                        ViewBag.Message = save.Result.message;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return View(ViewBag.State);
        }

        [HttpPost]
        public IActionResult SaveCMOAnalisa(string Capacity, string Capital, string Character, string Condition, string Collateral, string advantage_notes, string deficiency_notes, string flagAction)
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["Success"] = "";
            TempData["Message"] = "";

            CACarModels data = new CACarModels();
            LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

            data.credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            data.CreatedBy = lDAPModels.EmployeeId;
            data.LastUpdatedBy = lDAPModels.EmployeeId;

            data.Capacity = Capacity;
            data.Capital = Capital;
            data.Character = Character;
            data.Condition = Condition;
            data.Collateral = Collateral;
            data.advantage_notes = advantage_notes;
            data.deficiency_notes = deficiency_notes;
            data.StatusApproval = flagAction;

            var save = services.InsertCMOAnalisa(data);

            if (save.Result.status == Collection.Status.SUCCESS)
            {
                TempData["Success"] = Collection.Status.SUCCESS;
                HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);

                ViewBag.State = Collection.Status.SUCCESS;
            }
            else
            {
                TempData["Success"] = Collection.Status.FAILED;
                TempData["Message"] = save.Result.message;

                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = save.Result.message;
            }

            return View(ViewBag.State);
        }

        public FileResult DownloadFile()
        {
            var photo_type_id = Request.Query["photo_type_id"].ToString();
            var photo_id = Request.Query["photo_id"].ToString();
            var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            var is_new_zoom = Request.Query["is_new_zoom"].ToString();

            var CMPhotoTypeModels = services.Get_Tr_CM_photo_detail(credit_id, photo_type_id, photo_id).Result;

            string fileName = "";
            string filePath = "";

            List<CACarPhotoTypeModels> data = (List<CACarPhotoTypeModels>)services.Get_Tr_CM_photo_detail(credit_id, photo_type_id, photo_id).Result.data;
            if (data.Count > 0)
            {
                fileName = data[0].filename;
                filePath = data[0].filePath;
            }

            //download from ftp
            WebClient client = new WebClient();

            String RemoteFtpPath = "";
            String LocalDestinationPath = "";

            if (is_new_zoom == "1")
            {
                client.Credentials = new NetworkCredential(username_NewZoom, password_NewZoom);
                RemoteFtpPath = "ftp://" + host_NewZoom + ":" + port_NewZoom + "//" + filePath + fileName;
            }
            else
            {
                client.Credentials = new NetworkCredential(username, password);
                RemoteFtpPath = "ftp://" + host + ":" + port + "//" + filePath + fileName;
            }

            LocalDestinationPath = Path.Combine(this.environment.WebRootPath, "/FileUpload/AcquisitionDocument/" + "file_" + fileName);
            client.DownloadFile(RemoteFtpPath, LocalDestinationPath);

            //download from local folder
            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(LocalDestinationPath);
            //Send the File to Download.
            return File(bytes, "application/octet-stream", "file_" + fileName);

            if (System.IO.File.Exists(LocalDestinationPath))
            {
                System.IO.File.Delete(LocalDestinationPath);
            }
        }

        public IActionResult ActionBack()
        {
            HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);
            return RedirectToAction("IndexMobil", "Acquisition");
        }

        public IActionResult BackToCMCar()
        {
            var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            var Flag = HttpContext.Session.GetString("Flag");

            if (Flag == "Add" || Flag == "Edit" || Flag == "Correction")
            {
                return RedirectToAction("CMCar", "CMCar");
            }
            else
            {
                return RedirectToAction("CMCar", "CMCar", new { trans_id = credit_id });
            }
        }

        public IActionResult Approve(CACarModels modelca)
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["Success"] = "";
            TempData["Message"] = "";

            LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

            modelca.StatusApproval = "A";
            modelca.CreatedBy = lDAPModels.EmployeeId;
            modelca.LastUpdatedBy = lDAPModels.EmployeeId;

            var save = services.ApproveCM(modelca);

            if (save.Result.status == Collection.Status.SUCCESS)
            {
                TempData["Success"] = Collection.Status.SUCCESS;
                HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);

                ViewBag.State = Collection.Status.SUCCESS;

                var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
                POModels PO = new POModels();
                var POData = services.Get_PO_No(credit_id).Result;
                return RedirectToAction("OnPrintPOMobilfromApproval", "Acquisition", new { poNo = POData.data });

                //return RedirectToAction("Inbox", "Approval");
            }
            else
            {
                TempData["Success"] = Collection.Status.FAILED;
                TempData["Message"] = save.Result.message;

                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = save.Result.message;

                return RedirectToAction("CACar", "CACar");
            }
        }

        public IActionResult Reject(CACarModels modelca)
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["Success"] = "";
            TempData["Message"] = "";

            LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

            modelca.StatusApproval = "R";
            modelca.CreatedBy = lDAPModels.EmployeeId;
            modelca.LastUpdatedBy = lDAPModels.EmployeeId;

            var save = services.ApproveCM(modelca);

            if (save.Result.status == Collection.Status.SUCCESS)
            {
                TempData["Success"] = Collection.Status.SUCCESS;
                HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);

                ViewBag.State = Collection.Status.SUCCESS;

                return RedirectToAction("Inbox", "Approval");
            }
            else
            {
                TempData["Success"] = Collection.Status.FAILED;
                TempData["Message"] = save.Result.message;

                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = save.Result.message;

                return RedirectToAction("CACar", "CACar");
            }
        }

        public IActionResult Review(CACarModels modelca)
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["Success"] = "";
            TempData["Message"] = "";

            LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

            modelca.StatusApproval = "C";
            modelca.CreatedBy = lDAPModels.EmployeeId;
            modelca.LastUpdatedBy = lDAPModels.EmployeeId;

            var save = services.ApproveCM(modelca);

            if (save.Result.status == Collection.Status.SUCCESS)
            {
                TempData["Success"] = Collection.Status.SUCCESS;
                HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);

                ViewBag.State = Collection.Status.SUCCESS;

                return RedirectToAction("Inbox", "Approval");
            }
            else
            {
                TempData["Success"] = Collection.Status.FAILED;
                TempData["Message"] = save.Result.message;

                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = save.Result.message;

                return RedirectToAction("CACar", "CACar");
            }
        }

        public IActionResult Verify(CACarModels modelca)
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["Success"] = "";
            TempData["Message"] = "";

            LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

            modelca.StatusApproval = "V";
            modelca.CreatedBy = lDAPModels.EmployeeId;
            modelca.LastUpdatedBy = lDAPModels.EmployeeId;

            var save = services.ApproveCM(modelca);

            if (save.Result.status == Collection.Status.SUCCESS)
            {
                TempData["Success"] = Collection.Status.SUCCESS;
                HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);

                ViewBag.State = Collection.Status.SUCCESS;

                return RedirectToAction("Inbox", "Approval");
            }
            else
            {
                TempData["Success"] = Collection.Status.FAILED;
                TempData["Message"] = save.Result.message;

                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = save.Result.message;

                return RedirectToAction("CACar", "CACar");
            }
        }

        public IActionResult ViewScore()
        {
            NeoScoreModels neoscore = new NeoScoreModels();
            neoscore.html_neo = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <title>Score Boards</title>\r\n\r\n    <style>\r\n        body {\r\n            font-family: Open Sans, sans-serif;\r\n        }\r\n        table {\r\n          border-collapse: collapse;\r\n          width: 100%;\r\n        }\r\n        tr:nth-child(even) {\r\n            background-color: #E0E0E0;\r\n        }\r\n        td {\r\n            border-bottom: 2px solid #ddd;\r\n            padding: 8px;\r\n            text-align: left;\r\n            font-size: 8pt;\r\n        }\r\n        .tdh {\r\n            background-color: #f4ab53;\r\n            font-weight: bold;\r\n        }\r\n        div.a{      \r\n            font-size:10px;\r\n        }\r\n        .center {\r\n            margin: auto;\r\n            width: 100%;\r\n        }\r\n        .text-center {\r\n            text-align: center;\r\n        }\r\n        img.score-green {\r\n            height: 13px;\r\n            width: 13px;\r\n            background-color: #00cd00;\r\n            border-color: #00cd00;\r\n        }\r\n        img.score-orange {\r\n            height: 13px;\r\n            width: 13px;\r\n            background-color: #ff8000;\r\n            border-color: #ff8000;\r\n        }\r\n        img.score-red {\r\n            height: 13px;\r\n            width: 13px;\r\n            background-color: #FF0000;\r\n            border-color: #FF0000;\r\n        }\r\n        .mt-20 {\r\n            margin-top: 20px;\r\n        }\r\n        .mb-10 {\r\n            margin-bottom: 10px;\r\n        }\r\n        .ml-5 {\r\n            margin-left: 10px;\r\n        }\r\n        .mr-5 {\r\n            margin-right: 10px;\r\n        }\r\n        .column {\r\n            float: left;\r\n            width: 48%;\r\n        }\r\n        .column0 {\r\n            float: left;\r\n            width: 30%;\r\n        }\r\n        .column1 {\r\n            float: left;\r\n            width: 60%;\r\n        }\r\n        /* Clear floats after the columns */\r\n        .row:after {\r\n            content: \"\";\r\n            display: table;\r\n            clear: both;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"center\">\r\n        <div class=\"mb-10\" style=\"display: flex; align-items: flex-end;\">\r\n            <img src=\"http://neokarya.com/img/logo.png\" height=\"30\" style=\"padding-bottom: 5px; padding-right: 10px;\">\r\n            <h1 style=\"color: #f4ab53; padding: 0; margin: 0\">Score Board</h1>\r\n        </div>\r\n        <div class=\"row\">\r\n            <div class=\"column0 mr-5\">\r\n                <table>\r\n                    <tbody>\r\n                        <tr>\r\n                            <td colspan=\"2\" class=\"tdh\">\r\n                                Credit Score\r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td class=\"tdh\" width=\"20%\">Total Score</td>\r\n                            <td class=\"tdh\">687</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td class=\"tdh\">Rekomendasi</td>\r\n                            <td class=\"tdh\">Auto Approved</td>\r\n                        </tr>\r\n                    </tbody>\r\n                </table> \r\n                <table class=\"mt-20\">\r\n                    <thead>\r\n                         <tr>\r\n                            <td class=\"tdh\" colspan=\"4\">Detail Score</td>\r\n                        </tr> \r\n                    </thead>\r\n                    <tbody>\r\n                        <tr>\r\n                            <td width=\"65%\">Profesi</td>\r\n                            <td width=\"35%\">\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Status Tempat Tinggal</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                         <tr>\r\n                            <td>Tipe Industri</td>\r\n                            <td style=\"vertical-align: middle;\">\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                         <tr>\r\n                            <td>Ada Penjamin</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Tipe Kendaraan</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>  \r\n                        <tr>\r\n                            <td>Jumlah Tanggungan</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>  \r\n                        <tr>\r\n                            <td>Provinsi</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>  \r\n                        <tr>\r\n                            <td>Penghasilan Per Bulan</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>  \r\n                        <tr>\r\n                            <td>Jenis Kelamin</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Penghasilan Tambahan</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Usia</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Tenor</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Status Pernikahan</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Uang Muka</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                    </tbody>\r\n                </table>\r\n            </div>\r\n\r\n            <div class=\"column1 ml-5\">\r\n                <table>\r\n                    <thead>\r\n                        <tr>\r\n                            <td colspan=\"2\" class=\"tdh\">\r\n                                Customer Profiling\r\n                            </td>\r\n                        </tr>\r\n                    </thead>\r\n                </table>\r\n\r\n                <div class=\"row mt-20\">\r\n                    <div class=\"column mr-5\">\r\n                        <table>\r\n                            <thead>\r\n                                <tr>\r\n                                    <td colspan=\"2\" class=\"tdh\">\r\n                                        Detail Fintech\r\n                                    </td>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>\r\n                                <tr>\r\n                                    <td width=\"65%\">Jumlah Pengajuan Fintech Menggunakan KTP 0-30hari</td>\r\n                                    <td width=\"35%\">\r\n                                        1\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Jumlah Pengajuan Fintech Menggunakan KTP > 30hari</td>\r\n                                    <td>\r\n                                        1\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Jumlah Pengajuan Fintech Menggunakan No HP 0-30hari</td>\r\n                                    <td>\r\n                                        0\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Jumlah Pengajuan Fintech Menggunakan No HP >30hari</td>\r\n                                    <td>\r\n                                        0\r\n                                    </td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n\r\n                        <table class=\"mt-20\">\r\n                            <thead>\r\n                                <tr>\r\n                                    <td colspan=\"4\" class=\"tdh\">\r\n                                        Detail BPJS\r\n                                    </td>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>\r\n                                <tr>\r\n                                    <td width=\"65%\">Status BPJS</td>\r\n                                    <td width=\"35%\">\r\n                                        None\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Lama Keikutsertaan BPJS (Tahun)</td>\r\n                                    <td>\r\n                                        -\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Kelas BPJS</td>\r\n                                    <td>\r\n                                        -\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Terdaftar BPJS atau Tidak</td>\r\n                                    <td>\r\n                                        Tidak\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Jumlah Anggota dalam satu BPJS</td>\r\n                                    <td>\r\n                                        -\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Kode Jenis Kepesertaan</td>\r\n                                    <td>\r\n                                        -\r\n                                    </td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n                    </div>\r\n                    <div class=\"column ml-5\">\r\n                        <table>\r\n                            <thead>\r\n                                <tr>\r\n                                    <td colspan=\"4\" class=\"tdh\">\r\n                                        Detail Telepon\r\n                                    </td>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>\r\n                                <tr>\r\n                                    <td width=\"65%\">No tlp lainnya</td>\r\n                                    <td width=\"35%\">\r\n                                        \r\n                                        +6289630243965 <br>\r\n                                        \r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Phone Score (berdasarkan penggunaan No. telp)</td>\r\n                                    <td>\r\n                                        27\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Umur No tlp</td>\r\n                                    <td>\r\n                                        12month+\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Verifikasi no tlp vs no ktp</td>\r\n                                    <td>\r\n                                        NOT_MATCH\r\n                                    </td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n\r\n                        <table class=\"mt-20\">\r\n                            <thead>\r\n                                <tr>\r\n                                    <td colspan=\"4\" class=\"tdh\">\r\n                                        Detail Topup\r\n                                    </td>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>\r\n                                <tr>\r\n                                    <td width=\"35%\">Berapa kali Top up 0-60</td>\r\n                                    <td width=\"15%\">\r\n                                        9x\r\n                                    </td>\r\n                                    <td width=\"35%\">Rata-rata Top up 0-60</td>\r\n                                    <td width=\"15%\">\r\n                                        9k\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Minimal Top up 0-60</td>\r\n                                    <td>\r\n                                        9k\r\n                                    </td>\r\n                                    <td>Maximal Top up 0-60</td>\r\n                                    <td>\r\n                                        10k\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Berapa kali Top up 0-360</td>\r\n                                    <td>\r\n                                        39x\r\n                                    </td>\r\n                                    <td>Rata-rata Top up 0-360</td>\r\n                                    <td>\r\n                                        9k\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Minimal Top up 0-360</td>\r\n                                    <td>\r\n                                        5k\r\n                                    </td>\r\n                                    <td>Maximal Top up 0-360</td>\r\n                                    <td>\r\n                                        15k\r\n                                    </td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>";
            return View("~/Views/NeoScore.cshtml", neoscore);
        }
    }
}