namespace FINCORE.Models.Models.Acquisition.BPKB.Report
{
    public class BPKBPinjamReportModel
    {
        public string UserName { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPhone { get; set; }
        public DateTime LoanDate { get; set; }
        public string BPKBNo { get; set; }
        public string BrandName { get; set; }
        public string AssetTypeDesc { get; set; }
        public string ItemColor { get; set; }
        public string ChasisNo { get; set; }
        public string MachineNo { get; set; }
        public string ItemYear { get; set; }
        public string QQName { get; set; }
        public decimal MoneyReceived { get; set; }
        public string CreditId { get; set; }
        public string ContactPerson { get; set; }
    }
}