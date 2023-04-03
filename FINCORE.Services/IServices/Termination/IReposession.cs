using FINCORE.Models.Models.Termination;
using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.Termination
{
    public interface IReposession
    {
        Task<APIResponse> InsertRepo(ReposessionModels items);
    }
}