namespace FINCORE.Models.Models.Approval
{
    public class PaginationInboxApprovalModels
    {
        private DateTime transactionDate;

        public string? TransactionId { get; set; }

        //public string? CreditId { get; set; }
        public DateTime TransactionDate { get => transactionDate; set => transactionDate = value; }

        public string? Description { get; set; }
        public string? PICNIK { get; set; }
        public string? PICName { get; set; }
        public string? NextNIKPIC { get; set; }
        public string? NextPICName { get; set; }
        public string? Status { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string? ActionApproved { get; set; }
        public string? ActionView { get; set; }
        public string? ActionEdit { get; set; }

        public string GetFormatedString() => TransactionDate.ToString("dd-MM-yyyy");
    }
}