namespace FINCORE.Models.Models.Acquisition.NPP.Pagination
{
    public class PaginationNPP
    {
        public long? RowNumber { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string CreditId { get; set; }
        public string AgreementDate { get; set; }
        public string CustomerName { get; set; }
        public string ChasisNo { get; set; }
        public string MachineNo { get; set; }
        public string InstallmentDate { get; set; }
        public string AgreementStatus { get; set; }
        public int SumPrint { get; set; }
        public int SumPrintFidusia { get; set; }
    }
}