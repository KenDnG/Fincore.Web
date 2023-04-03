namespace FINCORE.Models.Models.Masters
{
    public class MsCustomerTypeModels
    {
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string? last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
        public string customer_type { get; set; }
        public string customer_type_description { get; set; }
        public bool is_active { get; set; }
    }
}