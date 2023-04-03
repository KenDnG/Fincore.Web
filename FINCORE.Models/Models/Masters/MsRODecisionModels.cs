namespace FINCORE.Models.Models.Acquisition.Masters
{
    public class MsRODecisionModels
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string RepeatOrderDecisionId { get; set; }
        public string RepeatOrderDecisionDesc { get; set; }
        public bool is_active { get; set; }
    }
}