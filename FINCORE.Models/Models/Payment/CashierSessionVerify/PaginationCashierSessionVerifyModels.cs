namespace FINCORE.Models.Models.Payment.CashierSessionVerify
{
    public class PaginationCashierSessionVerifyModels
    {
        public string Branch { get; set; }
        public string? SessionId { get; set; }
        public DateTime? CloseDate { get; set; }
        public decimal CloseAmount { get; set; }
        public int TransactionDetailReceipt { get; set; }
        public int TransactionDetailExpenses { get; set; }
        public decimal CashIncomeAmount { get; set; }
        public decimal CashExpensesAmount { get; set; }
        public decimal TotalCashAmount { get; set; }
        public string? UserId { get; set; }

        public string? GetFormatedCloseDate()
        {
            if (CloseDate is not null)
                return CloseDate?.ToString("dd-MM-yyyy HH:mm");

            return null;
        }

        public DateTime? VerifyDate { get; set; }
        public string? VerifyBy { get; set; }
    }
}