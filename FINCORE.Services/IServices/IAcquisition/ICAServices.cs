using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IAcquisition
{
    //Tidak terpakai
    public interface ICAServices
    {
        Task<APIResponse> GetDataCA();

        Task<APIResponse> GetPagingList(string SearchTerm, int PageIndex, int PageSize);
    }
}