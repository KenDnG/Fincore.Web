namespace FINCORE.Models.Models.Vertel
{
    public class PagingVertelLookupModels
    {
        public long? RowNumber { get; set; }
        public string CMNo { get; set; }
        public string BranchID { get; set; }
        public string NPPNo { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Item { get; set; }
        public string Merk { get; set; }
        public string Tipe { get; set; }
        public byte? Tenor { get; set; }
        public decimal? Installment { get; set; }
        public string TipeAplikasiID { get; set; }
        public string NamaMarketing { get; set; }
        public decimal? Pencairan { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public decimal DPSetor { get; set; }
        public string? NamaStnk { get; set; }
        public string? NamaPasanganStnk { get; set; }
    }
}