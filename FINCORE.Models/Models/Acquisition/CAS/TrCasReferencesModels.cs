namespace FINCORE.Models.Models.Acquisition.CAS
{
    public class TrCasReferencesModels
    {
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }
        public string credit_id { get; set; }
        public int reference_id { get; set; }
        public string references_name { get; set; }
        public string references_identity_type_id { get; set; }
        public string references_identity_number { get; set; }
        public string references_birth_place { get; set; }
        public DateTime references_birth_date { get; set; }
        public string references_address { get; set; }
        public string references_telephone_number { get; set; }
        public string references_occupation { get; set; }
        public decimal references_income { get; set; }
        public decimal references_other_income { get; set; }
        public string references_other_income_desc { get; set; }
        public int references_location_id { get; set; }
        public string references_fax { get; set; }
        public int references_relationship { get; set; }
        public string references_office_phone_number { get; set; }
        public string references_mobile_number { get; set; }
    }
}