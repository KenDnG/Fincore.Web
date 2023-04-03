namespace FINCORE.Models.Models.Masters
{
    public class MsIdentityTypeModels
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
        /// ID tipe identitas
        /// </summary>
        public string IdentityTypeId { get; set; }

        /// <summary>
        /// nama tipe identitas
        /// </summary>
        public string IdentityTypeName { get; set; }

        /// <summary>
        /// kode tipe konsumen
        /// </summary>
        public string CustomerType { get; set; }

        /// <summary>
        /// indikator aktif atau tidaknya master data
        /// </summary>
        public bool IsActive { get; set; }
    }
}