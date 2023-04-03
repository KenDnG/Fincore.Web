using FINCORE.Models.Models.Acquisition;
using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.CRUD
{
    public interface ICRUD
    {
        Task<APIResponse> InsertCRUD(CASModels item);

        Task<APIResponse> GetDataCRUD();

        Task<APIResponse> UpdateCRUD(CASModels item);

        Task<APIResponse> GetPagingList(string SearchTerm, int PageIndex, int PageSize);
    }
}