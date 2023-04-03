namespace FINCORE.Models.Models.Acquisition.Masters
{
    public class MsROCategoryModels
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string RepeatOrderCategoryId { get; set; }
        public string RepeatOrderCategoryDesc { get; set; }
        public string FlagItem { get; set; }
        public bool IsActive { get; set; }
    }
}