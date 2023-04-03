namespace FINCORE.Models.Models.Acquisition
{
    public class TrCreditTypeModels
    {
        public string created_by { get; set; }
        public DateTime? created_on { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
        public string credit_type_id { get; set; }
        public int id_type { get; set; }
        public string credit_id { get; set; }
        public DateTime valid_thru { get; set; }
        public DateTime issue_date { get; set; }
        public string id_no { get; set; }
        public string id_address { get; set; }
        public int? location_id { get; set; }
        public bool is_active { get; set; }
    }
}