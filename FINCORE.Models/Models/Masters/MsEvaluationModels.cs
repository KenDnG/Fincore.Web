namespace FINCORE.Models.Models.Masters
{
    public class MsEvaluationModels
    {
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string? last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
        public int evaluation_id { get; set; }
        public string evaluation_desc { get; set; }
        public bool is_active { get; set; }
    }
}