using System.ComponentModel.DataAnnotations;

namespace FINCORE.Models.Models.Acquisition.BPKB
{
    public class BPKBModels
    {
        //[JsonPropertyName("test")]//customize name
        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Jumlah karakter No.BPKB harus kurang dari sama dengan 10!")]
        [RegularExpression(@"^(?![a-zA-Z]+$)(?!\d+$)([a-zA-Z]|\d)+$", ErrorMessage = "Format BPKB No Salah!")]
        public string? BpkbNo { get; set; }

        public string? CompanyId { get; set; }
        public string? BranchId { get; set; }

        [Required]
        public string? CreditId { get; set; }

        [Required]
        [RegularExpression(@"([a-zA-Z]){1,2}(\d){1,4}([a-zA-Z]){1,3}", ErrorMessage = "Format No Polisi salah!")]
        public string? CarNo { get; set; }

        public string? InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? BpkbIn { get; set; }
        public string? SecquenceNo { get; set; }
        public DateTime? BpkbOut { get; set; }

        [Required]
        public string? OutCode { get; set; }

        [Required]
        public string? ReceiverCode { get; set; }

        public string? ApprovedBy { get; set; }
        public DateTime? BastDate { get; set; }
        public string? BastBy { get; set; }

        [Required]
        public string? LocationCode { get; set; }

        public string? BpkbStatus { get; set; }
        public bool? IsClose { get; set; }

        [Required]
        public DateTime? BpkbDate { get; set; }

        public bool? IsPrintSk { get; set; }
        public int? SumPrintSk { get; set; }
        public string? PrintedFirstBy { get; set; }
        public DateTime? PrintedFirstDate { get; set; }
        public string? PrintedLastBy { get; set; }
        public DateTime? PrintedLastDate { get; set; }
        public bool? IsMb { get; set; }
        public bool? IsPrintThirdParty { get; set; }
        public int? SumPrintThirdParty { get; set; }
        public string? PrintFirstByThirdParty { get; set; }
        public DateTime? PrintFirstDateThirdParty { get; set; }
        public string? PrintLastByThirdParty { get; set; }
        public DateTime? PrintLastDateThirdParty { get; set; }
        public string? BpkbDifferenceDesc { get; set; }
        public DateTime? DeadlineDate { get; set; }

        [Required]
        public string? ReEntryLocationCode { get; set; }
    }
}