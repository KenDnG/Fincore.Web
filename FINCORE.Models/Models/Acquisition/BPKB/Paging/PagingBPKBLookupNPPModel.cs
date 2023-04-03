namespace FINCORE.Models.Models.Acquisition.BPKB.Paging
{
    public class PagingBPKBLookupNPPModel
    {
        public long? RowNumber { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string AgreementNumber { get; set; }
        public string CustomerName { get; set; }
        public string QQName { get; set; }
        public string sBPKB { get; set; }
        public string ChasisNo { get; set; }
        public string MachineNo { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string ItemMerk { get; set; }
        public string ItemMerkTypeID { get; set; }
        public string ItemTypeName { get; set; }
        public string ItemYear { get; set; }
        public string ItemColor { get; set; }
        public string StatusNPP { get; set; }
        public string StatusLunasNPP { get; set; }
        public string STATUSBPKB { get; set; }
        public string TipeAplikasiID { get; set; }
        public string TipeAplikasiName { get; set; }
        public int IsSkema2 { get; set; }
        public decimal BBNCair { get; set; }
        public string CarNoTarikan { get; set; }
    }
}