namespace FINCORE.Models.Models.Vertel
{
    public class VertelApprovalModels
    {
        public int ReasonId { get; set; }
        public int Type { get; set; }
        public string ReasonDescription { get; set; }
        public string? EmployeeId { get; set; }
        public string TransId { get; set; }
    }
}