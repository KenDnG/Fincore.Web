namespace FINCORE.Models.Models.Acquisition.NPP.Report;

public sealed class PrintImportantNotice
{
    public string CustomerName { get; set; }
    public string CustomerAdress { get; set; }
    public decimal MonthlyInstallment { get; set; }
    public string FormatedMonthlyInstallment { get; set; }
    public string MonthlyInstallmentInWords { get; set; } = "";
    public string LetterDate { get; set; }
    public string BranchName { get; set; }
}