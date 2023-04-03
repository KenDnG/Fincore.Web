namespace FINCORE.Models.Models.Acquisition.NPP
{
    public class TrNppRequest
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string CreditId { get; set; }
        public DateTime? AgreementDate { get; set; }
        public string? CompanyId { get; set; }
        public string? BranchId { get; set; }
        public DateTime? BillReceivedDate { get; set; }
        public string? strBillReceivedDate { get; set; }
        public DateTime? BillReceiptDate { get; set; }
        public string? strBillReceiptDate { get; set; }
        public string? DownPaymentReceiptNo { get; set; }
        public DateTime? DownPaymentReceiptDate { get; set; }
        public string? strDownPaymentReceiptDate { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public string? strReceiptDate { get; set; }
        public string? BpkbLetterNo { get; set; }
        public DateTime? BpkbLetterDate { get; set; }
        public string? strBpkbLetterDate { get; set; }
        public string? BastNo { get; set; }
        public DateTime? BastDate { get; set; }
        public string? strBastDate { get; set; }
        public DateTime? InstallmentDate { get; set; }
        public string? strInstallmentDate { get; set; }
        public int BankReferenceId { get; set; }
        public string? AgreementStatus { get; set; }
        public bool DealerPaymentStatus { get; set; } = false;
        public bool ArScheduleStatus { get; set; } = false;
        public DateTime? ConsumenInstallmentDate { get; set; }
        public string? strConsumenInstallmentDate { get; set; }
        public decimal? NfaPercent { get; set; }
        public int? AccountId { get; set; }
        public bool IsPlafon { get; set; } = false;
        public decimal? AgreementValue { get; set; }
        public decimal? EstimatedInsuranceCost { get; set; }
        public string? InsuranceCoveragePeriod { get; set; }
        public decimal? OtherApValue { get; set; }
        public string? InsuranceBillingPeriodical { get; set; }
        public int BankReferenceSubId { get; set; }
        public string? DisbursalTypeUmc { get; set; }
        public decimal? TokenNominal { get; set; }
        public string? DisbursalTypeUmcIncentive { get; set; }
        public decimal? IncentiveNominal { get; set; }
        public string? InsuranceId { get; set; }

        //CM
        public string? ApplicationTypeId { get; set; }

        //Approval Reason
        public string? ReasonId { get; set; }

        public string? ReasonDesc { get; set; }
        public string? StatusApproval { get; set; }

        //Data Rapindo
        public string? UserName { get; set; }

        public string? BranchName { get; set; }

        public TrItemRequest TrItems { get; set; }

        public ICollection<TrDealerOrderSourceTacRequest> TrDealerOrderSourceTac { get; set; }
        public ICollection<TrDealerOrderSourceThirdPartyRequest> TrDealerOrderSourceThirdParty { get; set; }
    }
}