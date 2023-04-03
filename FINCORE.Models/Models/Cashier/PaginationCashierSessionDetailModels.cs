namespace FINCORE.Models.Models.Cashier
{
    public sealed class PaginationCashierSessionDetailModels
    {
        public string? SessionId { get; set; }
        public DateTime DateOfRecipt { get; set; }
        public string? BranchId { get; set; }
        public string? IncomeSource { get; set; }
        public DateTime PayDate { get; set; }
        public decimal PayAmount { get; set; }
        public string? ReferenceNumber { get; set; }

        public string GetFormatedDateOfRecipt() => DateOfRecipt.ToString("dd-MM-yyyy HH:mm");

        public string GetFormatedPayDate() => PayDate.ToString("dd-MM-yyyy HH:mm");
    }
}