using FINCORE.Models.Models.Termination;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.Termination;

namespace FINCORE.Services.Services.Termination
{
    public class ReposessionServices : SendRequest<APIResponse>, IReposession
    {
        public async Task<APIResponse> InsertRepo(ReposessionModels items)
        {
            throw new NotImplementedException();
        }
    }
}