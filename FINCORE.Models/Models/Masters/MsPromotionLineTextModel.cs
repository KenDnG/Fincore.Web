namespace FINCORE.Models.Models.Masters
{
    public class MsPromotionLineTextModel
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public int LineTextId { get; set; }
        public string LineTextColor { get; set; }
        public string LineTextName { get; set; }
        public bool IsActive { get; set; }
    }
}