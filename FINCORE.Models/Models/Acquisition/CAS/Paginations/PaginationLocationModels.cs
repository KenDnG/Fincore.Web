namespace FINCORE.Models.Models.Acquisition.CAS.Paginations
{
    public class PaginationLocationModels
    {
        public long? RowNumber { get; set; }
        public int location_id { get; set; }
        public string village_name { get; set; }
        public string district_name { get; set; }
        public string regency_name { get; set; }
        public string province_name { get; set; }
        public string zip_code { get; set; }
        public int? DT2Type { get; set; }
    }
}