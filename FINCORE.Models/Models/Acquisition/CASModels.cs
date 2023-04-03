namespace FINCORE.Models.Models.Acquisition
{
    public class CASModels
    {
        public string? ID { get; set; }
        public string KodeCabang { get; set; }
        public string PoNo { get; set; }
        public string TipeKonsumen { get; set; }
        public string ExistingCustomerRO { get; set; }
        public string NamaNasabah { get; set; }
        public string? JenisKelamin { get; set; }
        public string? NamaIbuKandung { get; set; }
    }

    public class XCASModel
    {
        public Int64 RowNumber { get; set; }
        public string CASId { get; set; }
        public string NamaPelanggan { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}