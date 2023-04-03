namespace FINCORE.Models.Models.Acquisition.BPKB.Paging
{
    public class PagingBPKBResultModel
    {
        public long? RowNumber { get; set; }
        public string CustomerName { get; set; }
        public string ChasisNo { get; set; }
        public string MachineNo { get; set; }
        public string BPKBNo { get; set; }
        public string CMNo { get; set; }
        public DateTime? BPKBIn { get; set; }
        public DateTime? BPKBOut { get; set; }
        public string StatusBPKB { get; set; }
        public bool? PrintedSK { get; set; }
        public string CompanyId { get; set; }
        public string ApplicationType { get; set; }
        public string StatusCredit { get; set; }
    }
}