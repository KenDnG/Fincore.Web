using System.ComponentModel.DataAnnotations;

namespace FINCORE.Models.Models.Acquisition.BPKB
{
    public class BPKBPinjamModels
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string? CreditId { get; set; }
        public string? CompanyId { get; set; }
        public string? BranchId { get; set; }
        public int? TotalOfLoan { get; set; }
        public DateTime? BpkbLoanDate { get; set; }
        public DateTime? BpkbPlanDate { get; set; }

        [Required]
        public string? LoanReason { get; set; }

        [Required]
        public string? LoanReceiverCode { get; set; }

        public string? PreviousLocationCode { get; set; }
        public string? PreviousSequenceNo { get; set; }
        public DateTime? BpkbReturn { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Address { get; set; }

        [Required]
        public string? ReceiverName { get; set; }

        [Required]
        public string? ReceiverPhone { get; set; }

        [Required]
        public decimal? MoneyReceived { get; set; }

        public string? TypeOfServiceBureauNecessity { get; set; }
        public string? ReceiverCode { get; set; }
        public int SequenceNo { get; set; }
    }
}