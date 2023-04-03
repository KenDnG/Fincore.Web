using FINCORE.Models.Models.Acquisition;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IAcquisition;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Acquisition
{
    public class CAServices : SendRequest<APIResponse>, ICAServices
    {
        private ObjectConverter converter = new ObjectConverter();

        public async Task<APIResponse> GetDataCA()
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/ca/GetCAList", Method.Get);
                data = new APIResponse
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<CAModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> GetPagingList(string SearchTerm, int PageIndex, int PageSize)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/ca/GetCAListPagination?SearchTerm={SearchTerm}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<CAModels>>(dataResponse.data);

                data = new APIResponse
                {
                    code = dataResponse.code,
                    data = dataPaging,
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