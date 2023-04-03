namespace FINCORE.Models.Models.Acquisition.CAS.ModelHelper
{
    public class DataReferencesListModels
    {
        public List<int> referensi_id { get; set; } = new List<int>();
        public List<string> nama_referensi { get; set; } = new List<string>();
        public List<int> hubungan_pemohon { get; set; } = new List<int>();
        public List<string> alamat_referensi { get; set; } = new List<string>();
        public List<string> no_telepon { get; set; } = new List<string>();
        public List<string> no_telp_kantor { get; set; } = new List<string>();
        public List<string> mobile_phone { get; set; } = new List<string>();
        public List<int> lokasi_id_referensi { get; set; } = new List<int>();
    }
}