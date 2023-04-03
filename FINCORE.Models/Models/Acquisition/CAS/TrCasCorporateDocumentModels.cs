using FINCORE.Models.Helpers;

namespace FINCORE.Models.Models.Acquisition.CAS
{
    public class TrCasCorporateDocumentModels
    {
        public string created_by { get; set; }
        public DateTime? created_on { get; set; } = DateTime.Now;
        public string last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
        public string credit_id { get; set; }
        public string corporate_commisioner_name { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string corporate_director_name { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string corporate_status { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public int corporate_industry_id { get; set; }
        public string corporate_other_industry { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public byte? corporate_year_period { get; set; }
        public byte? corporate_month_period { get; set; }
        public decimal? corporate_number_of_employee { get; set; }
        public string corporate_debitur_type { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string corporate_telephone_number { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string corporate_fax_number { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string corporate_site { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string corporate_email { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string founders_deed_number { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public DateTime founders_deed_date { get; set; } = ModelHelpers.SetDefaultDate();
        public string founders_notary_name { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string management_deed_number { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public DateTime management_deed_date { get; set; } = ModelHelpers.SetDefaultDate();
        public string management_notary_name { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string director_identity_number { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string commissioner_identity_number { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string corporate_tax_idNumber { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string director_tax_id_number { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string siup_id { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public DateTime siup_issue_date { get; set; } = ModelHelpers.SetDefaultDate();
        public DateTime siup_due_date { get; set; } = ModelHelpers.SetDefaultDate();
        public string tdp_id { get; set; }
        public DateTime tdp_issue_date { get; set; } = ModelHelpers.SetDefaultDate();
        public DateTime tdp_due_date { get; set; } = ModelHelpers.SetDefaultDate();
        public bool is_completeness_letter { get; set; }
        public DateTime completeness_letter_issue_date { get; set; } = ModelHelpers.SetDefaultDate();
        public DateTime completeness_letter_due_date { get; set; } = ModelHelpers.SetDefaultDate();
    }
}