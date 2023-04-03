using FINCORE.Models.Models.Acquisition;
using FINCORE.Models.Models.Acquisition.CA;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IAcquisition;
using RestSharp;
using Endpoint = FINCORE.Services.Helpers.Domain.EndpointAPI.Endpoint;
using Route = FINCORE.Services.Helpers.Domain.EndpointAPI.Route;

namespace FINCORE.Services.Services.Acquisition
{
    public class CACarServices : SendRequest<APIResponse>, ICACarServices
    {
        private ObjectConverter converter = new ObjectConverter();

        public async Task<APIResponse> Get_ca_car_detail(string credit_id, string CreatedBy)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CA}/{Route.ROUTE_CA_Car}/get-ca_car_detail?credit_id={credit_id}&CreatedBy={CreatedBy}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<CACarModels>(dataResponse.data).ToList(),
                    status = dataResponse.status,
                    message = dataResponse.message
                };
            }
            catch (Exception ex)
            {
                data = new APIResponse()
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.Message,
                    status = Collection.Status.FAILED,
                    data = null
                };
            }
            return data;
        }

        public async Task<APIResponse> Get_ms_photo_type(string credit_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CA}/{Route.ROUTE_CA_Car}/get-ms_photo_type?credit_id={credit_id}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_photo_typeModels>(dataResponse.data).ToList(),
                    status = dataResponse.status,
                    message = dataResponse.message
                };
            }
            catch (Exception ex)
            {
                data = new APIResponse()
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.Message,
                    status = Collection.Status.FAILED,
                    data = null
                };
            }
            return data;
        }

        public async Task<APIResponse> Get_Tr_CM_photo_detail(string credit_id, string photo_type_id, string photo_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CA}/{Route.ROUTE_CA_Car}/get-Tr_CM_photo_detail?credit_id={credit_id}&photo_type_id={photo_type_id}&photo_id={photo_id}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<CACarPhotoTypeModels>(dataResponse.data).ToList(),
                    status = dataResponse.status,
                    message = dataResponse.message
                };
            }
            catch (Exception ex)
            {
                data = new APIResponse()
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.Message,
                    status = Collection.Status.FAILED,
                    data = null
                };
            }
            return data;
        }

        public async Task<APIResponse> Get_ApprovalReason(string UserId, string reason_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM_Car}/get-approval_reason?UserId={UserId}&reason_id={reason_id}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ApprovalReasonModels>(dataResponse.data).ToList(),
                    status = dataResponse.status,
                    message = dataResponse.message
                };
            }
            catch (Exception ex)
            {
                data = new APIResponse()
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.Message,
                    status = Collection.Status.FAILED,
                    data = null
                };
            }
            return data;
        }

        public async Task<APIResponse> Get_History_Approval(string credit_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM_Car}/get-history_approval?credit_id={credit_id}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<history_approvalModels>(dataResponse.data).ToList(),
                    status = dataResponse.status,
                    message = dataResponse.message
                };
            }
            catch (Exception ex)
            {
                data = new APIResponse()
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.Message,
                    status = Collection.Status.FAILED,
                    data = null
                };
            }
            return data;
        }

        public async Task<APIResponse> InsertCMPhotoDetail(ViewModelCAPhotoType item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CA}/{Route.ROUTE_CA_Car}/insert-cm_photo_detail", Method.Post, item);
                data = dataResponse;
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

        public async Task<APIResponse> InsertCMOAnalisa(CACarModels item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CA}/{Route.ROUTE_CA_Car}/insert-cmo_analisa", Method.Post, item);
                data = dataResponse;
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

        public async Task<APIResponse> ApproveCM(CACarModels item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM_Car}/approve-cm", Method.Post, item);
                data = dataResponse;
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

        public async Task<APIResponse> Get_PO_No(string po_no)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-po_no?po_no={po_no}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<POModels>(dataResponse.data).ToList(),
                    status = dataResponse.status,
                    message = dataResponse.message
                };
            }
            catch (Exception ex)
            {
                data = new APIResponse()
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