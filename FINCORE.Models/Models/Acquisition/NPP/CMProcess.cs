namespace FINCORE.Models.Models.Acquisition.NPP
{
    public class CMProcess
    {
        public string CreditId { get; set; }
        public string CreditDate { get; set; }
        public string BranchId { get; set; }
        public string ItemId { get; set; }
        public string PONumber { get; set; }
        public string CustomerName { get; set; }
        public string InstallmentDate { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string DealerAddress { get; set; }
        public decimal? AssetCost { get; set; }
        public decimal? TACMax { get; set; }
        public decimal? InsuranceAmount { get; set; }
        public string AgreementValue { get; set; }
        public string ApplicationTypeId { get; set; }
        public decimal? NFAPercent { get; set; }
        public decimal? OtherAPValue { get; set; }
        public bool? IsTopupMS { get; set; }
        public string TopupNominal { get; set; }
        public int? IncentiveNominal { get; set; }
        public string ChasisNo { get; set; }
        public string MachineNo { get; set; }
        public string CustomerNameUMC { get; set; }
        public string CustomerBankAccountNoUMC { get; set; }
        public string StatusApproval { get; set; }
        public int IsNppExists { get; set; }
        public string TotalKewajibanNPPLama { get; set; }
    }
}