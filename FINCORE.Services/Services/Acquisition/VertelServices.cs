using FINCORE.Models.Models.Acquisition.Vertel;
using FINCORE.Models.Models.Acquisition.Vertel.Report;
using FINCORE.Models.Models.Vertel;
using FINCORE.Models.Models.Vertel.Report;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IAcquisition;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Acquisition
{
    public class VertelServices : SendRequest<APIResponse>, IVertel
    {
        private readonly ObjectConverter converter = new();

        public async Task<APIResponse> GetDataVertel()
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_VERTEL}/{Route.ROUTE_VERTEL}/GetVertelList", Method.Get);
                data = new APIResponse
                {
                    code = dataResponse.code,
                    data = converter.JsonToList<PagingVertelModels>(dataResponse.data).ToList(),
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
                    data = new { }
                };
            }
            return data;
        }

        public async Task<APIResponse> GetGetDocField(string CMNo)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_VERTEL}/{Route.ROUTE_VERTEL}/GetDocumentField?CreditId={CMNo}", Method.Get);
                data = new APIResponse
                {
                    code = dataResponse.code,
                    data = converter.JsonToList<DocFieldModels>(dataResponse.data).ToList(),
                    status = dataResponse.status,
                    message = dataResponse.message
                };
            }
            catch (Exception ex)
            {
                data = new APIResponse
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.InnerException?.Message ?? ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
            return data;
        }

        public async Task<APIResponse> GetPagingList(string SearchTerm, string BranchId, int PageIndex, int PageSize)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/vertel/pagination?SearchTerm={SearchTerm}&BranchId={BranchId}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = converter.JsonToObject<PaginationModels<PagingVertelModels>>(dataResponse.data);

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
                    message = ex.InnerException?.Message ?? ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
            return data;
        }

        public async Task<APIResponse> GetVertelLookupNPP(string SearchTerm, int PageIndex, int PageSize, string branchId)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/vertel/lookup-credit?SearchTerm={SearchTerm}&PageIndex={PageIndex}&PageSize={PageSize}&branchId={branchId}", Method.Get);
                var dataPaging = converter.JsonToObject<PaginationModels<PagingVertelLookupModels>>(dataResponse.data);

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
                    message = ex.InnerException?.Message ?? ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
            return data;
        }

        public Task<APIResponse> InsertVertel(PagingVertelModels item)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> InsertVertel(VertelModels item)
        {
            item.OptTglTerimaMotor = true;
            item.OptTipeMotor = true;
            item.OptTenor = true;
            item.OptDpsetor = true;
            item.OptAngsuran = true;
            var data = new APIResponse();
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_VERTEL}/{Route.ROUTE_VERTEL}/InsertVertel", Method.Post, item);
                data = dataResponse;
            }
            catch (Exception ex)
            {
                data = new APIResponse
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.InnerException?.Message ?? ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
            return data;
        }

        public async Task<APIResponse> GetDataVertifikasiKonsumen(string verifikasiNo, string employeeId)
        {
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_VERTEL}/{Route.ROUTE_VERTEL}/GetDataVerifikasiKonsumen?VerifikasiNo={verifikasiNo}&EmployeeId={employeeId}", Method.Get);
                var dataList = converter.JsonToList<VertelModels>(dataResponse.data).ToList();
                VertelModels data = dataList.FirstOrDefault();

                data.strTglTerimaTagihan = data.TglTerimaTagihan?.ToString("dd/MM/yyyy");
                data.strTglKonfirmasi = data.TglKonfirmasi?.ToString("dd/MM/yyyy");

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
                    message = ex.InnerException?.Message ?? ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
        }

        public Task<APIResponse> UpdateCRUD(PagingVertelModels item)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> UpdateVertel(VertelModels item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_VERTEL}/{Route.ROUTE_VERTEL}/UpdateVertel", Method.Post, item);
                data = dataResponse;
            }
            catch (Exception ex)
            {
                data = new APIResponse
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.InnerException?.Message ?? ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
            return data;
        }

        #region Approval Proccess

        public async Task<APIResponse> GetDataApproverType(string typeApproval)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/approval-reason/byType/{typeApproval}", Method.Get);
                return new APIResponse
                {
                    code = dataResponse.code,
                    data = converter.JsonToList<VertelApprovalModels>(dataResponse.data).ToList(),
                    status = dataResponse.status,
                    message = dataResponse.message
                };
            }
            catch (Exception ex)
            {
                data = new APIResponse
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.InnerException?.Message ?? ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
            return data;
        }

        public async Task<APIResponse> ApprovalProccess(VertelApprovalModels data)
        {
            var datas = new APIResponse();
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_VERTEL}/{Route.ROUTE_VERTEL}/ApprovalAction", Method.Post, data);
                datas = dataResponse;
            }
            catch (Exception ex)
            {
                datas = new APIResponse
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.InnerException?.Message ?? ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
            return datas;
        }

        #endregion Approval Proccess

        #region Print

        public async Task<APIResponse> PrintVerificationCustomer(string transId, string employeeId, string sessBranchId)
        {
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/vertel/print/print-vertel?transId={transId}&employeeId={employeeId}&sessBranchId={sessBranchId}", Method.Get);
                var dataPaging = converter.JsonToList<PrintVertelModels>(dataResponse.data);
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
                    message = ex.InnerException?.Message ?? ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
        }

        public async Task<APIResponse> PrintVerificationCustomerDokumen(string transId)
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/vertel/print/print-vertel-dokumen?transId={transId}", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<PrintVertelDokumen>(response.data).ToList(),
                    status = response.status,
                    message = response.message
                };
            }
            catch (Exception ex)
            {
                data = new APIResponse
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.InnerException?.Message ?? ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
            return data;
        }

        #endregion Print

        public async Task<APIResponse> GetPriceAwal(string creditId)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = GetAPIResponse($"{Endpoint.DOMAIN_VERTEL}/{Route.ROUTE_VERTEL}/GetPriceAwal?creditId={creditId}", Method.Post);
                var datas = converter.JsonToList<ViewPriceAwal>(dataResponse.data);
                if (datas.Count < 1)
                {
                    dataResponse.data = new List<ViewPriceAwal>
                    {
                        new ViewPriceAwal
                        {
                            CreditId = ""
                            ,Tenor = null
                            ,TotalUppingOtr = null
                            ,NettDownPayment = null
                            ,AdminFee = null
                            ,InsuranceFee = null
                            ,BiayaProvisiIns = null
                            ,GrossDownPayment = null
                            ,JmlPembiayaan = null
                            ,AmountInstallment = null
                            ,RateValue = null
                            ,FlatRate = null
                            ,dealer_admin_fee =  null
                            ,PersenNfa = null
                        }
                    };

                    return data = new APIResponse
                    {
                        code = dataResponse.code,
                        data = converter.JsonToList<ViewPriceAwal>(dataResponse.data).ToList(),
                        status = dataResponse.status,
                        message = dataResponse.message
                    };
                }

                data = new APIResponse
                {
                    code = dataResponse.code,
                    data = converter.JsonToList<ViewPriceAwal>(dataResponse.data),
                    status = dataResponse.status,
                    message = dataResponse.message
                };
            }
            catch (Exception ex)
            {
                data = new APIResponse
                {
                    code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    message = ex.InnerException?.Message ?? ex.Message,
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }
            return data;
        }
    }
}