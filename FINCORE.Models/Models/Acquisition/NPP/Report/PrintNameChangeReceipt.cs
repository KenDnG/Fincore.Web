namespace FINCORE.Models.Models.Acquisition.NPP.Report
{
    public sealed class PrintNameChangeReceipt
    {
        public string ReceiptNo { get; set; }
        public string PayerName { get; set; }
        public string PaymentAmountInWord { get; set; }
        public string PaymentPurpose { get; set; }
        public string PaymentDateDayMonth { get; set; }
        public string PaymentDateYear { get; set; }
        public int PaymentAmount { get; set; }
        public string PaymentAmountFormatted { get; set; }
    }
}