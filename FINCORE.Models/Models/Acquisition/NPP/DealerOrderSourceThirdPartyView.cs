namespace FINCORE.Models.Models.Acquisition.NPP
{
    public class DealerOrderSourceThirdPartyView
    {
        public int JobTitleId { get; set; }
        public int PersonnelId { get; set; }
        public string JobTitleDescription { get; set; }
        public string PersonnelName { get; set; }
        public decimal AmountProvisiRefund { get; set; }
        public decimal AmountProvisiRefundAfterTax { get; set; }
    }
}