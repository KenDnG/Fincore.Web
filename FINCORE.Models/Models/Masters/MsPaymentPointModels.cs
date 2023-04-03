namespace FINCORE.Models.Models.Acquisition.Masters
{
    public class MsPaymentPointModels
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public int PaymentPointId { get; set; }
        public string PaymentPointDesc { get; set; }
        public int PaymentTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}