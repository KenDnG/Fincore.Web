namespace FINCORE.Models.Models.Acquisition.Masters
{
    public class MsOwnershipProofModels
    {
        public string created_by { get; set; }
        public DateTime? created_on { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
        public int ownership_proof_id { get; set; }
        public string ownership_proof_desc { get; set; }
        public bool is_active { get; set; }
    }
}