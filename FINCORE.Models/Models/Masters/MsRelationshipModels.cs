namespace FINCORE.Models.Models.Acquisition.Masters
{
    public class MsRelationshipModels
    {
        public string created_by { get; set; }
        public DateTime? created_on { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
        public int relationship_id { get; set; }
        public string relationship_desc { get; set; }
        public string customer_type { get; set; }
        public bool is_active { get; set; }
    }
}