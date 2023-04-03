using FINCORE.Models.Models.Acquisition.BPKB.Paging;
using FINCORE.Services.Helpers.Models.Pagination;

namespace FINCORE.Services.Helpers.Models.ViewModels
{
    public class ViewModelBPKBLookup
    {
        public int page { get; set; }
        public PaginationModels<PagingBPKBLookupNPPModel> paging { get; set; }
        public PaginationModels<PagingBPKBReceiverLookup> PagingReceiver { get; set; }
        public PaginationModels<PagingBPKBTypeOfBureau> PagingBureau { get; set; }
        public PaginationModels<PagingBPKBDealerModel> PagingDealer { get; set; }
        public PaginationModels<PagingBPKBAsuransiLookup> PagingAsuransi { get; set; }
        public PaginationModels<PagingBPKBBiroJasaModel> PagingBiroJasa { get; set; }
        public PaginationModels<PagingBPKBKaryawanModel> PagingKaryawan { get; set; }
        public List<PagingBPKBLookupNPPModel> data { get; set; }//unnecessary,data is available inside paging
    }
}