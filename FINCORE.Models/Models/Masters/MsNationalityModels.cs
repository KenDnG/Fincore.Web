namespace FINCORE.Models.Models.Acquisition.Masters
{
    public class MsNationalityModels
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
        /// ID kewarganegaraan
        /// </summary>
        public int NationalityId { get; set; }

        /// <summary>
        /// deskripsi dari kewarganegaraan
        /// </summary>
        public string NationalityDesc { get; set; }

        /// <summary>
        /// indikator aktif atau tidaknya master data
        /// </summary>
        public bool IsActive { get; set; }
    }
}