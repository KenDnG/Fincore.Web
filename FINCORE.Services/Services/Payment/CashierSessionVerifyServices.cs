using FINCORE.Models.Models.Payment.CashierSessionVerify;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IPayment;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Payment
{
    public class CashierSessionVerifyServices : SendRequest<APIResponse>, ICashierSessionVerifyService
    {
        private readonly ObjectConverter converter = new();

        public APIResponse GetCashierSessionDetailExpenses(RequestParamExpenses param)
        {
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MiniAPI_CSV}/detail-expenses?BranchId={param.BranchId}&SessionId={param.SessionId}&SearchTerm={param.SearchTerm}&PageIndex={param.PageIndex}&PageSize={param.MaxPage}", Method.Get);
                var dataPaging = converter.JsonToObject<PaginationModels<TransactionDetailExpensesModels>>(dataResponse.data);

                return new APIResponse
                {
                    code = dataResponse.code,
                    data = dataPaging,
                    status = dataResponse.status,
                    message = dataResponse.message
                };
            }
            catch(Exception ex)
            {
                return new APIResponse
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
        }

        public APIResponse GetCashierSessionDetailReceipt(RequestParamReceipt param)
        {
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MiniAPI_CSV}/detail-receipt?BranchId={param.BranchId}&SessionId={param.SessionId}&SearchTerm={param.SearchTerm}&PageIndex={param.PageIndex}&PageSize={param.MaxPage}", Method.Get);
                var dataPaging = converter.JsonToObject<PaginationModels<TransactionDetailReceiptModels>>(dataResponse.data);

                return new APIResponse
                {
                    code = dataResponse.code,
                    data = dataPaging,
                    status = dataResponse.status,
                    message = dataResponse.message
                };
            }
            catch (Exception ex)
            {
                return new APIResponse
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
        }

        public APIResponse GetPaginationCashierSessionVerify(string branchId, int pageIndex, int pageSize, string? searchTerm = "")
        {
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MiniAPI_CSV}/pagination?BranchId={branchId}&SearchTerm={searchTerm}&PageIndex={pageIndex}&PageSize={pageSize}", Method.Get);
                var dataPaging = converter.JsonToObject<PaginationModels<PaginationCashierSessionVerifyModels>>(dataResponse.data);

                return new APIResponse
                {
                    code = dataResponse.code,
                    data = dataPaging,
                    status = dataResponse.status,
                    message = dataResponse.message
                };
            }
            catch (Exception ex)
            {
                return new APIResponse
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
        }

        public APIResponse Verify(string SessionId, string EmployeeId, string BranchId)
        {
            try
            {
                APIResponse dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_PAYMENT}/{Route.ROUTE_PAYMENT}/cashier-session-verify/verify-session", Method.Post, new { SessionId, EmployeeId, BranchId });

                return dataResponse;
            }
            catch (Exception ex)
            {
                return new APIResponse
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
        }
    }
}