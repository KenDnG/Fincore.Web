using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IMaster;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Masters
{
    public class MenuTreeServices : SendRequest<APIResponse>, IMenuTreeServices
    {
        public async Task<APIResponse> GetMenuTree(string nik)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/ef/getMenuTree?NIK=" + nik, Method.Get);
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
    }
}