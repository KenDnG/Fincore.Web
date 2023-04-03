using FINCORE.Models.Models.Vertel;
using FINCORE.Services.Helpers.Models.Pagination;

namespace FINCORE.Services.Helpers.Models.ViewModels
{
    public class ViewModelVertelLookup
    {
        public int page { get; set; }

        public PaginationModels<PagingVertelLookupModels> paging { get; set; }

        public List<PagingVertelLookupModels> data { get; set; }
    }
}