using FINCORE.Models.Helpers;
using FINCORE.Models.Models;
using FINCORE.Models.Models.Acquisition.CAS;
using FINCORE.Models.Models.Acquisition.CAS.ModelHelper;
using FINCORE.Models.Models.Acquisition.CAS.Paginations;
using FINCORE.Models.Models.Acquisition.CAS.References;
using FINCORE.Models.Models.Acquisition.Masters;
using FINCORE.Models.Models.Acquisition.Reports;
using FINCORE.Models.Models.Dukcapil;
using FINCORE.Models.Models.LDAP;
using FINCORE.Models.Models.Masters;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.Acquisition;
using FINCORE.Services.Services.Masters;
using FINCORE.Services.Services.ThirdParty;
using FINCORE.WEB.Helpers;
using FINCORE.WEB.Helpers.MessageBox;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Reporting.NETCore;
using ServiceStack;
using System.Net;
using System.Text;
using static FINCORE.Services.Helpers.Response.Collection;
using static FINCORE.WEB.Helpers.Commons;
using static ServiceStack.Svg;
using static System.Net.WebRequestMethods;

namespace FINCORE.WEB.Controllers.Acquisition
{
    public class CASController : Controller
    {
        //private TrCasServices services = new TrCasServices();

        private readonly TrCasServices services = new TrCasServices();

        private Message MsgAlert = new();
        private IWebHostEnvironment environment;
        private readonly string _FOLDER_TARGET = "\\Docs\\Temp\\";

        public CASController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public IActionResult IndexMobil()
        {
            Sessions.SetSessionAsJson(HttpContext.Session, Collection.SessionIdentity.ISEDIT_KEY_NAME, false);
            var sessionUser = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            if (sessionUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            InitBindAsync();
            return View("~/Views/Acquisition/CAS/Index.cshtml");
        }

        public IActionResult IndexMotor()
        {
            Sessions.SetSessionAsJson(HttpContext.Session, Collection.SessionIdentity.ISEDIT_KEY_NAME, false);
            var sessionUser = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            if (sessionUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            InitBindAsync();
            return View("~/Views/Acquisition/CAS/IndexMotor.cshtml");
        }

        [HttpPost]
        public IActionResult OnEditTrCasMobil(TrCasModels casModels)
        {
            var sessUserLdap = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
            CompanyBranchResponse CompanyBranchMdl = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);

            casModels.outlet_code = "";
            casModels.created_by = "";
            casModels.last_updated_by = sessUserLdap.EmployeeId;
            casModels.last_updated_on = DateTime.Now;
            casModels.identity_type_id = "1";
            casModels.company_id = CompanyBranchMdl.company_id; //from Login Session

            casModels.credit_source_status = "";

            casModels.branch_id = sessBranchId;

            //set default field Perorangan as empty string if customer type is Corporate/Badan Usaha.
            casModels.email = casModels.email is null ? String.Empty : casModels.email;
            casModels.mother_name = casModels.mother_name is null ? "" : casModels.mother_name;
            casModels.conclusion = String.Empty; //motor
            casModels.customer_name = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_name : casModels.customer_name;
            casModels.birth_place = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_birth_place : casModels.birth_place;

            casModels.company_name = casModels.customer_type == CustomerTypes.INDIVIDUAL_TYPE_CODE ? string.Empty : casModels.company_name;
            casModels.company_birth_place = casModels.customer_type == CustomerTypes.INDIVIDUAL_TYPE_CODE ? string.Empty : casModels.company_birth_place;

            casModels.npwp_no = casModels.npwp_no is null ? String.Empty : casModels.npwp_no;

            casModels.Apuppt = casModels.Apuppt is null ? new() { } : casModels.Apuppt;
            casModels.order_id = casModels.order_id is null ? "" : casModels.order_id;
            casModels.Installments = casModels.Installments is null ? new() : casModels.Installments;
            casModels.RepeatOrders = casModels.is_repeat_order is false ? new() : casModels.RepeatOrders;
            casModels.DtReferensi = casModels.DtReferensi is null ? new() : casModels.DtReferensi;

            if (casModels.is_repeat_order && casModels.RepeatOrders != null)
            {
                casModels.RepeatOrders.reference_source_desc = casModels.RepeatOrders.reference_source_desc_sr1 is null ? casModels.RepeatOrders.reference_source_desc_sr4 : casModels.RepeatOrders.reference_source_desc_sr1;
                casModels.RepeatOrders.reference_source_desc_sr1 = casModels.RepeatOrders.reference_source_desc_sr1 is null ? String.Empty : casModels.RepeatOrders.reference_source_desc_sr1;
                casModels.RepeatOrders.reference_source_desc_sr4 = casModels.RepeatOrders.reference_source_desc_sr4 is null ? String.Empty : casModels.RepeatOrders.reference_source_desc_sr4;
            }

            if (!IsFieldDataReferencesFilledOut(casModels.DtReferensi))
            {
                ViewBag.State = Collection.Status.WARNING;
                ViewBag.Message = "Mohon isi DATA REFERENSI dengan lengkap. Silahkan coba kembali";

                return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOBIL_CODE);
            }
            if (casModels.is_references)
            {
                casModels.DtReferensi = RemoveNullableReferences(casModels);
            }

            var data = services.UpdateTrCasMobil(casModels);

            if (data.Result.status == Collection.Status.SUCCESS)
            {
                ViewBag.State = Collection.Status.SUCCESS;
                ViewBag.Message = data.Result.message;
                Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.ISEDIT_KEY_NAME, false);
            }
            else
            {
                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = data.Result.message;
                Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.ISEDIT_KEY_NAME, false);
            }
            return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOBIL_CODE);
        }

        [HttpPost]
        public IActionResult OnPostTrCasMobil(TrCasModels casModels)
        {
            var userSession = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
            CompanyBranchResponse CompanyBranchMdl = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);

            #region Validate Data References

            if (casModels.is_references)
            {
                if (!IsReferenceInputSameWithMaxCol(casModels))
                {
                    ViewBag.State = Collection.Status.INFO;
                    ViewBag.Message = "Mohon isi jumlah DATA REFERENSI dengan sesuai. Silahkan coba kembali";

                    InitBindAsync();
                    return View("~/Views/Acquisition/CAS/Index.cshtml");
                }

                if (!IsFieldDataReferencesFilledOut(casModels.DtReferensi))
                {
                    InitBindAsync();

                    return MsgAlert.AlertWarning("Mohon isi DATA REFERENSI dengan lengkap. Silahkan coba kembali"
                        , "~/Views/Acquisition/CAS/Index.cshtml");
                }

                casModels.DtReferensi = RemoveNullableReferences(casModels);
            }

            #endregion Validate Data References

            casModels.credit_id = GenerateCreditID(sessBranchId);
            casModels.outlet_code = "";
            casModels.created_by = userSession.EmployeeId;

            casModels.identity_type_id = "1";

            casModels.credit_source_status = "";

            casModels.branch_id = sessBranchId;
            casModels.company_id = CompanyBranchMdl.company_id; //from Login Session

            casModels.email = casModels.email is null ? String.Empty : casModels.email;
            casModels.mother_name = casModels.mother_name is null ? String.Empty : casModels.mother_name;
            casModels.customer_name = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_name : casModels.customer_name;
            casModels.birth_place = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_birth_place : casModels.birth_place;

            casModels.company_name = casModels.company_name is null ? String.Empty : casModels.company_name;
            casModels.company_birth_place = casModels.company_birth_place is null ? String.Empty : casModels.company_birth_place;
            casModels.customer_name = casModels.customer_name is null ? String.Empty : casModels.customer_name;
            casModels.birth_place = casModels.birth_place is null ? String.Empty : casModels.birth_place;

            casModels.npwp_no = casModels.npwp_no is null ? String.Empty : casModels.npwp_no;

            casModels.order_id = casModels.order_id is null ? "" : casModels.order_id;
            casModels.Apuppt = casModels.Apuppt is null ? new() { } : casModels.Apuppt;
            casModels.Installments = casModels.Installments is null ? new() : casModels.Installments;

            casModels.is_repeat_order = casModels.RepeatOrders is null ? false : true;
            casModels.RepeatOrders = casModels.is_repeat_order is false ? new() : casModels.RepeatOrders;

            var data = services.InsertTrCasMobil(casModels);
            if (data.Result.status == Collection.Status.SUCCESS)
            {
                ViewBag.State = Collection.Status.SUCCESS;
                ViewBag.Message = data.Result.message;
            }
            else
            {
                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = data.Result.message;
            }
            //return data.Result.status == Status.SUCCESS ? NextToCMPage(ItemId.MOBIL_CODE, casModels.credit_id)
            //    : View("~/Views/Acquisition/CAS/Index.cshtml");

            return View("~/Views/Acquisition/CAS/Index.cshtml");
        }

        private bool IsInputRefMatchWithMaxKol(TrCasModels dataCas)
        {
            var isMatch = true;
            var maxKol = GetMaxKolReference(dataCas.identity_number);
            ViewBag.DataReferenceCount = maxKol;
            if (dataCas.DtReferensi.nama_referensi.Length != maxKol)
            {
                //Input Data Referensi NOT same with MaxKol
                isMatch = false;
            }
            else if (dataCas.DtReferensi.nama_referensi.Length == maxKol)
            {
                isMatch = true;
            }
            return isMatch;
        }

        [HttpPost]
        public IActionResult OnEditTrCasMotor(TrCasModels casModels)
        {
            var MessageBlackList = "";

            var userSession = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
            CompanyBranchResponse CompanyBranchMdl = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);

            casModels.outlet_code = "";
            casModels.created_by = "";
            casModels.last_updated_by = userSession.EmployeeId;
            casModels.last_updated_on = DateTime.Now;
            casModels.identity_type_id = "1";
            casModels.company_id = CompanyBranchMdl.company_id;

            casModels.Financials.primary_income = casModels.Financials.txtPrimaryIncome != null ? Int32.Parse(casModels.Financials.txtPrimaryIncome.Replace(".", "")) : 0;
            casModels.Financials.other_income = casModels.Financials.txtOther_income != null ? Int32.Parse(casModels.Financials.txtOther_income.Replace(".", "")) : 0;
            casModels.Financials.household_expenses = casModels.Financials.txtHousehold_expenses != null ? Int32.Parse(casModels.Financials.txtHousehold_expenses.Replace(".", "")) : 0;
            casModels.Financials.education_expenses = casModels.Financials.txtEducation_expenses != null ? Int32.Parse(casModels.Financials.txtEducation_expenses.Replace(".", "")) : 0;
            casModels.Financials.health_expenses = casModels.Financials.txtHealth_expenses != null ? Int32.Parse(casModels.Financials.txtHealth_expenses.Replace(".", "")) : 0;
            casModels.Financials.monthly_other_installment = casModels.Financials.txtMonthly_other_installment != null ? Int32.Parse(casModels.Financials.txtMonthly_other_installment.Replace(".", "")) : 0;

            casModels.DtPasangans.income = casModels.DtPasangans.txtIncome != null ? Int32.Parse(casModels.DtPasangans.txtIncome.Replace(".", "")) : 0;
            casModels.DtPasangans.other_income = casModels.DtPasangans.txtOther_income != null ? Int32.Parse(casModels.DtPasangans.txtOther_income.Replace(".", "")) : 0;

            casModels.credit_source_status = "";

            casModels.conclusion = String.Empty;
            casModels.analysis = String.Empty;
            casModels.branch_id = sessBranchId;
            casModels.order_id = casModels.order_id is null ? "" : casModels.order_id;

            //set default field Perorangan as empty string if customer type is Corporate/Badan Usaha.
            casModels.email = casModels.email is null ? String.Empty : casModels.email;
            casModels.mother_name = casModels.mother_name is null ? "" : casModels.mother_name;
            casModels.customer_name = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_name : casModels.customer_name;
            casModels.birth_place = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_birth_place : casModels.birth_place;

            casModels.company_name = casModels.customer_type == CustomerTypes.INDIVIDUAL_TYPE_CODE ? string.Empty : casModels.company_name;
            casModels.company_birth_place = casModels.customer_type == CustomerTypes.INDIVIDUAL_TYPE_CODE ? string.Empty : casModels.company_birth_place;

            casModels.npwp_no = casModels.npwp_no is null ? String.Empty : casModels.npwp_no;

            casModels.Apuppt = casModels.Apuppt is null ? new() { } : casModels.Apuppt;
            casModels.order_id = casModels.order_id is null ? "" : casModels.order_id;
            casModels.Installments = casModels.Installments is null ? new() : casModels.Installments;
            casModels.RepeatOrders = casModels.is_repeat_order is false ? new() : casModels.RepeatOrders;
            casModels.DtReferensi = casModels.DtReferensi is null ? new() : casModels.DtReferensi;

            casModels.repeat_order_reason = casModels.repeat_order_reason is null ? "" : casModels.repeat_order_reason;

            if (casModels.npwp_no is not null)
            {
                if (casModels.npwp_no.Length <= 15 && casModels.npwp_no.Length > 0)
                {
                    ViewBag.State = Collection.Status.WARNING;
                    ViewBag.Message = "Mohon masukan No NPWP dengan benar. Hilangkan seluruh angka di field NPWP jika Konsumen tidak punya NPWP. Silahkan check dan coba kembali.";

                    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                }
            }
            if (casModels.Financials.primary_income == 0)
            {
                ViewBag.State = Collection.Status.INFO;
                ViewBag.Message = "Mohon masukan Penghasilan Utama nasabah pada panel Data Penghasilan. Silahkan cek dan coba kembali.";

                return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
            }

            if (casModels.is_repeat_order && casModels.RepeatOrders != null)
            {
                casModels.RepeatOrders.reference_source_desc = casModels.RepeatOrders.reference_source_desc_sr1 is null ? casModels.RepeatOrders.reference_source_desc_sr4 : casModels.RepeatOrders.reference_source_desc_sr1;
                casModels.RepeatOrders.reference_source_desc_sr1 = casModels.RepeatOrders.reference_source_desc_sr1 is null ? String.Empty : casModels.RepeatOrders.reference_source_desc_sr1;
                casModels.RepeatOrders.reference_source_desc_sr4 = casModels.RepeatOrders.reference_source_desc_sr4 is null ? String.Empty : casModels.RepeatOrders.reference_source_desc_sr4;
            }

            if (casModels.is_references)
            {
                if (!IsFieldDataReferencesFilledOut(casModels.DtReferensi))
                {
                    ViewBag.State = Collection.Status.WARNING;
                    ViewBag.Message = "Mohon isi DATA REFERENSI dengan lengkap. Silahkan coba kembali";

                    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                }

                casModels.DtReferensi = RemoveNullableReferences(casModels);

                //if (!IsInputRefMatchWithMaxKol(casModels))
                //{
                //    var maxKol = ViewBag.DataReferenceCount;
                //    ViewBag.State = Collection.Status.WARNING;
                //    ViewBag.Message = $"Mohon isi DATA REFERENSI dengan sesuai Max Kol yang sudah di tetapkan. Anda diharuskan isi {maxKol} Data Referensi";

                //    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                //}
            }

            if (casModels.customer_type == "P")
            {
                if (Convert.ToDecimal((DateTime.Today - casModels.birth_date).TotalDays) / Convert.ToDecimal(365) < Convert.ToDecimal(21))
                    {
                    if (casModels.marital_status == 1 && casModels.marital_status != null)
                    {
                        ViewBag.State = Collection.Status.WARNING;
                        ViewBag.Message = "Minimal usia 21 tahun";

                        return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                    }
                    else
                    {
                        if (Convert.ToDecimal((DateTime.Today - casModels.birth_date).TotalDays) / Convert.ToDecimal(365) < Convert.ToDecimal(15))
                        {
                            ViewBag.State = Collection.Status.WARNING;
                            ViewBag.Message = "Minimal usia 15 tahun";

                            return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                        }
                    }
                }
            }

            var dataItemBlackList = new List<CheckBlacklistModels>();
            var dataBlackList = (List<CheckBlacklistModels>)services.CheckBlacklistKTP(casModels.identity_number).Result.data;
            if (dataBlackList.Count > 0)
            {
                var itemBlackList = dataBlackList.First();
                if (itemBlackList.is_blacklist == true)
                {
                    casModels.is_blacklist = true;

                    MessageBlackList = "Checking Blacklist Result: KTP " + casModels.identity_number + ", " + itemBlackList.message_error + ". Klik OK untuk lanjut ke halaman berikutnya.";

                }
            }

			var data = services.UpdateTrCasMotor(casModels);

            if (data.Result.status == Collection.Status.SUCCESS)
            {
                if (MessageBlackList != "")
                {
                    ViewBag.State = Collection.Status.WARNING_THEN_ROUTE;
                    ViewBag.Message = MessageBlackList;
                    ViewBag.UrlRoute = "/CM/CMMotorCycle";
                    HttpContext.Session.SetString(SessionIdentity.CREDIT_ID, casModels.credit_id);

                    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                }
                else
                {
                    ViewBag.StateRoute = Status.SUCCESS;
                    ViewBag.Message = data.Result.message;
                    ViewBag.UrlRoute = "/Acquisition/IndexMotor";
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.ISEDIT_KEY_NAME, false);
                }                
            }
            else
            {
                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = data.Result.message;
                Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.ISEDIT_KEY_NAME, false);
            }
            
            return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
        }

        [HttpPost]
        public IActionResult OnPostTrCasMotor(TrCasModels casModels)
        {
            var MessageBlackList = "";

            var userSession = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
            CompanyBranchResponse CompanyBranchMdl = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);

            if (casModels.npwp_no is not null || casModels.npwp_no != String.Empty)
            {
                if (casModels.npwp_no.Length <= 15 && casModels.npwp_no.Length > 0)
                {
                    ViewBag.State = Collection.Status.WARNING;
                    ViewBag.Message = "Mohon masukan No NPWP dengan benar. Hilangkan seluruh angka di field NPWP jika Konsumen tidak punya NPWP. Silahkan check dan coba kembali.";

                    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                }
            }

            if (casModels.is_references)
            {
                if (!IsFieldDataReferencesFilledOut(casModels.DtReferensi))
                {
                    ViewBag.State = Collection.Status.WARNING;
                    ViewBag.Message = "Mohon isi DATA REFERENSI dengan lengkap. Silahkan coba kembali";

                    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                }

                casModels.DtReferensi = RemoveNullableReferences(casModels);

                //if (!IsInputRefMatchWithMaxKol(casModels))
                //{
                //    var maxKol = ViewBag.DataReferenceCount;
                //    ViewBag.State = Collection.Status.WARNING;
                //    ViewBag.Message = $"Mohon isi DATA REFERENSI dengan sesuai Max Kol yang sudah di tetapkan. Anda diharuskan isi {maxKol} Data Referensi";

                //    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                //}
            }

            //#region Validate Data References
            //if (casModels.is_references)
            //{
            //    if (!IsReferenceInputSameWithMaxCol(casModels))
            //    {
            //        ViewBag.State = Collection.Status.INFO;
            //        ViewBag.Message = "Mohon isi jumlah DATA REFERENSI dengan sesuai. Silahkan coba kembali";

            //        InitBindAsync();
            //        return View("~/Views/Acquisition/CAS/IndexMotor.cshtml");
            //    }
            //    if (!IsFieldDataReferencesFilledOut(casModels.DtReferensi))
            //    {
            //        ViewBag.State = Collection.Status.WARNING;
            //        ViewBag.Message = "Mohon isi DATA REFERENSI dengan lengkap. Silahkan coba kembali";

            //        InitBindAsync();
            //        return View("~/Views/Acquisition/CAS/IndexMotor.cshtml");
            //    }

            //    casModels.DtReferensi = RemoveNullableReferences(casModels);

            //    //if (!IsInputRefMatchWithMaxKol(casModels))
            //    //{
            //    //    var maxKol = ViewBag.DataReferenceCount;
            //    //    ViewBag.State = Collection.Status.WARNING;
            //    //    ViewBag.Message = $"Mohon isi DATA REFERENSI dengan sesuai Max Kol yang sudah di tetapkan. Anda diharuskan isi {maxKol} Data Referensi";

            //    //    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
            //    //}
            //}
            //#endregion Validate Data References

            if (casModels.Financials.primary_income == 0)
            {
                ViewBag.State = Collection.Status.INFO;
                ViewBag.Message = "Mohon masukan Penghasilan Utama nasabah pada panel Data Penghasilan. Silahkan cek dan coba kembali.";

                return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
            }

            if (casModels.is_repeat_order && casModels.RepeatOrders != null)
            {
                casModels.RepeatOrders.reference_source_desc = casModels.RepeatOrders.reference_source_desc_sr1 is null ? casModels.RepeatOrders.reference_source_desc_sr4 : casModels.RepeatOrders.reference_source_desc_sr1;
                casModels.RepeatOrders.reference_source_desc_sr1 = casModels.RepeatOrders.reference_source_desc_sr1 is null ? String.Empty : casModels.RepeatOrders.reference_source_desc_sr1;
                casModels.RepeatOrders.reference_source_desc_sr4 = casModels.RepeatOrders.reference_source_desc_sr4 is null ? String.Empty : casModels.RepeatOrders.reference_source_desc_sr4;
            }

            if (casModels.customer_type == "P")
            {
                if (Convert.ToDecimal((DateTime.Today - casModels.birth_date).TotalDays) / Convert.ToDecimal(365) < Convert.ToDecimal(21))
                {
                    if (casModels.marital_status == 1 && casModels.marital_status != null)
                    {
                        ViewBag.State = Collection.Status.WARNING;
                        ViewBag.Message = "Minimal usia 21 tahun";

                        return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                    }
                    else
                    {
                        if (Convert.ToDecimal((DateTime.Today - casModels.birth_date).TotalDays) / Convert.ToDecimal(365) < Convert.ToDecimal(15))
                        {
                            ViewBag.State = Collection.Status.WARNING;
                            ViewBag.Message = "Minimal usia 15 tahun";

                            return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                        }
                    }
                }
            }

            var dataItemBlackList = new List<CheckBlacklistModels>();
            var dataBlackList = (List<CheckBlacklistModels>)services.CheckBlacklistKTP(casModels.identity_number).Result.data;
            if (dataBlackList.Count > 0)
            {
                var itemBlackList = dataBlackList.First();
                if (itemBlackList.is_blacklist == true)
                {
                    casModels.is_blacklist = true;

                    MessageBlackList = "Checking Blacklist Result: KTP " + casModels.identity_number + ", " + itemBlackList.message_error + ". Klik OK untuk lanjut ke halaman berikutnya.";

                }
            }

            casModels.credit_id = GenerateCreditID(sessBranchId);
            casModels.outlet_code = "";
            casModels.created_by = userSession.EmployeeId;

            casModels.identity_type_id = "1";

            casModels.credit_source_status = "";

            casModels.branch_id = sessBranchId;
            casModels.company_id = CompanyBranchMdl.company_id; //from Login Session

            casModels.Financials.primary_income = casModels.Financials.txtPrimaryIncome != null ? Int32.Parse(casModels.Financials.txtPrimaryIncome.Replace(".", "")) : 0;
            casModels.Financials.other_income = casModels.Financials.txtOther_income != null ? Int32.Parse(casModels.Financials.txtOther_income.Replace(".", "")) : 0;
            casModels.Financials.household_expenses = casModels.Financials.txtHousehold_expenses != null ? Int32.Parse(casModels.Financials.txtHousehold_expenses.Replace(".", "")) : 0;
            casModels.Financials.education_expenses = casModels.Financials.txtEducation_expenses != null ? Int32.Parse(casModels.Financials.txtEducation_expenses.Replace(".", "")) : 0;
            casModels.Financials.health_expenses = casModels.Financials.txtHealth_expenses != null ? Int32.Parse(casModels.Financials.txtHealth_expenses.Replace(".", "")) : 0;
            casModels.Financials.monthly_other_installment = casModels.Financials.txtMonthly_other_installment != null ? Int32.Parse(casModels.Financials.txtMonthly_other_installment.Replace(".", "")) : 0;

            casModels.DtPasangans.income = casModels.DtPasangans.txtIncome != null ? Int32.Parse(casModels.DtPasangans.txtIncome.Replace(".", "")) : 0;
            casModels.DtPasangans.other_income = casModels.DtPasangans.txtOther_income != null ? Int32.Parse(casModels.DtPasangans.txtOther_income.Replace(".", "")) : 0;

            casModels.email = casModels.email is null ? String.Empty : casModels.email;
            casModels.mother_name = casModels.mother_name is null ? String.Empty : casModels.mother_name;
            casModels.customer_name = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_name : casModels.customer_name;
            casModels.birth_place = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_birth_place : casModels.birth_place;

            casModels.company_name = casModels.company_name is null ? String.Empty : casModels.company_name;
            casModels.company_birth_place = casModels.company_birth_place is null ? String.Empty : casModels.company_birth_place;
            casModels.customer_name = casModels.customer_name is null ? String.Empty : casModels.customer_name;
            casModels.birth_place = casModels.birth_place is null ? String.Empty : casModels.birth_place;

            casModels.npwp_no = casModels.npwp_no is null ? String.Empty : casModels.npwp_no;

            casModels.order_id = casModels.order_id is null ? "" : casModels.order_id;
            casModels.Apuppt = casModels.Apuppt is null ? new() { } : casModels.Apuppt;
            casModels.Installments = casModels.Installments is null ? new() : casModels.Installments;

            casModels.RepeatOrders.reference_source_desc = casModels.RepeatOrders.reference_source_desc_sr1 is null ? casModels.RepeatOrders.reference_source_desc_sr4 : casModels.RepeatOrders.reference_source_desc_sr1;
            casModels.is_repeat_order = casModels.RepeatOrders is null ? false : true;
            casModels.RepeatOrders = casModels.is_repeat_order is false ? new() : casModels.RepeatOrders;
            casModels.conclusion = String.Empty;
            casModels.analysis = String.Empty;

            var data = services.InsertTrCasMotor(casModels);

            if (data.Result.status == Collection.Status.SUCCESS)
            {
                if (MessageBlackList != "")
                {
                    ViewBag.State = Collection.Status.WARNING_THEN_ROUTE;
                    ViewBag.Message = MessageBlackList;
                    ViewBag.UrlRoute = "/CM/CMMotorCycle";
                    HttpContext.Session.SetString(SessionIdentity.CREDIT_ID, casModels.credit_id);

                    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                }
                else
                {
                    ViewBag.State = Collection.Status.SUCCESS;
                    ViewBag.Message = data.Result.message;
                }
                
            }
            else
            {
                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = data.Result.message;
            }
            //return data.Result.status == Status.SUCCESS ? NextToCMPage(ItemId.MOTOR_CODE, casModels.credit_id)
            //    : View("~/Views/Acquisition/CAS/IndexMotor.cshtml");

            return View("~/Views/Acquisition/CAS/IndexMotor.cshtml");
        }

        [HttpGet]
        public IActionResult CreditSource()
        {
            var data = services.GetCreditSource().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult CreditEvaluations()
        {
            var data = services.GetCreditEvaluation().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult Marital()
        {
            var data = services.GetMaritalStatus().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult OtherInstallment()
        {
            var data = services.GetMonthlyOtherInstallment().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult Sources()
        {
            var data = services.GetSource().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult Profession()
        {
            var data = services.GetProfession().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult IndustryType()
        {
            var data = services.GetIndustryType().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult IdentityTypeByCustomer(string customerType)
        {
            var data = new APIResponse();
            if (customerType == null || customerType == string.Empty)
            {
                data = services.GetIdentityType().Result;
            }
            else
            {
                data = services.GetIdentityType(customerType).Result;
            }
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult OwnershipProof()
        {
            var data = services.GetOwnershipProof().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult ResidenceStatus()
        {
            var data = services.GetResidence().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult Relationship()
        {
            var data = services.GetRelationship().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult CustomerType()
        {
            var data = services.GetCustomerType().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult CustomerSource()
        {
            var data = services.GetCustomerSource().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult ROApplicantRelation()
        {
            var data = services.GetMsROApplicantRelation().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult ROCategory()
        {
            var data = services.GetMsROCategory().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult RODecision()
        {
            var data = services.GetMsRODecision().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult ROReferenceSource()
        {
            var data = services.GetMsROReferenceSource().Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult PaymentPointPlan()
        {
            var data = services.GetMsPaymentPoint(Commons.PAYMENT_TYPE.RENCANA_PEMBAYARAN).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult PaymentPointLocation()
        {
            var data = services.GetMsPaymentPoint(Commons.PAYMENT_TYPE.LOKASI_PEMBAYARAN).Result;
            return Ok(data.data);
        }

        #region Lookup & Pagination

        [HttpPost]
        public async Task<IActionResult> PaginationMsLocationKTP(int pageIndex, string searchTerm)
        {
            var dataPaging = new PaginationModels<PaginationLocationModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            var sessionLocation = Sessions.GetSessionFromJson<PaginationModels<PaginationLocationModels>>(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP);
            if (searchTerm == null || searchTerm == string.Empty)
            {
                if (sessionLocation is null)
                {
                    var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    if (pageIndex > 1)
                    {
                        var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                        //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                    }
                    else
                    {
                        dataPaging = sessionLocation;
                    }                    
                }
            }
            else
            {
                if (sessionLocation is null)
                {
                    //page = 1;
                    var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    //page = 1;
                    if (searchTerm is not null || searchTerm != string.Empty)
                    {
                        var data = services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.Result.data;
                    }
                    else
                    {
                        if (pageIndex > 1)
                        {
                            var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                            dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                            //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                        }
                        else
                        {
                            dataPaging = sessionLocation;
                        }
                    }
                }
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CAS/Lookup/LocationKtpPartialView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationMsLocation(int pageIndex, string searchTerm)
        {
            var dataPaging = new PaginationModels<PaginationLocationModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            var sessionLocation = Sessions.GetSessionFromJson<PaginationModels<PaginationLocationModels>>(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP);
            if (searchTerm == null || searchTerm == string.Empty)
            {
                if (sessionLocation is null)
                {
                    var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    if (pageIndex > 1)
                    {
                        var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                        //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                    }
                    else
                    {
                        dataPaging = sessionLocation;
                    }
                }
            }
            else
            {
                if (sessionLocation is null)
                {
                    //page = 1;
                    var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    //page = 1;
                    if (searchTerm is not null || searchTerm != string.Empty)
                    {
                        var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    }
                    else
                    {
                        if (pageIndex > 1)
                        {
                            var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                            dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                            //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                        }
                        else
                        {
                            dataPaging = sessionLocation;
                        }
                    }
                }
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CAS/Lookup/LocationPartialView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationMsLocationCorp(int pageIndex, string searchTerm)
        {
            var dataPaging = new PaginationModels<PaginationLocationModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            var sessionLocation = Sessions.GetSessionFromJson<PaginationModels<PaginationLocationModels>>(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP);
            if (searchTerm == null || searchTerm == string.Empty)
            {
                if (sessionLocation is null)
                {
                    var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    if (pageIndex > 1)
                    {
                        var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                        //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                    }
                    else
                    {
                        dataPaging = sessionLocation;
                    }
                }
            }
            else
            {
                if (sessionLocation is null)
                {
                    //page = 1;
                    var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    //page = 1;
                    if (searchTerm is not null || searchTerm != string.Empty)
                    {
                        var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    }
                    else
                    {
                        if (pageIndex > 1)
                        {
                            var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                            dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                            //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                        }
                        else
                        {
                            dataPaging = sessionLocation;
                        }
                    }
                }
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CAS/Lookup/LocationCorpPartialView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationMsLocationPenjamin(int pageIndex, string searchTerm)
        {
            var dataPaging = new PaginationModels<PaginationLocationModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            var sessionLocation = Sessions.GetSessionFromJson<PaginationModels<PaginationLocationModels>>(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP);
            if (searchTerm == null || searchTerm == string.Empty)
            {
                if (sessionLocation is null)
                {
                    var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    if (pageIndex > 1)
                    {
                        var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                        //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                    }
                    else
                    {
                        dataPaging = sessionLocation;
                    }
                }
            }
            else
            {
                if (sessionLocation is null)
                {
                    //page = 1;
                    var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    //page = 1;
                    if (searchTerm is not null || searchTerm != string.Empty)
                    {
                        var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    }
                    else
                    {
                        if (pageIndex > 1)
                        {
                            var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                            dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                            //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                        }
                        else
                        {
                            dataPaging = sessionLocation;
                        }
                    }
                }
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CAS/Lookup/LocationPenjaminPartialView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationMsLocationSpouse(int pageIndex, string searchTerm)
        {
            var dataPaging = new PaginationModels<PaginationLocationModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            var sessionLocation = Sessions.GetSessionFromJson<PaginationModels<PaginationLocationModels>>(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP);
            if (searchTerm == null || searchTerm == string.Empty)
            {
                if (sessionLocation is null)
                {
                    var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    if (pageIndex > 1)
                    {
                        var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                        //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                    }
                    else
                    {
                        dataPaging = sessionLocation;
                    }
                }
            }
            else
            {
                if (sessionLocation is null)
                {
                    //page = 1;
                    var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    //page = 1;
                    if (searchTerm is not null || searchTerm != string.Empty)
                    {
                        var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    }
                    else
                    {
                        if (pageIndex > 1)
                        {
                            var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                            dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                            //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                        }
                        else
                        {
                            dataPaging = sessionLocation;
                        }
                    }
                }
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CAS/Lookup/LocationPasanganPartialView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationMsLocationTagihanAsync(int pageIndex, string searchTerm)
        {
            var dataPaging = new PaginationModels<PaginationLocationModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            var sessionLocation = Sessions.GetSessionFromJson<PaginationModels<PaginationLocationModels>>(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP);
            if (searchTerm == null || searchTerm == string.Empty)
            {
                if (sessionLocation is null)
                {
                    var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    if (pageIndex > 1)
                    {
                        var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                        //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                    }
                    else
                    {
                        dataPaging = sessionLocation;
                    }
                }
            }
            else
            {
                if (sessionLocation is null)
                {
                    //page = 1;
                    var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    //page = 1;
                    if (searchTerm is not null || searchTerm != string.Empty)
                    {
                        var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    }
                    else
                    {
                        if (pageIndex > 1)
                        {
                            var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                            dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                            //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                        }
                        else
                        {
                            dataPaging = sessionLocation;
                        }
                    }
                }
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CAS/Lookup/LocationTagihanPartialView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationMsLocationRef1(int pageIndex, string searchTerm)
        {
            var dataPaging = new PaginationModels<PaginationLocationModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            var sessionLocation = Sessions.GetSessionFromJson<PaginationModels<PaginationLocationModels>>(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP);
            if (searchTerm == null || searchTerm == string.Empty)
            {
                if (sessionLocation is null)
                {
                    var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    if (pageIndex > 1)
                    {
                        var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                        //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                    }
                    else
                    {
                        dataPaging = sessionLocation;
                    }
                }
            }
            else
            {
                if (sessionLocation is null)
                {
                    //page = 1;
                    var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    //page = 1;
                    if (searchTerm is not null || searchTerm != string.Empty)
                    {
                        var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    }
                    else
                    {
                        if (pageIndex > 1)
                        {
                            var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                            dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                            //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                        }
                        else
                        {
                            dataPaging = sessionLocation;
                        }
                    }
                }
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CAS/Lookup/References/LocationRef1PartialView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationMsLocationRef2(int pageIndex, string searchTerm)
        {
            var dataPaging = new PaginationModels<PaginationLocationModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            var sessionLocation = Sessions.GetSessionFromJson<PaginationModels<PaginationLocationModels>>(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP);
            if (searchTerm == null || searchTerm == string.Empty)
            {
                if (sessionLocation is null)
                {
                    var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    if (pageIndex > 1)
                    {
                        var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                        //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                    }
                    else
                    {
                        dataPaging = sessionLocation;
                    }
                }
            }
            else
            {
                if (sessionLocation is null)
                {
                    //page = 1;
                    var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    //page = 1;
                    if (searchTerm is not null || searchTerm != string.Empty)
                    {
                        var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    }
                    else
                    {
                        if (pageIndex > 1)
                        {
                            var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                            dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                            //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                        }
                        else
                        {
                            dataPaging = sessionLocation;
                        }
                    }
                }
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CAS/Lookup/References/LocationRef2PartialView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationMsLocationRef3(int pageIndex, string searchTerm)
        {
            var dataPaging = new PaginationModels<PaginationLocationModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            var sessionLocation = Sessions.GetSessionFromJson<PaginationModels<PaginationLocationModels>>(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP);
            if (searchTerm == null || searchTerm == string.Empty)
            {
                if (sessionLocation is null)
                {
                    var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    if (pageIndex > 1)
                    {
                        var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                        //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                    }
                    else
                    {
                        dataPaging = sessionLocation;
                    }
                }
            }
            else
            {
                if (sessionLocation is null)
                {
                    //page = 1;
                    var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                }
                else
                {
                    //page = 1;
                    if (searchTerm is not null || searchTerm != string.Empty)
                    {
                        var data = await services.GetPaginationMsLocation(searchTerm, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                    }
                    else
                    {
                        if (pageIndex > 1)
                        {
                            var data = await services.GetPaginationMsLocation(string.Empty, page, Paginations.MaxPerPageLookup);
                            dataPaging = (PaginationModels<PaginationLocationModels>)data.data;
                            //Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.LOCATION_DATA_TEMP, dataPaging);
                        }
                        else
                        {
                            dataPaging = sessionLocation;
                        }
                    }
                }
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CAS/Lookup/References/LocationRef3PartialView.cshtml", dataPaging);
        }

        [HttpPost]
        public async Task<IActionResult> PaginationMsBank(int pageIndex, string searchTerm)
        {
            var dataPaging = new PaginationModels<PaginationBankModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = await services.GetPaginationMsBank(string.Empty, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationBankModels>)data.data;
            }
            else
            {
                //page = 1;
                var data = await services.GetPaginationMsBank(searchTerm, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationBankModels>)data.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CAS/Lookup/MasterBankPartialView.cshtml", dataPaging);
        }

        public async Task<IActionResult> PaginationPoolingOrder(string tipeOrder, int pageIndex, string searchTerm)
        {
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
            if (tipeOrder != null)
                Sessions.SetSessionAsJson(HttpContext.Session, "TipeOrder", tipeOrder);
            else
                tipeOrder = Sessions.GetSessionFromJson<string>(HttpContext.Session, "TipeOrder");

            var branchId = sessBranchId;

            var dataPaging = new PaginationModels<PaginationPoolingOrderModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = await services.GetPaginationPoolingOrder(string.Empty, tipeOrder, branchId, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationPoolingOrderModels>)data.data;
            }
            else
            {
                //page = 1;
                var data = await services.GetPaginationPoolingOrder(searchTerm, tipeOrder, branchId, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationPoolingOrderModels>)data.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CAS/Lookup/OrderIdPartialView.cshtml", dataPaging);
        }

        public async Task<IActionResult> PaginationAgreementOld(string searchTerm, string lesseeId, int pageIndex)
        {
            var dataPaging = new PaginationModels<PaginationAgreementNumberOldModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;
            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            var sessionAgreementOld = Sessions.GetSessionFromJson<PaginationModels<PaginationAgreementNumberOldModels>>(HttpContext.Session, SessionIdentity.AGREEMENT_OLD_DATA_TEMP);
            if (searchTerm == null || searchTerm == string.Empty)
            {
                if (sessionAgreementOld is null)
                {
                    var data = await services.GetPaginationAgreementOld(string.Empty, lesseeId, page, Paginations.MaxPerPageLookup);
                    dataPaging = (PaginationModels<PaginationAgreementNumberOldModels>)data.data;
                    Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.AGREEMENT_OLD_DATA_TEMP, dataPaging);
                }
                else
                {
                    if (pageIndex > 1)
                    {
                        var data = await services.GetPaginationAgreementOld(string.Empty, lesseeId, page, Paginations.MaxPerPageLookup);
                        dataPaging = (PaginationModels<PaginationAgreementNumberOldModels>)data.data;
                    }
                    else
                    {
                        dataPaging = sessionAgreementOld;
                    }                    
                }
            }
            else
            {
                //page = 1;
                var data = await services.GetPaginationAgreementOld(searchTerm, lesseeId, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationAgreementNumberOldModels>)data.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CAS/Lookup/AgreementOldPartialView.cshtml", dataPaging);
        }

        [HttpPost]
        public IActionResult PaginationNIKKonsumen(string employeeName, int pageIndex)
        {
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
            CompanyBranchResponse CompanyBranchMdl = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
            var dataPaging = new PaginationModels<PaginationNikKonsumenModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;

            var isKonsol = GetCompanyBranch(sessBranchId, Convert.ToInt32(CompanyBranchMdl.company_id)).IsKonsol;
            if (employeeName == null || employeeName == string.Empty)
            {
                employeeName = string.Empty;
                var data = services.GetPaginationNikKonsumen(employeeName, sessBranchId, isKonsol, false, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationNikKonsumenModels>)data.Result.data;
            }
            else
            {
                //page = 1;
                var data = services.GetPaginationNikKonsumen(employeeName, sessBranchId, isKonsol, false, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationNikKonsumenModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CAS/Lookup/NikKonsumenPartialView.cshtml", dataPaging);
        }

        public MsCompanyBranchModels GetCompanyBranch(string branchId, int companyId)
        {
            var services = new CompanyBranchServices();

            var data = services.GetCompanyBranchDetail(branchId).Result;
            var mData = (MsCompanyBranchModels)data.data;

            return mData;
        }

        #endregion Lookup & Pagination

        /// <summary>
        /// View CAS Mobil/Motor with ReadOnly
        /// </summary>
        /// <param name="creditId">credit id</param>
        /// <param name="item">Motor:001,Mobil:002</param>
        /// <returns></returns>
        public IActionResult ViewTrCas(string creditId, string item)
        {
            InitBindAsync();
            var data = (List<TrCasModels>)services.GetTrCasByCreditId(creditId).Result.data;
            Sessions.SetSessionAsJson(HttpContext.Session, Collection.SessionIdentity.ISEDIT_KEY_NAME, true);

            Sessions.SetSessionAsJson(HttpContext.Session, "IsView", true);
            Sessions.SetSessionAsJson(HttpContext.Session, "IsApprove", false);

            if (data.First().is_repeat_order is true)
            {
                data.First().RepeatOrders.reference_source_desc_sr4 = data.First().RepeatOrders.reference_source_desc;
                data.First().RepeatOrders.reference_source_desc_sr1 = data.First().RepeatOrders.reference_source_desc;
            }

            var viewName = item == ItemId.MOBIL_CODE ? "ViewMobil.cshtml" : "ViewMotor.cshtml";
            return View($"~/Views/Acquisition/CAS/{viewName}", data.FirstOrDefault());
        }

        public IActionResult ApproveTrCas(string creditId, string item)
        {
            InitBindAsync();
            var data = (List<TrCasModels>)services.GetTrCasByCreditId(creditId).Result.data;
            Sessions.SetSessionAsJson(HttpContext.Session, Collection.SessionIdentity.ISEDIT_KEY_NAME, true);

            Sessions.SetSessionAsJson(HttpContext.Session, "IsView", true);
            Sessions.SetSessionAsJson(HttpContext.Session, "IsApprove", true);

            if (data.First().is_repeat_order is true)
            {
                data.First().RepeatOrders.reference_source_desc_sr4 = data.First().RepeatOrders.reference_source_desc;
                data.First().RepeatOrders.reference_source_desc_sr1 = data.First().RepeatOrders.reference_source_desc;
            }

            var viewName = item == ItemId.MOBIL_CODE ? "ViewMobil.cshtml" : "ViewMotor.cshtml";
            return View($"~/Views/Acquisition/CAS/{viewName}", data.FirstOrDefault());
        }

        [HttpPost]
        public IActionResult GotoCMCar(TrCasModels casModels)
        {
            var IsEdit = Sessions.GetSessionFromJson<string>(HttpContext.Session, Collection.SessionIdentity.ISEDIT_KEY_NAME);
            var IsView = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsView");
            var IsApprove = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsApprove");

            var sessUserLdap = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
            CompanyBranchResponse CompanyBranchMdl = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);

            HttpContext.Session.SetString(SessionIdentity.CREDIT_ID, casModels.credit_id);

            if (IsEdit == "true")
            {
                #region Edit CAS data when Next button

                casModels.outlet_code = "";
                casModels.created_by = "";
                casModels.last_updated_by = sessUserLdap.EmployeeId;
                casModels.last_updated_on = DateTime.Now;
                casModels.identity_type_id = "1";
                casModels.company_id = CompanyBranchMdl.company_id; //from Login Session

                casModels.Financials.primary_income = casModels.Financials.txtPrimaryIncome != null ? Int32.Parse(casModels.Financials.txtPrimaryIncome.Replace(".", "")) : 0;
                casModels.Financials.other_income = casModels.Financials.txtOther_income != null ? Int32.Parse(casModels.Financials.txtOther_income.Replace(".", "")) : 0;
                casModels.Financials.household_expenses = casModels.Financials.txtHousehold_expenses != null ? Int32.Parse(casModels.Financials.txtHousehold_expenses.Replace(".", "")) : 0;
                casModels.Financials.education_expenses = casModels.Financials.txtEducation_expenses != null ? Int32.Parse(casModels.Financials.txtEducation_expenses.Replace(".", "")) : 0;
                casModels.Financials.health_expenses = casModels.Financials.txtHealth_expenses != null ? Int32.Parse(casModels.Financials.txtHealth_expenses.Replace(".", "")) : 0;
                casModels.Financials.monthly_other_installment = casModels.Financials.txtMonthly_other_installment != null ? Int32.Parse(casModels.Financials.txtMonthly_other_installment.Replace(".", "")) : 0;

                casModels.DtPasangans.income = casModels.DtPasangans.txtIncome != null ? Int32.Parse(casModels.DtPasangans.txtIncome.Replace(".", "")) : 0;
                casModels.DtPasangans.other_income = casModels.DtPasangans.txtOther_income != null ? Int32.Parse(casModels.DtPasangans.txtOther_income.Replace(".", "")) : 0;

                casModels.credit_source_status = "OKE";

                casModels.branch_id = sessBranchId;

                //set default field Perorangan as empty string if customer type is Corporate/Badan Usaha.
                casModels.email = casModels.email is null ? String.Empty : casModels.email;
                casModels.mother_name = casModels.mother_name is null ? "" : casModels.mother_name;
                casModels.conclusion = String.Empty; //motor
                casModels.customer_name = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_name : casModels.customer_name;
                casModels.birth_place = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_birth_place : casModels.birth_place;

                casModels.company_name = casModels.customer_type == CustomerTypes.INDIVIDUAL_TYPE_CODE ? string.Empty : casModels.company_name;
                casModels.company_birth_place = casModels.customer_type == CustomerTypes.INDIVIDUAL_TYPE_CODE ? string.Empty : casModels.company_birth_place;

                casModels.npwp_no = casModels.npwp_no is null ? String.Empty : casModels.npwp_no;

                casModels.Apuppt = casModels.Apuppt is null ? new() { } : casModels.Apuppt;
                casModels.order_id = casModels.order_id is null ? "" : casModels.order_id;
                casModels.Installments = casModels.Installments is null ? new() : casModels.Installments;
                casModels.RepeatOrders = casModels.is_repeat_order is false ? new() : casModels.RepeatOrders;
                casModels.DtReferensi = casModels.DtReferensi is null ? new() : casModels.DtReferensi;

                if (casModels.is_repeat_order && casModels.RepeatOrders != null)
                {
                    casModels.RepeatOrders.reference_source_desc = casModels.RepeatOrders.reference_source_desc_sr1 is null ? casModels.RepeatOrders.reference_source_desc_sr4 : casModels.RepeatOrders.reference_source_desc_sr1;
                    casModels.RepeatOrders.reference_source_desc_sr1 = casModels.RepeatOrders.reference_source_desc_sr1 is null ? String.Empty : casModels.RepeatOrders.reference_source_desc_sr1;
                    casModels.RepeatOrders.reference_source_desc_sr4 = casModels.RepeatOrders.reference_source_desc_sr4 is null ? String.Empty : casModels.RepeatOrders.reference_source_desc_sr4;
                }

                if (!IsFieldDataReferencesFilledOut(casModels.DtReferensi))
                {
                    ViewBag.State = Collection.Status.WARNING;
                    ViewBag.Message = "Mohon isi DATA REFERENSI dengan lengkap. Silahkan coba kembali";

                    InitBindAsync();
                    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOBIL_CODE);
                }
                if (casModels.is_references)
                {
                    casModels.DtReferensi = RemoveNullableReferences(casModels);
                }

                var data = services.UpdateTrCasMobil(casModels);

                if (data.Result.status == Collection.Status.SUCCESS)
                {
                    ViewBag.State = Collection.Status.SUCCESS;
                    ViewBag.Message = data.Result.message;
                }
                else
                {
                    ViewBag.State = Collection.Status.FAILED;
                    ViewBag.Message = data.Result.message;
                }

                #endregion Edit CAS data when Next button
            }
            else
            {
                if (!IsCreditIdExist(casModels.credit_id))
                {
                    if (!IsReferenceInputSameWithMaxCol(casModels))
                    {
                        ViewBag.State = Collection.Status.INFO;
                        ViewBag.Message = "Mohon isi jumlah DATA REFERENSI dengan sesuai. Silahkan coba kembali";

                        InitBindAsync();
                        return View("~/Views/Acquisition/CAS/Index.cshtml");
                    }

                    if (!IsFieldDataReferencesFilledOut(casModels.DtReferensi))
                    {
                        ViewBag.State = Collection.Status.WARNING;
                        ViewBag.Message = "Mohon isi DATA REFERENSI dengan lengkap. Silahkan coba kembali";

                        InitBindAsync();
                        return View("~/Views/Acquisition/CAS/Index.cshtml");
                    }
                    if (casModels.is_references)
                    {
                        casModels.DtReferensi = RemoveNullableReferences(casModels);
                    }
                    casModels.credit_id = GenerateCreditID(sessBranchId);
                    casModels.outlet_code = "";
                    casModels.created_by = sessUserLdap.EmployeeId;
                    casModels.identity_type_id = "1";
                    casModels.credit_source_status = "OKE";
                    casModels.branch_id = sessBranchId;
                    casModels.company_id = CompanyBranchMdl.company_id; //from Login Session

                    casModels.Financials.primary_income = casModels.Financials.txtPrimaryIncome != null ? Int32.Parse(casModels.Financials.txtPrimaryIncome.Replace(".", "")) : 0;
                    casModels.Financials.other_income = casModels.Financials.txtOther_income != null ? Int32.Parse(casModels.Financials.txtOther_income.Replace(".", "")) : 0;
                    casModels.Financials.household_expenses = casModels.Financials.txtHousehold_expenses != null ? Int32.Parse(casModels.Financials.txtHousehold_expenses.Replace(".", "")) : 0;
                    casModels.Financials.education_expenses = casModels.Financials.txtEducation_expenses != null ? Int32.Parse(casModels.Financials.txtEducation_expenses.Replace(".", "")) : 0;
                    casModels.Financials.health_expenses = casModels.Financials.txtHealth_expenses != null ? Int32.Parse(casModels.Financials.txtHealth_expenses.Replace(".", "")) : 0;
                    casModels.Financials.monthly_other_installment = casModels.Financials.txtMonthly_other_installment != null ? Int32.Parse(casModels.Financials.txtMonthly_other_installment.Replace(".", "")) : 0;

                    casModels.DtPasangans.income = casModels.DtPasangans.txtIncome != null ? Int32.Parse(casModels.DtPasangans.txtIncome.Replace(".", "")) : 0;
                    casModels.DtPasangans.other_income = casModels.DtPasangans.txtOther_income != null ? Int32.Parse(casModels.DtPasangans.txtOther_income.Replace(".", "")) : 0;

                    casModels.email = casModels.email is null ? String.Empty : casModels.email;
                    casModels.mother_name = casModels.mother_name is null ? String.Empty : casModels.mother_name;
                    casModels.customer_name = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_name : casModels.customer_name;
                    casModels.birth_place = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_birth_place : casModels.birth_place;

                    casModels.company_name = casModels.company_name is null ? String.Empty : casModels.company_name;
                    casModels.company_birth_place = casModels.company_birth_place is null ? String.Empty : casModels.company_birth_place;
                    casModels.customer_name = casModels.customer_name is null ? String.Empty : casModels.customer_name;
                    casModels.birth_place = casModels.birth_place is null ? String.Empty : casModels.birth_place;

                    casModels.npwp_no = casModels.npwp_no is null ? String.Empty : casModels.npwp_no;

                    casModels.order_id = casModels.order_id is null ? "" : casModels.order_id;
                    casModels.Apuppt = casModels.Apuppt is null ? new() { } : casModels.Apuppt;
                    casModels.Installments = casModels.Installments is null ? new() : casModels.Installments;

                    casModels.is_repeat_order = casModels.RepeatOrders is null ? false : true;
                    casModels.RepeatOrders = casModels.is_repeat_order is false ? new() : casModels.RepeatOrders;

                    if (casModels.is_references)
                    {
                        casModels.DtReferensi = RemoveNullableReferences(casModels);
                    }

                    var data = services.InsertTrCasMobil(casModels);
                    if (data.Result.status == Collection.Status.SUCCESS)
                    {
                        ViewBag.State = Collection.Status.SUCCESS;
                        ViewBag.Message = data.Result.message;
                    }
                    else
                    {
                        ViewBag.State = Collection.Status.FAILED;
                        ViewBag.Message = data.Result.message;
                    }
                }
            }

            if (IsApprove == "true" || IsView == "true")
            {
                return RedirectToAction("CMCar", "CMCar", new { trans_id = casModels.credit_id });
            }
            else
            {
                return RedirectToAction("CMCar", "CMCar");
            }

            HttpContext.Session.SetString(SessionIdentity.CREDIT_ID, casModels.credit_id);
            return RedirectToAction("CMCar", "CMCar");
        }

        [HttpPost]
        public IActionResult GotoCMMotorCycle(TrCasModels casModels)
        {
            var MessageBlackList = "";

            TempData.Remove("SuccessCM");
            TempData.Remove("MessageCM");

            var IsEdit = Sessions.GetSessionFromJson<string>(HttpContext.Session, Collection.SessionIdentity.ISEDIT_KEY_NAME);
            var IsView = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsView");
            var IsApprove = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsApprove");

            var userSession = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);
            CompanyBranchResponse CompanyBranchMdl = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);

            HttpContext.Session.SetString(SessionIdentity.CREDIT_ID, casModels.credit_id);

            if (IsEdit == "true")
            {
                casModels.outlet_code = "";
                casModels.created_by = "";
                casModels.last_updated_by = userSession.EmployeeId;
                casModels.last_updated_on = DateTime.Now;
                casModels.identity_type_id = "1";
                casModels.company_id = CompanyBranchMdl.company_id;

                casModels.Financials.primary_income = casModels.Financials.txtPrimaryIncome != null ? Int32.Parse(casModels.Financials.txtPrimaryIncome.Replace(".","")) : 0;
                casModels.Financials.other_income = casModels.Financials.txtOther_income != null ? Int32.Parse(casModels.Financials.txtOther_income.Replace(".","")) : 0;
                casModels.Financials.household_expenses = casModels.Financials.txtHousehold_expenses != null ? Int32.Parse(casModels.Financials.txtHousehold_expenses.Replace(".","")) : 0;
                casModels.Financials.education_expenses = casModels.Financials.txtEducation_expenses != null ? Int32.Parse(casModels.Financials.txtEducation_expenses.Replace(".","")) : 0;
                casModels.Financials.health_expenses = casModels.Financials.txtHealth_expenses != null ? Int32.Parse(casModels.Financials.txtHealth_expenses.Replace(".","")) : 0;
                casModels.Financials.monthly_other_installment = casModels.Financials.txtMonthly_other_installment != null ? Int32.Parse(casModels.Financials.txtMonthly_other_installment.Replace(".","")) : 0;

                casModels.DtPasangans.income = casModels.DtPasangans.txtIncome != null ? Int32.Parse(casModels.DtPasangans.txtIncome.Replace(".", "")) : 0;
                casModels.DtPasangans.other_income = casModels.DtPasangans.txtOther_income != null ? Int32.Parse(casModels.DtPasangans.txtOther_income.Replace(".", "")) : 0;

                casModels.credit_source_status = "OKE";

                casModels.conclusion = casModels.conclusion;
                casModels.branch_id = sessBranchId;
                casModels.order_id = casModels.order_id is null ? "" : casModels.order_id;

                //set default field Perorangan as empty string if customer type is Corporate/Badan Usaha.
                casModels.email = casModels.email is null ? String.Empty : casModels.email;
                casModels.mother_name = casModels.mother_name is null ? "" : casModels.mother_name;
                casModels.customer_name = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_name : casModels.customer_name;
                casModels.birth_place = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_birth_place : casModels.birth_place;

                casModels.company_name = casModels.customer_type == CustomerTypes.INDIVIDUAL_TYPE_CODE ? string.Empty : casModels.company_name;
                casModels.company_birth_place = casModels.customer_type == CustomerTypes.INDIVIDUAL_TYPE_CODE ? string.Empty : casModels.company_birth_place;

                casModels.npwp_no = casModels.npwp_no is null ? String.Empty : casModels.npwp_no;

                casModels.Apuppt = casModels.Apuppt is null ? new() { } : casModels.Apuppt;
                casModels.order_id = casModels.order_id is null ? "" : casModels.order_id;
                casModels.Installments = casModels.Installments is null ? new() : casModels.Installments;
                casModels.RepeatOrders = casModels.is_repeat_order is false ? new() : casModels.RepeatOrders;
                casModels.DtReferensi = casModels.DtReferensi is null ? new() : casModels.DtReferensi;

                if (IsView == "false")
                {

                    if (casModels.npwp_no is not null || casModels.npwp_no != String.Empty)
                    {
                        if (casModels.npwp_no.Length <= 15 && casModels.npwp_no.Length > 0)
                        {
                            ViewBag.State = Collection.Status.WARNING;
                            ViewBag.Message = "Mohon masukan No NPWP dengan benar. Hilangkan seluruh angka di field NPWP jika Konsumen tidak punya NPWP. Silahkan check dan coba kembali.";

                            return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                        }
                    }

                    if (casModels.is_references)
                    {
                        if (!IsFieldDataReferencesFilledOut(casModels.DtReferensi))
                        {
                            ViewBag.State = Collection.Status.WARNING;
                            ViewBag.Message = "Mohon isi DATA REFERENSI dengan lengkap. Silahkan coba kembali";

                            return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                        }

                        casModels.DtReferensi = RemoveNullableReferences(casModels);

                        //if (!IsInputRefMatchWithMaxKol(casModels))
                        //{
                        //    var maxKol = ViewBag.DataReferenceCount;
                        //    ViewBag.State = Collection.Status.WARNING;
                        //    ViewBag.Message = $"Mohon isi DATA REFERENSI dengan sesuai Max Kol yang sudah di tetapkan. Anda diharuskan isi {maxKol} Data Referensi";

                        //    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                        //}
                    }

                    if (casModels.Financials.primary_income == 0)
                    {
                        ViewBag.State = Collection.Status.INFO;
                        ViewBag.Message = "Mohon masukan Penghasilan Utama nasabah pada panel Data Penghasilan. Silahkan cek dan coba kembali.";

                        return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                    }

                    if (casModels.is_repeat_order && casModels.RepeatOrders != null)
                    {
                        casModels.RepeatOrders.reference_source_desc = casModels.RepeatOrders.reference_source_desc_sr1 is null ? casModels.RepeatOrders.reference_source_desc_sr4 : casModels.RepeatOrders.reference_source_desc_sr1;
                        casModels.RepeatOrders.reference_source_desc_sr1 = casModels.RepeatOrders.reference_source_desc_sr1 is null ? String.Empty : casModels.RepeatOrders.reference_source_desc_sr1;
                        casModels.RepeatOrders.reference_source_desc_sr4 = casModels.RepeatOrders.reference_source_desc_sr4 is null ? String.Empty : casModels.RepeatOrders.reference_source_desc_sr4;
                    }

                    if (casModels.customer_type == "P")
                    {
                        if (Convert.ToDecimal((DateTime.Today - casModels.birth_date).TotalDays) / Convert.ToDecimal(365) < Convert.ToDecimal(21))
                        {
                            if (casModels.marital_status == 1 && casModels.marital_status != null)
                            {
                                ViewBag.State = Collection.Status.WARNING;
                                ViewBag.Message = "Minimal usia 21 tahun";

                                return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                            }
                            else
                            {
                                if (Convert.ToDecimal((DateTime.Today - casModels.birth_date).TotalDays) / Convert.ToDecimal(365) < Convert.ToDecimal(15))
                                {
                                    ViewBag.State = Collection.Status.WARNING;
                                    ViewBag.Message = "Minimal usia 15 tahun";

                                    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                                }
                            }
                        }
                    }

                    var dataItemBlackList = new List<CheckBlacklistModels>();
                    var dataBlackList = (List<CheckBlacklistModels>)services.CheckBlacklistKTP(casModels.identity_number).Result.data;
                    if (dataBlackList.Count > 0)
                    {
                        var itemBlackList = dataBlackList.First();
                        if (itemBlackList.is_blacklist == true)
                        {
                            casModels.is_blacklist = true;

                            MessageBlackList = "Checking Blacklist Result: KTP " + casModels.identity_number + ", " + itemBlackList.message_error + ". Klik OK untuk lanjut ke halaman berikutnya.";

                        }
                    }

                    var data = services.UpdateTrCasMotor(casModels);

                    if (data.Result.status == Collection.Status.SUCCESS)
                    {
                        if (MessageBlackList != "")
                        {
                            ViewBag.State = Collection.Status.WARNING_THEN_ROUTE;
                            ViewBag.Message = MessageBlackList;
                            ViewBag.UrlRoute = "/CM/CMMotorCycle";
                            HttpContext.Session.SetString(SessionIdentity.CREDIT_ID, casModels.credit_id);

                            return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                        }
                        else
                        {
                            ViewBag.State = Collection.Status.SUCCESS;
                            ViewBag.Message = data.Result.message;
                        }                        
                    }
                    else
                    {
                        ViewBag.State = Collection.Status.FAILED;
                        ViewBag.Message = data.Result.message;
                        InitBindAsync();
                        return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                    }
                }
            }
            else
            {
                //#region Validate Data References

                //if (!IsReferenceInputSameWithMaxCol(casModels))
                //{
                //    ViewBag.State = Collection.Status.INFO;
                //    ViewBag.Message = "Mohon isi jumlah DATA REFERENSI dengan sesuai. Silahkan coba kembali";

                //    InitBindAsync();
                //    return View("~/Views/Acquisition/CAS/IndexMotor.cshtml");
                //}
                //if (!IsFieldDataReferencesFilledOut(casModels.DtReferensi))
                //{
                //    ViewBag.State = Collection.Status.WARNING;
                //    ViewBag.Message = "Mohon isi DATA REFERENSI dengan lengkap. Silahkan coba kembali";

                //    InitBindAsync();
                //    return View("~/Views/Acquisition/CAS/IndexMotor.cshtml");
                //}
                //if (casModels.is_references)
                //{
                //    casModels.DtReferensi = RemoveNullableReferences(casModels);
                //}

                //#endregion Validate Data References

                if (casModels.npwp_no is not null || casModels.npwp_no != String.Empty)
                {
                    if (casModels.npwp_no.Length <= 15 && casModels.npwp_no.Length > 0)
                    {
                        ViewBag.State = Collection.Status.WARNING;
                        ViewBag.Message = "Mohon masukan No NPWP dengan benar. Hilangkan seluruh angka di field NPWP jika Konsumen tidak punya NPWP. Silahkan check dan coba kembali.";

                        return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                    }
                }

                if (casModels.is_references)
                {
                    if (!IsFieldDataReferencesFilledOut(casModels.DtReferensi))
                    {
                        ViewBag.State = Collection.Status.WARNING;
                        ViewBag.Message = "Mohon isi DATA REFERENSI dengan lengkap. Silahkan coba kembali";

                        return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                    }

                    casModels.DtReferensi = RemoveNullableReferences(casModels);

                    //if (!IsInputRefMatchWithMaxKol(casModels))
                    //{
                    //    var maxKol = ViewBag.DataReferenceCount;
                    //    ViewBag.State = Collection.Status.WARNING;
                    //    ViewBag.Message = $"Mohon isi DATA REFERENSI dengan sesuai Max Kol yang sudah di tetapkan. Anda diharuskan isi {maxKol} Data Referensi";

                    //    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                    //}
                }

                if (casModels.Financials.primary_income == 0)
                {
                    ViewBag.State = Collection.Status.INFO;
                    ViewBag.Message = "Mohon masukan Penghasilan Utama nasabah pada panel Data Penghasilan. Silahkan cek dan coba kembali.";

                    return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                }

                if (casModels.is_repeat_order && casModels.RepeatOrders != null)
                {
                    casModels.RepeatOrders.reference_source_desc = casModels.RepeatOrders.reference_source_desc_sr1 is null ? casModels.RepeatOrders.reference_source_desc_sr4 : casModels.RepeatOrders.reference_source_desc_sr1;
                    casModels.RepeatOrders.reference_source_desc_sr1 = casModels.RepeatOrders.reference_source_desc_sr1 is null ? String.Empty : casModels.RepeatOrders.reference_source_desc_sr1;
                    casModels.RepeatOrders.reference_source_desc_sr4 = casModels.RepeatOrders.reference_source_desc_sr4 is null ? String.Empty : casModels.RepeatOrders.reference_source_desc_sr4;
                }

                if (casModels.customer_type == "P")
                {
                    if (Convert.ToDecimal((DateTime.Today - casModels.birth_date).TotalDays) / Convert.ToDecimal(365) < Convert.ToDecimal(21))
                    {
                        if (casModels.marital_status == 1 || casModels.marital_status != null)
                        {
                            ViewBag.State = Collection.Status.WARNING;
                            ViewBag.Message = "Minimal usia 21 tahun";

                            return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                        }
                        else
                        {
                            if (Convert.ToDecimal((DateTime.Today - casModels.birth_date).TotalDays) / Convert.ToDecimal(365) < Convert.ToDecimal(15))
                            {
                                ViewBag.State = Collection.Status.WARNING;
                                ViewBag.Message = "Minimal usia 15 tahun";

                                return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                            }
                        }
                    }
                }

                var dataItemBlackList = new List<CheckBlacklistModels>();
                var dataBlackList = (List<CheckBlacklistModels>)services.CheckBlacklistKTP(casModels.identity_number).Result.data;
                if (dataBlackList.Count > 0)
                {
                    var itemBlackList = dataBlackList.First();
                    if (itemBlackList.is_blacklist == true)
                    {
                        casModels.is_blacklist = true;

                        MessageBlackList = "Checking Blacklist Result: KTP " + casModels.identity_number + ", " + itemBlackList.message_error;

                    }
                }

                casModels.credit_id = GenerateCreditID(sessBranchId);
                casModels.outlet_code = "";
                casModels.created_by = userSession.EmployeeId;

                casModels.identity_type_id = "1";
                casModels.credit_source_status = "OKE";

                casModels.branch_id = sessBranchId;
                casModels.company_id = CompanyBranchMdl.company_id; //from Login Session

                casModels.Financials.primary_income = casModels.Financials.txtPrimaryIncome != null ? Int32.Parse(casModels.Financials.txtPrimaryIncome.Replace(".", "")) : 0;
                casModels.Financials.other_income = casModels.Financials.txtOther_income != null ? Int32.Parse(casModels.Financials.txtOther_income.Replace(".", "")) : 0;
                casModels.Financials.household_expenses = casModels.Financials.txtHousehold_expenses != null ? Int32.Parse(casModels.Financials.txtHousehold_expenses.Replace(".", "")) : 0;
                casModels.Financials.education_expenses = casModels.Financials.txtEducation_expenses != null ? Int32.Parse(casModels.Financials.txtEducation_expenses.Replace(".", "")) : 0;
                casModels.Financials.health_expenses = casModels.Financials.txtHealth_expenses != null ? Int32.Parse(casModels.Financials.txtHealth_expenses.Replace(".", "")) : 0;
                casModels.Financials.monthly_other_installment = casModels.Financials.txtMonthly_other_installment != null ? Int32.Parse(casModels.Financials.txtMonthly_other_installment.Replace(".", "")) : 0;

                casModels.DtPasangans.income = casModels.DtPasangans.txtIncome != null ? Int32.Parse(casModels.DtPasangans.txtIncome.Replace(".", "")) : 0;
                casModels.DtPasangans.other_income = casModels.DtPasangans.txtOther_income != null ? Int32.Parse(casModels.DtPasangans.txtOther_income.Replace(".", "")) : 0;

                casModels.email = casModels.email is null ? String.Empty : casModels.email;
                casModels.mother_name = casModels.mother_name is null ? String.Empty : casModels.mother_name;
                casModels.customer_name = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_name : casModels.customer_name;
                casModels.birth_place = casModels.customer_type == CustomerTypes.CORPORATE_TYPE_CODE ? casModels.company_birth_place : casModels.birth_place;

                casModels.company_name = casModels.company_name is null ? String.Empty : casModels.company_name;
                casModels.company_birth_place = casModels.company_birth_place is null ? String.Empty : casModels.company_birth_place;
                casModels.customer_name = casModels.customer_name is null ? String.Empty : casModels.customer_name;
                casModels.birth_place = casModels.birth_place is null ? String.Empty : casModels.birth_place;

                casModels.npwp_no = casModels.npwp_no is null ? String.Empty : casModels.npwp_no;

                casModels.order_id = casModels.order_id is null ? "" : casModels.order_id;
                casModels.Apuppt = casModels.Apuppt is null ? new() { } : casModels.Apuppt;
                casModels.Installments = casModels.Installments is null ? new() : casModels.Installments;

                casModels.RepeatOrders.reference_source_desc = casModels.RepeatOrders.reference_source_desc_sr1 is null ? casModels.RepeatOrders.reference_source_desc_sr4 : casModels.RepeatOrders.reference_source_desc_sr1;
                casModels.is_repeat_order = casModels.RepeatOrders is null ? false : true;
                casModels.RepeatOrders = casModels.is_repeat_order is false ? new() : casModels.RepeatOrders;
                casModels.conclusion = "";

                if (casModels.is_references)
                {
                    casModels.DtReferensi = RemoveNullableReferences(casModels);
                }

                var data = services.InsertTrCasMotor(casModels);

                if (data.Result.status == Collection.Status.SUCCESS)
                {
                    if (MessageBlackList != "")
                    {
                        ViewBag.State = Collection.Status.WARNING_THEN_ROUTE;
                        ViewBag.Message = MessageBlackList;
                        ViewBag.UrlRoute = "/CM/CMMotorCycle";
                        HttpContext.Session.SetString(SessionIdentity.CREDIT_ID, casModels.credit_id);

                        return GetTrCasByCreditId(casModels.credit_id, true, ItemId.MOTOR_CODE);
                    }
                    else
                    {
                        ViewBag.State = Collection.Status.SUCCESS;
                        ViewBag.Message = data.Result.message;
                    }                    
                }
                else
                {
                    ViewBag.State = Collection.Status.FAILED;
                    ViewBag.Message = data.Result.message;
                }
            }

            if (IsApprove == "true" || IsView == "true")
            {
                return RedirectToAction("CMMotorCycle", "CM", new { trans_id = casModels.credit_id });
            }
            else
            {
                return RedirectToAction("CMMotorCycle", "CM");
            }

            HttpContext.Session.SetString(SessionIdentity.CREDIT_ID, casModels.credit_id);
            return RedirectToAction("CMMotorCycle", "CM");
        }

        /// <summary>
        /// Goto CM Page after Save CAS successfully
        /// </summary>
        /// <param name="mItemId"></param>
        /// <param name="mCreditId"></param>
        /// <returns></returns>
        public IActionResult NextToCMPage(string mItemId, string mCreditId)
        {
            string actionName = "", controllerName = "";
            if (mItemId == ItemId.MOBIL_CODE)
            {
                actionName = "CMCar";
                controllerName = "CMCar";
            }
            else if (mItemId == ItemId.MOTOR_CODE)
            {
                actionName = "CMMotorCycle";
                controllerName = "CM";
            }
            HttpContext.Session.SetString(SessionIdentity.CREDIT_ID, mCreditId);
            return RedirectToAction(actionName, controllerName);
        }

        /// <summary>
        /// View TrCas with View Edit
        /// </summary>
        /// <param name="creditId"></param>
        /// <param name="isEdit"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        ///
        [HttpPost]
        public IActionResult GetTrCasByCreditId(string creditId, bool isEdit, string item)
        {
            InitBindAsync();
            var data = (List<TrCasModels>)services.GetTrCasByCreditId(creditId).Result.data;
            var dataCas = data.FirstOrDefault();

            if (dataCas.DtReferensi != null)
            {
                dataCas.DtReferensi = dataCas.DtReferensi.referensi_id.Length < 3 ? AddNewRowIntoDataReferences(dataCas) : dataCas.DtReferensi;
            }
            if(dataCas.is_repeat_order is true)
            {
                dataCas.RepeatOrders.reference_source_desc_sr1 = dataCas.RepeatOrders.reference_source_desc;
                dataCas.RepeatOrders.reference_source_desc_sr4 = dataCas.RepeatOrders.reference_source_desc;
            }
            CreateReportSchema();

            Sessions.SetSessionAsJson(HttpContext.Session, Collection.SessionIdentity.ISEDIT_KEY_NAME, isEdit);
            Sessions.SetSessionAsJson(HttpContext.Session, SessionIdentity.ISVIEW, false);

            var viewName = "Index.cshtml"; //default Mobil
            if (item == Collection.ItemId.MOTOR_CODE)
            {
                viewName = "IndexMotor.cshtml";
            }
            return View($"~/Views/Acquisition/CAS/{viewName}", dataCas);
        }

        public IActionResult GetTrCasByCreditId_2()
        {
            var creditId = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            //var creditId = Sessions.GetSessionFromJson<string>(HttpContext.Session, "credit_id");
            var isEdit = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsEdit");
            var item = Sessions.GetSessionFromJson<string>(HttpContext.Session, "item");

            InitBindAsync();

            var data = (List<TrCasModels>)services.GetTrCasByCreditId(creditId).Result.data;
            var dataCas = data.FirstOrDefault();
            if (dataCas.DtReferensi != null)
            {
                dataCas.DtReferensi = dataCas.DtReferensi.referensi_id.Length < 3 ? AddNewRowIntoDataReferences(dataCas) : dataCas.DtReferensi;
            }
            if (dataCas.is_repeat_order is true)
            {
                dataCas.RepeatOrders.reference_source_desc_sr4 = dataCas.RepeatOrders.reference_source_desc;
                dataCas.RepeatOrders.reference_source_desc_sr1 = dataCas.RepeatOrders.reference_source_desc;
            }


            Sessions.SetSessionAsJson(HttpContext.Session, Collection.SessionIdentity.ISEDIT_KEY_NAME, isEdit);

            Sessions.SetSessionAsJson(HttpContext.Session, "IsView", false);

            var viewName = "Index.cshtml"; //default Mobil
            if (item == Collection.ItemId.MOTOR_CODE)
            {
                viewName = "IndexMotor.cshtml";
            }
            return View($"~/Views/Acquisition/CAS/{viewName}", data.FirstOrDefault());
        }

        [HttpGet]
        public IActionResult GetDataPoolingOrder(string orderId)
        {
            var data = (List<PoolingOrderModels>)services.GetDataPoolingOrder(orderId).Result.data;
            Sessions.SetSessionAsJson(HttpContext.Session, Collection.SessionIdentity.ISEDIT_KEY_NAME, false);
            InitBindAsync();
            return Ok(data.ToList().FirstOrDefault());
        }

        [HttpGet]
        public IActionResult CheckBlacklist(string ktp, string itemId)
        {
            var dataItem = new List<CheckBlacklistModels>();
            var data = (List<CheckBlacklistModels>)services.CheckBlacklistKTP(ktp).Result.data;
            if (data.Count > 0)
            {
                var items = data.First();
                if (itemId == ItemId.MOBIL_CODE)
                {
                    if (items.reason_id == 5 || items.reason_id == 2 || items.reason_id == 4)
                    {
                        dataItem.Add(new CheckBlacklistModels
                        {
                            reason_id = items.reason_id,
                            is_blacklist = items.is_blacklist,
                            message_error = items.message_error,
                            is_allow = false
                        });
                    }
                    else if (items.reason_id == 1 || items.reason_id == 3)
                    {
                        dataItem.Add(new CheckBlacklistModels
                        {
                            reason_id = items.reason_id,
                            is_blacklist = items.is_blacklist,
                            message_error = items.message_error,
                            is_allow = true
                        });
                    }
                }
                else if (itemId == ItemId.MOTOR_CODE)
                {
                    dataItem.Add(new CheckBlacklistModels
                    {
                        reason_id = items.reason_id,
                        is_blacklist = items.is_blacklist,
                        message_error = items.message_error,
                        is_allow = true
                    });
                }
            }
            return Ok(dataItem);
        }

        [HttpGet]
        public IActionResult CheckApuppt(ApupptParamModels items)
        {
            var data = (List<CheckApupptModels>)services.CheckApuppt(items).Result.data;
            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetNppLama(string ktp)
        {
            var data = services.GetNppLamaRO(ktp).Result;
            return Ok(data);
        }

        public IActionResult GetDataReferensi(string nik)
        {
            var data = (FclResultSlikModels)services.GetDataReferensiSlik(nik).Result.data;
            var maxKol = data is null ? 0 : data.MaxKol;

            var requiredMaxKol = Convert.ToInt32(maxKol) <= 3 ? 2 : 3;
            ViewBag.DataReferenceCount = requiredMaxKol;

            return Ok(requiredMaxKol);
        }

        public int GetMaxKolReference(string nik)
        {
            var data = (FclResultSlikModels)services.GetDataReferensiSlik(nik).Result.data;
            var maxKol = data is null ? 0 : data.MaxKol;

            var requiredMaxKol = Convert.ToInt32(maxKol) <= 3 ? 2 : 3;
            return requiredMaxKol;
        }

        public IActionResult DownloadDokumen(string credit_id, string item_Id, string photo_type, string actionName)
        {
            try
            {
                var data = new List<DataPhotoModels>();
                var LocalDestinationPathTemp = $"{this.environment.WebRootPath}{_FOLDER_TARGET}";

                if (photo_type == PhotoTypeName.KTP)
                {
                    var response = services.GetPathKTPKonsumen(credit_id);
                    data = (List<DataPhotoModels>)response.Result.data;
                }
                else if (photo_type == PhotoTypeName.KARTU_KELUARGA)
                {
                    var response = services.GetPathKartuKeluargaKonsumen(credit_id);
                    data = (List<DataPhotoModels>)response.Result.data;
                }
                else if (photo_type == PhotoTypeName.SLIP_GAJI)
                {
                    var response = services.GetPathSlipGajiKonsumen(credit_id);
                    data = (List<DataPhotoModels>)response.Result.data;
                }
                else if (photo_type == PhotoTypeName.KTP_PASANGAN)
                {
                    var response = services.GetPathKTPPasngan(credit_id);
                    data = (List<DataPhotoModels>)response.Result.data;
                }
                else if (photo_type == PhotoTypeName.KEPEMILIKAN_RUMAH)
                {
                    var response = services.GetPathKepemilikanRumah(credit_id);
                    data = (List<DataPhotoModels>)response.Result.data;
                }
                RemovingFile(data.First().filename, LocalDestinationPathTemp);

                var urlFoto = Path.Combine(data.First().filePath, data.First().filename);
                var extensionFile = Path.GetExtension(data.First().filename);

                //download from ftp
                WebClient client = new();
                String RemoteFtpPath = "";
                string userNameFtpCredential = FTP.AcquisitionDoc.FTP_USER_NAME;
                var passwordFtpCredential = FTP.AcquisitionDoc.FTP_PASSWORD;
                var portFtpCredential = FTP.AcquisitionDoc.FTP_PORT;
                var hostFtpCredential = FTP.AcquisitionDoc.FTP_HOST;
                var pathDocument = FTP.AcquisitionDoc.FTP_PATH_ACQUISITION_DOCUMENT;
                if (data.First().is_new_zoom == 1)
                {
                    userNameFtpCredential = FTP.AcquisitionDoc_NewZoom.FTP_USER_NAME_NEW_ZOOM;
                    passwordFtpCredential = FTP.AcquisitionDoc_NewZoom.FTP_PASSWORD_NEW_ZOOM;
                    portFtpCredential = FTP.AcquisitionDoc_NewZoom.FTP_PORT_NEW_ZOOM;
                    hostFtpCredential = FTP.AcquisitionDoc_NewZoom.FTP_HOST_NEW_ZOOM;
                    pathDocument = FTP.AcquisitionDoc_NewZoom.FTP_PATH_ACQUISITION_DOCUMENT_NEW_ZOOM;
                }
                client.Credentials = new NetworkCredential(userNameFtpCredential, passwordFtpCredential);
                RemoteFtpPath = $"ftp://{hostFtpCredential}:{portFtpCredential}{pathDocument}{urlFoto}";

                CreateFolderOnDir(this.environment.WebRootPath, _FOLDER_TARGET);

                var destinationPath = $"{LocalDestinationPathTemp}{data.First().filename}";
                client.DownloadFile(RemoteFtpPath, destinationPath);

                byte[] bytes = System.IO.File.ReadAllBytes(destinationPath);

                return File(bytes, GetContentType(RemoteFtpPath), Path.GetFileName(RemoteFtpPath));
            }
            catch (Exception ex)
            {
                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = $"Failed to Download {photo_type}: {ex.Message} {ex.InnerException}. Mohon kontak administrator";

                return actionName == SessionIdentity.ISEDIT_KEY_NAME ? GetTrCasByCreditId(credit_id, true, item_Id) 
                    : ViewTrCas(credit_id, item_Id);
            }
        }

        #region RDLC Print

        public void CreateReportSchema()
        {
            try
            {
                var types = new[] { typeof(PrintLAHSModels) };//put model in here
                var xri = new System.Xml.Serialization.XmlReflectionImporter();
                var xss = new System.Xml.Serialization.XmlSchemas();
                var xse = new System.Xml.Serialization.XmlSchemaExporter(xss);
                foreach (var type in types)
                {
                    var xtm = xri.ImportTypeMapping(type);
                    xse.ExportTypeMapping(xtm);
                }
                using var sw = new StreamWriter("ReportLAHSSchemas.xsd", false, Encoding.UTF8);
                for (int i = 0; i < xss.Count; i++)
                {
                    var xs = xss[i];
                    xs.Id = "GetMobileSurvey";
                    xs.Write(sw);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        public IActionResult PrintLAHS(string creditId, string itemType, TrCasModels model)
        {
            //creditId = "1152000014";
            var dataSource = (List<PrintLAHSModels>)services.PrintLAHSCas(creditId).Result.data;
            var userSession = Sessions.GetSessionFromJson<LDAPModels>(HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
            var sessBranchId = Sessions.GetSessionFromJson<string>(HttpContext.Session, SessionIdentity.BRANCH_ID);

            var branchId = sessBranchId;
            var printId = userSession.EmployeeId;
            var targetRptName = "";

            //var targetRptName = "FINCORE.WEB.Views.Acquisition.CAS.Reports.RptLAHSPekerja.rdlc";
            //var targetRptName_ = "FINCORE.WEB.Views.Acquisition.CAS.Reports.RptLAHSWirausaha.rdlc";

            if (model.Financials.profession_id == "017" || model.Financials.profession_id == "018" || model.Financials.profession_id == "019")
            {
                targetRptName = "FINCORE.WEB.Views.Acquisition.CAS.Reports.RptLAHSWirausaha.rdlc";
            }
            else
            {
                targetRptName = "FINCORE.WEB.Views.Acquisition.CAS.Reports.RptLAHSPekerja.rdlc";
            }

            byte[] bytePdf;
            var fileName = $"LAHS_{creditId}";

            try
            {
                using var localReport = new Microsoft.Reporting.NETCore.LocalReport();
                localReport.DataSources.Clear();

                Helpers.Report.Load(localReport, dataSource.First(), targetRptName);

                //LoadDokumen(localReport, dataSource.First(), fileName);
                bytePdf = localReport.Render(Helpers.FileExtensions.PDF);

                return File(bytePdf, Helpers.MimeTypes.PDF, $"{fileName}." + Helpers.FileExtensions.PDF);
            }
            catch (Exception ex)
            {
                string msgError = "";
                if (dataSource.Count == 0)
                {
                    msgError = $"Data Survey LAHS dengan Credit Id {creditId} tidak ditemukan. ";
                }
                ViewBag.StateRoute = Collection.Status.FAILED;
                ViewBag.Message = $"{msgError}Failed Print LAHS, {ex.Message} {ex.InnerException}. Mohon hubungi administrator.";
                ViewBag.UrlRoute = "/Acquisition/IndexMotor";

                return GetTrCasByCreditId(creditId, true, itemType);
            }
        }

        #endregion RDLC Print

        #region Setup Select2 Dropdown

        public IActionResult LocationReferenceData(string searchTerm)
        {
            searchTerm = searchTerm is null ? string.Empty : searchTerm;
            var data = services.GetPaginationMsLocation(searchTerm, 1, Paginations.MaxPerPageLookup);
            var x = (PaginationModels<PaginationLocationModels>)data.Result.data;

            return Ok(x);
        }

        #endregion Setup Select2 Dropdown

        #region Function Helper

        public bool BirthDateIsFalid(string birthDate)
        { return true; }

        public IActionResult CheckFotoDokumenIsFound(string credit_id)
        {
            var disabledList = new List<bool>();
            var responseKtp = services.GetPathKTPKonsumen(credit_id);
            var dataKtp = (List<DataPhotoModels>)responseKtp.Result.data;

            var responseKk = services.GetPathKartuKeluargaKonsumen(credit_id);
            var dataKk = (List<DataPhotoModels>)responseKk.Result.data;

            var responseSlip = services.GetPathSlipGajiKonsumen(credit_id);
            var dataSlip = (List<DataPhotoModels>)responseSlip.Result.data;

            var responseKtpSpouse = services.GetPathKTPPasngan(credit_id);
            var dataKtpSpouse = (List<DataPhotoModels>)responseKtpSpouse.Result.data;

            var responseDocKepemilikan = services.GetPathKTPPasngan(credit_id);
            var dataDocKepemilikan = (List<DataPhotoModels>)responseDocKepemilikan.Result.data;

            //send & set state button disabled is true or false
            disabledList.Add(dataKtp.Count == 0 || dataKtp == null ? true : false);
            disabledList.Add(dataKk.Count == 0 || dataKk == null ? true : false);
            disabledList.Add(dataSlip.Count == 0 || dataSlip == null ? true : false);
            disabledList.Add(dataKtpSpouse.Count == 0 || dataKtpSpouse == null ? true : false);
            disabledList.Add(dataDocKepemilikan.Count == 0 || dataDocKepemilikan == null ? true : false);

            return Ok(disabledList);
        }

        /// <summary>
        /// Check apakah Input Data Reference User sudah sesuai dengan MaxColl.
        ///
        /// True: Sesuai MaxColl
        /// False: Tidak Sesuai MaxColl
        /// </summary>
        /// <param name="nik"></param>
        /// <param name="casModels"></param>
        /// <returns></returns>
        public bool IsReferenceInputSameWithMaxCol(TrCasModels casModels)
        {
            var data = (FclResultSlikModels)services.GetDataReferensiSlik(casModels.identity_number).Result.data;
            var maxKolRequired = data is null ? 0 : data.MaxKol;
            var xMaxKolRequired = Convert.ToInt32(maxKolRequired) <= 3 ? 2 : 3;

            var userReferenceInputCount = casModels.DtReferensi.alamat_referensi.Where(m => m != null).ToArray().Count();

            return userReferenceInputCount != xMaxKolRequired ? false : true;
        }

        public async void InitBindAsync()
        {
            var dataLocationPayment = await services.GetMsPaymentPoint(Commons.PAYMENT_TYPE.LOKASI_PEMBAYARAN);
            var dataPaymentPlan = await services.GetMsPaymentPoint(Commons.PAYMENT_TYPE.RENCANA_PEMBAYARAN);
            var dataMonthlyOtherInstallments = await services.GetMonthlyOtherInstallment();
            var dataCreditSource = await services.GetCreditSource();
            var dataCustomerType = await services.GetCustomerType();
            var dataCustomersource = await services.GetCustomerSource();
            var dataMarital = await services.GetMaritalStatus();
            var dataIndustryType = await services.GetIndustryType();
            var dataIdentityType = await services.GetIdentityType();
            var dataRelationship = await services.GetRelationship();
            var dataReferenceTypeName = await services.GetMsReferenceTypeName();
            var dataSource = await services.GetSource();
            var dataROReferencesSource = await services.GetMsROReferenceSource();
            var dataRODecision = await services.GetMsRODecision();
            var dataROCategory = await services.GetMsROCategory();
            var dataResidencestate = await services.GetResidence();
            var dataOwnershipProof = await services.GetOwnershipProof();
            var dataProfession = await services.GetProfession();
            var dataEvaluation = await services.GetCreditEvaluation();
            var dataROApplicantRelation = await services.GetMsROApplicantRelation();
            var dataNationality = await services.GetMsNationality();
            var dataMailtosource = await services.GetMailToSource();

            ViewBag.ReferencesTypeName = dataReferenceTypeName.data;
            ViewBag.OtherInstallments = dataMonthlyOtherInstallments.data;
            ViewBag.PaymentPlan = dataPaymentPlan.data;
            ViewBag.LocationPayment = dataLocationPayment.data;
            ViewBag.SelectOwnershipProof = new SelectList((List<MsOwnershipProofModels>)dataOwnershipProof.data, "ownership_proof_id", "ownership_proof_desc");
            ViewBag.SelectROApplicantRelation = new SelectList((List<MsROApplicantRelationModels>)dataROApplicantRelation.data, "RepeatOrderApplicantRelationId", "RepeatOrderApplicantRelationDesc");
            ViewBag.SelectEvaluations = new SelectList((List<MsEvaluationModels>)dataEvaluation.data, "evaluation_id", "evaluation_desc");
            ViewBag.SelectResidence = new SelectList((List<MsResidenceStatusModels>)dataResidencestate.data, "residence_status_id", "residence_status_desc");
            ViewBag.CustomerSource = new SelectList((List<MsCustomerSourceModels>)dataCustomersource.data, "customer_source_id", "customer_source_desc");
            ViewBag.CASSource = new SelectList((List<MsCreditSourceModels>)dataCreditSource.data, "credit_source_id", "credit_source_desc");
            ViewBag.CustomerType = new SelectList((List<MsCustomerTypeModels>)dataCustomerType.data, "customer_type", "customer_type_description");
            ViewBag.SelectPaymentPlan = new SelectList((List<MsPaymentPointModels>)dataPaymentPlan.data, "PaymentPointId", "PaymentPointDesc");
            ViewBag.SelectLocationPayment = new SelectList((List<MsPaymentPointModels>)dataLocationPayment.data, "PaymentPointId", "PaymentPointDesc");
            ViewBag.SelectMarital = new SelectList((List<MsMaritalModels>)dataMarital.data, "marital_id", "marital_desc");
            ViewBag.SelectIndustryType = new SelectList((List<MsIndustryTypeModels>)dataIndustryType.data, "industry_type_id", "industry_type_desc");
            ViewBag.SelectRelationship = new SelectList((List<MsRelationshipModels>)dataRelationship.data, "relationship_id", "relationship_desc");
            ViewBag.SelectIdentityType = new SelectList((List<MsIdentityTypeModels>)dataIdentityType.data, "IdentityTypeId", "IdentityTypeName");
            ViewBag.SelectDataSource = new SelectList((List<MsSourceModels>)dataSource.data, "source_id", "source_desc");
            ViewBag.SelectDataROReferencesSource = new SelectList((List<MsROReferenceSourceModels>)dataROReferencesSource.data, "RepeatOrderReferenceSourceId", "RepeatOrderReferenceSourceDesc");
            ViewBag.SelectDataRODecision = new SelectList((List<MsRODecisionModels>)dataRODecision.data, "RepeatOrderDecisionId", "RepeatOrderDecisionDesc");
            ViewBag.SelectDataROCategory = new SelectList((List<MsROCategoryModels>)dataROCategory.data, "RepeatOrderCategoryId", "RepeatOrderCategoryDesc");
            ViewBag.SelectProfession = new SelectList((List<MsProfessionModels>)dataProfession.data, "profession_id", "profession_desc");
            ViewBag.SelectNationality = new SelectList((List<MsNationalityModels>)dataNationality.data, "NationalityId", "NationalityDesc");
            ViewBag.SelectMailtosource = new SelectList((List<MsMailToSourceModels>)dataMailtosource.data, "MailToSourceId", "MailToSourceDesc");

            var dataROReasons = services.GetRepeatOrderReason().Result;
            ViewBag.dataROReasons = dataROReasons.data;

        }

        [HttpGet]
        public IActionResult GetGenderByNIK(string nik)
        {
            return Ok(Commons.GetGenderByNIK(nik));
        }

        /// <summary>
        /// Check credit id is already exist or not
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns>(boolean) 'true' for data is exist, 'false' for data not exist</returns>
        public bool IsCreditIdExist(string creditId)
        {
            var data = (List<TrCasModels>)services.GetTrCasByCreditId(creditId).Result.data;
            return data.Count == 0 ? false : true;
        }

        /// <summary>
        /// Remove Nullable on Data References
        /// </summary>
        /// <param name="casModels"></param>
        /// <returns></returns>
        public DataReferensiModels RemoveNullableReferences(TrCasModels casModels)
        {
            var data = new DataReferensiModels();
            var countFilled = casModels.DtReferensi.nama_referensi.Where(m => m != null).Count();
            var takeRefId = casModels.DtReferensi.referensi_id.Where(m => m != null).Take(countFilled);

            data.referensi_id = takeRefId.ToArray();
            data.alamat_referensi = casModels.DtReferensi.alamat_referensi.Where(m => m != null).ToArray();
            data.mobile_phone = casModels.DtReferensi.mobile_phone.Where(m => m != null).ToArray();
            data.nama_referensi = casModels.DtReferensi.nama_referensi.Where(m => m != null).ToArray();
            data.no_telepon = casModels.DtReferensi.no_telepon.Where(m => m != null).ToArray();
            data.no_telp_kantor = casModels.DtReferensi.no_telp_kantor.Where(m => m != null).ToArray();
            //data.hubungan_pemohon = casModels.DtReferensi.hubungan_pemohon.ToArray();
            data.hubungan_pemohon = casModels.DtReferensi.hubungan_pemohon.Where(m => m != null && m != 0).ToArray();
            data.lokasi_id_referensi = casModels.DtReferensi.lokasi_id_referensi.Where(m => m != null && m != 0).ToArray();

            return data;
        }

        /// <summary>
        /// Manipulation Data Count for Data References
        /// </summary>
        /// <param name="casModels">data source from TrCasModels</param>
        /// <returns></returns>
        public DataReferensiModels AddNewRowIntoDataReferences(TrCasModels casModels)
        {
            var newRow = new DataReferencesListModels();
            var newData = new DataReferensiModels();

            #region Convert Array To List data

            var newList = new DataReferencesListModels();
            for (int y = 0; y < casModels.DtReferensi.referensi_id.Length; y++)
            {
                newList.referensi_id.Add(casModels.DtReferensi.referensi_id[y]);
                newList.nama_referensi.Add(casModels.DtReferensi.nama_referensi[y]);
                newList.no_telepon.Add(casModels.DtReferensi.no_telepon[y]);
                newList.no_telp_kantor.Add(casModels.DtReferensi.no_telp_kantor[y]);
                newList.mobile_phone.Add(casModels.DtReferensi.mobile_phone[y]);
                newList.alamat_referensi.Add(casModels.DtReferensi.alamat_referensi[y]);
                newList.hubungan_pemohon.Add(casModels.DtReferensi.hubungan_pemohon[y]);
                newList.lokasi_id_referensi.Add(casModels.DtReferensi.lokasi_id_referensi[y]);
            }

            #endregion Convert Array To List data

            #region Add New Row base on Count data existing

            var dtLength = casModels.DtReferensi.referensi_id.Length;
            var spaceCount = 0;
            if (dtLength == 1)
            {
                spaceCount += 2;
            }
            else if (dtLength == 2)
            {
                spaceCount += 1;
            }
            var arrIds = GetIdDataReferences(casModels.DtReferensi);
            for (int i = 0; i < spaceCount; i++)
            {
                newRow.referensi_id.Add(arrIds[i]);
                newRow.nama_referensi.Add(null);
                newRow.no_telepon.Add(null);
                newRow.no_telp_kantor.Add(null);
                newRow.mobile_phone.Add(null);
                newRow.alamat_referensi.Add(null);
                newRow.hubungan_pemohon.Add(new());
                newRow.lokasi_id_referensi.Add(new());
            }

            #endregion Add New Row base on Count data existing

            #region Assign new Row into List References

            newList.referensi_id.AddRange(newRow.referensi_id);
            newList.nama_referensi.AddRange(newRow.nama_referensi);
            newList.no_telepon.AddRange(newRow.no_telepon);
            newList.no_telp_kantor.AddRange(newRow.no_telp_kantor);
            newList.mobile_phone.AddRange(newRow.mobile_phone);
            newList.alamat_referensi.AddRange(newRow.alamat_referensi);
            newList.hubungan_pemohon.AddRange(newRow.hubungan_pemohon);
            newList.lokasi_id_referensi.AddRange(newRow.lokasi_id_referensi);

            #endregion Assign new Row into List References

            #region Convert back to Array and Assing as new Data References

            newData.referensi_id = newList.referensi_id.ToArray();
            newData.nama_referensi = newList.nama_referensi.ToArray();
            newData.no_telepon = newList.no_telepon.ToArray();
            newData.no_telp_kantor = newList.no_telp_kantor.ToArray();
            newData.mobile_phone = newList.mobile_phone.ToArray();
            newData.alamat_referensi = newList.alamat_referensi.ToArray();
            newData.hubungan_pemohon = newList.hubungan_pemohon.ToArray();
            newData.lokasi_id_referensi = newList.lokasi_id_referensi.ToArray();

            #endregion Convert back to Array and Assing as new Data References

            return newData;
        }

        /// <summary>
        /// Find ID Reference on Original ID Collection with Except Existing Data References
        /// </summary>
        /// <param name="dataReferensi">existing data references</param>
        /// <returns></returns>
        public int[] GetIdDataReferences(DataReferensiModels dataReferensi)
        {
            var referencesId = ReferencesCollection.REFERENCE_ID; //original Id Collection

            var ids = referencesId.Except(dataReferensi.referensi_id); //Find id in original Id collection Except existing id data
            return ids.ToArray();
        }

        /// <summary>
        /// Check all field Data Reference is filled out
        /// </summary>
        /// <param name="dataReferensi"></param>
        /// <returns></returns>
        public bool IsFieldDataReferencesFilledOut(DataReferensiModels dataReferensi)
        {
            var countInput = 0;
            var maxKol = ViewBag.DataReferenceCount == null ? 2 : (int)ViewBag.DataReferenceCount;
            var isFilled = true;
            var checkList = new List<bool>();

            #region Find Max Array Input References

            var cas = new TrCasModels();
            cas.DtReferensi = dataReferensi;

            var collectionArrayCount = new List<int>();
            var dataWithoutNull = RemoveNullableReferences(cas);

            countInput = FindMaxLengthDataReferences(dataWithoutNull);

            #endregion Find Max Array Input References

            for (int i = 0; i < countInput; i++)
            {
                if (dataReferensi.nama_referensi[i] is null || dataReferensi.nama_referensi[i] == String.Empty ||
                    dataReferensi.no_telepon[i] is null || dataReferensi.no_telepon[i] == String.Empty ||
                    dataReferensi.no_telp_kantor[i] is null || dataReferensi.no_telp_kantor[i] == String.Empty ||
                    dataReferensi.alamat_referensi[i] is null || dataReferensi.alamat_referensi[i] == String.Empty ||
                    dataReferensi.mobile_phone[i] is null || dataReferensi.mobile_phone[i] == String.Empty ||
                    dataReferensi.hubungan_pemohon[i] == 0 || dataReferensi.mobile_phone[i] is null ||
                    dataReferensi.lokasi_id_referensi[i] == 0)
                {
                    checkList.Add(false);
                }
                else
                {
                    checkList.Add(true);
                }
            }
            var checkResult = checkList.ToList().Where(m => m.Equals(false)).ToList();
            isFilled = checkResult.Count >= 1 ? false : true;

            return isFilled;
        }

        /// <summary>
        /// Max Length by user input
        /// </summary>
        /// <param name="dataReferensi"></param>
        /// <returns></returns>
        public int FindMaxLengthDataReferences(DataReferensiModels dataReferensi)
        {
            var collectionArrayCount = new List<int>();

            collectionArrayCount.Add(dataReferensi.no_telepon.Length);
            collectionArrayCount.Add(dataReferensi.nama_referensi.Length);
            collectionArrayCount.Add(dataReferensi.no_telp_kantor.Length);
            collectionArrayCount.Add(dataReferensi.mobile_phone.Length);
            collectionArrayCount.Add(dataReferensi.alamat_referensi.Length);
            collectionArrayCount.Add(dataReferensi.lokasi_id_referensi.Length);

            return collectionArrayCount.Max();
        }

        #endregion Function Helper
    }
}