namespace FINCORE.Models.Models.Acquisition.NPP;

public class TrNppPrintRequest
{
    public string? CreditId { get; set; }
    public string? UserName { get; set; }
    public bool IsPrintPK { get; set; }
    public bool IsPrintFidusia { get; set; }
    public bool IsPrintAkad { get; set; }
}