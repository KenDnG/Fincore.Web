namespace FINCORE.Models.Models.Acquisition.NPP.Report;

public sealed class PrintMemorandumOfUnderstanding
{
    public string CreditId { get; set; }
    public string AgreementDay { get; set; }
    public string AgreementDate { get; set; }
    public string CustomerName { get; set; }
    public int NumberOfUnits { get; set; }
    public string BrandType { get; set; }
    public string ChasisNumber { get; set; }
    public string MachineNumber { get; set; }
    public string ItemYearColor { get; set; }
    public string BpkbName { get; set; }
    public string BpkpNo { get; set; }
    public decimal PurchasePrice { get; set; }
    public string PurchasePriceFormated { get; set; }
    public decimal DownPayment { get; set; }
    public string DownPaymentFormated { get; set; }
    public decimal PrincipalFinancing { get; set; }
    public string PrincipalFinancingFormated { get; set; }
    public decimal InterestAmount { get; set; }
    public string InterestAmountFormated { get; set; }
    public double InterestRate { get; set; }
    public decimal DebtAmount { get; set; }
    public string DebtAmountFormated { get; set; }
    public int Tenor { get; set; }
    public decimal MonthlyPayment { get; set; }
    public string MonthlyPaymentFormated { get; set; }
    public string FirstPayment { get; set; }
    public string LastPayment { get; set; }
    public int PaymentDueDate { get; set; }
    public decimal AdminFee { get; set; }
    public string AdminFeeFormated { get; set; }
    public decimal ProvisionFee { get; set; }
    public string ProvisionFeeFormated { get; set; }
    public decimal InsuranceFee { get; set; }
    public string InsuranceFeeFormated { get; set; }
    public decimal SurveyFee { get; set; }
    public string SurveyFeeFormatted { get; set; }
    public string DebtAmountInWords { get; set; }
    public string AssetType { get; set; }
    public byte BrandNew { get; set; }
    public string LetterDateDayMonth { get; set; }
    public string LetterDateYear { get; set; }
    public string BranchName { get; set; }
    public string ReferenceName { get; set; }
    public string CrediturName { get; set; }
    public string BrandNewMotorcycleMark { get; set; } = "";
    public string UsedMotorcycleMark { get; set; } = "";
    public string BrandNewCarMark { get; set; } = "";
    public string UsedCarMark { get; set; } = "";
}