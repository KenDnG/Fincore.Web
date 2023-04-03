using FINCORE.Models.Helpers;
using FINCORE.Models.Models.Acquisition.CM;
using FINCORE.Models.Models.Acquisition.CM.Paginations;
using FINCORE.Models.Models.Acquisition.PO;
using FINCORE.Models.Models.Acquisition.Reports;
using FINCORE.Models.Models.LDAP;
using FINCORE.Models.Models.Masters;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.Acquisition;
using FINCORE.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using System.Net;
using System.Text;
using static FINCORE.Services.Helpers.Response.Collection;
using static FINCORE.WEB.Helpers.Commons;
using FileExtensions = FINCORE.WEB.Helpers.FileExtensions;

namespace FINCORE.WEB.Controllers.Acquisition
{
    public class CMController : Controller
    {
        private CMServices services = new CMServices();
        private AcquisitionServices servicesAcq = new AcquisitionServices();

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

        private readonly string _FOLDER_TARGET = "\\FileUpload\\AcquisitionDocument\\";

        public CMController(IWebHostEnvironment environment)
                   => this.environment = environment;

        public IActionResult CMMotorCycle(string trans_id)
        {
            HttpContext.Session.SetString("SuccessCM", "");

            ViewBag.StateCM = TempData["SuccessCM"];
            ViewBag.MessageCM = TempData["MessageCM"];

            LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

            var branch_id = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
            CompanyBranchResponse CompanyBranchMdl = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
            var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            var IsEdit = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsEdit");
            var IsView = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsView");
            var IsApprove = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsApprove");
            var Flag = "View";

            HttpContext.Session.SetString("CompanyId", CompanyBranchMdl.company_id);
            HttpContext.Session.SetString("BranchId", branch_id);

            if (IsEdit == "true")
            {
                HttpContext.Session.SetString("Flag", "Edit"); //Add,Edit,View,Approval
                Flag = "Edit";
            }
            else
            {
                HttpContext.Session.SetString("Flag", "Add"); //Add,Edit,View,Approval
                Flag = "Add";
            }

            if (trans_id != null)
            {
                if (IsApprove == "true")
                {
                    Flag = "Approval";
                }
                else
                {
                    Flag = "View";
                }

                HttpContext.Session.SetString("credit_id", trans_id);
            }

            HttpContext.Session.SetString("Flag", Flag);

            CMModels CM = new CMModels();

            if (Flag != "Add")
            {
                var CMdata = services.Get_Tr_CM(credit_id, lDAPModels.EmployeeId).Result;
                ViewBag.CMModels = CMdata.data;
            }

            var ms_asset_kind = services.Get_ms_asset_kind("001").Result; //motor
            ViewBag.ms_asset_kind = ms_asset_kind.data;

            var ms_application_type = services.Get_ms_application_type().Result;
            ViewBag.ms_application_type = ms_application_type.data;

            var ms_product_finance = services.Get_ms_product_finance().Result;
            ViewBag.ms_product_finance = ms_product_finance.data;

            var TACMax = services.Get_ms_general_parameter("TACMax").Result;
            ViewBag.TACMax = TACMax.data;

            var ODRate = services.Get_ms_general_parameter("ODRate").Result;
            ViewBag.ODRate = ODRate.data;

            var Installment = services.Get_Installment("001").Result;
            ViewBag.Installment = Installment.data;

            var TipePerantara = services.Get_tipe_perantara().Result;
            ViewBag.TipePerantara = TipePerantara.data;

            var AccountOwner = services.Get_account_owner().Result;
            ViewBag.AccountOwner = AccountOwner.data;            

            var PlatPrefix = services.Get_plat_prefix(branch_id).Result;
            ViewBag.PlatPrefix = PlatPrefix.data;

            var STNK_status = services.Get_STNK_status().Result;
            ViewBag.STNK_status = STNK_status.data;

            var PlatPrefix = services.Get_plat_prefix(branch_id).Result;
            ViewBag.PlatPrefix = PlatPrefix.data;

            var STNK_status = services.Get_STNK_status().Result;
            ViewBag.STNK_status = STNK_status.data;

            return View("~/Views/Acquisition/CM/CMMotorCycle.cshtml", CM);
        }

        public IActionResult CMMotor_UploadDoc()
        {
            HttpContext.Session.SetString("SuccessCM", "");

            ViewBag.StateCM = TempData["SuccessCM"];
            ViewBag.MessageCM = TempData["MessageCM"];

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

            var ms_photo_type = services.Get_ms_photo_type(credit_id).Result;
            ViewBag.ms_photo_type = ms_photo_type.data;

            var SkalaResiko = services.Get_SkalaResiko(lDAPModels.EmployeeId).Result;
            ViewBag.SkalaResiko = SkalaResiko.data;

            var SkalaResikoValue = services.Get_SkalaResikoValue("-").Result;
            ViewBag.SkalaResikoValue = SkalaResikoValue.data;

            if (Flag != "Add")
            {
                var CMdata = services.Get_Tr_CM(credit_id, lDAPModels.EmployeeId).Result;
                ViewBag.CMModels = CMdata.data;
            }

            //if (Flag == "Approval")
            //{
                var ApprReason = services.Get_ApprovalReason(lDAPModels.EmployeeId, "-").Result;
                ViewBag.ApprReason = ApprReason.data;

                var HistoryApproval = services.Get_HistoryApproval(credit_id).Result;
                ViewBag.HistoryApproval = HistoryApproval.data;
            //}

            CMPhotoTypeModels CMPhotoType = new CMPhotoTypeModels();

            ViewBag.User = lDAPModels.EmployeeId;
            ViewBag.CreditId = credit_id;

            return View("~/Views/Acquisition/CM/CMMotorCycle_UploadDoc.cshtml", CMPhotoType);
        }

        #region Get Data

        [HttpGet]
        public IActionResult GetReasonType(string reason_id)
        {
            LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");
            var data = services.Get_ApprovalReason(lDAPModels.EmployeeId, reason_id).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetItemBrand(string item_id)
        {
            var data = services.Get_ms_item_brand(item_id).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetAssetKindClass(string item_id)
        {
            var data = services.Get_ms_asset_kind_class(item_id).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetYear(string item_id, string Item_Brand_Id, string asset_kind_class_id, string asset_type_id)
        {
            var data = services.Get_year(item_id, Item_Brand_Id, asset_kind_class_id, asset_type_id).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetMsProduct(string item_id)
        {
            var data = services.Get_ms_product(item_id).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetMsProductMarketing(string company_id)
        {
            var data = services.Get_ms_product_marketing(company_id, "001").Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetMsSTNKName(string where)
        {
            var data = services.Get_ms_STNK_name(where).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetMsProvenancePurchaseOrder(string where)
        {
            var data = services.Get_ms_provenance_purchase_order(where).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetMsUsageType(string where)
        {
            var data = services.Get_ms_usage_type(where).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetMsAR(string where)
        {
            var data = services.Get_ms_ar(where).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetMsInsuranceCoverType(string where)
        {
            var data = services.Get_ms_insurance_cover_type(where).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetMsInsuranceType(string where)
        {
            var data = services.Get_ms_insurance_type(where).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetMsInterestRateType(string where)
        {
            var data = services.Get_ms_interest_rate_type(where).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetInsuranceFee(string asset_kind_id, string OTR, string CompanyId, string BranchId, string Tenor)
        {
            var data = services.Get_InsuranceFee(asset_kind_id, OTR, CompanyId, BranchId, Tenor).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetMarketPrice(string asset_kind_id, string CompanyId, string BranchId, string asset_type_id, string Year, string credit_id, string tipe_kerja_sama, string status_stnk_id, string Faktur_BPKB, string destination_bank_account_id_umc)
        {
            var data = services.Get_MarketPrice(asset_kind_id, CompanyId, BranchId, asset_type_id, Year, credit_id, tipe_kerja_sama, status_stnk_id, Faktur_BPKB, destination_bank_account_id_umc).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult Get_Tr_CM_photo_detail(string credit_id, string photo_type_id, string photo_id)
        {
            var data = services.Get_Tr_CM_photo_detail(credit_id, photo_type_id, photo_id).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetChasisCode(string year)
        {
            var data = services.Get_ChasisCode(year).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetBankNameDestination(string destination_bank_account_id_umc)
        {
            var data = services.GetBankNameDestination(destination_bank_account_id_umc).Result;
            return Ok(data.data);
        }

        #endregion Get Data

        #region Save

        [HttpPost]
        public IActionResult SaveCMMotor(CMModels model)
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["SuccessCM"] = "";
            TempData["MessageCM"] = "";

            ViewModelCM data = new ViewModelCM();

            LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

            data.credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            data.CreatedBy = lDAPModels.EmployeeId;
            data.LastUpdatedBy = lDAPModels.EmployeeId;
            data.application_type_id = model.application_type_id;
            data.product_finance_id = model.product_finance_id;
            data.is_item_new = model.is_item_new;
            data.asset_kind_id = model.asset_kind_id;
            data.item_brand_id = model.item_brand_id;
            data.item_brand_type_id = model.item_brand_type_id;
            data.asset_kind_class_id = model.asset_kind_class_id;
            data.year = model.year;
            data.product_id = model.product_id;
            data.dealer_code = model.dealer_code;
            data.surveyor_id = model.surveyor_id;
            data.marketinghead_id = model.marketinghead_id;
            data.product_marketing_id = model.product_marketing_id;
            data.provenance_PO_id = model.provenance_PO_id;
            data.CC = model.CC;
            data.usage_type_id = model.usage_type_id;
            data.ar = model.ar;
            data.tipe_cover = model.tipe_cover;
            data.insurance_type_id = model.insurance_type_id;
            data.installment_id = model.installment_id;  //add by fajero 22/12/2022
            data.interest_rate_type_id = model.interest_rate_type_id;
            data.tenor = model.tenor;
            data.asset_cost = model.asset_cost;
            data.gross_down_payment = model.gross_down_payment;
            data.admin_fee = model.admin_fee;
            data.biaya_provisi = model.biaya_provisi;
            data.insurance_fee = model.insurance_fee;
            data.uang_muka_murni_kons = model.uang_muka_murni_kons;
            data.jml_pembiayaan = model.jml_pembiayaan;
            data.amount_installment = model.amount_installment;
            data.effective_rate = model.effective_rate;
            data.flat_rate = model.flat_rate;
            data.disc_deposit = model.disc_deposit;
            data.overdue_rate = model.overdue_rate;
            data.CGSCabangNo = model.CGSCabangNo;
            data.STNK_name_id = model.STNK_name_id;
            data.STNK_name_description = model.STNK_name_description;
            data.disc_type = model.disc_type;
            data.TAC_max = model.TAC_max;
            data.komper_max = model.komper_max;
            data.branch_id = model.branch_id;
            data.company_id = model.company_id;
            data.deposit_installment = model.deposit_installment;
            data.is_topup_ms = model.is_topup_ms;
            data.old_npp = model.old_npp;
            data.skema_id = model.skema_id;
            data.perantara_type_id = model.perantara_type_id;
            data.agent_id = model.agent_id;
            data.agent_name = model.agent_name;
            data.ownership_account_type_id = model.ownership_account_type_id;
            data.bank_account_id_umc = model.bank_account_id_umc;
            data.bank_id_umc = model.bank_id_umc;
            data.bank_name_umc = model.bank_name_umc;
            data.account_no_umc = model.account_no_umc;
            data.account_name_umc = model.account_name_umc;
            
            data.is_package_product = model.is_package_product;
            data.package_product_amount = model.package_product_amount;
            data.chasis_no = model.chasis_no;
            data.machine_no = model.machine_no;
            data.prefix_plat = model.prefix_plat;
            data.plat_no = model.plat_no;
            data.plat_code = model.plat_code;

            data.destination_bank_id_umc = model.destination_bank_id_umc;
            data.destination_bank_account_id_umc = model.destination_bank_account_id_umc;
            data.destination_account_no_umc = model.destination_account_no_umc;
            data.destination_account_name_umc = model.destination_account_name_umc;

            data.STNK_status_id = model.STNK_status_id;
            data.BPKB_invoice = model.BPKB_invoice;

            var save = services.InsertCM(data);

            var x = save.Result.data;
           
            if (save.Result.status == Collection.Status.SUCCESS)
            {
                TempData["SuccessCM"] = Collection.Status.SUCCESS;
                ViewBag.StateCM = Collection.Status.SUCCESS;
               
                HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);

                return RedirectToAction("IndexMotor", "Acquisition");
            }
            else
            {
                TempData["SuccessCM"] = Collection.Status.FAILED;
                TempData["MessageCM"] = save.Result.message;

                ViewBag.StateCM = Collection.Status.FAILED;
                ViewBag.MessageCM = save.Result.message;

                return RedirectToAction("CMMotorCycle", "CM");
            }
        }

        //public IActionResult SaveCMMotor_PhotoDetail([FromBody] List<PhotoDetailResultModels> PhotoDetailResult)
        //{
        //    HttpContext.Session.SetString("SuccessCM", "");
        //    TempData["SuccessCM"] = "";
        //    TempData["MessageCM"] = "";

        //    ViewBag.State = Collection.Status.SUCCESS;

        //    LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");
        //    var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);

        //    for (int i = 0; i < PhotoDetailResult.Count; i++)
        //    {
        //        if (PhotoDetailResult[i].fileUpload != null)
        //        {
        //            //upload FTP
        //            string uploadRoot = Path.Combine(this.environment.WebRootPath, "FileUpload\\AcquisitionDocument\\");

        //            string uniqueFileName = "Foto_Detail_" + credit_id + "_" + PhotoDetailResult[i].photo_type_id + "_" + PhotoDetailResult[i].photo_id + Path.GetExtension(PhotoDetailResult[i].fileUpload.FileName);
        //            string filePath = System.IO.Path.Combine(this.environment.WebRootPath + "\\FileUpload\\AcquisitionDocument\\", uniqueFileName);

        //            using (var stream = System.IO.File.Create(filePath))
        //            {
        //                PhotoDetailResult[i].fileUpload.CopyToAsync(stream);
        //            }

        //            byte[] bytesFile = System.IO.File.ReadAllBytes(filePath);
        //            string year = DateTime.Now.Year.ToString();
        //            string period = DateTime.Now.ToString("yyyyMM");

        //            FTPService _ftpService;
        //            _ftpService = new FTPService("ftp://" + host + ":" + port, username, password);
        //            _ftpService.CreateFTPDirectory(ftp_path + "/" + year + "/");
        //            _ftpService.CreateFTPDirectory(ftp_path + "/" + year + "/" + period + "/");
        //            _ftpService.CreateFTPDirectory(ftp_path + "/" + year + "/" + period + "/" + credit_id + "/");
        //            _ftpService.CreateFileToFolder(ftp_path + "/" + year + "/" + period + "/" + credit_id + "/" + uniqueFileName, bytesFile);

        //            if (System.IO.File.Exists(Path.Combine(filePath)))
        //            {
        //                System.IO.File.Delete(Path.Combine(filePath));
        //            }
        //            //end upload FTP

        //            ViewModelCMPhotoType data = new ViewModelCMPhotoType();
        //            data.credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
        //            data.CreatedBy = lDAPModels.EmployeeId;
        //            data.LastUpdatedBy = lDAPModels.EmployeeId;

        //            data.photo_type_id = PhotoDetailResult[i].photo_type_id;
        //            data.photo_id = PhotoDetailResult[i].photo_id;
        //            data.filename = uniqueFileName;
        //            data.filePath = ftp_path + "/" + year + "/" + period + "/" + credit_id + "/";
        //            data.StatusApproval = PhotoDetailResult[i].flagAction;

        //            var save = services.InsertCMPhotoDetail(data);

        //            if (save.Result.status == Collection.Status.SUCCESS)
        //            {
        //                ViewBag.State = Collection.Status.SUCCESS;
        //            }
        //            else
        //            {
        //                ViewBag.State = Collection.Status.FAILED;
        //                ViewBag.Message = save.Result.message;
        //            }
        //        }
        //    }

        //    return View(ViewBag.State);
        //}

        [HttpPost]
        public IActionResult SaveCMMotor_PhotoDetail(string photo_type_id, IFormFile fileUpload, string photo_id, string flagAction)
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["SuccessCM"] = "";
            TempData["MessageCM"] = "";

            ViewBag.StateCM = Collection.Status.SUCCESS;

            if (fileUpload != null)
            {
                LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

                var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);

                //upload FTP
                string uploadRoot = Path.Combine(this.environment.WebRootPath, "FileUpload\\AcquisitionDocument\\");

                string uniqueFileName = "Foto_Detail_" + credit_id + "_" + photo_type_id + "_" + photo_id + Path.GetExtension(fileUpload.FileName);
                //string filePath = System.IO.Path.Combine(this.environment.WebRootPath + "\\FileUpload\\AcquisitionDocument\\", uniqueFileName);

                var pathFolderFtp = "";
                FTPUpload fTPUpload = new FTPUpload();

                pathFolderFtp = fTPUpload.UploadFile(environment, fileUpload, FTP.AcquisitionDoc.FTP_PORT.ToString(),
                                                       FTP.AcquisitionDoc.FTP_HOST, FTP.AcquisitionDoc.FTP_USER_NAME, FTP.AcquisitionDoc.FTP_PASSWORD,
                                                       FTP.AcquisitionDoc.FTP_PATH_ACQUISITION_DOCUMENT, credit_id, uniqueFileName
                                                       );

                //using (var stream = System.IO.File.Create(filePath))
                //{
                //    fileUpload.CopyToAsync(stream);
                //}

                //byte[] bytesFile = System.IO.File.ReadAllBytes(filePath);
                //string year = DateTime.Now.Year.ToString();
                //string period = DateTime.Now.ToString("yyyyMM");

                //FTPService _ftpService;
                //_ftpService = new FTPService("ftp://" + host + ":" + port, username, password);
                //_ftpService.CreateFTPDirectory(ftp_path + "/" + year + "/");
                //_ftpService.CreateFTPDirectory(ftp_path + "/" + year + "/" + period + "/");
                //_ftpService.CreateFTPDirectory(ftp_path + "/" + year + "/" + period + "/" + credit_id + "/");
                //_ftpService.CreateFileToFolder(ftp_path + "/" + year + "/" + period + "/" + credit_id + "/" + uniqueFileName, bytesFile);

                //if (System.IO.File.Exists(Path.Combine(filePath)))
                //{
                //    System.IO.File.Delete(Path.Combine(filePath));
                //}
                //end upload FTP

                ViewModelCMPhotoType data = new ViewModelCMPhotoType();
                data.credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
                data.CreatedBy = lDAPModels.EmployeeId;
                data.LastUpdatedBy = lDAPModels.EmployeeId;

                data.photo_type_id = photo_type_id;
                data.photo_id = photo_id;
                data.filename = uniqueFileName;
                //data.filePath = ftp_path + "/" + year + "/" + period + "/" + credit_id + "/";
                data.filePath = pathFolderFtp.Replace("ftp://" + host + ":" + port + "/" + ftp_path, "");
                data.StatusApproval = flagAction;

                var save = services.InsertCMPhotoDetail(data);

                if (save.Result.status == Collection.Status.SUCCESS)
                {
                    ViewBag.StateCM = Collection.Status.SUCCESS;
                }
                else
                {
                    TempData["SuccessCM"] = Collection.Status.FAILED;
                    TempData["MessageCM"] = save.Result.message;

                    ViewBag.StateCM = Collection.Status.FAILED;
                    ViewBag.MessageCM = save.Result.message;
                }
            }

            return View(ViewBag.StateCM);
        }

        public IActionResult RFACM(CMModels model)
        {
            if (model.Result == "Failed")
            {
                TempData["SuccessCM"] = Collection.Status.FAILED;
                TempData["MessageCM"] = model.ResultMessage;

                ViewBag.StateCM = Collection.Status.FAILED;
                ViewBag.MessageCM = model.ResultMessage;
                return RedirectToAction("CMMotor_UploadDoc", "CM");
            }
            else
            {
                HttpContext.Session.SetString("SuccessCM", "");
                TempData["SuccessCM"] = "";
                TempData["MessageCM"] = "";

                LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

                var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);

                ViewModelCMPhotoType data = new ViewModelCMPhotoType();
                data.credit_id = credit_id;
                data.CreatedBy = lDAPModels.EmployeeId;
                data.LastUpdatedBy = lDAPModels.EmployeeId;
                //data.StatusApproval = StatusApproval;
                data.StatusApproval = "0";

                var save = services.RFACM(data);

                if (save.Result.status == Collection.Status.SUCCESS)
                {
                    TempData["SuccessCM"] = Collection.Status.SUCCESS;
                    ViewBag.StateCM = Collection.Status.SUCCESS;

                    HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);

                    return RedirectToAction("IndexMotor", "Acquisition");
                }
                else
                {
                    TempData["SuccessCM"] = Collection.Status.FAILED;
                    TempData["MessageCM"] = save.Result.message;

                    ViewBag.StateCM = Collection.Status.FAILED;
                    ViewBag.MessageCM = save.Result.message;

                    return RedirectToAction("CMMotor_UploadDoc", "CM");
                }

                //if (save.Result.status == Collection.Status.SUCCESS)
                //{
                //    ViewBag.StateCM = Collection.Status.SUCCESS;

                //    HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);
                //    return RedirectToAction("IndexMotor", "Acquisition");
                //}
                //else
                //{
                //    TempData["SuccessCM"] = Collection.Status.FAILED;
                //    TempData["MessageCM"] = save.Result.message;

                //    ViewBag.StateCM = Collection.Status.FAILED;
                //    ViewBag.MessageCM = save.Result.message;
                //}

                //return View(ViewBag.StateCM);
            }
        }

        #endregion Save

        #region Get Lookup Data

        [HttpPost]
        public async Task<IActionResult> PaginationItem(int pageIndex, string searchTerm, string item_id, string item_brand_id, string asset_kind_class_id)
        {
            var dataPaging = new PaginationModels<PaginationItemModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;

            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPaginationItem(string.Empty, item_id, item_brand_id, asset_kind_class_id, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationItemModels>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationItem(searchTerm, item_id, item_brand_id, asset_kind_class_id, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationItemModels>)data.Result.data;
            }

            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/ItemView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationDealer(int pageIndex, string searchTerm, string item_id, string is_item_new, string item_merk)
        {
            string branch_id = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
            var dataPaging = new PaginationModels<PaginationDealerModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;

            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPaginationDealer(string.Empty, item_id, is_item_new, branch_id, item_merk, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationDealerModels>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationDealer(searchTerm, item_id, is_item_new, branch_id, item_merk, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationDealerModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/DealerView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationSurveyor(int pageIndex, string searchTerm, string item_id)
        {
            var dataPaging = new PaginationModels<PaginationSurveyorModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;

            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPaginationSurveyor(string.Empty, item_id, Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id"), page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationSurveyorModels>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationSurveyor(searchTerm, item_id, Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id"), page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationSurveyorModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/SurveyorView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationMarketingHead(int pageIndex, string searchTerm, string item_id)
        {
            var dataPaging = new PaginationModels<PaginationMarketingHeadModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;

            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPaginationMarketingHead(string.Empty, item_id, Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id"), page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationMarketingHeadModels>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationMarketingHead(searchTerm, item_id, Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id"), page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationMarketingHeadModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/MarketingHeadView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationCGSNo(int pageIndex, string searchTerm, string BranchId, string CompanyId)
        {
            var dataPaging = new PaginationModels<PaginationCGSNoModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;

            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPaginationCGSNo(string.Empty, BranchId, CompanyId, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationCGSNoModels>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationCGSNo(searchTerm, BranchId, CompanyId, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationCGSNoModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/CGSNoView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationOldNPP(int pageIndex, string searchTerm, string BranchId, string CompanyId, string ItemMerkTypeID)
        {
            var dataPaging = new PaginationModels<PaginationOldNPPModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;

            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPaginationOldNPP(string.Empty, BranchId, CompanyId, ItemMerkTypeID, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationOldNPPModels>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationOldNPP(searchTerm, BranchId, CompanyId, ItemMerkTypeID, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationOldNPPModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/OldNPPView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationPerantara(int pageIndex, string searchTerm, string BranchId, string CompanyId, string TipePerantara)
        {
            var dataPaging = new PaginationModels<PaginationPerantaraModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;

            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPaginationPerantara(string.Empty, BranchId, CompanyId, TipePerantara, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationPerantaraModels>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationPerantara(searchTerm, BranchId, CompanyId, TipePerantara, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationPerantaraModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/PerantaraView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationBankName(int pageIndex, string searchTerm, string BranchId, string CompanyId, string PemilikRekening)
        {
            var dataPaging = new PaginationModels<PaginationBankNameModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;

            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPaginationBankName(string.Empty, BranchId, CompanyId, PemilikRekening, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationBankNameModels>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationBankName(searchTerm, BranchId, CompanyId, PemilikRekening, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationBankNameModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/BankNameView.cshtml", dataPaging);
        }

        #endregion Get Lookup Data

        public FileResult DownloadFile()
        {
            var photo_type_id = Request.Query["photo_type_id"].ToString();
            var photo_id = Request.Query["photo_id"].ToString();
            var is_new_zoom = Request.Query["is_new_zoom"].ToString();

            var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            var CMPhotoTypeModels = services.Get_Tr_CM_photo_detail(credit_id, photo_type_id, photo_id).Result;

            string fileName = "";
            string filePath = "";

            List<CMPhotoTypeModels> data = (List<CMPhotoTypeModels>)services.Get_Tr_CM_photo_detail(credit_id, photo_type_id, photo_id).Result.data;
            if (data.Count > 0)
            {
                fileName = data[0].filename;
                //filePath = data[0].filePath;
                is_new_zoom = data[0].is_new_zoom;

                if (is_new_zoom == "1")
                {
                    filePath = "ftp://" + host_NewZoom + ":" + port_NewZoom + "/" + ftp_path_NewZoom + data[0].filePath;
                }
                else
                {
                    filePath = "ftp://" + host + ":" + port + "/" + ftp_path + data[0].filePath;
                }
            }

            ////download from ftp
            //WebClient client = new WebClient();
            //String RemoteFtpPath = "";
            //String LocalDestinationPath = "";

            //if (is_new_zoom == "1")
            //{
            //    client.Credentials = new NetworkCredential(username_NewZoom, password_NewZoom);
            //    RemoteFtpPath = "ftp://" + host_NewZoom + ":" + port_NewZoom + "//" + filePath + fileName;
            //}
            //else
            //{
            //    client.Credentials = new NetworkCredential(username, password);
            //    RemoteFtpPath = "ftp://" + host + ":" + port + "//" + filePath + fileName;
            //}

            //LocalDestinationPath = Path.Combine(this.environment.WebRootPath, "FileUpload\\AcquisitionDocument\\" + "file_" + fileName);

            //client.DownloadFile(RemoteFtpPath, LocalDestinationPath);

            ////download from local folder
            ////Read the File data into Byte Array.
            //byte[] bytes = System.IO.File.ReadAllBytes(LocalDestinationPath);
            ////Send the File to Download.
            //return File(bytes, "application/octet-stream", "file_" + fileName);

            //if (System.IO.File.Exists(LocalDestinationPath))
            //{
            //    System.IO.File.Delete(LocalDestinationPath);
            //}

            //download from ftp
            var wwwrootPath = Path.Combine(this.environment.WebRootPath, "FileUpload\\AcquisitionDocument\\");
            if (!Directory.Exists(wwwrootPath))
            {
                Directory.CreateDirectory(wwwrootPath);
            }

            string localPath = Path.Combine(wwwrootPath, fileName);
            string remoteFtpPath = filePath + fileName;

            if (System.IO.File.Exists(localPath))
            {
                System.IO.File.Delete(localPath);
            }

            WebClient client = new WebClient();

            if (!remoteFtpPath.Contains(FTP.AcquisitionDoc.FTP_HOST))
            {
                client.Credentials = new NetworkCredential(username, password);
                remoteFtpPath = $"ftp://{host}:{port}/{remoteFtpPath}";
                client.DownloadFile(remoteFtpPath, localPath);
            }
            else if (!remoteFtpPath.Contains(FTP.AcquisitionDoc_NewZoom.FTP_HOST_NEW_ZOOM))
            {
                client.Credentials = new NetworkCredential(username_NewZoom, password_NewZoom);
                remoteFtpPath = $"ftp://{host_NewZoom}:{port_NewZoom}/{remoteFtpPath}/{fileName}";
                client.DownloadFile(remoteFtpPath, localPath);
            }
            else
            {
                if (is_new_zoom == "1")
                {
                    client.Credentials = new NetworkCredential(username_NewZoom, password_NewZoom);
                }
                else
                {
                    client.Credentials = new NetworkCredential(username, password);
                }

                client.DownloadFile(remoteFtpPath, localPath);
            }

            byte[] bytes = System.IO.File.ReadAllBytes(localPath);

            return File(bytes, "application/octet-stream", "file_" + fileName);

            if (System.IO.File.Exists(localPath))
            {
                System.IO.File.Delete(localPath);
            }
            //end download from ftp
        }

        [HttpPost]
        public IActionResult BackToCASMotor(CMModels model)
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["SuccessCM"] = "";
            TempData["MessageCM"] = "";

            var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            var IsEdit = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsEdit");
            var Flag = HttpContext.Session.GetString("Flag");

            if (Flag == "Add" || Flag == "Edit" || Flag == "Correction")
            {
                ViewModelCM data = new ViewModelCM();

                LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

                data.credit_id = credit_id;
                data.CreatedBy = lDAPModels.EmployeeId;
                data.LastUpdatedBy = lDAPModels.EmployeeId;

                data.application_type_id = model.application_type_id;
                data.product_finance_id = model.product_finance_id;
                data.is_item_new = model.is_item_new;
                data.asset_kind_id = model.asset_kind_id;
                data.item_brand_id = model.item_brand_id;
                data.item_brand_type_id = model.item_brand_type_id;
                data.asset_kind_class_id = model.asset_kind_class_id;
                data.year = model.year;
                data.product_id = model.product_id;
                data.dealer_code = model.dealer_code;
                data.surveyor_id = model.surveyor_id;
                data.marketinghead_id = model.marketinghead_id;
                data.product_marketing_id = model.product_marketing_id;
                data.provenance_PO_id = model.provenance_PO_id;
                data.CC = model.CC;
                data.usage_type_id = model.usage_type_id;
                data.ar = model.ar;
                data.tipe_cover = model.tipe_cover;
                data.insurance_type_id = model.insurance_type_id;
                data.installment_id = model.installment_id;  //add by fajero 22/12/2022
                data.interest_rate_type_id = model.interest_rate_type_id;
                data.tenor = model.tenor;
                data.asset_cost = model.asset_cost;
                data.gross_down_payment = model.gross_down_payment;
                data.admin_fee = model.admin_fee;
                data.biaya_provisi = model.biaya_provisi;
                data.insurance_fee = model.insurance_fee;
                data.uang_muka_murni_kons = model.uang_muka_murni_kons;
                data.jml_pembiayaan = model.jml_pembiayaan;
                data.amount_installment = model.amount_installment;
                data.effective_rate = model.effective_rate;
                data.flat_rate = model.flat_rate;
                data.disc_deposit = model.disc_deposit;
                data.overdue_rate = model.overdue_rate;
                data.CGSCabangNo = model.CGSCabangNo;
                data.STNK_name_id = model.STNK_name_id;
                data.STNK_name_description = model.STNK_name_description;
                data.disc_type = model.disc_type;
                data.TAC_max = model.TAC_max;
                data.komper_max = model.komper_max;
                data.branch_id = model.branch_id;
                data.company_id = model.company_id;
                data.deposit_installment = model.deposit_installment;
                data.is_topup_ms = model.is_topup_ms;
                data.old_npp = model.old_npp;
                data.skema_id = model.skema_id;
                data.perantara_type_id = model.perantara_type_id;
                data.agent_id = model.agent_id;
                data.agent_name = model.agent_name;
                data.ownership_account_type_id = model.ownership_account_type_id;
                data.bank_account_id_umc = model.bank_account_id_umc;
                data.bank_id_umc = model.bank_id_umc;
                data.bank_name_umc = model.bank_name_umc;
                data.account_no_umc = model.account_no_umc;
                data.account_name_umc = model.account_name_umc;

                data.is_package_product = model.is_package_product;
                data.package_product_amount = model.package_product_amount;
                data.chasis_no = model.chasis_no;
                data.machine_no = model.machine_no;
                data.prefix_plat = model.prefix_plat;
                data.plat_no = model.plat_no;
                data.plat_code = model.plat_code;

                data.destination_bank_id_umc = model.destination_bank_id_umc;
                data.destination_bank_account_id_umc = model.destination_bank_account_id_umc;
                data.destination_account_no_umc = model.destination_account_no_umc;
                data.destination_account_name_umc = model.destination_account_name_umc;

                data.STNK_status_id = model.STNK_status_id;
                data.BPKB_invoice = model.BPKB_invoice;

                var save = services.InsertCM(data);

                if (save.Result.status == Collection.Status.SUCCESS)
                {
                    HttpContext.Session.SetString("item", "001");
                    return RedirectToAction("GetTrCasByCreditId_2", "CAS");
                }
                else
                {
                    TempData["SuccessCM"] = Collection.Status.FAILED;
                    TempData["MessageCM"] = save.Result.message;

                    ViewBag.StateCM = Collection.Status.FAILED;
                    ViewBag.MessageCM = save.Result.message;
               
                    return RedirectToAction("CMMotorCycle", "CM");
                }
            }

            if (Flag == "Add" || Flag == "Edit" || Flag == "Correction")
            {
                HttpContext.Session.SetString("item", "001");
                return RedirectToAction("GetTrCasByCreditId_2", "CAS");
            }
            else
            {
                var IsApprove = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsApprove");
                if (IsApprove == "true")
                {
                    return RedirectToAction("ApproveTrCas", "CAS", new { creditId = credit_id, item = "001" });
                }
                else
                {
                    return RedirectToAction("ViewTrCas", "CAS", new { creditId = credit_id, item = "001" });
                }
            }
        }

        public IActionResult BackToCMMotor()
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["SuccessCM"] = "";
            TempData["MessageCM"] = "";

            var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            var Flag = HttpContext.Session.GetString("Flag");

            if (Flag == "Add" || Flag == "Edit" || Flag == "Correction")
            {
                return RedirectToAction("CMMotorCycle", "CM");
            }
            else
            {
                return RedirectToAction("CMMotorCycle", "CM", new { trans_id = credit_id });
            }
        }

        [HttpPost]
        public IActionResult NextToCMUpload(CMModels model)
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["SuccessCM"] = "";
            TempData["MessageCM"] = "";

            var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            var IsEdit = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsEdit");
            var Flag = HttpContext.Session.GetString("Flag");

            if (Flag == "Add" || Flag == "Edit" || Flag == "Correction")
            {
                ViewModelCM data = new ViewModelCM();

                LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

                data.credit_id = credit_id;
                data.CreatedBy = lDAPModels.EmployeeId;
                data.LastUpdatedBy = lDAPModels.EmployeeId;

                data.application_type_id = model.application_type_id;
                data.product_finance_id = model.product_finance_id;
                data.is_item_new = model.is_item_new;
                data.asset_kind_id = model.asset_kind_id;
                data.item_brand_id = model.item_brand_id;
                data.item_brand_type_id = model.item_brand_type_id;
                data.asset_kind_class_id = model.asset_kind_class_id;
                data.year = model.year;
                data.product_id = model.product_id;
                data.dealer_code = model.dealer_code;
                data.surveyor_id = model.surveyor_id;
                data.marketinghead_id = model.marketinghead_id;
                data.product_marketing_id = model.product_marketing_id;
                data.provenance_PO_id = model.provenance_PO_id;
                data.CC = model.CC;
                data.usage_type_id = model.usage_type_id;
                data.ar = model.ar;
                data.tipe_cover = model.tipe_cover;
                data.insurance_type_id = model.insurance_type_id;
                data.installment_id = model.installment_id;  //add by fajero 22/12/2022
                data.interest_rate_type_id = model.interest_rate_type_id;
                data.tenor = model.tenor;
                data.asset_cost = model.asset_cost;
                data.gross_down_payment = model.gross_down_payment;
                data.admin_fee = model.admin_fee;
                data.biaya_provisi = model.biaya_provisi;
                data.insurance_fee = model.insurance_fee;
                data.uang_muka_murni_kons = model.uang_muka_murni_kons;
                data.jml_pembiayaan = model.jml_pembiayaan;
                data.amount_installment = model.amount_installment;
                data.effective_rate = model.effective_rate;
                data.flat_rate = model.flat_rate;
                data.disc_deposit = model.disc_deposit;
                data.overdue_rate = model.overdue_rate;
                data.CGSCabangNo = model.CGSCabangNo;
                data.STNK_name_id = model.STNK_name_id;
                data.STNK_name_description = model.STNK_name_description;
                data.disc_type = model.disc_type;
                data.TAC_max = model.TAC_max;
                data.komper_max = model.komper_max;
                data.branch_id = model.branch_id;
                data.company_id = model.company_id;
                data.deposit_installment = model.deposit_installment;
                data.is_topup_ms = model.is_topup_ms;
                data.old_npp = model.old_npp;
                data.skema_id = model.skema_id;
                data.perantara_type_id = model.perantara_type_id;
                data.agent_id = model.agent_id;
                data.agent_name = model.agent_name;
                data.ownership_account_type_id = model.ownership_account_type_id;
                data.bank_account_id_umc = model.bank_account_id_umc;
                data.bank_id_umc = model.bank_id_umc;
                data.bank_name_umc = model.bank_name_umc;
                data.account_no_umc = model.account_no_umc;
                data.account_name_umc = model.account_name_umc;

                data.is_package_product = model.is_package_product;
                data.package_product_amount = model.package_product_amount;
                data.chasis_no = model.chasis_no;
                data.machine_no = model.machine_no;
                data.prefix_plat = model.prefix_plat;
                data.plat_no = model.plat_no;
                data.plat_code = model.plat_code;

                data.destination_bank_id_umc = model.destination_bank_id_umc;
                data.destination_bank_account_id_umc = model.destination_bank_account_id_umc;
                data.destination_account_no_umc = model.destination_account_no_umc;
                data.destination_account_name_umc = model.destination_account_name_umc;

                data.STNK_status_id = model.STNK_status_id;
                data.BPKB_invoice = model.BPKB_invoice;

                var save = services.InsertCM(data);

                if (save.Result.status == Collection.Status.SUCCESS)
                {
                    return RedirectToAction("CMMotor_UploadDoc", "CM");
                }
                else
                {
                    TempData["SuccessCM"] = Collection.Status.FAILED;
                    TempData["MessageCM"] = save.Result.message;

                    ViewBag.StateCM = Collection.Status.FAILED;
                    ViewBag.MessageCM = save.Result.message;

                    return RedirectToAction("CMMotorCycle", "CM");
                }
            }

            if (Flag == "Add" || Flag == "Edit" || Flag == "Correction")
            {
                return RedirectToAction("CMMotor_UploadDoc", "CM");
            }
            else
            {
                return RedirectToAction("CMMotor_UploadDoc", "CM", new { trans_id = credit_id });
            }
        }

        public IActionResult SaveCMMotor_UploadDoc(CMModels model)
        {
            if (model.Result == "Failed")
            {
                TempData["SuccessCM"] = Collection.Status.FAILED;
                TempData["MessageCM"] = model.ResultMessage;

                ViewBag.StateSendCM = Collection.Status.FAILED;
                ViewBag.MessageSendCM = model.ResultMessage;

                return RedirectToAction("CMMotor_UploadDoc", "CM");
            }
            else
            {
                TempData["SuccessCM"] = Collection.Status.SUCCESS;
                TempData["MessageCM"] = "Success";

                ViewBag.StateCM = Collection.Status.SUCCESS;
                ViewBag.MessageCM = "Success";

                HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);
                return RedirectToAction("IndexMotor", "Acquisition");
            }
        }

        public IActionResult Approval(CMModels model)
        {
            if (model.Result == "Failed")
            {
                TempData["SuccessCM"] = Collection.Status.FAILED;
                TempData["MessageCM"] = model.ResultMessage;

                ViewBag.StateSendCM = Collection.Status.FAILED;
                ViewBag.MessageSendCM = model.ResultMessage;

                return RedirectToAction("CMMotor_UploadDoc", "CM");
            }
            else
            {
                return RedirectToAction("Inbox", "Approval");
            }
        }

        public IActionResult ShowPopup()
        {
            return PartialView("~/Views/Acquisition/CM/PartialPopup.cshtml");
        }

        [HttpPost]
        public IActionResult ProcessApproval([FromBody] List<RiskScaleResultModels> RiskScaleResult)
        {
            CMModels modelcm = new CMModels();

            string credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);

            HttpContext.Session.SetString("SuccessCM", "");
            TempData["SuccessCM"] = "";
            TempData["MessageCM"] = "";

            LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

            modelcm.CreatedBy = lDAPModels.EmployeeId;
            modelcm.LastUpdatedBy = lDAPModels.EmployeeId;

            for (int i = 0; i < RiskScaleResult.Count; i++)
            {
                if (i == 0)
                {
                    modelcm.credit_id = credit_id;
                    modelcm.StatusApproval = RiskScaleResult[i].status_approval;
                    modelcm.reason_id = RiskScaleResult[i].reason_id;
                    modelcm.reason = RiskScaleResult[i].reason;
                    modelcm.analysis = RiskScaleResult[i].analysis;
                    modelcm.conclusion = RiskScaleResult[i].conclusion;
                }

                RiskScaleResultModels riskScaleResultModels = new RiskScaleResultModels();
                riskScaleResultModels.credit_id = credit_id;
                riskScaleResultModels.risk_scale_id = RiskScaleResult[i].risk_scale_id;
                riskScaleResultModels.risk_scale_value = RiskScaleResult[i].risk_scale_value;
                riskScaleResultModels.CreatedBy = lDAPModels.EmployeeId;

                var saveSkalaResiko = services.SaveSkalaResiko(riskScaleResultModels);
                if (saveSkalaResiko.Result.status != Collection.Status.SUCCESS)
                {
                    TempData["SuccessCM"] = Collection.Status.FAILED;
                    TempData["MessageCM"] = saveSkalaResiko.Result.message;

                    ViewBag.StateCM = Collection.Status.FAILED;
                    ViewBag.MessageCM = saveSkalaResiko.Result.message;

                    //return RedirectToAction("CMMotor_UploadDoc", "CM");
                    return Json(new { success = false, messages = saveSkalaResiko.Result.message, action = "/CM/CMMotor_UploadDoc" });
                }
            }

            var save = services.ApproveCM(modelcm);
            if (save.Result.status == Collection.Status.SUCCESS)
            {
                TempData["SuccessCM"] = Collection.Status.SUCCESS;
                HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);

                ViewBag.StateCM = Collection.Status.SUCCESS;

                if (modelcm.StatusApproval == "A")
                {
                    POModels POData = (POModels)services.Get_PO_No(credit_id).Result.data;
                    //return RedirectToAction("OnPrintPOMotorfromApproval", "Acquisition", new { poNo = POData.pO_no });
                    var poNo = POData.pO_no;

                    string year = DateTime.Now.Year.ToString();
                    string period = DateTime.Now.ToString("yyyyMM");
                    var userSession = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
                    var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);

                    var branchId = sessBranchId;
                    var printId = userSession.DisplayName;
                    var targetRptName = "FINCORE.WEB.Views.Acquisition.CAS.Reports.PrintPOMotor.rdlc";
                    byte[] bytePdf;

                    var pofileName = poNo.Contains('/') ? poNo.Replace("/", "_") : poNo;
                    var fileName = $"{pofileName}.{FileExtensions.PDF}";
                    var wwwrootPath = $"{this.environment.WebRootPath}{_FOLDER_TARGET}";

                    var filePath = Path.Combine(wwwrootPath, fileName);

                    try
                    {
                        var dataSource = (List<PrintPOMotorModels>)servicesAcq.PrintPOMotor(poNo, branchId, printId).Result.data;

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
                            ViewBag.StateSendCM = Status.SUCCESS;
                            ViewBag.MessageSendCM = isSend.message;
                            return Json(new { success = true, messages = isSend.message, action = "/Approval/Inbox" });
                        }
                        else
                        {
                            ViewBag.StateSendCM = Status.FAILED;
                            ViewBag.MessageSendCM = isSend.message;
                            return Json(new { success = false, messages = isSend.message, action = "/CM/CMMotor_UploadDoc" });
                        }
                    }
                    catch (Exception ex)
                    {
                        var err = $"Failed to PrintPO Motor. {ex.Message}";
                        ViewBag.StateSendCM = Status.FAILED;
                        ViewBag.MessageSendCM = err;
                        return Json(new { success = false, messages = err, action = "/CM/CMMotor_UploadDoc" });
                    }

                    //return RedirectToAction("Inbox", "Approval");
                }
                else
                {
                    //return RedirectToAction("Inbox", "Approval");
                    return Json(new { success = true, messages = "", action = "/Approval/Inbox" });
                }
            }
            else
            {
                TempData["SuccessCM"] = Collection.Status.FAILED;
                TempData["MessageCM"] = save.Result.message;

                ViewBag.StateCM = Collection.Status.FAILED;
                ViewBag.MessageCM = save.Result.message;

                //return RedirectToAction("CMMotor_UploadDoc", "CM");
                return Json(new { success = false, messages = save.Result.message, action = "/CM/CMMotor_UploadDoc" });
            }
        }

        public IActionResult Reject(CMModels modelcm)
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["SuccessCM"] = "";
            TempData["MessageCM"] = "";

            LDAPModels lDAPModels = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, "SessionUser");

            modelcm.StatusApproval = "R";
            modelcm.CreatedBy = lDAPModels.EmployeeId;
            modelcm.LastUpdatedBy = lDAPModels.EmployeeId;

            var save = services.ApproveCM(modelcm);

            if (save.Result.status == Collection.Status.SUCCESS)
            {
                TempData["SuccessCM"] = Collection.Status.SUCCESS;
                HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);

                ViewBag.StateCM = Collection.Status.SUCCESS;

                return RedirectToAction("Inbox", "Approval");
            }
            else
            {
                TempData["SuccessCM"] = Collection.Status.FAILED;
                TempData["MessageCM"] = save.Result.message;

                ViewBag.StateCM = Collection.Status.FAILED;
                ViewBag.MessageCM = save.Result.message;

                return RedirectToAction("CMMotor_UploadDoc", "CM");
            }
        }

        public IActionResult ViewScore(string creditid, string userid)
        {
            var service = new NeoScoreServices();

            APIResponse response = service.GetNeoScoreData(creditid, userid);

            NeoScoreModels neoscore = new() { html_neo = response.data.ToString() };

            //neoscore.html_neo = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <title>Score Boards</title>\r\n\r\n    <style>\r\n        body {\r\n            font-family: Open Sans, sans-serif;\r\n        }\r\n        table {\r\n          border-collapse: collapse;\r\n          width: 100%;\r\n        }\r\n        tr:nth-child(even) {\r\n            background-color: #E0E0E0;\r\n        }\r\n        td {\r\n            border-bottom: 2px solid #ddd;\r\n            padding: 8px;\r\n            text-align: left;\r\n            font-size: 8pt;\r\n        }\r\n        .tdh {\r\n            background-color: #f4ab53;\r\n            font-weight: bold;\r\n        }\r\n        div.a{      \r\n            font-size:10px;\r\n        }\r\n        .center {\r\n            margin: auto;\r\n            width: 100%;\r\n        }\r\n        .text-center {\r\n            text-align: center;\r\n        }\r\n        img.score-green {\r\n            height: 13px;\r\n            width: 13px;\r\n            background-color: #00cd00;\r\n            border-color: #00cd00;\r\n        }\r\n        img.score-orange {\r\n            height: 13px;\r\n            width: 13px;\r\n            background-color: #ff8000;\r\n            border-color: #ff8000;\r\n        }\r\n        img.score-red {\r\n            height: 13px;\r\n            width: 13px;\r\n            background-color: #FF0000;\r\n            border-color: #FF0000;\r\n        }\r\n        .mt-20 {\r\n            margin-top: 20px;\r\n        }\r\n        .mb-10 {\r\n            margin-bottom: 10px;\r\n        }\r\n        .ml-5 {\r\n            margin-left: 10px;\r\n        }\r\n        .mr-5 {\r\n            margin-right: 10px;\r\n        }\r\n        .column {\r\n            float: left;\r\n            width: 48%;\r\n        }\r\n        .column0 {\r\n            float: left;\r\n            width: 30%;\r\n        }\r\n        .column1 {\r\n            float: left;\r\n            width: 60%;\r\n        }\r\n        /* Clear floats after the columns */\r\n        .row:after {\r\n            content: \"\";\r\n            display: table;\r\n            clear: both;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"center\">\r\n        <div class=\"mb-10\" style=\"display: flex; align-items: flex-end;\">\r\n            <img src=\"http://neokarya.com/img/logo.png\" height=\"30\" style=\"padding-bottom: 5px; padding-right: 10px;\">\r\n            <h1 style=\"color: #f4ab53; padding: 0; margin: 0\">Score Board</h1>\r\n        </div>\r\n        <div class=\"row\">\r\n            <div class=\"column0 mr-5\">\r\n                <table>\r\n                    <tbody>\r\n                        <tr>\r\n                            <td colspan=\"2\" class=\"tdh\">\r\n                                Credit Score\r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td class=\"tdh\" width=\"20%\">Total Score</td>\r\n                            <td class=\"tdh\">687</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td class=\"tdh\">Rekomendasi</td>\r\n                            <td class=\"tdh\">Auto Approved</td>\r\n                        </tr>\r\n                    </tbody>\r\n                </table> \r\n                <table class=\"mt-20\">\r\n                    <thead>\r\n                         <tr>\r\n                            <td class=\"tdh\" colspan=\"4\">Detail Score</td>\r\n                        </tr> \r\n                    </thead>\r\n                    <tbody>\r\n                        <tr>\r\n                            <td width=\"65%\">Profesi</td>\r\n                            <td width=\"35%\">\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Status Tempat Tinggal</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                         <tr>\r\n                            <td>Tipe Industri</td>\r\n                            <td style=\"vertical-align: middle;\">\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                         <tr>\r\n                            <td>Ada Penjamin</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Tipe Kendaraan</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>  \r\n                        <tr>\r\n                            <td>Jumlah Tanggungan</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>  \r\n                        <tr>\r\n                            <td>Provinsi</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>  \r\n                        <tr>\r\n                            <td>Penghasilan Per Bulan</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>  \r\n                        <tr>\r\n                            <td>Jenis Kelamin</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Penghasilan Tambahan</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Usia</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Tenor</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Status Pernikahan</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Uang Muka</td>\r\n                            <td>\r\n                                \r\n                                valid\r\n                                \r\n                            </td>\r\n                        </tr>\r\n                    </tbody>\r\n                </table>\r\n            </div>\r\n\r\n            <div class=\"column1 ml-5\">\r\n                <table>\r\n                    <thead>\r\n                        <tr>\r\n                            <td colspan=\"2\" class=\"tdh\">\r\n                                Customer Profiling\r\n                            </td>\r\n                        </tr>\r\n                    </thead>\r\n                </table>\r\n\r\n                <div class=\"row mt-20\">\r\n                    <div class=\"column mr-5\">\r\n                        <table>\r\n                            <thead>\r\n                                <tr>\r\n                                    <td colspan=\"2\" class=\"tdh\">\r\n                                        Detail Fintech\r\n                                    </td>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>\r\n                                <tr>\r\n                                    <td width=\"65%\">Jumlah Pengajuan Fintech Menggunakan KTP 0-30hari</td>\r\n                                    <td width=\"35%\">\r\n                                        1\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Jumlah Pengajuan Fintech Menggunakan KTP > 30hari</td>\r\n                                    <td>\r\n                                        1\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Jumlah Pengajuan Fintech Menggunakan No HP 0-30hari</td>\r\n                                    <td>\r\n                                        0\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Jumlah Pengajuan Fintech Menggunakan No HP >30hari</td>\r\n                                    <td>\r\n                                        0\r\n                                    </td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n\r\n                        <table class=\"mt-20\">\r\n                            <thead>\r\n                                <tr>\r\n                                    <td colspan=\"4\" class=\"tdh\">\r\n                                        Detail BPJS\r\n                                    </td>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>\r\n                                <tr>\r\n                                    <td width=\"65%\">Status BPJS</td>\r\n                                    <td width=\"35%\">\r\n                                        None\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Lama Keikutsertaan BPJS (Tahun)</td>\r\n                                    <td>\r\n                                        -\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Kelas BPJS</td>\r\n                                    <td>\r\n                                        -\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Terdaftar BPJS atau Tidak</td>\r\n                                    <td>\r\n                                        Tidak\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Jumlah Anggota dalam satu BPJS</td>\r\n                                    <td>\r\n                                        -\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Kode Jenis Kepesertaan</td>\r\n                                    <td>\r\n                                        -\r\n                                    </td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n                    </div>\r\n                    <div class=\"column ml-5\">\r\n                        <table>\r\n                            <thead>\r\n                                <tr>\r\n                                    <td colspan=\"4\" class=\"tdh\">\r\n                                        Detail Telepon\r\n                                    </td>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>\r\n                                <tr>\r\n                                    <td width=\"65%\">No tlp lainnya</td>\r\n                                    <td width=\"35%\">\r\n                                        \r\n                                        +6289630243965 <br>\r\n                                        \r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Phone Score (berdasarkan penggunaan No. telp)</td>\r\n                                    <td>\r\n                                        27\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Umur No tlp</td>\r\n                                    <td>\r\n                                        12month+\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Verifikasi no tlp vs no ktp</td>\r\n                                    <td>\r\n                                        NOT_MATCH\r\n                                    </td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n\r\n                        <table class=\"mt-20\">\r\n                            <thead>\r\n                                <tr>\r\n                                    <td colspan=\"4\" class=\"tdh\">\r\n                                        Detail Topup\r\n                                    </td>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>\r\n                                <tr>\r\n                                    <td width=\"35%\">Berapa kali Top up 0-60</td>\r\n                                    <td width=\"15%\">\r\n                                        9x\r\n                                    </td>\r\n                                    <td width=\"35%\">Rata-rata Top up 0-60</td>\r\n                                    <td width=\"15%\">\r\n                                        9k\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Minimal Top up 0-60</td>\r\n                                    <td>\r\n                                        9k\r\n                                    </td>\r\n                                    <td>Maximal Top up 0-60</td>\r\n                                    <td>\r\n                                        10k\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Berapa kali Top up 0-360</td>\r\n                                    <td>\r\n                                        39x\r\n                                    </td>\r\n                                    <td>Rata-rata Top up 0-360</td>\r\n                                    <td>\r\n                                        9k\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td>Minimal Top up 0-360</td>\r\n                                    <td>\r\n                                        5k\r\n                                    </td>\r\n                                    <td>Maximal Top up 0-360</td>\r\n                                    <td>\r\n                                        15k\r\n                                    </td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>";
            return View("~/Views/NeoScore.cshtml", neoscore);
        }

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
                var res = servicesAcq.InsertTrPoSendEmail(itemsPoEmail);
                if (res.Result.status == Status.SUCCESS)
                    if (res.Result.status == Status.SUCCESS)
                    {
                        //send email with execute sp
                        var sendMailRespons = (List<SendEmailPrintPOModels>)servicesAcq.SendEmailPrintPO(poNo).Result.data;
                        var isSended = sendMailRespons.First();
                        if (isSended.SendStatus == SendingStatus.STATUS_SENDED)
                        {
                            var sumRes = servicesAcq.UpdateSumPrintPO(poNo, userSession.EmployeeId);
                            sendRepospone = new APIResponse(Codes.CREATED, Status.SUCCESS, $"File PO: {poNo} berhasil terkirim. Filename: {fileName}", "");
                        }
                        else
                        {
                            sendRepospone = new APIResponse(Codes.INTERNAL_SERVER_ERROR
                                    , Status.FAILED, $"Failed to Send email. Send Status: {isSended.SendStatus}", "");
                            var sumRes = servicesAcq.UpdateSumPrintPO(poNo, userSession.EmployeeId);
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
    }
}