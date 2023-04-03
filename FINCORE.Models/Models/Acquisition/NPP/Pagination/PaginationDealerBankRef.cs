namespace FINCORE.Models.Models.Acquisition.NPP.Pagination
{
    public class PaginationDealerBankRef
    {
        public long? RowNumber { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public string DealerCode { get; set; }
        public int BankReferenceId { get; set; }
        public string BankAccountType { get; set; }
        public string BankAccountDescription { get; set; }
        public string BankId { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountName { get; set; }
        public bool IsBankCharges { get; set; }
        public string BankReferenceStatus { get; set; }
        public string ActivationDate { get; set; }
        public string DeactivationDate { get; set; }
        public string BankName { get; set; }
        public int? BranchId { get; set; }
    }
}