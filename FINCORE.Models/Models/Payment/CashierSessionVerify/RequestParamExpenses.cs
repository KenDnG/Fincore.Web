namespace FINCORE.Models.Models.Payment.CashierSessionVerify
{
    public class RequestParamExpenses
    {
        public int PageIndex { get; set; }
        public int MaxPage { get; set; }
        public string? SearchTerm { get; set; } = "";
        public string? BranchId { get; set; }
        public string? SessionId { get; set; }
    }
}