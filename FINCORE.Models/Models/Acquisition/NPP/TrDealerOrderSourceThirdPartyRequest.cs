namespace FINCORE.Models.Models.Acquisition.NPP
{
    public class TrDealerOrderSourceThirdPartyRequest
    {
        public string CreditId { get; set; }
        public int JobTitleId { get; set; }
        public int PersonelId { get; set; }
        public string PersonelName { get; set; }
        public decimal RateProvisiRefund { get; set; }
        public decimal AmountProvisiRefund { get; set; }
        public decimal AmountProvisiRefundAfterTax { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
    }
}