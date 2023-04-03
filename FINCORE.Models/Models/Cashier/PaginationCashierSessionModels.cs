namespace FINCORE.Models.Models.Cashier
{
    public sealed class PaginationCashierSessionModels
    {
        public string? SessionId { get; set; }

        public string? BranchId { get; set; }

        public string? UserId { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public decimal CurrentAmount { get; set; }

        public string GetFormatedOpenDate() => OpenDate.ToString("dd-MM-yyyy HH:mm");

        public string? GetFormatedCloseDate()
        {
            if (CloseDate is not null)
                return CloseDate?.ToString("dd-MM-yyyy HH:mm");

            return null;
        }

        public string GetFormatedCurrentAmount()
        {
            return Convert.ToDecimal(CurrentAmount).ToString("N0");
        }
    }
}