namespace FINCORE.Models.Models.Acquisition.Masters
{
    public class MsProfessionModels
    {
        public string created_by { get; set; }
        public DateTime? created_on { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
        public string profession_id { get; set; }
        public string profession_desc { get; set; }
        public bool is_active { get; set; }
    }
}