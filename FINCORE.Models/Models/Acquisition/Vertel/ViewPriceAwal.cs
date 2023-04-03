namespace FINCORE.Models.Models.Acquisition.Vertel
{
    public class ViewPriceAwal
    {
        public string CreditId { get; set; }
        public byte? Tenor { get; set; }
        public decimal? GrossDownPayment { get; set; }
        public decimal? AdminFee { get; set; }
        public decimal? InsuranceFee { get; set; }
        public decimal? NettDownPayment { get; set; }
        public decimal? JmlPembiayaan { get; set; }
        public decimal? AmountInstallment { get; set; }
        public decimal? RateValue { get; set; }
        public decimal? FlatRate { get; set; }
        public decimal? PersenNfa { get; set; }
        public decimal? dealer_admin_fee { get; set; }
        public decimal? TotalUppingOtr { get; set; }
        public decimal? BiayaProvisiIns { get; set; }
        public decimal? UangMuka { get; set; }
        public decimal? DiscDeposit { get; set; }
        public decimal? AssetCost { get; set; }
    }
}