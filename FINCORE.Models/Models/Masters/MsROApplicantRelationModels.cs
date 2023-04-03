namespace FINCORE.Models.Models.Acquisition.Masters
{
    public class MsROApplicantRelationModels
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string RepeatOrderApplicantRelationId { get; set; }
        public string RepeatOrderApplicantRelationDesc { get; set; }
        public bool IsActive { get; set; }
    }
}