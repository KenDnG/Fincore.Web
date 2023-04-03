using FINCORE.Models.Models.Payment.CashierSessionVerify;
using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IPayment
{
    internal interface ICashierSessionVerifyService
    {
        APIResponse GetPaginationCashierSessionVerify(string branchId, int pageIndex, int pageSize, string? searchTerm = "");

        APIResponse Verify(string SessionId, string EmployeeId, string BranchId);

        #region View Detail

        APIResponse GetCashierSessionDetailExpenses(RequestParamExpenses param);

        #endregion View Detail
    }
}