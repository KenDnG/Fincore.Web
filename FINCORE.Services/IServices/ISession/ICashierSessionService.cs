using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.ISession
{
    internal interface ICashierSessionService
    {
        APIResponse GetPaginationCashierSession(string? employeeId, int pageIndex, int pageSize, string? searchTerm = "");

        APIResponse GetPaginationCashierSessionDetail(string? sessionId, int pageIndex, int pageSize, string? searchTerm = "");

        APIResponse InsertNewCashierSession(string userId, string userName, string branchId);

        public APIResponse CloseCashierSession(string? sessionId, string userName, double currentAmount, double cashAmount);
    }
}