using FINCORE.Models.Models.Acquisition.OMA;
using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IAcquisition
{
    public interface IOMAServices
    {
        public Task<APIResponse> GetOMAPaging(string SearchTerm, int PageIndex, int PageSize);

        public Task<APIResponse> GetOMAById(string id);

        public Task<APIResponse> UpdateOMA(OMAModel item);
    }
}