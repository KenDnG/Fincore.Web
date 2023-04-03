using FINCORE.Models.Models.Cashier;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.ISession;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Session
{
    public class CashierSessionServices : SendRequest<APIResponse>, ICashierSessionService
    {
        private readonly ObjectConverter _converter = new();

        private readonly string _endpointUrl;

        public CashierSessionServices()
        {
            _endpointUrl = $"{Endpoint.DOMAIN_CS_SESSION}/{Route.ROUTE_CS_SESSION}";
        }

        public APIResponse GetPaginationCashierSession(string? BranchId, int pageIndex, int pageSize, string? searchTerm = "")
        {
            try
            {
                string endpointUrl = _endpointUrl + $"?BranchId={BranchId}&PageIndex={pageIndex}&PageSize={pageSize}";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    endpointUrl += $"&SearchTerm={searchTerm}";
                }

                APIResponse dataResponse = this.GetAPIResponse(endpointUrl, Method.Get);

                var dataPaging = _converter.JsonToObject<PaginationModels<PaginationCashierSessionModels>>(dataResponse.data);

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

        public APIResponse InsertNewCashierSession(string userId, string userName, string branchId)
        {
            try
            {
                APIResponse dataResponse = this.GetAPIResponse(_endpointUrl, Method.Post, new
                {
                    userId,
                    userName,
                    branchId
                });

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

        public APIResponse CloseCashierSession(string? sessionId, string userName, double currentAmount, double cashAmount)
        {
            try
            {
                APIResponse dataResponse = this.GetAPIResponse(_endpointUrl, Method.Put, new
                {
                    sessionId,
                    userName,
                    currentAmount,
                    cashAmount
                });

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

        public APIResponse GetPaginationCashierSessionDetail(string? sessionId, int pageIndex, int pageSize, string? searchTerm = "")
        {
            throw new NotImplementedException();
        }
    }
}