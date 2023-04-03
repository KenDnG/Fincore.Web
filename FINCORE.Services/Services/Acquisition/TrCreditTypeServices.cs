using FINCORE.Models.Models.Acquisition;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IAcquisition;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Acquisition
{
    public class TrCreditTypeServices : SendRequest<APIResponse>, ITrCreditTypeServices
    {
        private ObjectConverter converter = new ObjectConverter();

        public async Task<APIResponse> GetList()
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/api/v0/services/trcredit/GetList", Method.Get);
                data = new APIResponse
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<TrCreditTypeModels>(dataResponse.data).ToList(),
                    status = dataResponse.status,
                    message = dataResponse.message
                };
            }
            catch (Exception ex)
            {
                data = new APIResponse
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.Message,
                    status = Collection.Status.FAILED,
                    data = null
                };
            }
            return data;
        }
    }
}