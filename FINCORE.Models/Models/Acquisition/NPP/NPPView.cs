namespace FINCORE.Models.Models.Acquisition.NPP
{
    public class NPPView
    {
		public string CreditId { get; set; }
        public string AgreementOld { get; set; }
        public string AgreementDay { get; set; }
		public string AgreementDate { get; set; }
		public string CreditDate { get; set; }
		public string BranchId { get; set; }
		public bool? IsTopupMS { get; set; }
		public string ItemId { get; set; }
		public string ApplicationTypeId { get; set; }
		public string PONumber { get; set; }
		public string CustomerName { get; set; }
		public string DealerCode { get; set; }
		public string DealerName { get; set; }
		public string DealerAddress { get; set; }
		public string ChasisNo { get; set; }
		public string MachineNo { get; set; }
		public string ItemColor { get; set; }
		public string BillReceivedDate { get; set; }
		public string BillReceiptDate { get; set; }
		public string DownPaymentReceiptNo { get; set; }
		public string DownPaymentReceiptDate { get; set; }
		public string ReceiptNo { get; set; }
		public string ReceiptDate { get; set; }
		public string BPKBLetterNo { get; set; }
		public string BPKBLetterDate { get; set; }
		public string BASTNo { get; set; }
		public string BASTDate { get; set; }
		public string InstallmentDate { get; set; }
		public string BankAccountNo { get; set; }
		public string BankAccountName { get; set; }
		public string BankName { get; set; }
		public string BankAccountDescription { get; set; }
		public string AssetCost { get; set; }
		public string TACMax { get; set; }
		public string InsuranceAmount { get; set; }
		public string PaymentAmount { get; set; }
		public string TotalAmountTAC { get; set; }
		public string TotalAmountTACAfterTax { get; set; }
		public string TotalAmountTP { get; set; }
		public string TotalAmountTPAfterTax { get; set; }
		public string TopupNominal { get; set; }
		public string IncentiveNominal { get; set; }
		public string DisbursalTypeUMCDescription { get; set; }
		public string StatusApproval { get; set; }
		public string IsApprover { get; set; }
		public string IsLastApprover { get; set; }
        public string TotalKewajibanNPPLama { get; set; }
    }
}