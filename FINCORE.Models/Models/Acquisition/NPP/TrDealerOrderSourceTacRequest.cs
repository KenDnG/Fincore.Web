namespace FINCORE.Models.Models.Acquisition.NPP
{
    public class TrDealerOrderSourceTacRequest
    {
        public string CreditId { get; set; }
        public int JobTitleId { get; set; }
        public int PersonelId { get; set; }
        public string PersonelName { get; set; }
        public decimal RateTacRefund { get; set; }
        public decimal AmountTacRefund { get; set; }
        public decimal AmountTacRefundAfterTax { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
    }
}