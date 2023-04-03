namespace FINCORE.Models.Models.Acquisition.NPP.Report
{
    public sealed class PrintSecondStatementLetter
    {
        public string CreditId { get; set; }
        public string CustomerName { get; set; }
        public string Occupation { get; set; }
        public string CustomerAddress { get; set; }
        public string IdentityNumber { get; set; }
        public string ApproveDate { get; set; }
        public string PurchasePrice { get; set; }
        public string DownPayment { get; set; }
        public string PrincipalFinancing { get; set; }
        public string InterestAmount { get; set; }
        public double InterestRate { get; set; }
        public string DebtAmount { get; set; }
        public string AdminFee { get; set; }
        public string ProvisionFee { get; set; }
        public string InsuranceFee { get; set; }
        public string SurveyFee { get; set; }
        public string LetterDateDayMonth { get; set; }
        public string LetterDateYear { get; set; }
        public string BranchName { get; set; }
    }
}