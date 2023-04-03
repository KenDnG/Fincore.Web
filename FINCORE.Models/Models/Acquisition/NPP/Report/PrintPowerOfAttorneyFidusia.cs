namespace FINCORE.Models.Models.Acquisition.NPP.Report;

public sealed class PrintPowerOfAttorneyFidusia
{
    public string CreditId { get; set; }
    public string AgreementDate { get; set; }
    public string CustomerName { get; set; }
    public string Occupation { get; set; }
    public string IdentityNumber { get; set; }
    public string CustomerAddress { get; set; }
    public string OnBehalfOf { get; set; }
    public string PowerAttorneyName { get; set; }
    public string PowerAttorneyPosition { get; set; }
    public string PowerAttorneyAddress { get; set; }
    public string MCFHOAdress { get; set; }
    public string MAFHOAdress { get; set; }
    public string MCFBranchAddress { get; set; }
    public string MAFBranchAddress { get; set; }
    public decimal DebtAmount { get; set; }
    public string DebtAmountFormatted { get; set; }
    public string LetterDateDayMonth { get; set; }
    public string LetterDateYear { get; set; }
    public string BranchName { get; set; }
}