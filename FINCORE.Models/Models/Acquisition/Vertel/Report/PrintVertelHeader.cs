using FINCORE.Models.Models.Vertel.Report;

namespace FINCORE.Models.Models.Acquisition.Vertel.Report
{
    public class PrintVertelHeader
    {
        public List<PrintVertelDokumen> PrintVertelDokumens { get; set; }
        public List<PrintVertelModels> PrintVertelField { get; set; }
    }
}