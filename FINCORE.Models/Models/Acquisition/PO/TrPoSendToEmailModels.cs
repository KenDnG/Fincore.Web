namespace FINCORE.Models.Models.Acquisition.PO
{
    public class TrPoSendToEmailModels
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public int SendEmailId { get; set; }
        public string PoNo { get; set; }
        public string BranchId { get; set; }
        public string DealerCode { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public bool? IsSend { get; set; }
    }
}