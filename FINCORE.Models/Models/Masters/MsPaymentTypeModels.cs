namespace FINCORE.Models.Models.Acquisition.Masters
{
    public class MsPaymentTypeModels
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<MsPaymentPointModels> PaymentPoints { get; set; }
    }
}