using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FINCORE.Models.Models.Vertel
{
    public class VertelModels
    {
        public string? VerifikasiNo { get; set; }

        [Required]
        public string Cmno { get; set; }

        public string? StatusVerifikasi { get; set; }

        [Required]
        public int? OptBisaDiHubungi { get; set; }

        public string? NamaStnk { get; set; }
        public string? NamaPasanganStnk { get; set; }
        public DateTime? TglTerimaTagihan { get; set; }
        public string? strTglTerimaTagihan { get; set; }
        public DateTime? TglKonfirmasi { get; set; }
        public string? strTglKonfirmasi { get; set; }
        public string JamKonfirmasi { get; set; }
        public DateTime? TglApproveNpp { get; set; }
        public bool? OptTglTerimaMotor { get; set; }
        public string? strTglTerimaMotorSebenarnya { get; set; }
        public DateTime? OptTglTerimaMotorSebenarnya { get; set; }
        public bool? OptTipeMotor { get; set; }
        public string? OptTipeMotorSebenarnya { get; set; }
        public bool? OptAngsuran { get; set; }
        public decimal? OptAngsuranSebenarnya { get; set; }
        public bool? OptTenor { get; set; }
        public int? OptTenorSebenarnya { get; set; }
        public string? NamaPenerimaMotor { get; set; }
        public string? HubunganPenerimaMotor { get; set; }
        public string? PermintaanJt { get; set; }

        [Required]
        public string CatatanLain { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? OptPencairanMb { get; set; }
        public decimal? OptPencairanMbsebenarnya { get; set; }
        public bool? OptDpsetor { get; set; }
        public decimal? OptDpsetorSebenarnya { get; set; }
        public bool? BiayaAdminLainnya { get; set; }
        public decimal? JumlahNominalAdmin { get; set; }
        public DateTime? TanggalBayar { get; set; }
        public int? LokasiMotorDiTerima { get; set; }
        public string? LokasiDiTerima { get; set; }
        public string? NamaPelanggan { get; set; }
        public string? Phone { get; set; }
        public string? AlamatPelanggan { get; set; }

        public DateTime? TglTerimaPinjaman { get; set; }
        public string? Tipe { get; set; }
        public decimal? Installment { get; set; }
        public int? Tenor { get; set; }
        public decimal? DPSetor { get; set; }

        public List<DocFieldModels>? DocFieldModels { get; set; }
        public List<IFormFile>? DokumenIn { get; set; }
        public string? SaveType { get; set; }

        public int? OptBiayaAdmin { get; set; }
        public decimal? BiayaAdmin { get; set; }

        #region Approver Check

        public bool? IsApprover { get; set; }
        public string? NextApprover { get; set; }

        #endregion Approver Check
    }
}