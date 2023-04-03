namespace FINCORE.Models.Models.Acquisition.Masters
{
    public class MsSourceModels
    {
        public string created_by { get; set; }
        public DateTime? created_on { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
        public int source_id { get; set; }
        public string source_desc { get; set; }
        public bool is_active { get; set; }
    }
}