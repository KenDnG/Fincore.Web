namespace FINCORE.Models.Models.Payment.CashierSessionVerify
{
    public class TransactionDetailExpensesModels
    {
        public string? SessionId { get; set; }
        public DateTime OutDate { get; set; }
        public string? BranchId { get; set; }
        public string? SourceExpenditure { get; set; }
        public DateTime PayDate { get; set; }
        public decimal PayAmount { get; set; }
        public string? ReferenceNumber { get; set; }

        public string GetFormatedOutDate() => OutDate.ToString("dd-MM-yyyy HH:mm");

        public string GetFormatedPayDate() => PayDate.ToString("dd-MM-yyyy HH:mm");
    }
}