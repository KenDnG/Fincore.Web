using FINCORE.Models.Models.Acquisition.Masters;
using FINCORE.Models.Models.Dukcapil;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.Helpers.Response.ThirdParty.Dukcapil;
using FINCORE.Services.IServices.IThirdParty;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.ThirdParty
{
    public class DukcapilService : SendRequest<APIResponse>, IDukcapilService
    {
        private ObjectConverter converter = new ObjectConverter();
        public async Task<APIResponse> GetDataDukcapil(DukcapilModels dataRequest)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_DUKCAPIL}/{Route.ROUTE_DUKCAPIL}"
                                , Method.Post, dataRequest);
                if (response is not null)
                {
                    if (response.status == "true")
                    {
                        return new APIResponse
                        {
                            code = response.code,
                            data = this.converter.JsonToList<DukcapilModels>(response.data).ToList(),
                            status = response.status,
                            message = response.message
                        };
                    }
                    else
                    {
                        return new APIResponse
                        {
                            code = response.code,
                            data = this.converter.JsonToList<DukcapilResponseError>(response.data).ToList(),
                            status = response.status,
                            message = response.message
                        };
                    }
                }
                else
                {
                    return new APIResponse
                    {
                        code = Collection.Codes.INTERNAL_SERVER_ERROR,
                        message = "Cannot getting the response. The result is null.",
                        status = Collection.Status.FAILED,
                        data = null
                    };
                }
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
