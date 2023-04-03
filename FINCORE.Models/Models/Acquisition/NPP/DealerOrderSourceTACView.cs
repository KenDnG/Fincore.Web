namespace FINCORE.Models.Models.Acquisition.NPP
{
    public class DealerOrderSourceTACView
    {
        public int JobTitleId { get; set; }
        public int PersonnelId { get; set; }
        public string JobTitleDescription { get; set; }
        public string PersonnelName { get; set; }
        public decimal AmountTacRefund { get; set; }
        public decimal AmountTacRefundAfterTax { get; set; }
    }
}