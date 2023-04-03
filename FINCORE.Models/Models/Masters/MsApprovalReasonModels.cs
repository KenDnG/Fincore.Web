namespace FINCORE.Models.Models.Masters
{
    public class MsApprovalReason
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
        /// ID alasan
        /// </summary>
        public string ReasonId { get; set; }

        /// <summary>
        /// deskripsi dari alasan
        /// </summary>
        public string ReasonDescription { get; set; }

        /// <summary>
        /// tipe
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// indikator aktif atau tidaknya master data
        /// </summary>
        public bool IsActive { get; set; }
    }
}