namespace FINCORE.Models.Models.Masters
{
    public class CompanyBranchResponse
    {
        public string company_id { get; set; }
        public string branch_id { get; set; }
        public string area_id { get; set; }
        public string parent_branch_id { get; set; }
        public bool is_head_office { get; set; }
        public string branch_name { get; set; }
        public string branch_address { get; set; }
        public string branch_province { get; set; }
        public bool is_active { get; set; }
        public string branch_partner_id { get; set; }
    }
}