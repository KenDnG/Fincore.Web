namespace FINCORE.Models.Models.Acquisition.CAS.Paginations
{
    public class PaginationNikKonsumenModels
    {
        public long? RowNumber { get; set; }
        public string employee_id { get; set; }
        public string employee_name { get; set; }
        public string address_name { get; set; }
        public string city { get; set; }
        public string gender { get; set; }
        public string phone_number { get; set; }
        public int? company_id { get; set; }
        public string company_name { get; set; }
        public string KdCabang { get; set; }
        public string branch_name { get; set; }
        public string position_id { get; set; }
        public string position_name { get; set; }
        public DateTime? tglmasuk { get; set; }
        public DateTime? TglKeluar { get; set; }
    }
}