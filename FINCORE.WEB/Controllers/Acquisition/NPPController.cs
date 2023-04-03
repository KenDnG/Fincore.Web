using FINCORE.Models.Models.Acquisition.NPP;
using FINCORE.Models.Models.Acquisition.NPP.Pagination;
using FINCORE.Models.Models.Acquisition.NPP.Report;
using FINCORE.Models.Models.LDAP;
using FINCORE.Models.Models.Masters;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.Acquisition;
using FINCORE.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System.Dynamic;
using System.Text;
using static FINCORE.Services.Helpers.Response.Collection;
using static FINCORE.WEB.Helpers.Commons;
using static FINCORE.WEB.Helpers.Report;

namespace FINCORE.WEB.Controllers.Acquisition
{
    public sealed class NPPController : Controller
    {
        private NPPServices services = new NPPServices();

        //[HttpGet("index")]
        public IActionResult Index(int pageIndex, string searchTerm)
        {
            //CreateReportSchema();

            CompanyBranchResponse companyData = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
            PaginationModels<PaginationNPP> dataPaging;// = new PaginationModels<PaginationNPP>();

            var page = pageIndex == 0 ? 1 : pageIndex;

            searchTerm ??= Request.Query["searchTerm"].ToString();

            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPaginationNPP(string.Empty, companyData.branch_id, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationNPP>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationNPP(searchTerm, companyData.branch_id, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationNPP>)data.Result.data;
            }

            ViewBag.CurrentPage = page;
            return View($"~/Views/Acquisition/NPP/Index.cshtml", dataPaging);
        }

        public IActionResult Add()
        {
            dynamic model = new ExpandoObject();

            DateTime dtToday = DateTime.Today;
            model.DateToday = dtToday.Year.ToString() + "/" + dtToday.Month.ToString() + "/" + dtToday.Day.ToString();
            model.DealerJobTitle = services.GetDealerJobTitleList().Result.data;

            return View("~/Views/Acquisition/NPP/Add.cshtml", model);
        }

        public IActionResult ViewNppById(string creditId, string trans_id)
        {
            LDAPModels ldap = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

            creditId = creditId is not null ? creditId : trans_id;

            dynamic model = new ExpandoObject();

            model.NPP = services.GetNPPViewById(creditId, ldap.EmployeeId).Result.data;
            model.ActionTitle = "VIEW";

            if (model.NPP.ItemId == "002")
            {
                model.OrderSourceTAC = services.GetOrderSourceTACByCreditId(creditId).Result.data;
                model.OrderSourceTP = services.GetOrderSourceTPByCreditId(creditId).Result.data;
            }
            return View("~/Views/Acquisition/NPP/View.cshtml", model);
        }

        [HttpPost]
        public ActionResult EditNppById(string creditId)
        {
            //string creditIdx = Request.Body.ToString();
            //string creditIdy = creditIdx;
            dynamic model = new ExpandoObject();
            model.NPP = services.GetNPPEditById(creditId).Result.data;

            if (model.NPP.ItemId == "002")
            {
                model.OrderSourceTAC = services.GetOrderSourceTACByCreditId(creditId).Result.data;
                model.OrderSourceTP = services.GetOrderSourceTPByCreditId(creditId).Result.data;
                model.DealerJobTitle = services.GetDealerJobTitleList().Result.data;
            }

            if (model.NPP.ApplicationTypeId == "03")
            {
                model.DisbursalType = services.GetDisbursalTypeUMCListByBranchId(model.NPP.BranchId).Result.data;
            }

            model.Flag = model.NPP.ItemId == "001" ? "editMotorcycle" : "editCar";

            return View("~/Views/Acquisition/NPP/Edit.cshtml", model);
        }

        public IActionResult ApprovalNppById(string creditId, string trans_id)
        {
            LDAPModels ldap = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

            creditId = creditId is not null ? creditId : trans_id;

            dynamic model = new ExpandoObject();

            model.NPP = services.GetNPPViewById(creditId, ldap.EmployeeId).Result.data;
            model.ActionTitle = "APPROVAL";

            if (model.NPP.ItemId == "002")
            {
                model.OrderSourceTAC = services.GetOrderSourceTACByCreditId(creditId).Result.data;
                model.OrderSourceTP = services.GetOrderSourceTPByCreditId(creditId).Result.data;
            }
            return View("~/Views/Acquisition/NPP/View.cshtml", model);
        }

        [HttpPost]
        public IActionResult InsertNPPMotorcycle(TrNppRequest npp, TrItemRequest item)
        {
            try
            {
                CompanyBranchResponse companyData = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                LDAPModels ldap = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

                npp.CreatedBy = ldap.EmployeeId;
                npp.CompanyId = companyData.company_id;
                npp.TrItems = item;
                var data = services.InsertNPPMotorcyle(npp);
                if (data.Result.status == Collection.Status.FAILED.ToString())
                {
                    return Json(new { success = false, messages = "Data Gagal Di Input \n " + data.Result.message });
                }

                return Json(new { success = true, messages = "Data Berhasil Di Input", action = "/NPP/" });

                //return RedirectToAction("Add");
            }
            catch (Exception ex)
            {
                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = "Aksi Gagal";

                return BadRequest(new { responseMsg = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult InsertNPPCar(TrNppRequest npp
                                            , TrItemRequest item
                                            , List<TrDealerOrderSourceTacRequest> osTAC
                                            , List<TrDealerOrderSourceThirdPartyRequest> osTP)
        {
            try
            {
                CompanyBranchResponse companyData = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                LDAPModels ldap = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

                npp.CreatedBy = ldap.EmployeeId;
                npp.CompanyId = companyData.company_id;
                npp.TrItems = item;
                npp.TrDealerOrderSourceTac = osTAC;
                npp.TrDealerOrderSourceThirdParty = osTP;
                var data = services.InsertNPPCar(npp);

                if (data.Result.status == Collection.Status.FAILED.ToString())
                {
                    ViewBag.State = Collection.Status.FAILED;
                    ViewBag.Message = data.Result.message;
                }
                else
                {
                    ViewBag.State = Collection.Status.SUCCESS;

                    return Json(new { success = true, messages = "Data Berhasil Di Input", action = "Index" });
                    //return RedirectToAction("Index");
                }

                return RedirectToAction("Add");
            }
            catch (Exception ex)
            {
                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = "Aksi Gagal";

                return BadRequest(new { responseMsg = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult UpdateNPPMotorcycle(TrNppRequest npp, TrItemRequest item)
        {
            try
            {
                CompanyBranchResponse companyData = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                LDAPModels ldap = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

                npp.CreatedBy = ldap.EmployeeId;
                npp.LastUpdatedBy = ldap.EmployeeId;
                npp.CompanyId = companyData.company_id;
                npp.ConsumenInstallmentDate = npp.InstallmentDate;
                npp.TrItems = item;
                var data = services.UpdateNPPMotorcyle(npp);
                if (data.Result.status == Collection.Status.FAILED.ToString())
                {
                    ViewBag.State = Collection.Status.FAILED;
                    ViewBag.Message = data.Result.message;
                }
                else
                {
                    ViewBag.State = Collection.Status.SUCCESS;

                    //return RedirectToAction("Index");
                    return Json(new { success = true, messages = "Data Berhasil Di Input", action = "Index" });
                }

                return RedirectToAction("EditNppById");
            }
            catch (Exception ex)
            {
                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = "Aksi Gagal";

                return BadRequest(new { responseMsg = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult UpdateNPPCar(TrNppRequest npp
                                            , TrItemRequest item
                                            , List<TrDealerOrderSourceTacRequest> osTAC
                                            , List<TrDealerOrderSourceThirdPartyRequest> osTP)
        {
            try
            {
                CompanyBranchResponse companyData = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                LDAPModels ldap = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

                npp.CreatedBy = ldap.EmployeeId;
                npp.LastUpdatedBy = ldap.EmployeeId;
                npp.CompanyId = companyData.company_id;
                npp.TrItems = item;
                npp.TrDealerOrderSourceTac = osTAC;
                npp.TrDealerOrderSourceThirdParty = osTP;
                var data = services.UpdateNPPCar(npp);

                if (data.Result.status == Collection.Status.FAILED.ToString())
                {
                    ViewBag.State = Collection.Status.FAILED;
                    ViewBag.Message = data.Result.message;
                }
                else
                {
                    ViewBag.State = Collection.Status.SUCCESS;

                    //return RedirectToAction("Index");
                    return Json(new { success = true, messages = "Data Berhasil Di Input", action = "Index" });
                }

                return RedirectToAction("EditNppById");
            }
            catch (Exception ex)
            {
                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = "Aksi Gagal";

                return BadRequest(new { responseMsg = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ApprovalNppProcess(TrNppRequest npp)
        {
            try
            {
                CompanyBranchResponse companyData = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                LDAPModels ldap = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

                npp.CreatedBy = ldap.EmployeeId;
                npp.CreatedOn = DateTime.Now;
                npp.CompanyId = companyData.company_id;
                npp.BranchId = companyData.branch_id;

                //Data Rapindo
                npp.UserName = ldap.EmployeeId;
                npp.BranchName = companyData.branch_name;

                var data = services.ApprovalNPP(npp);

                if (data.Result.status == Collection.Status.FAILED.ToString())
                {
                    ViewBag.State = Collection.Status.FAILED;
                    ViewBag.Message = data.Result.message;
                }
                else
                {
                    ViewBag.State = Collection.Status.SUCCESS;

                    //return RedirectToAction("Index");
                    return Json(new { success = true, messages = "Data Berhasil Di Approve", action = "../../Approval/Inbox" });
                }

                return Json(new { success = false, messages = "Data Gagal Di Approve \n " + data.Result.message });
                //return RedirectToAction("ApprovalNppById");
            }
            catch (Exception ex)
            {
                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = "Aksi Gagal";

                return BadRequest(new { responseMsg = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult PaginationDealerBankRef(int pageIndex, string searchTerm)
        {
            var branch_id = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
            var dataPaging = new PaginationModels<PaginationDealerBankRef>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPaginationDealerBankRef(branch_id, string.Empty, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationDealerBankRef>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationDealerBankRef(branch_id, searchTerm, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationDealerBankRef>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/NPP/Lookup/DealerBankRefPartialView.cshtml", dataPaging);
        }


        [HttpPost]
        public IActionResult PaginationLookupNppProcessCredit(int pageIndex, string searchTerm)
        {
            var branch_id = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
            var dataPaging = new PaginationModels<PaginationLookupNppProcessCredit>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPaginationLookupNppProcessCredit(branch_id, string.Empty, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationLookupNppProcessCredit>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationLookupNppProcessCredit(branch_id, searchTerm, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationLookupNppProcessCredit>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/NPP/Lookup/NppProcessCreditPartialView.cshtml", dataPaging);
        }


        public IActionResult PopupApprovalReason(string statusApproval, string typeApproval)
        {
            dynamic model = new ExpandoObject();
            model.DataApproval = services.GetApprovalReasonListByReasonId(typeApproval).Result.data;
            model.StatusApproval = statusApproval;

            return PartialView("~/Views/Acquisition/NPP/Lookup/ApprovalActionPartialView.cshtml", model);
        }

        public IActionResult GetDataCMByCreditId(string creditId)
        {
            var result = services.GetDataProcessCMByCreditId(creditId).Result.data;
            return Json(result);
        }

        public IActionResult GetDisbursalTypeUMCListByBranchId(string branchId)
        {
            var result = services.GetDisbursalTypeUMCListByBranchId(branchId).Result.data;
            return Json(result);
        }

        public IActionResult GetDealerJobTitleList()
        {
            return Json(services.GetDealerJobTitleList().Result.data);
        }

        public IActionResult GetDealerPersonnelListByJobTitle(int jobTitleId)
        {
            return Json(services.GetDealerPersonnelListByJobTitle(jobTitleId).Result.data);
        }

        [HttpPost]
        public IActionResult CheckBillingReceivedDate(string day)
        {
            var validation = services.CheckBillingReceivedDate(day);
            return Json(validation.Result);
        }

        [HttpPost]
        public IActionResult CheckChasisCode(string creditId, string chassisCode)
        {
            var validation = services.CheckChassisCode(creditId, chassisCode);
            return Json(validation.Result);
        }

        [HttpPost]
        public IActionResult CheckRapindo(string chassisno)
        {
            try
            {
                CompanyBranchResponse companyData = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                LDAPModels ldap = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

                var data = services.CheckRapindo(chassisno, companyData.branch_name, companyData.branch_id, ldap.EmployeeId, companyData.company_id);

                if (data.Result.status == Collection.Status.SUCCESS.ToString())
                {
                    return Ok(data.Result.status);
                }
                else
                {
                    ViewBag.State = Collection.Status.FAILED;

                    return Ok(data.Result.message);
                }
            }
            catch (Exception ex)
            {
                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = "Aksi Gagal";

                return BadRequest(new { responseMsg = ex.Message });
            }
        }

        public IActionResult PrintMOU(string creditId) => PrepareReport("PDF", "pdf", "application/pdf", "rptPerjanjianPembiayaan", creditId);

        public IActionResult PrintPemberitahuanPenting(string creditId) => PrepareReport("PDF", "pdf", "application/pdf", "rptPemberitahuanPenting", creditId);

        public IActionResult PrintFidusia(string creditId) => PrepareReport("PDF", "pdf", "application/pdf", "rptKuasaFidusia", creditId);

        public IActionResult PrintKuasaPenarikan(string creditId) => PrepareReport("PDF", "pdf", "application/pdf", "rptKuasaPenarikan", creditId);

        public IActionResult PrintPernyataanPageOne(string creditId) => PrepareReport("PDF", "pdf", "application/pdf", "rptSuratPernyataan", creditId);

        public IActionResult PrintPernyataanPageTwo(string creditId) => PrepareReport("PDF", "pdf", "application/pdf", "rptSuratPernyataan-2", creditId);

        public IActionResult PrintPersetujuan(string creditId) => PrepareReport("PDF", "pdf", "application/pdf", "rptSuratPersetujuan", creditId);

        public IActionResult PrintPK(string creditId) => PrepareReport("PDF", "pdf", "application/pdf", "rptPernyataanKonsumen", creditId);

        public IActionResult PrintBalikNamaBPKB(string creditId) => PrepareReport("PDF", "pdf", "application/pdf", "rptBalikNamaBPKB", creditId);

        public IActionResult PrintBalikNamaBPKBRecipt(string creditId) => PrepareReport("PDF", "pdf", "application/pdf", "rptBalikNamaBPKBRecipt", creditId);

        private IActionResult PrepareReport(string renderFormat, string extension, string mimeType, string PrintType, string creditId)
        {
            byte[] pdf;

            TrNppPrintRequest printLogParam = new()
            {
                CreditId = creditId,
            };

            try
            {
                LDAPModels ldap = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

                using var report = new LocalReport();

                string ReportName = "";

                if (PrintType == "rptPerjanjianPembiayaan")
                {
                    ReportName = "FINCORE.WEB.Views.Acquisition.NPP.Reports." + PrintType + ".rdlc";

                    APIResponse serviceResult = services.GetReportMemorandumOfUnderstanding(creditId).Result;

                    if (serviceResult.code is StatusCodes.Status500InternalServerError)
                        return ReturnErrorMessage();

                    if (serviceResult.code is StatusCodes.Status404NotFound or StatusCodes.Status422UnprocessableEntity)
                        return ReturnNotFoundBadDataMessage(creditId);

                    PrintMemorandumOfUnderstanding ReportData = (PrintMemorandumOfUnderstanding)serviceResult.data;

                    FillItemType(ReportData);

                    var debtInWords = NumberToWords.ConvertNumberToWords((long)ReportData.DebtAmount);

                    if (string.IsNullOrEmpty(debtInWords))
                        debtInWords = "Nol";

                    ReportData.DebtAmountInWords = $"{debtInWords} Rupiah";

                    Load(report, ReportData, ReportName);

                    // save print log
                    printLogParam.UserName = GetUserData().DisplayName;
                    printLogParam.IsPrintPK = true;

                    APIResponse logResult = services.InsertReportLog(printLogParam);
                }
                else if (PrintType == "rptPemberitahuanPenting")
                {
                    ReportName = "FINCORE.WEB.Views.Acquisition.NPP.Reports." + PrintType + ".rdlc";

                    APIResponse serviceResult = services.GetImportantNotice(creditId).Result;

                    if (serviceResult.code is StatusCodes.Status500InternalServerError)
                        return ReturnErrorMessage();

                    if (serviceResult.code is StatusCodes.Status404NotFound or StatusCodes.Status422UnprocessableEntity)
                        return ReturnNotFoundBadDataMessage(creditId);

                    PrintImportantNotice ReportData = (PrintImportantNotice)serviceResult.data;

                    var monthylyInstallmentInWords = NumberToWords.ConvertNumberToWords((long)ReportData.MonthlyInstallment);

                    if (string.IsNullOrEmpty(monthylyInstallmentInWords))
                        monthylyInstallmentInWords = "Nol";

                    ReportData.MonthlyInstallmentInWords = $"{monthylyInstallmentInWords} Rupiah";

                    Load(report, ReportData, ReportName);
                }
                else if (PrintType == "rptKuasaFidusia")
                {
                    ReportName = "FINCORE.WEB.Views.Acquisition.NPP.Reports." + PrintType + ".rdlc";

                    APIResponse serviceResult = services.GetReportPowerOfAttorneyFidusia(creditId).Result;

                    if (serviceResult.code is StatusCodes.Status500InternalServerError)
                        return ReturnErrorMessage();

                    if (serviceResult.code is StatusCodes.Status404NotFound or StatusCodes.Status422UnprocessableEntity)
                        return ReturnNotFoundBadDataMessage(creditId);

                    PrintPowerOfAttorneyFidusia ReportData = (PrintPowerOfAttorneyFidusia)serviceResult.data;

                    SetDynamicValueFidusiaRDLC(ReportData);

                    printLogParam.UserName = GetUserData().DisplayName;
                    printLogParam.IsPrintFidusia = true;

                    APIResponse logResult = services.InsertReportLog(printLogParam);

                    Load(report, ReportData, ReportName);
                }
                else if (PrintType == "rptKuasaPenarikan")
                {
                    ReportName = "FINCORE.WEB.Views.Acquisition.NPP.Reports." + PrintType + ".rdlc";

                    APIResponse serviceResult = services.GetReportPowerOfAttorney(creditId).Result;

                    if (serviceResult.code is StatusCodes.Status500InternalServerError)
                        return ReturnErrorMessage();

                    if (serviceResult.code is StatusCodes.Status404NotFound or StatusCodes.Status422UnprocessableEntity)
                        return ReturnNotFoundBadDataMessage(creditId);

                    PrintPowerOfAttorney ReportData = (PrintPowerOfAttorney)serviceResult.data;

                    SetDynamicValuePowerOfAttorneyRDLC(ReportData);

                    ReportData.NumberOfUnitInWord = NumberToWords.ConvertNumberToWords(ReportData.NumberOfUnit);

                    Load(report, ReportData, ReportName);
                }
                else if (PrintType == "rptSuratPernyataan")
                {
                    ReportName = "FINCORE.WEB.Views.Acquisition.NPP.Reports." + PrintType + ".rdlc";

                    APIResponse serviceResult = services.GetReportStatementLetter(creditId).Result;

                    if (serviceResult.code is StatusCodes.Status500InternalServerError)
                        return ReturnErrorMessage();

                    if (serviceResult.code is StatusCodes.Status404NotFound or StatusCodes.Status422UnprocessableEntity)
                        return ReturnNotFoundBadDataMessage(creditId);

                    PrintStatementLetter ReportData = (PrintStatementLetter)serviceResult.data;

                    Load(report, ReportData, ReportName);
                }
                else if (PrintType == "rptSuratPernyataan-2")
                {
                    ReportName = "FINCORE.WEB.Views.Acquisition.NPP.Reports." + PrintType + ".rdlc";

                    APIResponse serviceResult = services.GetReportSecondStatementLetter(creditId).Result;

                    if (serviceResult.code is StatusCodes.Status500InternalServerError)
                        return ReturnErrorMessage();

                    if (serviceResult.code is StatusCodes.Status404NotFound or StatusCodes.Status422UnprocessableEntity)
                        return ReturnNotFoundBadDataMessage(creditId);

                    PrintSecondStatementLetter ReportData = (PrintSecondStatementLetter)serviceResult.data;

                    Load(report, ReportData, ReportName);
                }
                else if (PrintType == "rptPernyataanKonsumen")
                {
                    ReportName = "FINCORE.WEB.Views.Acquisition.NPP.Reports." + PrintType + ".rdlc";

                    APIResponse serviceResult = services.GetReportConsumentStatementLetter(creditId).Result;

                    if (serviceResult.code is StatusCodes.Status500InternalServerError)
                        return ReturnErrorMessage();

                    if (serviceResult.code is StatusCodes.Status404NotFound or StatusCodes.Status422UnprocessableEntity)
                        return ReturnNotFoundBadDataMessage(creditId);

                    PrintConsumentStatementLetter ReportData = (PrintConsumentStatementLetter)serviceResult.data;

                    Load(report, ReportData, ReportName);
                }
                else if (PrintType == "rptBalikNamaBPKB")
                {
                    ReportName = "FINCORE.WEB.Views.Acquisition.NPP.Reports." + PrintType + ".rdlc";

                    APIResponse serviceResult = services.GetReportNameChangeStatementLetter(creditId).Result;

                    if (serviceResult.code is StatusCodes.Status500InternalServerError)
                        return ReturnErrorMessage();

                    if (serviceResult.code is StatusCodes.Status404NotFound or StatusCodes.Status422UnprocessableEntity)
                        return ReturnNotFoundBadDataMessage(creditId);

                    if (serviceResult.code == StatusCodes.Status403Forbidden)
                        return ReturnForbidenActionMessage(creditId);

                    PrintNameChangeStatementLetter ReportData = (PrintNameChangeStatementLetter)serviceResult.data;

                    Load(report, ReportData, ReportName);
                }
                else if (PrintType == "rptSuratPersetujuan")
                {
                    ReportName = "FINCORE.WEB.Views.Acquisition.NPP.Reports." + PrintType + ".rdlc";

                    APIResponse serviceResult = services.GetApprovalLetter(creditId).Result;

                    if (serviceResult.code is StatusCodes.Status500InternalServerError)
                        return ReturnErrorMessage();

                    if (serviceResult.code is StatusCodes.Status404NotFound or StatusCodes.Status422UnprocessableEntity)
                        return ReturnNotFoundBadDataMessage(creditId);

                    PrintApprovalLetter ReportData = (PrintApprovalLetter)serviceResult.data;

                    Load(report, ReportData, ReportName);
                }
                else if (PrintType == "rptBalikNamaBPKBRecipt")
                {
                    ReportName = "FINCORE.WEB.Views.Acquisition.NPP.Reports." + PrintType + ".rdlc";

                    APIResponse serviceResult = services.GetReportNameChangeReceipt(creditId).Result;

                    if (serviceResult.code is StatusCodes.Status500InternalServerError)
                        return ReturnErrorMessage();

                    if (serviceResult.code is StatusCodes.Status404NotFound or StatusCodes.Status422UnprocessableEntity)
                        return ReturnNotFoundBadDataMessage(creditId);

                    PrintNameChangeReceipt ReportData = (PrintNameChangeReceipt)serviceResult.data;

                    Load(report, ReportData, ReportName);
                }

                #region unknown commented region

                //else if (PrintType == "SK")
                //{
                //    ReportName = "FINCORE.WEB.Views.Acquisition.BPKB.Reports.rptEPBPKBSK.rdlc";

                //    BPKBSKReportModel ReportData = new BPKBSKReportModel();
                //    CompanyBranchModel branch = (CompanyBranchModel)services.GetBranchDetail("00115").Result.data;
                //    BPKBSKDetailModel data = (BPKBSKDetailModel)services.GetBPKBSKDetail(Model).Result.data;
                //    ReportData.CreditId = creditid;
                //    ReportData.BranchAddress = branch.BranchAddress;
                //    ReportData.BranchName = branch.BranchName;
                //    ReportData.PersonPosition = "Kepala Cabang";
                //    ReportData.CompanyName = branch.CompanyId == 2 ? "Mega Auto Finance" : "Mega Central Finance";
                //    ReportData.Month = GetMonth();
                //    ReportData.ContactPerson = branch.ContactPerson;
                //    ReportData.CustomerAddress = data.CustomerAddress;
                //    ReportData.CustomerName = data.CustomerName;
                //    ReportData.BPKBNo = data.BpkbNo;
                //    ReportData.IdentityNumber = data.IdentityNo;
                //    ReportData.MachineNo = data.MachineNo;
                //    ReportData.ChassisNo = data.ChasisNo;
                //    ReportData.AssetTypeDescription = data.AssetTypeDescription;
                //    ReportData.CarNo = data.CarNo;
                //    ReportData.ItemColor = data.ItemColor;
                //    ReportData.SumPrintSK = data.SumPrintSK;

                //    Report.Load(report, ReportData, ReportName);

                //    Model.PrintedFirstBy = "FirstPrinter";
                //    Model.PrintedFirstDate = DateTime.Today;
                //    Model.PrintedLastBy = "LastPrinter";
                //    Model.PrintedLastDate = DateTime.Today;
                //    Model.IsPrintSk = true;
                //}
                //else if (PrintType == "PinjamInside")
                //{
                //    Model.PrintFirstByThirdParty = "FirstThirdPartyPrinter";
                //    Model.PrintFirstDateThirdParty = DateTime.Today;
                //    Model.PrintLastByThirdParty = "LastThirdPartyPrinter";
                //    Model.PrintLastDateThirdParty = DateTime.Today;
                //    Model.IsPrintThirdParty = true;
                //}

                #endregion unknown commented region

                pdf = report.Render(renderFormat);
                //services.UpdatePrintBPKB(Model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Cannot create document or report, Error has happen. Try again later. {ex.Message} ";

                return RedirectToAction("Index");
            }

            var filename = $"{PrintType}-{creditId}-{DateTime.Now:yyyyMMdd-HHmm}.{extension}";

            return File(pdf, mimeType, filename);
        }

        private IActionResult ReturnForbidenActionMessage(string creditId)
        {
            TempData["Warning"] = $"Data for this report with CreditId: {creditId} Not Available, These report only for used car or motorcycle";

            return RedirectToAction("Index");
        }

        private IActionResult ReturnNotFoundBadDataMessage(string creditId)
        {
            TempData["Warning"] = $"Data for this report with CreditId: {creditId} Not found or Corrupt";

            return RedirectToAction("Index");
        }

        private IActionResult ReturnErrorMessage()
        {
            TempData["Error"] = $"Error Has Occured, try again later!!!";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Karena perbedaan posisi untuk field alamat HO dan branch pada dokumen Fidusia untuk MCF dan MAF
        /// Maka perlu dibuatkan dua field HO dan branch
        /// Ketika user yang melakukan print login dengan company 1 atau MCF maka field untuk MAF dikosongkan
        ///  2 = MAF, 3 = MCF
        /// </summary>
        /// <param name="ReportData"></param>
        private void SetDynamicValueFidusiaRDLC(PrintPowerOfAttorneyFidusia ReportData)
        {
            //get company id from session then parse and assign to companyId
            _ = int.TryParse(GetBranchData().company_id, out int companyId);

            switch (companyId)
            {
                case 2:
                    ReportData.MCFHOAdress = "";
                    ReportData.MCFBranchAddress = "";
                    break;

                case 3:
                    ReportData.MAFHOAdress = "";
                    ReportData.MAFBranchAddress = "";
                    break;

                default:
                    ReportData.MAFHOAdress = "";
                    ReportData.MAFBranchAddress = "";
                    ReportData.MCFHOAdress = "";
                    ReportData.MCFBranchAddress = "";
                    break;
            }
        }

        private void SetDynamicValuePowerOfAttorneyRDLC(PrintPowerOfAttorney ReportData)
        {
            //get company id from session then parse and assign to companyId
            _ = int.TryParse(GetBranchData().company_id, out int companyId);

            switch (companyId)
            {
                case 2:
                    ReportData.BranchAddressMAF = ReportData.BranchAddress;
                    ReportData.BranchAddress = "";
                    break;

                default:
                    ReportData.BranchAddressMAF = "";
                    ReportData.BranchAddress = "";
                    break;
            }
        }

        /// <summary>
        /// Add value and logic to non presistance data (mark for brand new or used car/ motorbike)
        /// </summary>
        /// <param name="reportData"></param>
        private static void FillItemType(PrintMemorandumOfUnderstanding reportData)
        {
            var isBrandNew = Convert.ToBoolean(reportData.BrandNew);

            if (reportData.AssetType.ToLower().Contains("motor"))
            {
                if (isBrandNew)
                {
                    reportData.BrandNewMotorcycleMark = "X";
                }
                else
                {
                    reportData.UsedMotorcycleMark = "X";
                }
            }
            else if (reportData.AssetType.ToLower().Contains("mobil"))
            {
                if (isBrandNew)
                {
                    reportData.BrandNewCarMark = "X";
                }
                else
                {
                    reportData.UsedCarMark = "X";
                }
            }
        }

        /// <summary>
        /// run this func to create xsd file based on Print Model Class
        /// </summary>
        public void CreateReportSchema()
        {
            try
            {
                //put model in here
                var types = new[]
                {
                    typeof(PrintConsumentStatementLetter),
                    typeof(PrintMemorandumOfUnderstanding),
                    typeof(PrintImportantNotice),
                    typeof(PrintNameChangeStatementLetter),
                    typeof(PrintNameChangeReceipt),
                    typeof(PrintPowerOfAttorney),
                    typeof(PrintPowerOfAttorneyFidusia),
                    typeof(PrintSecondStatementLetter),
                    typeof(PrintStatementLetter),
                    typeof(PrintApprovalLetter)
                };

                var xri = new System.Xml.Serialization.XmlReflectionImporter();
                var xss = new System.Xml.Serialization.XmlSchemas();
                var xse = new System.Xml.Serialization.XmlSchemaExporter(xss);

                foreach (var type in types)
                {
                    var xtm = xri.ImportTypeMapping(type);
                    xse.ExportTypeMapping(xtm);
                }

                using var sw = new StreamWriter("ReportNPPSchemas.xsd", false, Encoding.UTF8);
                for (int i = 0; i < xss.Count; i++)
                {
                    var xs = xss[i];
                    xs.Id = "ReportItemSchemas";
                    xs.Write(sw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private LDAPModels GetUserData() =>
            Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

        private CompanyBranchResponse GetBranchData() =>
            Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
    }
}