namespace FINCORE.Models.Models.Acquisition.BPKB
{
    public class BPKBReasonModel
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string ReasonId { get; set; }
        public string ReasonName { get; set; }
        public bool IsActive { get; set; }
    }
}