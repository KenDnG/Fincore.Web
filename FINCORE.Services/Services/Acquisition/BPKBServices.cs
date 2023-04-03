using FINCORE.Models.Models.Acquisition.BPKB;
using FINCORE.Models.Models.Acquisition.BPKB.Paging;
using FINCORE.Models.Models.Acquisition.BPKB.Report;
using FINCORE.Models.Models.Users;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Models.ViewModels;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IAcquisition;
using Newtonsoft.Json;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Acquisition
{
    public class BPKBServices : SendRequest<APIResponse>, IBPKBServices
    {
        private ObjectConverter converter = new ObjectConverter();

        public async Task<APIResponse> GetDataBPKB(BPKBModels item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/EF/BPKB/CRUD/GetBPKB", Method.Post, item);
                data = new APIResponse
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToObject<ViewModelBPKB>(dataResponse.data),
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

        public async Task<APIResponse> GetPagingList(string SearchTerm, string BranchID, int PageIndex, int PageSize)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/BPKB/EF/BPKB/LIST/GetBPKBListPaginationEF?SearchTerm={SearchTerm}&BranchID={BranchID}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PagingBPKBResultModel>>(dataResponse.data);

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

        public async Task<APIResponse> InsertBPKB(ViewModelBPKB item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/EF/BPKB/CRUD/InsertBPKBEF", Method.Post, item);
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

        public async Task<APIResponse> UpdateBPKB(ViewModelBPKB item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/EF/BPKB/CRUD/UpdateBPKBEF", Method.Post, item);
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

        public async Task<APIResponse> DeleteBPKB(BPKBModels item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/DeleteBPKB", Method.Post, item);
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

        public async Task<APIResponse> GetBPKBLookupNPP(string SearchTerm, string BranchID, int PageIndex, int PageSize)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/BPKB/EF/BPKB/LIST/GetBPKBLookupNPP?SearchTerm={SearchTerm}&BranchID={BranchID}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PagingBPKBLookupNPPModel>>(dataResponse.data);

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

        public async Task<APIResponse> GetBPKBLocation(string BranchId)
        {
            BPKBModels item = new BPKBModels();
            item.BranchId = BranchId;
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/BPKBLocation/get?branchid={BranchId}", Method.Post);

                data = new APIResponse
                {
                    code = Collection.Codes.SUCCESS,
                    message = Collection.Messages.SUCCESS,
                    status = Collection.Status.SUCCESS,
                    data = JsonConvert.DeserializeObject<List<BPKBLocationModel>>(dataResponse.data.ToString())
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

        public async Task<APIResponse> SaveBPKBPinjam(ViewModelBPKB item)
        {
            var data = new APIResponse();
            try
            {
                item.BPKBPinjam.CreditId = item.BPKB.CreditId;
                item.BPKBPinjam.BranchId = item.BPKB.BranchId;
                item.BPKBPinjam.CompanyId = item.BPKB.CompanyId;
                item.BPKBPinjam.CreatedOn = DateTime.Now;
                item.BPKBPinjam.LastUpdatedOn = DateTime.Now;
                //item.BPKBPinjam.CreatedBy = "me";//from session
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/EF/BPKB/CRUD/SaveBPKBPinjamEF", Method.Post, item.BPKBPinjam);
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

        public async Task<APIResponse> GetBPKBReceiverLookup(string SearchTerm, int PageIndex, int PageSize)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/BPKBReceiver/getPaging?SearchTerm={SearchTerm}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PagingBPKBReceiverLookup>>(dataResponse.data);

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

        public async Task<APIResponse> ReEntryBPKB(ViewModelBPKB item)
        {
            var data = new APIResponse();
            try
            {
                item.BPKBPinjam.BranchId = item.BPKB.BranchId;
                item.BPKBPinjam.CreditId = item.BPKB.CreditId;
                item.BPKBPinjam.CompanyId = item.BPKB.CompanyId;
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/EF/BPKB/CRUD/ReEntryPinjamEF", Method.Post, item);
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

        public async Task<APIResponse> GetBPKBReasonList()
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/BPKBReason/get", Method.Get);

                data = new APIResponse
                {
                    code = Collection.Codes.SUCCESS,
                    message = Collection.Messages.SUCCESS,
                    status = Collection.Status.SUCCESS,
                    data = JsonConvert.DeserializeObject<List<BPKBReasonModel>>(dataResponse.data.ToString())
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

        public async Task<APIResponse> OutBPKB(ViewModelBPKB item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/EF/BPKB/CRUD/OutBPKBEF", Method.Post, item);
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

        public async Task<APIResponse> GetBPKBPagingTypeOfBureau(string SearchTerm, int PageIndex, int PageSize)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/BPKB/EF/BPKB/LIST/GetBPKBPagingTypeOfBureau?SearchTerm={SearchTerm}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PagingBPKBTypeOfBureau>>(dataResponse.data);

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

        public async Task<APIResponse> GetBPKBBureauList()
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/TypeOfServiceBureauNecessity/get", Method.Get);

                data = new APIResponse
                {
                    code = Collection.Codes.SUCCESS,
                    message = Collection.Messages.SUCCESS,
                    status = Collection.Status.SUCCESS,
                    data = JsonConvert.DeserializeObject<List<BPKBBureauModel>>(dataResponse.data.ToString())
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

        public async Task<APIResponse> GetBPKBDealerLookup(string SearchTerm, string CreditId, int PageIndex, int PageSize)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/BPKB/EF/BPKB/LIST/GetBPKBPagingDealer?SearchTerm={SearchTerm}&CreditID={CreditId}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PagingBPKBDealerModel>>(dataResponse.data);

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

        public async Task<APIResponse> GetBPKBAsuransiLookup(string SearchTerm, int PageIndex, int PageSize)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/BPKB/EF/BPKB/LIST/GetBPKBPagingAsuransi?SearchTerm={SearchTerm}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PagingBPKBAsuransiLookup>>(dataResponse.data);

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

        public async Task<APIResponse> GetCM(BPKBModels item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/EF/BPKB/CRUD/GetCM", Method.Post, item);
                data = new APIResponse
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToObject<BPKBCMModel>(dataResponse.data),
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

        public async Task<APIResponse> GetBranchDetail(string BranchID)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/company_branch/ef/GetBranchDetail?branch_id={BranchID}", Method.Get);

                data = new APIResponse
                {
                    code = Collection.Codes.SUCCESS,
                    message = Collection.Messages.SUCCESS,
                    status = Collection.Status.SUCCESS,
                    data = this.converter.JsonToObject<CompanyBranchModel>(dataResponse.data)
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

        public async Task<APIResponse> GetReportData(BPKBModels item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/EF/BPKB/REPORT/GetBastDetail", Method.Post, item);

                data = new APIResponse
                {
                    code = Collection.Codes.SUCCESS,
                    message = Collection.Messages.SUCCESS,
                    status = Collection.Status.SUCCESS,
                    data = this.converter.JsonToObject<BPKBBastDetailModel>(dataResponse.data)
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

        public async Task<APIResponse> UpdatePrintBPKB(BPKBModels item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/EF/BPKB/CRUD/UpdatePrintBPKBEF", Method.Post, item);
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

        public async Task<APIResponse> GetBPKBBiroJasaLookup(string SearchTerm, int PageIndex, int PageSize, string BranchId)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/BPKB/EF/BPKB/LIST/GetBPKBPagingBiroJasa?SearchTerm={SearchTerm}&BranchId={BranchId}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PagingBPKBBiroJasaModel>>(dataResponse.data);

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

        public async Task<APIResponse> GetBPKBKaryawanLookup(string SearchTerm, int PageIndex, int PageSize, string BranchId)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/BPKB/EF/BPKB/LIST/GetBPKBPagingKaryawan?SearchTerm={SearchTerm}&BranchId={BranchId}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PagingBPKBKaryawanModel>>(dataResponse.data);

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

        public async Task<APIResponse> RapindoBPKBIn(ViewModelBPKB item, UsersModels user, string branchid, string branchname)
        {
            RapindoHeaderRequest RapindoHeaders = new RapindoHeaderRequest();
            RapindoHeaders.token_code = user.CompanyId == "2" ? Token.TokenCodeRapindoMAFUAT : Token.TokenCodeRapindoMCFUAT;
            RapindoHeaders.x_tag_user = user.Username;
            RapindoHeaders.nama_cabang = branchname;
            RapindoHeaders.kode_cabang = branchid;

            object input = new
            {
                chassisNo = item.ChasisNo,
                engineNo = item.MachineNo,
                licensePlate = item.BPKB.CarNo,
                type = "RODA_EMPAT",
                vehicleType = item.ItemTypeName,
                manufactureYear = item.ItemYear,
                brand = item.ItemMerk,
                bpkb = item.BPKB.BpkbNo,
                reason = "BPKB IN"
            };
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.RAPINDOUATURL}RAPINDO/UpdateAssetDetail", Method.Post, input, RapindoHeaders);
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

        public async Task<APIResponse> GetBPKBSKDetail(BPKBModels item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/EF/BPKB/REPORT/GetSKDetail", Method.Post, item);

                data = new APIResponse
                {
                    code = Collection.Codes.SUCCESS,
                    message = Collection.Messages.SUCCESS,
                    status = Collection.Status.SUCCESS,
                    data = this.converter.JsonToObject<BPKBSKDetailModel>(dataResponse.data)
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

        public async Task<APIResponse> GetBPKBPinjamDetail(BPKBModels item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/EF/BPKB/REPORT/GetPinjamDetail", Method.Post, item);

                data = new APIResponse
                {
                    code = Collection.Codes.SUCCESS,
                    message = Collection.Messages.SUCCESS,
                    status = Collection.Status.SUCCESS,
                    data = this.converter.JsonToObject<BPKBPinjamDetailModel>(dataResponse.data)
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

        public async Task<APIResponse> GetBASTInDetail(BPKBModels item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/EF/BPKB/REPORT/GetBastInDetail", Method.Post, item);

                data = new APIResponse
                {
                    code = Collection.Codes.SUCCESS,
                    message = Collection.Messages.SUCCESS,
                    status = Collection.Status.SUCCESS,
                    data = this.converter.JsonToObject<BPKBBastInDetailModel>(dataResponse.data)
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

        public async Task<APIResponse> GetThirdPartyDetail(BPKBModels item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.bpkbURL}/{Route.ROUTE_BPKB}/EF/BPKB/REPORT/GetThirdPartyDetail", Method.Post, item);

                data = new APIResponse
                {
                    code = Collection.Codes.SUCCESS,
                    message = Collection.Messages.SUCCESS,
                    status = Collection.Status.SUCCESS,
                    data = this.converter.JsonToObject<BPKBThirdPartyReportModel>(dataResponse.data)
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