namespace FINCORE.Models.Models.Acquisition
{
    public class CAModels
    {
        public Int64 RowNumber { get; set; }
        public string? CAId { get; set; }
        public string? CMId { get; set; }
        public DateTime CMDate { get; set; }
        public string? CustomerName { get; set; }
        public string? Branch { get; set; }
        public string? Dealer { get; set; }
        public string? Merk { get; set; }
        public string? Type { get; set; }
        public string? AppsTypeName { get; set; }
        public string? RiskCategory { get; set; }
        public string? CarRiskCategory { get; set; }
        public string? Status { get; set; }
        public string? LKK { get; set; }
    }
}