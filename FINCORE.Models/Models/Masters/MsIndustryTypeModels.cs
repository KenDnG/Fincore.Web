namespace FINCORE.Models.Models.Masters
{
    public class MsIndustryTypeModels
    {
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string? last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
        public int industry_type_id { get; set; }
        public string industry_type_desc { get; set; }
        public bool is_active { get; set; }
    }
}