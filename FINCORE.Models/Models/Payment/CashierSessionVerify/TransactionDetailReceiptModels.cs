namespace FINCORE.Models.Models.Payment.CashierSessionVerify
{
    public class TransactionDetailReceiptModels
    {
        public string? SessionId { get; set; }
        public DateTime DateReceipt { get; set; }
        public string? BranchId { get; set; }
        public DateTime PayDate { get; set; }
        public string? SourceAcceptance { get; set; }
        public decimal PayAmount { get; set; }
        public string? ReferenceNumber { get; set; }

        public string GetFormatedDateOfReceipt() => DateReceipt.ToString("dd-MM-yyyy HH:mm");
        public string GetFormatedPayDate() => PayDate.ToString("dd-MM-yyyy HH:mm");
    }
}