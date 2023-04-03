using FINCORE.Models.Models.Acquisition.CM;
using FINCORE.Models.Models.Acquisition.CM.Paginations;
using FINCORE.Models.Models.LDAP;
using FINCORE.Models.Models.Masters;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Services.Acquisition;
using FINCORE.WEB.Helpers;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using System.Collections.Specialized;
using System.Text;
using static FINCORE.Services.Helpers.Response.Collection;
using static FINCORE.WEB.Helpers.Commons;

namespace FINCORE.WEB.Controllers.Acquisition
{
    public class CMCarController : Controller
    {
        private CMCarServices services = new CMCarServices();

        public IActionResult CMCar(string trans_id)
        {
            HttpContext.Session.SetString("SuccessCM", "");

            ViewBag.State = TempData["Success"];
            ViewBag.Message = TempData["Message"];

            var branch_id = Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id");
            CompanyBranchResponse CompanyBranchMdl = Sessions.GetSessionFromJson<CompanyBranchResponse>(HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
            var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            var IsEdit = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsEdit");
            var IsView = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsView");
            var IsApprove = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsApprove");
            var Flag = HttpContext.Session.GetString("Flag");

            HttpContext.Session.SetString("CompanyId", CompanyBranchMdl.company_id);
            HttpContext.Session.SetString("BranchId", branch_id);

            if (IsEdit == "true")
            {
                HttpContext.Session.SetString("Flag", "Edit"); //Add,Edit,View,Approval
                Flag = "Edit";
            }
            else
            {
                if (Flag != "View" && Flag != "Approval")
                {
                    HttpContext.Session.SetString("Flag", "Add"); //Add,Edit,View,Approval
                    Flag = "Add";
                }
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

            CMCarModels CM = new CMCarModels();

            if (Flag != "Add")
            {
                var CMdata = services.Get_Tr_CM(credit_id).Result;
                ViewBag.CMCarModels = CMdata.data;
            }

            var ms_asset_kind = services.Get_ms_asset_kind("002").Result; //mobil
            ViewBag.ms_asset_kind = ms_asset_kind.data;

            var ms_application_type = services.Get_ms_application_type().Result;
            ViewBag.ms_application_type = ms_application_type.data;

            var ms_product_finance = services.Get_ms_product_finance().Result;
            ViewBag.ms_product_finance = ms_product_finance.data;

            var TACMax = services.Get_ms_general_parameter("BATASMAXTAC").Result;
            ViewBag.TACMax = TACMax.data;

            var ODRate = services.Get_ms_general_parameter("ODRate").Result;
            ViewBag.ODRate = ODRate.data;

            var ASURANSITACMAX = services.Get_ms_general_parameter("ASURANSITACMAX").Result;
            ViewBag.ASURANSITACMAX = ASURANSITACMAX.data;

            var BranchException = services.Get_BranchException(branch_id).Result;
            ViewBag.BranchException = BranchException.data;

            var Installment = services.Get_Installment("002").Result;
            ViewBag.Installment = Installment.data;

            var TipePerantara = services.Get_tipe_perantara().Result;
            ViewBag.TipePerantara = TipePerantara.data;

            var AccountOwner = services.Get_account_owner().Result;
            ViewBag.AccountOwner = AccountOwner.data;

            return View("~/Views/Acquisition/CM/CMCar.cshtml", CM);
        }

        #region Get Data

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
            var data = services.Get_ms_product_marketing(company_id, "002").Result;
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
        public IActionResult GetMarketPrice(string asset_kind_id, string CompanyId, string BranchId, string asset_type_id, string Year, string credit_id)
        {
            var data = services.Get_MarketPrice(asset_kind_id, CompanyId, BranchId, asset_type_id, Year, credit_id).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetFinancingPackage(string Tenor, string OTR, string ARType)
        {
            var data = services.Get_FinancingPackage(Tenor, OTR, ARType).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetProcessFee(string Tenor, string OTR, string InsCoverType, string BranchId)
        {
            var data = services.Get_ProcessFee(Tenor, OTR, InsCoverType, BranchId).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetLifeInsuranceCredit(string OTR, string DP, string AdminFeeKredit, string ProvisiFeeKredit, string BiayaProsesKredit, string BranchIdAsuransi, string Tenor)
        {
            var data = services.Get_LifeInsuranceCredit(OTR, DP, AdminFeeKredit, ProvisiFeeKredit, BiayaProsesKredit, BranchIdAsuransi, Tenor).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetAdminProvisiInterestFee(string PackageID, string Tenor, string OTR, string ARType)
        {
            var data = services.Get_AdminProvisiInterestFee(PackageID, Tenor, OTR, ARType).Result;
            return Ok(data.data);
        }

        [HttpGet]
        public IActionResult GetProcessProvisiIns(string BiayaProsesID, string Tenor, string OTR, string InsCoverType, string BranchId, string ItemYear, string credit_id, string modelid, string loss_fee, string loss_fee_kredit, string usage_type_id)
        {
            if (loss_fee_kredit == null || loss_fee_kredit == "")
            {
                loss_fee_kredit = "0";
            }
            var data = services.Get_ProcessProvisiIns(BiayaProsesID, Tenor, OTR, InsCoverType, BranchId, ItemYear, credit_id, modelid, loss_fee, loss_fee_kredit, usage_type_id).Result;
            return Ok(data.data);
        }

        #endregion Get Data

        #region Save

        [HttpPost]
        public IActionResult SaveCM(CMCarModels model)
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["Success"] = "";
            TempData["Message"] = "";

            ViewModelCMCar data = new ViewModelCMCar();

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
            //data.insurance_type_id = model.insurance_type_id;
            data.interest_rate_type_id = model.interest_rate_type_id;
            data.tenor = model.tenor;
            data.asset_cost = model.asset_cost;
            data.gross_down_payment = model.gross_down_payment;
            data.admin_fee = model.admin_fee;
            data.admin_fee_kredit = model.admin_fee_kredit;
            data.biaya_provisi = model.biaya_provisi;
            data.biaya_provisi_kredit = model.biaya_provisi_kredit;
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
            data.komper_max = "0";

            data.is_avalis = model.is_avalis;
            data.installment_type = model.installment_type;
            data.package_id = model.package_id;
            data.biaya_proses_id = model.biaya_proses_id;

            data.nominal_biaya_proses = model.nominal_biaya_proses;
            data.nominal_biaya_proses_kredit = model.nominal_biaya_proses_kredit;
            data.loss_fee = model.loss_fee;
            data.loss_fee_kredit = model.loss_fee_kredit;
            data.provisi_ins_fee = model.provisi_ins_fee;
            data.provisi_ins_fee_kredit = model.provisi_ins_fee_kredit;
            data.customer_pay_amount = model.customer_pay_amount;

            data.installment_code = model.installment_code;
            data.residual_value = model.residual_value;

            data.discount_dealer = model.discount_dealer;

            data.ongkos_BBN = model.ongkos_BBN;
            data.ongkos_tagih = model.ongkos_tagih;

            data.SubsidiFinance = model.SubsidiFinance;
            data.SubsidiDealer = model.SubsidiDealer;
            data.SubsidiMainDealer = model.SubsidiMainDealer;
            data.SubsidiATPM = model.SubsidiATPM;
            data.SubsidiPihakKetiga = model.SubsidiPihakKetiga;
            data.SubsidiBunga = model.SubsidiBunga;

            data.mega_insurance_fee = model.mega_insurance_fee;
            data.mega_insurance_fee_kredit = model.mega_insurance_fee_kredit;
            data.handphone_fee = model.handphone_fee;
            data.handphone_fee_kredit = model.handphone_fee_kredit;

            data.branch_id = model.branch_id;
            data.is_life_ins = model.is_life_ins;
            data.company_id = model.company_id;

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

            var save = services.InsertCM(data);

            if (save.Result.status == Collection.Status.SUCCESS)
            {
                TempData["Success"] = Collection.Status.SUCCESS;
                ViewBag.State = Collection.Status.SUCCESS;

                HttpContext.Session.SetString("SuccessCM", Collection.Status.SUCCESS);

                return RedirectToAction("IndexMobil", "Acquisition");
            }
            else
            {
                TempData["Success"] = Collection.Status.FAILED;
                TempData["Message"] = save.Result.message;

                ViewBag.State = Collection.Status.FAILED;
                ViewBag.Message = save.Result.message;

                return RedirectToAction("CMCar", "CMCar");
            }
        }

        [HttpPost]
        public IActionResult BackToCASCar(CMCarModels model)
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["Success"] = "";
            TempData["Message"] = "";

            var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            var IsEdit = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsEdit");
            var Flag = HttpContext.Session.GetString("Flag");

            //NameValueCollection param_credit = new NameValueCollection();

            if (Flag == "Add" || Flag == "Edit" || Flag == "Correction")
            {
                ViewModelCMCar data = new ViewModelCMCar();

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
                //data.insurance_type_id = model.insurance_type_id;
                data.interest_rate_type_id = model.interest_rate_type_id;
                data.tenor = model.tenor;
                data.asset_cost = model.asset_cost;
                data.gross_down_payment = model.gross_down_payment;
                data.admin_fee = model.admin_fee;
                data.admin_fee_kredit = model.admin_fee_kredit;
                data.biaya_provisi = model.biaya_provisi;
                data.biaya_provisi_kredit = model.biaya_provisi_kredit;
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
                data.komper_max = "0";

                data.is_avalis = model.is_avalis;
                data.installment_type = model.installment_type;
                data.package_id = model.package_id;
                data.biaya_proses_id = model.biaya_proses_id;

                data.nominal_biaya_proses = model.nominal_biaya_proses;
                data.nominal_biaya_proses_kredit = model.nominal_biaya_proses_kredit;
                data.loss_fee = model.loss_fee;
                data.loss_fee_kredit = model.loss_fee_kredit;
                data.provisi_ins_fee = model.provisi_ins_fee;
                data.provisi_ins_fee_kredit = model.provisi_ins_fee_kredit;
                data.customer_pay_amount = model.customer_pay_amount;

                data.installment_code = model.installment_code;
                data.residual_value = model.residual_value;

                data.discount_dealer = model.discount_dealer;

                data.ongkos_BBN = model.ongkos_BBN;
                data.ongkos_tagih = model.ongkos_tagih;

                data.SubsidiFinance = model.SubsidiFinance;
                data.SubsidiDealer = model.SubsidiDealer;
                data.SubsidiMainDealer = model.SubsidiMainDealer;
                data.SubsidiATPM = model.SubsidiATPM;
                data.SubsidiPihakKetiga = model.SubsidiPihakKetiga;
                data.SubsidiBunga = model.SubsidiBunga;

                data.mega_insurance_fee = model.mega_insurance_fee;
                data.mega_insurance_fee_kredit = model.mega_insurance_fee_kredit;
                data.handphone_fee = model.handphone_fee;
                data.handphone_fee_kredit = model.handphone_fee_kredit;

                data.branch_id = model.branch_id;
                data.is_life_ins = model.is_life_ins;
                data.company_id = model.company_id;

                var save = services.InsertCM(data);

                if (save.Result.status == Collection.Status.SUCCESS)
                {
                    HttpContext.Session.SetString("item", "002");
                    return RedirectToAction("GetTrCasByCreditId_2", "CAS");
                }
                else
                {
                    TempData["Success"] = Collection.Status.FAILED;
                    TempData["Message"] = save.Result.message;

                    ViewBag.State = Collection.Status.FAILED;
                    ViewBag.Message = save.Result.message;

                    return RedirectToAction("CMCar", "CMCar");
                }
            }

            if (Flag == "Add" || Flag == "Edit" || Flag == "Correction")
            {
                HttpContext.Session.SetString("item", "002");
                return RedirectToAction("GetTrCasByCreditId_2", "CAS");
            }
            else
            {
                var IsApprove = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsApprove");
                if (IsApprove == "true")
                {
                    return RedirectToAction("ApproveTrCas", "CAS", new { creditId = credit_id, item = "002" });
                }
                else
                {
                    return RedirectToAction("ViewTrCas", "CAS", new { creditId = credit_id, item = "003" });
                }
            }
        }

        [HttpPost]
        public IActionResult NextToCACar(CMCarModels model)
        {
            HttpContext.Session.SetString("SuccessCM", "");
            TempData["Success"] = "";
            TempData["Message"] = "";

            var credit_id = HttpContext.Session.GetString(SessionIdentity.CREDIT_ID);
            var IsEdit = Sessions.GetSessionFromJson<string>(HttpContext.Session, "IsEdit");
            var Flag = HttpContext.Session.GetString("Flag");

            if (Flag == "Add" || Flag == "Edit" || Flag == "Correction")
            {
                ViewModelCMCar data = new ViewModelCMCar();

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
                //data.insurance_type_id = model.insurance_type_id;
                data.interest_rate_type_id = model.interest_rate_type_id;
                data.tenor = model.tenor;
                data.asset_cost = model.asset_cost;
                data.gross_down_payment = model.gross_down_payment;
                data.admin_fee = model.admin_fee;
                data.admin_fee_kredit = model.admin_fee_kredit;
                data.biaya_provisi = model.biaya_provisi;
                data.biaya_provisi_kredit = model.biaya_provisi_kredit;
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
                data.komper_max = "0";

                data.is_avalis = model.is_avalis;
                data.installment_type = model.installment_type;
                data.package_id = model.package_id;
                data.biaya_proses_id = model.biaya_proses_id;

                data.nominal_biaya_proses = model.nominal_biaya_proses;
                data.nominal_biaya_proses_kredit = model.nominal_biaya_proses_kredit;
                data.loss_fee = model.loss_fee;
                data.loss_fee_kredit = model.loss_fee_kredit;
                data.provisi_ins_fee = model.provisi_ins_fee;
                data.provisi_ins_fee_kredit = model.provisi_ins_fee_kredit;
                data.customer_pay_amount = model.customer_pay_amount;

                data.installment_code = model.installment_code;
                data.residual_value = model.residual_value;

                data.discount_dealer = model.discount_dealer;

                data.ongkos_BBN = model.ongkos_BBN;
                data.ongkos_tagih = model.ongkos_tagih;

                data.SubsidiFinance = model.SubsidiFinance;
                data.SubsidiDealer = model.SubsidiDealer;
                data.SubsidiMainDealer = model.SubsidiMainDealer;
                data.SubsidiATPM = model.SubsidiATPM;
                data.SubsidiPihakKetiga = model.SubsidiPihakKetiga;
                data.SubsidiBunga = model.SubsidiBunga;

                data.mega_insurance_fee = model.mega_insurance_fee;
                data.mega_insurance_fee_kredit = model.mega_insurance_fee_kredit;
                data.handphone_fee = model.handphone_fee;
                data.handphone_fee_kredit = model.handphone_fee_kredit;

                data.branch_id = model.branch_id;
                data.is_life_ins = model.is_life_ins;
                data.company_id = model.company_id;

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

                var save = services.InsertCM(data);

                if (save.Result.status == Collection.Status.SUCCESS)
                {
                    return RedirectToAction("CACar", "CACar");
                }
                else
                {
                    TempData["Success"] = Collection.Status.FAILED;
                    TempData["Message"] = save.Result.message;

                    ViewBag.State = Collection.Status.FAILED;
                    ViewBag.Message = save.Result.message;

                    return RedirectToAction("CMCar", "CMCar");
                }
            }

            if (Flag == "Add" || Flag == "Edit" || Flag == "Correction")
            {
                return RedirectToAction("CACar", "CACar");
            }
            else
            {
                return RedirectToAction("CACar", "CACar", new { trans_id = credit_id });
            }
        }

        #endregion Save

        #region Get Lookup Data

        [HttpPost]
        public IActionResult PaginationItem(int pageIndex, string searchTerm, string item_id, string item_brand_id, string asset_kind_class_id)
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
                page = 1;
                var data = services.GetPaginationItem(searchTerm, item_id, item_brand_id, asset_kind_class_id, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationItemModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/ItemView.cshtml", dataPaging);
        }

        [HttpPost]
        public IActionResult PaginationDealer(int pageIndex, string searchTerm, string item_id, string is_item_new, string item_merk)
        {
            var dataPaging = new PaginationModels<PaginationDealerModels>();
            var page = pageIndex == 0 ? 1 : pageIndex;

            if (searchTerm == null)
            {
                searchTerm = Request.Query["searchTerm"].ToString();
            }
            if (searchTerm == null || searchTerm == string.Empty)
            {
                var data = services.GetPaginationDealer(string.Empty, item_id, is_item_new, item_merk, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationDealerModels>)data.Result.data;
            }
            else
            {
                page = 1;
                var data = services.GetPaginationDealer(searchTerm, item_id, is_item_new, item_merk, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationDealerModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/DealerView.cshtml", dataPaging);
        }

        [HttpPost]
        public IActionResult PaginationSurveyor(int pageIndex, string searchTerm, string item_id)
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
                page = 1;
                var data = services.GetPaginationSurveyor(searchTerm, item_id, Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id"), page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationSurveyorModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/SurveyorView.cshtml", dataPaging);
        }

        [HttpPost]
        public IActionResult PaginationMarketingHead(int pageIndex, string searchTerm, string item_id)
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
                page = 1;
                var data = services.GetPaginationMarketingHead(searchTerm, item_id, Sessions.GetSessionFromJson<string>(HttpContext.Session, "branch_id"), page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationMarketingHeadModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/MarketingHeadView.cshtml", dataPaging);
        }

        [HttpPost]
        public IActionResult PaginationCGSNo(int pageIndex, string searchTerm, string BranchId, string CompanyId)
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
                page = 1;
                var data = services.GetPaginationCGSNo(searchTerm, BranchId, CompanyId, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationCGSNoModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/CGSNoView.cshtml", dataPaging);
        }

        [HttpPost]
        public IActionResult PaginationPerantara(int pageIndex, string searchTerm, string BranchId, string CompanyId, string TipePerantara)
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
                page = 1;
                var data = services.GetPaginationPerantara(searchTerm, BranchId, CompanyId, TipePerantara, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationPerantaraModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/PerantaraView.cshtml", dataPaging);
        }

        [HttpPost]
        public IActionResult PaginationBankName(int pageIndex, string searchTerm, string BranchId, string CompanyId, string PemilikRekening)
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
                page = 1;
                var data = services.GetPaginationBankName(searchTerm, BranchId, CompanyId, PemilikRekening, page, Paginations.MaxPerPageLookup);
                dataPaging = (PaginationModels<PaginationBankNameModels>)data.Result.data;
            }
            ViewBag.CurrentPage = page;
            return PartialView($"~/Views/Acquisition/CM/Lookup/BankNameView.cshtml", dataPaging);
        }

        #endregion Get Lookup Data

        private static String PreparePOSTForm(string action, string controller, NameValueCollection data)

        {
            //Set a name for the form
            string formID = "Postform";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" asp-action=\"" + action + "\" asp-controller=\"" + controller + "\" method=\"POST\">");

            foreach (string key in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key +
                               "\" value=\"" + data[key] + "\">");
            }

            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }
    }
}