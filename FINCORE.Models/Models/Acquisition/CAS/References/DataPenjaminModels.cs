namespace FINCORE.Models.Models.Acquisition.CAS.References
{
    public class DataPenjaminModels
    {
        public int id_penjamin { get; set; }
        public string nama_penjamin { get; set; }
        public string tipe_identitas { get; set; }
        public string no_identitas { get; set; }
        public string tempat_lahir { get; set; }
        public DateTime tanggal_lahir { get; set; }
        public string alamat { get; set; }
        public int? lokasi_penjamin_id { get; set; }
    }
}