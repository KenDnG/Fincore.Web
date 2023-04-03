using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IApproval
{
    public interface IApprovalServices
    {
        APIResponse GetPaginationInboxApproval(string? employeeId, int pageIndex, int pageSize, string? searchTerm = "");
    }
}