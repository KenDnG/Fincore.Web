using FINCORE.Models.Helpers;
using FINCORE.Models.Models.Acquisition.CAS.References;

namespace FINCORE.Models.Models.Acquisition.CAS
{
    public class TrCasModels
    {
        public string? created_by { get; set; }

        public DateTime? created_on { get; set; } = ModelHelpers.SetDefaultDate();
        public string? last_updated_by { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public DateTime? last_updated_on { get; set; } = ModelHelpers.SetDefaultDate();
        public string credit_id { get; set; }
        public string company_id { get; set; }
        public string outlet_code { get; set; }
        public string credit_source_id { get; set; }
        public string order_id { get; set; }
        public string branch_id { get; set; }
        public string customer_type { get; set; }
        public bool is_repeat_order { get; set; }
        public bool is_instant_approval { get; set; }
        public string repeat_order_reason { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string customer_name { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string birth_place { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public DateTime birth_date { get; set; } = ModelHelpers.SetDefaultDate();
        public string gender { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string? mother_name { get; set; } = "";
        public string email { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string identity_type_id { get; set; }
        public string? identity_number { get; set; }
        public DateTime? valid_thru { get; set; } = ModelHelpers.SetDefaultDate();
        public DateTime issue_date { get; set; } = ModelHelpers.SetDefaultDate();
        public string identity_address { get; set; }
        public int identity_location_id { get; set; }
        public bool is_blacklist { get; set; }
        public string customer_address { get; set; }
        public int customer_location_id { get; set; }
        public string npwp_no { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string telephone_number { get; set; }
        public string mobile_phone { get; set; }
        public int residence_distance { get; set; }
        public string customer_source_id { get; set; }
        public bool? is_surveyed { get; set; }
        public int sources_id { get; set; }
        public string sources_name { get; set; }
        public string sources_address { get; set; }
        public int evaluation_id { get; set; }
        public string residence_status_id { get; set; }
        public int ownership_proof { get; set; }
        public string ownership_proof_name { get; set; }
        public string residence_condition { get; set; }
        public int marital_status { get; set; }
        public int? mail_to_source_id { get; set; }
        public string mail_to_address { get; set; }
        public int mail_to_location_id { get; set; }
        public string? mail_to_telephone_number { get; set; }
        public string? credit_source_status { get; set; }
        public string analysis { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string conclusion { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public bool is_references { get; set; }
        public bool is_apuppt_filled { get; set; }
        public int is_apuppt { get; set; }
        public string company_name { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string company_birth_place { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public TrCasCorporateDocumentModels CorporateDocuments { get; set; }
        public TrCasFinancialModels Financials { get; set; }
        public TrCasRepeatOrderModels RepeatOrders { get; set; }
        public TrCasInstallmentModels Installments { get; set; }
        public TrCasPaymentPointModels PaymentPoints { get; set; }
        public DataPenjaminModels DtPenjamins { get; set; }
        public DataPasanganModels DtPasangans { get; set; }
        public DataReferensiModels DtReferensi { get; set; }
        public TrCasApupptModels Apuppt { get; set; }
    }
}