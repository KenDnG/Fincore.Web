using FINCORE.Models.Models.Acquisition.BPKB;
using FINCORE.Models.Models.Acquisition.BPKB.Paging;
using FINCORE.Models.Models.Acquisition.BPKB.Report;
using FINCORE.Models.Models.LDAP;
using FINCORE.Models.Models.Masters;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Models.ViewModels;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.Acquisition;
using FINCORE.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Reporting.NETCore;
using System.Text;
using static FINCORE.Services.Helpers.Response.Collection;
using static FINCORE.WEB.Helpers.Commons;
using Report = FINCORE.WEB.Views.Acquisition.BPKB.Reports.Report;

//using Rotativa.AspNetCore;

namespace FINCORE.WEB.Controllers.Acquisition
{
    public class BPKBController : Controller
    {
        //private CompanyBranchResponse CompanyBranchCredential;
        //private LDAPModels UserLDAPCredential;
        private BPKBServices services = new BPKBServices();

        public IActionResult Index(string AgreementNumber, string EditType, string Status)
        {
            try
            {
                ModelState.Clear();
                var CompanyBranchCredential = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                var UserLDAPCredential = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

                //get branchid session
                string branchid = Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id");
                setListsBPKB(branchid);
                //prepare VM
                BPKBModels bpkbm = new BPKBModels();
                ViewModelBPKB VM = new ViewModelBPKB();
                BPKBCMModel CM = new BPKBCMModel();

                //set session type
                Sessions.SetSessionAsJson(HttpContext.Session, "Type", EditType);

                ViewBag.UMC = false;
                Sessions.SetSessionAsJson(HttpContext.Session, "IsMB", false);
                if (AgreementNumber != "" && AgreementNumber != null)//edit
                {
                    //set parameter to get VM data
                    bpkbm.CreditId = AgreementNumber;
                    bpkbm.BpkbStatus = Status;
                    bpkbm.BranchId = branchid;
                    ViewBag.EditType = EditType;
                    ViewBag.DisableControls = "false";
                    CM = (BPKBCMModel)services.GetCM(bpkbm).Result.data;
                    if (CM != null)
                    {
                        if (CM.ApplicationTypeId == "03")
                        {
                            Sessions.SetSessionAsJson(HttpContext.Session, "IsMB", true);
                            ViewBag.UMC = true;
                        }
                    }
                    //set session mode
                    Sessions.SetSessionAsJson(HttpContext.Session, "IsEdit", "Edit");
                    //populate VM
                    VM = (ViewModelBPKB)services.GetDataBPKB(bpkbm).Result.data;
                    if (EditType != "ReEntry")
                    {
                        VM.BPKBPinjam = new BPKBPinjamModels();
                    }
                    if (EditType == "View")
                    {
                        ViewBag.DisableControls = "true";
                    }
                    if (EditType == "Pinjam")
                    {
                    }
                }
                else
                {
                    Sessions.SetSessionAsJson(HttpContext.Session, "IsEdit", "Add");
                    ViewBag.AddEdit = "Add";
                }

                return View("~/Views/Acquisition/BPKB/Index.cshtml", VM);
            }
            catch (Exception ex)
            {
                string state = Collection.Status.FAILED;
                string msg = ex.Message;
                Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                return RedirectToAction("List");
            }
        }

        public IActionResult List(int pageIndex, string searchTerm)
        {
            try
            {
                //CreateReportSchema();
                var CompanyBranchCredential = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                var UserLDAPCredential = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);

                string BranchID = Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id"); ;//"00115";//hardcoded
                var dataPaging = new PaginationModels<PagingBPKBResultModel>();
                var page = pageIndex == 0 ? 1 : pageIndex;
                if (searchTerm == null)
                {
                    searchTerm = Request.Query["searchTerm"].ToString();
                }
                if (searchTerm == null || searchTerm == string.Empty)
                {
                    var data = services.GetPagingList(string.Empty, BranchID, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBResultModel>)data.Result.data;
                }
                else
                {
                    page = 1;
                    var data = services.GetPagingList(searchTerm, BranchID, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBResultModel>)data.Result.data;
                }
                ViewBag.CurrentPage = page;
                ViewBag.PagingBPKB = dataPaging;
                ViewBag.BPKBModels = dataPaging.Model;
                ViewBag.Message = Sessions.GetSessionFromJson<string>(HttpContext.Session, "msg");

                ViewBag.State = Sessions.GetSessionFromJson<string>(HttpContext.Session, "state");

                Sessions.RemoveSessionByKey(HttpContext.Session, "msg");
                Sessions.RemoveSessionByKey(HttpContext.Session, "state");
                return View("~/Views/Acquisition/BPKB/List.cshtml");
            }
            catch (Exception ex)
            {
                string state = Collection.Status.FAILED;
                string msg = ex.Message;
                Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                return RedirectToAction("List");
            }
        }

        public IActionResult Cancel()
        {
            Sessions.SetSessionAsJson(HttpContext.Session, "state", "Cancel");
            return RedirectToAction("List");
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert([FromBody] ViewModelBPKB item)
        {
            var data = services.InsertBPKB(item);
            return Ok(data);
        }

        public (string state, string message) InsertRazor([Bind] ViewModelBPKB item)
        {
            string message = "";
            string state = "";
            var data = services.InsertBPKB(item);
            if (data.Result.status == Collection.Status.SUCCESS)
            {
                state = Collection.Status.SUCCESS;
            }
            else
            {
                state = Collection.Status.FAILED;
                message = data.Result.message;
            }
            return (state, message);
        }

        [HttpPost]
        public IActionResult Update([FromBody] ViewModelBPKB item)
        {
            var data = services.UpdateBPKB(item);
            return Ok(data);
        }

        public (string state, string message) UpdateRazor([Bind] ViewModelBPKB item)
        {
            //decide status bpkb
            string statusbpkb = Sessions.GetSessionFromJson<string>(HttpContext.Session, "Type");
            string message = "";
            string state = "";
            if (statusbpkb == "Pinjam")
            {
                var data = services.SaveBPKBPinjam(item);
                if (data.Result.status == Collection.Status.SUCCESS)
                {
                    state = Collection.Status.SUCCESS;
                }
                else
                {
                    state = Collection.Status.FAILED;
                    message = data.Result.message;
                }
            }
            else if (statusbpkb == "Out")
            {
                var data = services.OutBPKB(item);
                if (data.Result.status == Collection.Status.SUCCESS)
                {
                    state = Collection.Status.SUCCESS;
                }
                else
                {
                    state = Collection.Status.FAILED;
                    message = data.Result.message;
                }
            }
            else if (statusbpkb == "ReEntry")
            {
                item.BPKB.LocationCode = item.BPKB.ReEntryLocationCode;
                var data = services.ReEntryBPKB(item);
                if (data.Result.status == Collection.Status.SUCCESS)
                {
                    state = Collection.Status.SUCCESS;
                }
                else
                {
                    state = Collection.Status.FAILED;
                    message = data.Result.message;
                }
            }
            else
            {
                var data = services.UpdateBPKB(item);
                if (data.Result.status == Collection.Status.SUCCESS)
                {
                    state = Collection.Status.SUCCESS;
                }
                else
                {
                    state = Collection.Status.FAILED;
                    message = data.Result.message;
                }
            }

            return (state, message);
        }

        [HttpPost]
        public IActionResult BPKBPopupTest(int pageIndex, string searchTerm)
        {
            try
            {
                var CompanyBranchCredential = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                var UserLDAPCredential = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
                string BranchID = Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id"); //"00115";//hardcoded
                ModelState.Clear();
                ViewModelBPKBLookup VM = new ViewModelBPKBLookup();
                var dataPaging = new PaginationModels<PagingBPKBLookupNPPModel>();
                var page = pageIndex == 0 ? 1 : pageIndex;
                if (searchTerm == null)
                {
                    searchTerm = Request.Query["searchTerm"].ToString();
                }
                if (searchTerm == null || searchTerm == string.Empty)
                {
                    var data = services.GetBPKBLookupNPP(string.Empty, BranchID, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBLookupNPPModel>)data.Result.data;
                }
                else
                {
                    page = 1;
                    var data = services.GetBPKBLookupNPP(searchTerm, BranchID, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBLookupNPPModel>)data.Result.data;
                }

                VM.page = page;
                VM.paging = dataPaging;
                VM.data = dataPaging.Model;
                ViewBag.Title = "Popup CreditID";
                return PartialView("~/Views/Acquisition/BPKB/Lookups/PartialViewBPKB.cshtml", VM);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Save(ViewModelBPKB item)
        {
            var CompanyBranchCredential = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
            var UserLDAPCredential = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            var bid = Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id");
            var bnm = Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_name");
            string branchid = Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id");
            try
            {
                var isedit = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsEdit");
                var type = Sessions.GetSessionFromJson<string>(HttpContext.Session, "Type");
                //disable some validation
                if (type == null || type == "In" || type == "Pinjam")
                {
                    ModelState.Remove("BPKB.OutCode");
                    ModelState.Remove("BPKB.ReceiverCode");
                    ModelState.Remove("BPKB.ReEntryLocationCode");
                }
                else if (type == "Out")
                {
                    ModelState.Remove("BPKB.ReEntryLocationCode");
                }
                else if (type == "ReEntry")
                {
                    ModelState.Remove("BPKB.OutCode");
                    ModelState.Remove("BPKB.ReceiverCode");
                }
                item.BPKB.IsMb = Sessions.GetSessionFromJson<bool>(HttpContext.Session, "IsMB");

                //ModelState.Values.SelectMany(y => y.Errors) //to check modelstate validation
                if (ModelState.IsValid)
                {
                    var locname = Request.Form["location_name"];
                    item.BranchName = bnm;
                    item.BranchId = bid;
                    item.CompanyId = CompanyBranchCredential.company_id;
                    item.UserName = UserLDAPCredential.EmployeeId;
                    //item.Lokasi = locname;
                    string state;
                    string msg;
                    if (isedit == "Add")
                    {
                        //set data for insert

                        //set ismb for insert
                        BPKBCMModel CM = new BPKBCMModel();
                        CM = (BPKBCMModel)services.GetCM(item.BPKB).Result.data;
                        if (CM != null)
                        {
                            if (CM.ApplicationTypeId == "03")
                            {
                                item.BPKB.IsMb = true;
                            }
                        }

                        item.BPKB.CreatedOn = DateTime.Now;
                        item.BPKB.CreatedBy = UserLDAPCredential.EmployeeId;
                        item.BPKB.CompanyId = CompanyBranchCredential.company_id;
                        item.BPKB.SecquenceNo = "0";
                        item.BPKB.BpkbStatus = "I";

                        (state, msg) = InsertRazor(item);
                        Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                        Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                        if (state == Collection.Status.SUCCESS)
                        {
                            return RedirectToAction("List");
                        }
                        else
                        {
                            #region kembali ke page index

                            ViewBag.State = state;
                            ViewBag.Message = msg;
                            setListsBPKB(branchid);
                            return View("~/Views/Acquisition/BPKB/Index.cshtml", item);

                            #endregion kembali ke page index
                        }
                    }
                    else
                    {
                        if (item.BPKBPinjam != null)
                        {
                            item.BPKBPinjam.CreatedBy = UserLDAPCredential.EmployeeId;
                            item.BPKBPinjam.LastUpdatedBy = UserLDAPCredential.EmployeeId;
                        }

                        (state, msg) = UpdateRazor(item);
                        Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                        Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                        if (state == Collection.Status.SUCCESS)
                        {
                            return RedirectToAction("List");
                        }
                        else
                        {
                            #region kembali ke page index

                            //setListsBPKB(branchid);
                            //ViewBag.EditType = type;
                            //return View("~/Views/Acquisition/BPKB/Index.cshtml", item);

                            #endregion kembali ke page index

                            #region ke paging

                            return RedirectToAction("List");

                            #endregion ke paging
                        }
                    }
                }
                else
                {
                    setListsBPKB(branchid);
                    if (isedit == "Edit")
                    {
                        ViewBag.EditType = type;
                    }
                    return View("~/Views/Acquisition/BPKB/Index.cshtml", item);
                }
            }
            catch (Exception ex)
            {
                string state = Collection.Status.FAILED;
                string msg = ex.Message;
                Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public IActionResult BPKBPopupReceiver(int pageIndex, string searchTerm)
        {
            try
            {
                ModelState.Clear();
                ViewModelBPKBLookup VM = new ViewModelBPKBLookup();
                var dataPaging = new PaginationModels<PagingBPKBReceiverLookup>();
                var page = pageIndex == 0 ? 1 : pageIndex;
                if (searchTerm == null)
                {
                    searchTerm = Request.Query["searchTerm"].ToString();
                }
                if (searchTerm == null || searchTerm == string.Empty)
                {
                    var data = services.GetBPKBReceiverLookup(string.Empty, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBReceiverLookup>)data.Result.data;
                }
                else
                {
                    page = 1;
                    var data = services.GetBPKBReceiverLookup(searchTerm, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBReceiverLookup>)data.Result.data;
                }

                VM.page = page;
                VM.PagingReceiver = dataPaging;
                ViewBag.Title = "Popup Receiver";

                return PartialView("~/Views/Acquisition/BPKB/Lookups/PartialViewBPKBReceiver.cshtml", VM);
            }
            catch (Exception ex)
            {
                string state = Collection.Status.FAILED;
                string msg = ex.Message;
                Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public IActionResult BPKBPopupReceiverOut(int pageIndex, string searchTerm)
        {
            try
            {
                ModelState.Clear();
                ViewModelBPKBLookup VM = new ViewModelBPKBLookup();
                var dataPaging = new PaginationModels<PagingBPKBReceiverLookup>();
                var page = pageIndex == 0 ? 1 : pageIndex;
                if (searchTerm == null)
                {
                    searchTerm = Request.Query["searchTerm"].ToString();
                }
                if (searchTerm == null || searchTerm == string.Empty)
                {
                    var data = services.GetBPKBReceiverLookup(string.Empty, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBReceiverLookup>)data.Result.data;
                }
                else
                {
                    page = 1;
                    var data = services.GetBPKBReceiverLookup(searchTerm, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBReceiverLookup>)data.Result.data;
                }

                VM.page = page;
                VM.PagingReceiver = dataPaging;
                ViewBag.Title = "Popup Receiver Out";
                return PartialView("~/Views/Acquisition/BPKB/Lookups/PartialViewBPKBReceiverOut.cshtml", VM);
            }
            catch (Exception ex)
            {
                string state = Collection.Status.FAILED;
                string msg = ex.Message;
                Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public IActionResult BPKBPopupBureau(int pageIndex, string searchTerm)
        {
            try
            {
                ModelState.Clear();
                ViewModelBPKBLookup VM = new ViewModelBPKBLookup();
                var dataPaging = new PaginationModels<PagingBPKBTypeOfBureau>();
                var page = pageIndex == 0 ? 1 : pageIndex;
                if (searchTerm == null)
                {
                    searchTerm = Request.Query["searchTerm"].ToString();
                }
                if (searchTerm == null || searchTerm == string.Empty)
                {
                    var data = services.GetBPKBPagingTypeOfBureau(string.Empty, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBTypeOfBureau>)data.Result.data;
                }
                else
                {
                    page = 1;
                    var data = services.GetBPKBPagingTypeOfBureau(searchTerm, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBTypeOfBureau>)data.Result.data;
                }

                VM.page = page;
                VM.PagingBureau = dataPaging;
                ViewBag.Title = "Popup Bureau";
                return PartialView("~/Views/Acquisition/BPKB/Lookups/PartialViewBPKBBureau.cshtml", VM);
            }
            catch (Exception ex)
            {
                string state = Collection.Status.FAILED;
                string msg = ex.Message;
                Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public IActionResult BPKBPopupDealer(int pageIndex, string searchTerm, string CreditId)
        {
            try
            {
                var CompanyBranchCredential = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                var UserLDAPCredential = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
                //string BranchID = Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id");//"704";//hardcoded
                ModelState.Clear();
                ViewModelBPKBLookup VM = new ViewModelBPKBLookup();
                var dataPaging = new PaginationModels<PagingBPKBDealerModel>();
                var page = pageIndex == 0 ? 1 : pageIndex;
                if (searchTerm == null)
                {
                    searchTerm = Request.Query["searchTerm"].ToString();
                }
                if (searchTerm == null || searchTerm == string.Empty)
                {
                    var data = services.GetBPKBDealerLookup(string.Empty, CreditId, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBDealerModel>)data.Result.data;
                }
                else
                {
                    page = 1;
                    var data = services.GetBPKBDealerLookup(searchTerm, CreditId, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBDealerModel>)data.Result.data;
                }

                VM.page = page;
                VM.PagingDealer = dataPaging;
                ViewBag.Title = "Popup Dealer";
                return PartialView("~/Views/Acquisition/BPKB/Lookups/PartialViewBPKBDealer.cshtml", VM);
            }
            catch (Exception ex)
            {
                string state = Collection.Status.FAILED;
                string msg = ex.Message;
                Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public IActionResult BPKBPopupAsuransi(int pageIndex, string searchTerm)
        {
            try
            {
                ModelState.Clear();
                ViewModelBPKBLookup VM = new ViewModelBPKBLookup();
                var dataPaging = new PaginationModels<PagingBPKBAsuransiLookup>();
                var page = pageIndex == 0 ? 1 : pageIndex;
                if (searchTerm == null)
                {
                    searchTerm = Request.Query["searchTerm"].ToString();
                }
                if (searchTerm == null || searchTerm == string.Empty)
                {
                    var data = services.GetBPKBAsuransiLookup(string.Empty, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBAsuransiLookup>)data.Result.data;
                }
                else
                {
                    page = 1;
                    var data = services.GetBPKBAsuransiLookup(searchTerm, page, Paginations.MaxPerPage);
                    dataPaging = (PaginationModels<PagingBPKBAsuransiLookup>)data.Result.data;
                }

                VM.page = page;
                VM.PagingAsuransi = dataPaging;
                ViewBag.Title = "Popup Asuransi";
                return PartialView("~/Views/Acquisition/BPKB/Lookups/PartialViewBPKBAsuransi.cshtml", VM);
            }
            catch (Exception ex)
            {
                string state = Collection.Status.FAILED;
                string msg = ex.Message;
                Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public IActionResult BPKBPopupBiroJasa(int pageIndex, string searchTerm)
        {
            try
            {
                ModelState.Clear();
                var CompanyBranchCredential = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                var UserLDAPCredential = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
                string BranchID = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
                ViewModelBPKBLookup VM = new ViewModelBPKBLookup();
                var dataPaging = new PaginationModels<PagingBPKBBiroJasaModel>();
                var page = pageIndex == 0 ? 1 : pageIndex;
                if (searchTerm == null)
                {
                    searchTerm = Request.Query["searchTerm"].ToString();
                }
                if (searchTerm == null || searchTerm == string.Empty)
                {
                    var data = services.GetBPKBBiroJasaLookup(string.Empty, page, Paginations.MaxPerPage, BranchID);
                    dataPaging = (PaginationModels<PagingBPKBBiroJasaModel>)data.Result.data;
                }
                else
                {
                    page = 1;
                    var data = services.GetBPKBBiroJasaLookup(searchTerm, page, Paginations.MaxPerPage, BranchID);
                    dataPaging = (PaginationModels<PagingBPKBBiroJasaModel>)data.Result.data;
                }

                VM.page = page;
                VM.PagingBiroJasa = dataPaging;
                ViewBag.Title = "Popup Biro Jasa";
                return PartialView("~/Views/Acquisition/BPKB/Lookups/PartialViewBPKBBiroJasa.cshtml", VM);
            }
            catch (Exception ex)
            {
                string state = Collection.Status.FAILED;
                string msg = ex.Message;
                Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public IActionResult BPKBPopupKaryawan(int pageIndex, string searchTerm)
        {
            try
            {
                ModelState.Clear();
                var CompanyBranchCredential = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                var UserLDAPCredential = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
                string BranchID = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
                ViewModelBPKBLookup VM = new ViewModelBPKBLookup();
                var dataPaging = new PaginationModels<PagingBPKBKaryawanModel>();
                var page = pageIndex == 0 ? 1 : pageIndex;
                if (searchTerm == null)
                {
                    searchTerm = Request.Query["searchTerm"].ToString();
                }
                if (searchTerm == null || searchTerm == string.Empty)
                {
                    var data = services.GetBPKBKaryawanLookup(string.Empty, page, Paginations.MaxPerPage, BranchID);
                    dataPaging = (PaginationModels<PagingBPKBKaryawanModel>)data.Result.data;
                }
                else
                {
                    page = 1;
                    var data = services.GetBPKBKaryawanLookup(searchTerm, page, Paginations.MaxPerPage, BranchID);
                    dataPaging = (PaginationModels<PagingBPKBKaryawanModel>)data.Result.data;
                }

                VM.page = page;
                VM.PagingKaryawan = dataPaging;
                ViewBag.Title = "Popup Karyawan";
                return PartialView("~/Views/Acquisition/BPKB/Lookups/PartialViewBPKBKaryawan.cshtml", VM);
            }
            catch (Exception ex)
            {
                string state = Collection.Status.FAILED;
                string msg = ex.Message;
                Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                return RedirectToAction("List");
            }
        }

        public void setListsBPKB(string BranchID)
        {
            try
            {
                //BranchID = "115";//hardcoded
                var locdata = services.GetBPKBLocation(BranchID);
                var reasondata = services.GetBPKBReasonList();
                var bureaudata = services.GetBPKBBureauList();
                List<BPKBLocationModel> loclist = (List<BPKBLocationModel>)locdata.Result.data;
                List<BPKBReasonModel> reasonlist = (List<BPKBReasonModel>)reasondata.Result.data;
                List<BPKBBureauModel> bureaulist = (List<BPKBBureauModel>)bureaudata.Result.data;
                ViewBag.ReasonList = new SelectList(reasonlist, "ReasonId", "ReasonName");
                ViewBag.LocationList = new SelectList(loclist, "LocationCode", "LocationName");
                ViewBag.BureauList = new SelectList(bureaulist, "ReasonId", "ReasonName");
                ViewBag.UMC = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region printing with rdlc

        public void CreateReportSchema()//run this func to create xsd file
        {
            try
            {
                var types = new[] { typeof(BPKBBastReportModel), typeof(BPKBModels), typeof(BPKBSKReportModel), typeof(BPKBPinjamReportModel), typeof(BPKBBASTINReportModel), typeof(BPKBThirdPartyReportModel) };//put model in here
                var xri = new System.Xml.Serialization.XmlReflectionImporter();
                var xss = new System.Xml.Serialization.XmlSchemas();
                var xse = new System.Xml.Serialization.XmlSchemaExporter(xss);
                foreach (var type in types)
                {
                    var xtm = xri.ImportTypeMapping(type);
                    xse.ExportTypeMapping(xtm);
                }
                using var sw = new StreamWriter("ReportBPKBSchemas.xsd", false, Encoding.UTF8);
                for (int i = 0; i < xss.Count; i++)
                {
                    var xs = xss[i];
                    xs.Id = "ReportItemSchemas";
                    xs.Write(sw);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult PrintBAST(string creditid, string StatusBPKB, string CompanyId) => PrepareReport("PDF", "pdf", "application/pdf", "BAST", creditid, StatusBPKB, CompanyId);

        public IActionResult PrintSK(string creditid, string StatusBPKB, string CompanyId) => PrepareReport("PDF", "pdf", "application/pdf", "SK", creditid, StatusBPKB, CompanyId);

        public IActionResult PrintPinjam(string creditid, string StatusBPKB, string CompanyId) => PrepareReport("PDF", "pdf", "application/pdf", "Pinjam", creditid, StatusBPKB, CompanyId);

        public IActionResult PrintThirdParty(string creditid, string StatusBPKB, string CompanyId) => PrepareReport("PDF", "pdf", "application/pdf", "Pihak3", creditid, StatusBPKB, CompanyId);

        private IActionResult PrepareReport(string renderFormat, string extension, string mimeType, string PrintType, string creditid, string StatusBPKB, string CompanyId)
        {
            byte[] pdf;
            try
            {
                using var report = new LocalReport();
                BPKBModels Model = new BPKBModels();
                var CompanyBranchCredential = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
                var UserLDAPCredential = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
                Model.CreditId = creditid;
                Model.CompanyId = "0" + CompanyId;
                string ReportName = "";
                if (PrintType == "BAST")
                {
                    if (StatusBPKB == "I")
                    {
                        ReportName = "FINCORE.WEB.Views.Acquisition.BPKB.Reports.BASTDIN1.rdlc";
                        BPKBBASTINReportModel ReportData = new BPKBBASTINReportModel();
                        //CompanyBranchModel branch = (CompanyBranchModel)services.GetBranchDetail(CompanyBranchCredential.branch_id).Result.data;
                        BPKBBastInDetailModel data = (BPKBBastInDetailModel)services.GetBASTInDetail(Model).Result.data;
                        ReportData.CreditId = creditid;
                        ReportData.BranchName = CompanyBranchCredential.branch_name;
                        ReportData.BranchAddress = CompanyBranchCredential.branch_address;
                        ReportData.CustomerName = data.CustomerName;
                        ReportData.QQName = data.QQName;
                        ReportData.BpkbNo = data.BpkbNo;
                        ReportData.ChasisNo = data.ChasisNo;
                        ReportData.MachineNo = data.MachineNo;
                        ReportData.ItemColor = data.ItemColor;
                        ReportData.BrandName = data.BrandName;

                        Report.Load(report, ReportData, ReportName);

                        Model.BastDate = DateTime.Now;
                        Model.BastBy = UserLDAPCredential.EmployeeId;
                        pdf = report.Render(renderFormat);
                        return File(pdf, mimeType, "report." + extension);
                    }
                    else
                    {
                        ReportName = "FINCORE.WEB.Views.Acquisition.BPKB.Reports.rptBAST.rdlc";
                        BPKBBastReportModel ReportData = new BPKBBastReportModel();
                        //CompanyBranchModel branch = (CompanyBranchModel)services.GetBranchDetail(CompanyBranchCredential.branch_id).Result.data;
                        BPKBBastDetailModel data = (BPKBBastDetailModel)services.GetReportData(Model).Result.data;
                        ReportData.CreditId = creditid;
                        ReportData.BranchName = CompanyBranchCredential.branch_name;
                        ReportData.BranchAddress = CompanyBranchCredential.branch_address;
                        ReportData.CustomerAddress = data.CustomerAddress;
                        ReportData.CompanyName = CompanyBranchCredential.company_id == "2" ? "Mega Auto Finance" : "Mega Central Finance";
                        ReportData.CustomerName = data.CustomerName;
                        ReportData.BPKBNo = data.BpkbNo;
                        ReportData.BastDetail = data.BastDetail;
                        ReportData.UserName = UserLDAPCredential.DisplayName;
                        Report.Load(report, ReportData, ReportName);

                        Model.BastDate = DateTime.Now;
                        Model.BastBy = "Ken";
                    }
                }
                else if (PrintType == "SK")
                {
                    ReportName = "FINCORE.WEB.Views.Acquisition.BPKB.Reports.rptEPBPKBSK.rdlc";

                    BPKBSKReportModel ReportData = new BPKBSKReportModel();
                    CompanyBranchModel branch = (CompanyBranchModel)services.GetBranchDetail(CompanyBranchCredential.branch_id).Result.data;//hardcoded
                    BPKBSKDetailModel data = (BPKBSKDetailModel)services.GetBPKBSKDetail(Model).Result.data;
                    ReportData.CreditId = creditid;
                    ReportData.BranchAddress = CompanyBranchCredential.branch_address;
                    ReportData.BranchName = CompanyBranchCredential.branch_name;
                    ReportData.PersonPosition = "Kepala Cabang";
                    ReportData.CompanyName = CompanyBranchCredential.company_id == "2" ? "Mega Auto Finance" : "Mega Central Finance";
                    ReportData.Month = GetMonth();
                    ReportData.ContactPerson = branch.ContactPerson;
                    ReportData.CustomerAddress = data.CustomerAddress;
                    ReportData.CustomerName = data.CustomerName;
                    ReportData.BPKBNo = data.BpkbNo;
                    ReportData.IdentityNumber = data.IdentityNo;
                    ReportData.MachineNo = data.MachineNo;
                    ReportData.ChassisNo = data.ChasisNo;
                    ReportData.AssetTypeDescription = data.AssetTypeDescription;
                    ReportData.CarNo = data.CarNo;
                    ReportData.ItemColor = data.ItemColor;
                    ReportData.SumPrintSK = data.SumPrintSK;

                    Report.Load(report, ReportData, ReportName);

                    Model.PrintedFirstBy = UserLDAPCredential.EmployeeId;
                    Model.PrintedFirstDate = DateTime.Today;
                    Model.PrintedLastBy = UserLDAPCredential.EmployeeId;
                    Model.PrintedLastDate = DateTime.Today;
                    Model.IsPrintSk = true;
                }
                else if (PrintType == "Pinjam")
                {
                    ReportName = "FINCORE.WEB.Views.Acquisition.BPKB.Reports.rptEPSTBPKBPinjam.rdlc";

                    BPKBPinjamReportModel ReportData = new BPKBPinjamReportModel();
                    CompanyBranchModel branch = (CompanyBranchModel)services.GetBranchDetail(CompanyBranchCredential.branch_id).Result.data;
                    BPKBPinjamDetailModel data = (BPKBPinjamDetailModel)services.GetBPKBPinjamDetail(Model).Result.data;

                    ReportData.UserName = UserLDAPCredential.DisplayName;
                    ReportData.CreditId = creditid;
                    ReportData.ContactPerson = branch.ContactPerson;
                    ReportData.AssetTypeDesc = data.AssetTypeDesc;
                    ReportData.LoanDate = data.LoanDate.Date;
                    ReportData.ReceiverName = data.ReceiverName;
                    ReportData.ReceiverAddress = data.ReceiverAddress;
                    ReportData.ReceiverPhone = data.ReceiverPhone;
                    ReportData.BPKBNo = data.BPKBNo;
                    ReportData.BrandName = data.BrandName;
                    ReportData.ItemColor = data.ItemColor;
                    ReportData.MachineNo = data.MachineNo;
                    ReportData.ChasisNo = data.ChasisNo;
                    ReportData.ItemYear = data.ItemYear;
                    ReportData.QQName = data.QQName;
                    ReportData.MoneyReceived = (decimal)data.MoneyReceived;

                    Report.Load(report, ReportData, ReportName);
                    pdf = report.Render(renderFormat);
                    return File(pdf, mimeType, "report." + extension);
                }
                else if (PrintType == "Pihak3")
                {
                    ReportName = "FINCORE.WEB.Views.Acquisition.BPKB.Reports.rptEPBPKBSKPihakTiga.rdlc";

                    BPKBThirdPartyReportModel ReportData = (BPKBThirdPartyReportModel)services.GetThirdPartyDetail(Model).Result.data;

                    Report.Load(report, ReportData, ReportName);

                    Model.PrintFirstByThirdParty = UserLDAPCredential.EmployeeId;
                    Model.PrintFirstDateThirdParty = DateTime.Today;
                    Model.PrintLastByThirdParty = UserLDAPCredential.EmployeeId;
                    Model.PrintLastDateThirdParty = DateTime.Today;
                    Model.IsPrintThirdParty = true;
                }

                pdf = report.Render(renderFormat);
                services.UpdatePrintBPKB(Model);
            }
            catch (Exception ex)
            {
                string state = Collection.Status.FAILED;
                string msg = ex.Message;
                Sessions.SetSessionAsJson(HttpContext.Session, "state", state);

                Sessions.SetSessionAsJson(HttpContext.Session, "msg", msg);
                return RedirectToAction("List");
            }

            return File(pdf, mimeType, "report." + extension);
        }

        public string GetMonth()
        {
            string result = "";
            switch (DateTime.Now.Month)
            {
                case 1:
                    result = "I";
                    break;

                case 2:
                    result = "II";
                    break;

                case 3:
                    result = "III";
                    break;

                case 4:
                    result = "IV";
                    break;

                case 5:
                    result = "V";
                    break;

                case 6:
                    result = "VI";
                    break;

                case 7:
                    result = "VII";
                    break;

                case 8:
                    result = "VIII";
                    break;

                case 9:
                    result = "IX";
                    break;

                case 10:
                    result = "X";
                    break;

                case 11:
                    result = "XI";
                    break;

                case 12:
                    result = "XII";
                    break;
            }
            return result;
        }

        #endregion printing with rdlc
    }
}