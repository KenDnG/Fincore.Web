namespace FINCORE.Models.Models.Acquisition.CM
{
    public class CMModels
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string? credit_id { get; set; }

        public string? is_item_new { get; set; }

        public string? application_type_id { get; set; }
        public string? application_type_name { get; set; }

        public string? product_finance_id { get; set; }
        public string? product_finance_name { get; set; }

        public string? item_brand_id { get; set; }
        public string? item_brand { get; set; }

        public string? asset_kind_id { get; set; }
        public string? asset_kind_class_id { get; set; }
        public string? asset_kind_class_name { get; set; }

        public string? year { get; set; }

        public string? product_id { get; set; }
        public string? product_name { get; set; }

        public string? product_marketing_id { get; set; }
        public string? product_marketing_name { get; set; }

        public string? STNK_name_id { get; set; }
        public string? STNK_name_description { get; set; }

        public string? provenance_PO_id { get; set; }
        public string? provenance_PO_description { get; set; }

        public string? usage_type_id { get; set; }
        public string? usage_type_name { get; set; }

        public string? item_brand_type_id { get; set; }
        public string? item_type_name { get; set; }

        public string? dealer_code { get; set; }
        public string? dealer_name { get; set; }

        public string? surveyor_id { get; set; }
        public string? surveyor_name { get; set; }

        public string? marketinghead_id { get; set; }
        public string? marketinghead_name { get; set; }

        public string? CC { get; set; }

        public string? ar { get; set; }
        public string? tipe_cover { get; set; }
        public string? insurance_type_id { get; set; }
        public string? interest_rate_type_id { get; set; }

        public string? tenor { get; set; }
        public string? asset_cost { get; set; }
        public string? gross_down_payment { get; set; }
        public string? admin_fee { get; set; }
        public string? biaya_provisi { get; set; }
        public string? insurance_fee { get; set; }
        public string? uang_muka_murni_kons { get; set; }
        public string? jml_pembiayaan { get; set; }
        public string? amount_installment { get; set; }
        public string? effective_rate { get; set; }
        public string? flat_rate { get; set; }
        public string? disc_deposit { get; set; }
        public string? overdue_rate { get; set; }

        public string? CGSCabangNo { get; set; }
        public string? disc_type { get; set; }

        public string? TAC_max { get; set; }
        public string? komper_max { get; set; }
        public string? market_price { get; set; }
        public string? max_plafon_pencairan { get; set; }

        public string? branch_id { get; set; }
        public string? company_id { get; set; }

        public string? deposit_installment { get; set; }
        public string? is_topup_ms { get; set; }
        public string? old_npp { get; set; }
        public string? skema_id { get; set; }
        public string? perantara_type_id { get; set; }
        public string? agent_id { get; set; }
        public string? agent_name { get; set; }
        public string? ownership_account_type_id { get; set; }
        public string? bank_account_id_umc { get; set; }
        public string? bank_id_umc { get; set; }
        public string? bank_name_umc { get; set; }
        public string? account_no_umc { get; set; }
        public string? account_name_umc { get; set; }
        public string? installment_id { get; set; } //add by fajero 22/12/2022

        public string? reason { get; set; }
        public string? reason_id { get; set; }

        public string? StatusApproval { get; set; }
        public string? IsApprover { get; set; }
        public string? IsLastApprover { get; set; }
        public string? IsCreditAnalyst { get; set; }

        public string? analysis { get; set; }
        public string? conclusion { get; set; }

        public string? is_package_product { get; set; }
        public string? package_product_amount { get; set; }

        public string? NPWP_no { get; set; }

        public string? chasis_no { get; set; }
        public string? machine_no { get; set; }
        public string? prefix_plat { get; set; }
        public string? plat_no { get; set; }
        public string? plat_code { get; set; }

        public string? chasis_code { get; set; }

        public string? FPK1 { get; set; }
        public string? FPK2 { get; set; }

        public string customer_name { get; set; }
        public string customer_reference_1 { get; set; }

        public string? Result { get; set; }
        public string? ResultMessage { get; set; }

        public string? bank_id { get; set; }
        public string? bank_name { get; set; }
        public string? destination_bank_id_umc { get; set; }
        public string? destination_bank_account_id_umc { get; set; }
        public string? destination_account_no_umc { get; set; }
        public string? destination_account_name_umc { get; set; }

        public string? STNK_status_id { get; set; }
        public string? BPKB_invoice { get; set; }

    }
}