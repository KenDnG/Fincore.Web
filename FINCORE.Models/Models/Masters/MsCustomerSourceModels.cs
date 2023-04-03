namespace FINCORE.Models.Models.Masters
{
    public class MsCustomerSourceModels
    {
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string? last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
        public string customer_source_id { get; set; }
        public string customer_source_desc { get; set; }
        public bool is_active { get; set; }
    }
}