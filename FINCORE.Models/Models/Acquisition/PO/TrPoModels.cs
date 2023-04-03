namespace FINCORE.Models.Models.Acquisition.PO
{
    public class TrPoModels
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string PoNo { get; set; }
        public string CreditId { get; set; }
        public DateTime? PoDate { get; set; }
        public string CompanyId { get; set; }
        public string BranchId { get; set; }
        public string StatusPo { get; set; }
        public bool IsPrint { get; set; }
        public int SumOfPrint { get; set; }
        public DateTime? FirstPrintDate { get; set; }
        public DateTime? LastPrintDate { get; set; }
        public string FirstPrintBy { get; set; }
        public string LastPrintBy { get; set; }
    }
}