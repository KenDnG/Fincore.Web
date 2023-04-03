using FINCORE.Models.Models.Masters;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IMaster;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Masters
{
    public class CompanyBranchServices : SendRequest<APIResponse>, ICompanyBranchServices
    {
        private ObjectConverter converter = new ObjectConverter();

        public async Task<APIResponse> GetCompanyBranch(string company_id, string NIK)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/company_branch/ef/getCompanyBranch?company_id=" + company_id + "&NIK=" + NIK, Method.Get);
                data = dataResponse;
            }
            catch (Exception ex)
            {
                //data = new LDAPResponse
                //{
                //    err_code = Collection.Codes.INTERNAL_SERVER_ERROR,
                //    err_message = ex.Message,
                //    status = Collection.Status.FAILED,
                //    data = null
                //};
            }
            return data;
        }

        public async Task<APIResponse> GetCompanyBranchDetail(string branch_id)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/company_branch/ef/GetBranchDetail?branch_id={branch_id}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToObject<MsCompanyBranchModels>(response.data),
                    status = response.status,
                    message = response.message
                };
            }
            catch (Exception ex)
            {
                return new APIResponse
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.Message,
                    status = Collection.Status.FAILED,
                    data = null
                };
            }
        }
    }
}