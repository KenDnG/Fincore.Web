namespace FINCORE.Models.Models.Acquisition.BPKB.Paging
{
    public class PagingBPKBKaryawanModel
    {
        public long? RowNumber { get; set; }
        public string NoPeg { get; set; }
        public string Nama { get; set; }
        public string Sex { get; set; }
        public string KdPerusahaan { get; set; }
        public string NoRekening { get; set; }
        public string NamaRekening { get; set; }
        public string KdBank { get; set; }
        public string NmBank { get; set; }
        public string Alamat { get; set; }
        public string Kota { get; set; }
        public string AlamatID { get; set; }
        public string KotaID { get; set; }
        public string Telp { get; set; }
        public string NoHP { get; set; }
        public string Email { get; set; }
        public byte? FKeluar { get; set; }
        public string NoKTP { get; set; }
        public string NoSIM { get; set; }
        public string NoPassport { get; set; }
        public string KdCabang { get; set; }
        public string KdJabat { get; set; }
        public string NmJabat { get; set; }
        public DateTime? TglKeluar { get; set; }
        public DateTime? tglmasuk { get; set; }
        public byte? stpegawai { get; set; }
        public DateTime? tglendkontrak { get; set; }
        public int? kontrakke { get; set; }
        public DateTime? tglgroup { get; set; }
        public string company_name { get; set; }
        public string branch_name { get; set; }
        public string position_description { get; set; }
        public string konsol_branch_id { get; set; }
        public string CompanyBranchIDSearch { get; set; }
        public string CompanyIDSearch { get; set; }
        public string PositionIDSearch { get; set; }
    }
}