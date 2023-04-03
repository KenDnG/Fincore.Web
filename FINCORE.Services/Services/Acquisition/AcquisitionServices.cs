using FINCORE.Models.Models.Acquisition.Paginations;
using FINCORE.Models.Models.Acquisition.PO;
using FINCORE.Models.Models.Acquisition.Reports;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IAcquisition;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Acquisition
{
    public class AcquisitionServices : SendRequest<APIResponse>, IAcquisitionServices
    {
        private ObjectConverter converter = new ObjectConverter();

        public async Task<APIResponse> CheckUserPositionPrintPO(string employeeId)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_ACQUISITION}/check/user-allow-printpo?employeeId={employeeId}", Method.Get);
                return new APIResponse
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<PositionUserPrintPOModels>(dataResponse.data),
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

        public async Task<APIResponse> GetPaginationAcquisitionMobil(string branch_id, string SearchTerm, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/credits/pagination/mobil?branch_id={branch_id}&SearchTerm={SearchTerm}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationAcquisitionMobilModels>>(dataResponse.data);
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
                    data = null
                };
            }
        }

        public async Task<APIResponse> GetPaginationAcquisitionMotor(string branch_id, string SearchTerm, string SearchTerm1, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/credits/pagination/motor?branch_id={branch_id}&SearchTerm={SearchTerm}&SearchTerm1={SearchTerm1}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationAcquisitionMotorModels>>(dataResponse.data);
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
                    data = null
                };
            }
        }

        public async Task<APIResponse> GetPoNoByCreditId(string creditId)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_ACQUISITION}/get/pono-bycredit?creditId={creditId}", Method.Get);
                var dataPaging = this.converter.JsonToList<TrPoModels>(dataResponse.data);
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
                    data = null
                };
            }
        }

        public async Task<APIResponse> InsertTrPoSendEmail(TrPoSendToEmailModels poSendToEmailModels)
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_ACQUISITION}/insert/trpo-sendemail", Method.Post, poSendToEmailModels);
                data = response;
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

        public async Task<APIResponse> OpenCM(string poNo, string creditId)
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_ACQUISITION}/transaction/open-cm?poNo={poNo}&creditId={creditId}", Method.Post);
                data = response;
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

        public async Task<APIResponse> PrintPOMobil(string poNo, string branchId, string printBy)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/acquisition/print/po-mobil?poNo={poNo}&branchId={branchId}&printBy={printBy}", Method.Get);
                var dataPaging = this.converter.JsonToList<PrintPOMobilModels>(dataResponse.data);
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
                    data = null
                };
            }
        }

        public async Task<APIResponse> PrintPOMotor(string poNo, string branchId, string printBy)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/acquisition/print/po-motor?poNo={poNo}&branchId={branchId}&printBy={printBy}", Method.Get);
                var dataPaging = this.converter.JsonToList<PrintPOMotorModels>(dataResponse.data);
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
                    data = null
                };
            }
        }

        /// <summary>
        /// Send Email with Store Procedure
        /// </summary>
        /// <param name="poNo"></param>
        /// <returns></returns>
        public async Task<APIResponse> SendEmailPrintPO(string poNo)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_ACQUISITION}/email/sendemail-po?poNo={poNo}", Method.Get);
                var data = this.converter.JsonToList<SendEmailPrintPOModels>(dataResponse.data);
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

        public async Task<APIResponse> UpdateSumPrintPO(string poNo, string printBy)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_ACQUISITION}/update/sumprint-po?poNo={poNo}&printBy={printBy}", Method.Post);
                return data = dataResponse;
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