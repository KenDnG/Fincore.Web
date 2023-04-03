namespace FINCORE.Models.Models.Acquisition.BPKB
{
    public class CompanyBranchModel
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public int CompanyId { get; set; }
        public string BranchId { get; set; }
        public string AreaId { get; set; }
        public string KonsolBranchId { get; set; }
        public string ParentBranchId { get; set; }
        public bool IsKonsol { get; set; }
        public bool IsHeadOffice { get; set; }
        public string BranchName { get; set; }
        public string BranchEmail { get; set; }
        public string BranchAddress { get; set; }
        public string BranchProvince { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public bool? IsActive { get; set; }
        public string BranchPartnerId { get; set; }
    }
}