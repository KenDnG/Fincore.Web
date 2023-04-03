namespace FINCORE.Models.Models
{
    public class MsCreditSourceModels
    {
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string? last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
        public int credit_source_id { get; set; }
        public string credit_source_desc { get; set; }
        public bool is_active { get; set; }
    }
}