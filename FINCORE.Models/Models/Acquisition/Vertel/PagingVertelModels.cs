namespace FINCORE.Models.Models.Vertel
{
    public class PagingVertelModels
    {
        public Int64 RowNumber { get; set; }
        public string VerifikasiNo { get; set; }
        public string CMNo { get; set; }
        public string AgreementNumber { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string CustomerName { get; set; }
        public string TipeKendaraan { get; set; }
        public string StatusVerifikasi { get; set; }

        public string? GetFormatedApproveDate()
        {
            if (ApproveDate is not null)
                return ApproveDate?.ToString("dd-MM-yyyy");

            return null;
        }
    }
}