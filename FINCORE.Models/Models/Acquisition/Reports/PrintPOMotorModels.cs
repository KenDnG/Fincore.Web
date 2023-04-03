namespace FINCORE.Models.Models.Acquisition.Reports
{
    public class PrintPOMotorModels
    {
        public string po_no { get; set; }
        public string branch_id { get; set; }
        public string branch_name { get; set; }
        public string branch_address { get; set; }
        public string branch_province { get; set; }
        public string branch_phone { get; set; }
        public string dealer_fax { get; set; }
        public string item_desc { get; set; }
        public decimal price { get; set; }
        public decimal deposit { get; set; }
        public decimal nfa { get; set; }
        public string customer_name { get; set; }
        public string qq_name { get; set; }
        public string mail_address { get; set; }
        public DateTime print_date { get; set; }
        public string approved_by { get; set; }
        public int sum_of_print { get; set; }
        public decimal first_payment { get; set; }
        public decimal amount_installment { get; set; }
        public int tenor { get; set; }
        public int is_subsidi_dealer { get; set; }
        public string item_id { get; set; }
        public string asset_kind_id { get; set; }
        public string asset_kind_description { get; set; }
        public string asset_brand_id { get; set; }
        public string asset_brand_name { get; set; }
        public string asset_type_id { get; set; }
        public string asset_series_id { get; set; }
        public string asset_type_description { get; set; }
        public string year_item { get; set; }
        public string print_by { get; set; }
        public string company_id { get; set; }
        public string company_name { get; set; }
        public string dealer_code { get; set; }
        public string dealer_name { get; set; }
        public string dealer_address { get; set; }
        public string dealer_phone { get; set; }
    }
}