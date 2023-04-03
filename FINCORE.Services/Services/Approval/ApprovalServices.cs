using FINCORE.Models.Models.Approval;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IApproval;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Approval
{
    public class ApprovalServices : SendRequest<APIResponse>, IApprovalServices
    {
        private readonly ObjectConverter _converter = new();

        public APIResponse GetPaginationInboxApproval(
            string? employeeId, int pageIndex, int pageSize, string? searchTerm = "")
        {
            try
            {
                //string endpointUrl = $"{Endpoint.DOMAIN_MINIAPI_APPROVAL}/{Route.ROUTE_MINIAPI}/approval/pagination/inbox?EmployeeId=220080932&PageIndex={pageIndex}&PageSize={pageSize}";

                string endpointUrl = $"{Endpoint.DOMAIN_MINIAPI_APPROVAL}/{Route.ROUTE_MINIAPI_APPROVAL}/inbox-approval/pagination/inbox";

                //if (!string.IsNullOrEmpty(searchTerm))
                //    endpointUrl += $"&SearchTerm={searchTerm}";

                APIResponse dataResponse = this.GetAPIResponse(endpointUrl, Method.Post, new
                {
                    employeeId,
                    searchTerm,
                    pageIndex,
                    pageSize
                });

                var dataPaging = _converter.JsonToObject<PaginationModels<PaginationInboxApprovalModels>>(dataResponse.data);

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
                    //data = null
                };
            }
        }
    }
}