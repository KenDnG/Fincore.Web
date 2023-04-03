using FINCORE.Models.Helpers;

namespace FINCORE.Models.Models.Acquisition.CAS
{
    public class TrCasRepeatOrderModels
    {
        public string created_by { get; set; } = string.Empty;
        public DateTime? created_on { get; set; } = ModelHelpers.SetDefaultDate();
        public string last_updated_by { get; set; } = string.Empty;
        public DateTime? last_updated_on { get; set; } = ModelHelpers.SetDefaultDate();
        public string credit_id { get; set; } = string.Empty;
        public string agreement_number_old { get; set; } = string.Empty;
        public string repeat_order_description { get; set; } = string.Empty;
        public string? repeat_order_category_id { get; set; } = string.Empty;
        public string repeat_order_decision_id { get; set; } = string.Empty;
        public string repeat_order_reference_source_id { get; set; } = string.Empty;
        public string repeat_order_applicant_relation_id { get; set; } = string.Empty;
        public string bank_id { get; set; } = string.Empty;
        public string account_name { get; set; } = string.Empty;
        public string account_number { get; set; } = string.Empty;
        public string telephone_number { get; set; } = string.Empty;
        public string reference_source_desc { get; set; } = string.Empty;
        public string reference_source_desc_sr1 { get; set; } = string.Empty;
        public string reference_source_desc_sr4 { get; set; } = string.Empty;
    }
}