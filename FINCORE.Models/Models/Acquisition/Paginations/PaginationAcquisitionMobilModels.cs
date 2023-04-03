namespace FINCORE.Models.Models.Acquisition.Paginations
{
    public class PaginationAcquisitionMobilModels
    {
        public long? RowNumber { get; set; }
        public string credit_id { get; set; }
        public string PO_no { get; set; }
        public DateTime? cm_date { get; set; }
        public string mobile_cas_id { get; set; }
        public string customer_name { get; set; }
        public string dealer_code { get; set; }
        public string dealer_name { get; set; }
        public string item_id { get; set; }
        public string asset_kind_description { get; set; }
        public string item_merk_id { get; set; }
        public string asset_brand_name { get; set; }
        public string item_merk_type_id { get; set; }
        public string asset_type_description { get; set; }
        public string lkk { get; set; }
        public string kategori_resiko { get; set; }
        public string kategori_mobil { get; set; }
        public string status_ro { get; set; }
        public string status_po { get; set; }
        public int sum_of_print_po { get; set; }
        public string status_cm { get; set; }
        public string credit_source_id { get; set; }
        public string credit_source_desc { get; set; }
        public int po_days { get; set; }
    }
}