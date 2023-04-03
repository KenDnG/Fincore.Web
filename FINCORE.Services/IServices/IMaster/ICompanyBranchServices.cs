using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IMaster
{
    public interface ICompanyBranchServices
    {
        Task<APIResponse> GetCompanyBranch(string company_id, string NIK);

        Task<APIResponse> GetCompanyBranchDetail(string branch_id);
    }
}