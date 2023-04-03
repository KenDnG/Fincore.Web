namespace FINCORE.Models.Models.Acquisition.CAS.References
{
    public class DataPasanganModels
    {
        public int pasangan_id { get; set; }
        public string nama_pasangan { get; set; }
        public string tipe_identitas { get; set; }
        public string no_identitas { get; set; }
        public string tempat_lahir { get; set; }
        public DateTime tanggal_lahir { get; set; }
        public string alamat_pasangan { get; set; }
        public string no_telepon { get; set; }
        public string pekerjaan_pasangan { get; set; }
        public int income { get; set; }
        public int other_income { get; set; }
        public string income_desc { get; set; }
        public string lokasi_pasangan_id { get; set; }
        public bool is_merried { get; set; } = false;


        //Helper Variable
        public string txtIncome { get; set; }
        public string txtOther_income { get; set; }
    }
}