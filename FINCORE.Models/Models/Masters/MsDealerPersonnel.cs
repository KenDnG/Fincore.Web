namespace FINCORE.Models.Models.Masters
{
    public class MsDealerPersonnel
    {
        public string DealerCode { get; set; }
        public int PersonnelId { get; set; }
        public string PersonnelName { get; set; }
        public string KtpNo { get; set; }
        public int? JobTitleId { get; set; }
        public string Address { get; set; }
        public int LocationId { get; set; }
        public string BirthPlace { get; set; }
        public DateTime BirthDate { get; set; }
        public bool? IsNpwp { get; set; }
        public string NpwpNo { get; set; }
        public string NpwpName { get; set; }
        public string NpwpAddress { get; set; }
        public string NpwpType { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string FaxNo { get; set; }
        public int? BankReferenceId { get; set; }
        public string PersonnelStatus { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public decimal RateRefundPersonnel { get; set; }
        public decimal? PkpCumulative { get; set; }
        public string MpMasterDealerFile { get; set; }
        public string KtpOwnerFile { get; set; }
        public string SptAccountBookFile { get; set; }
        public string NpwpFile { get; set; }
        public string IdCardFile { get; set; }
        public string StatementLetterFile { get; set; }
        public int? VipId { get; set; }
        public int? LamaPencairan { get; set; }
    }
}