namespace FINCORE.Models.Models.Masters
{
    public class MsCompanyBranchModels
    {
        /// <summary>
        /// user yang membuat master data
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// tanggal &amp; waktu pembuatan master data
        /// </summary>
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// user yang mengubah master data
        /// </summary>
        public string LastUpdatedBy { get; set; }
        /// <summary>
        /// tanggal &amp; waktu perubahan master data
        /// </summary>
        public DateTime? LastUpdatedOn { get; set; }
        /// <summary>
        /// ID perusahaan
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// ID cabang
        /// </summary>
        public string BranchId { get; set; }
        /// <summary>
        /// ID wilayah
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// ID konsol cabang
        /// </summary>
        public string KonsolBranchId { get; set; }
        /// <summary>
        /// ID induk cabang
        /// </summary>
        public string ParentBranchId { get; set; }
        /// <summary>
        /// indikator apakah konsolidasi cabang atau tidak
        /// </summary>
        public bool IsKonsol { get; set; }
        /// <summary>
        /// indikator apakah head office atau tidak
        /// </summary>
        public bool IsHeadOffice { get; set; }
        /// <summary>
        /// nama cabang
        /// </summary>
        public string BranchName { get; set; }
        /// <summary>
        /// email cabang
        /// </summary>
        public string BranchEmail { get; set; }
        /// <summary>
        /// alamat cabang
        /// </summary>
        public string BranchAddress { get; set; }
        /// <summary>
        /// provinsi cabang
        /// </summary>
        public string BranchProvince { get; set; }
        /// <summary>
        /// nomor telepon cabang
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// orang yang dapat dihubungi
        /// </summary>
        public string ContactPerson { get; set; }
        /// <summary>
        /// indikator aktif atau tidaknya master data
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// ID rekan cabang
        /// </summary>
        public string BranchPartnerId { get; set; }
    }
}