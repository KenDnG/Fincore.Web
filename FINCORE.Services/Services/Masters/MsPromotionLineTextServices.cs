using FINCORE.Models.Models.Masters;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IMasters;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Masters
{
    public class MsPromotionLineTextServices : SendRequest<APIResponse>, IMsPromotionLineTextServices
    {
        private ObjectConverter converter = new ObjectConverter();
        private static readonly HttpClient client = new HttpClient();

        public async Task<APIResponse> GetPromotionLineTextList()
        {
            try
            {
                var dataResponse = await this.GetAPIResponseAsync($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/promotion-line-text/all", Method.Get);
                var data = this.converter.JsonToList<MsPromotionLineTextModel>(dataResponse.data);

                return new APIResponse
                {
                    code = dataResponse.code,
                    data = data,
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
                    data = null
                };
            }
        }
    }
}