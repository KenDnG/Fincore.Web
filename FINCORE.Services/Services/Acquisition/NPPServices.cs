using FINCORE.Models.Models.Acquisition.NPP;
using FINCORE.Models.Models.Acquisition.NPP.Pagination;
using FINCORE.Models.Models.Acquisition.NPP.Report;
using FINCORE.Models.Models.Masters;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IAcquisition;
using RestSharp;

//using System.Drawing.Printing;
//using System;
//using System.Collections.Generic;
using System.Globalization;

//using System.Linq;
using System.Net;

//using System.Text;
//using System.Threading.Tasks;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Acquisition
{
    public sealed class NPPServices : SendRequest<APIResponse>, INPPServices
    {
        private ObjectConverter converter = new();
        //private static readonly HttpClient client = new HttpClient();

        public async Task<APIResponse> GetPaginationNPP(string SearchTerm, string BranchId, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/npp/pagination?SearchTerm={SearchTerm}&BranchId={BranchId}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationNPP>>(dataResponse.data);

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

        public async Task<APIResponse> GetNPPViewById(string creditId, string userId)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_NPP}/{Route.ROUTE_NPP}/viewById/{creditId}/{userId}", Method.Get);
                var data = this.converter.JsonToObject<NPPView>(dataResponse.data);

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

        public async Task<APIResponse> GetNPPEditById(string creditId)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_NPP}/{Route.ROUTE_NPP}/editById/{creditId}", Method.Get);
                var data = this.converter.JsonToObject<NPPEdit>(dataResponse.data);

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

        public async Task<APIResponse> GetOrderSourceTACByCreditId(string creditId)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_NPP}/{Route.ROUTE_NPP}/dealer-order-source-tac/manipulateDatabyCreditId/{creditId}", Method.Get);
                var data = this.converter.JsonToList<DealerOrderSourceTACView>(dataResponse.data);

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

        public async Task<APIResponse> GetOrderSourceTPByCreditId(string creditId)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_NPP}/{Route.ROUTE_NPP}/dealer-order-source-tp/manipulateDatabyCreditId/{creditId}", Method.Get);
                var data = this.converter.JsonToList<DealerOrderSourceThirdPartyView>(dataResponse.data);

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

        public async Task<APIResponse> InsertNPPMotorcyle(TrNppRequest nppRequest)
        {
            var data = new APIResponse();
            try
            {
                nppRequest = ConvertDateRequestModel(nppRequest);
                data = this.GetAPIResponse($"{Endpoint.DOMAIN_NPP}/{Route.ROUTE_NPP}/insert", Method.Post, nppRequest);
                //data = response;
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

        public async Task<APIResponse> InsertNPPCar(TrNppRequest nppRequest)
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_NPP}/{Route.ROUTE_NPP}/car/insert", Method.Post, nppRequest);
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

        public async Task<APIResponse> UpdateNPPMotorcyle(TrNppRequest nppRequest)
        {
            var data = new APIResponse();
            try
            {
                //nppRequest = ConvertDateRequestModel(nppRequest);
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_NPP}/{Route.ROUTE_NPP}/update", Method.Post, nppRequest);
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

        public async Task<APIResponse> UpdateNPPCar(TrNppRequest nppRequest)
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_NPP}/{Route.ROUTE_NPP}/car/update", Method.Post, nppRequest);
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

        public async Task<APIResponse> ApprovalNPP(TrNppRequest nppRequest)
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_NPP}/{Route.ROUTE_NPP}/approval", Method.Post, nppRequest);
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

        public async Task<APIResponse> GetDataProcessCMByCreditId(string creditId)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_NPP}/{Route.ROUTE_NPP}/CMByCreditId/{creditId}", Method.Get);
                var data = this.converter.JsonToObject<CMProcess>(dataResponse.data);

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

        public async Task<APIResponse> CheckRapindo(string ChassisNo, string BranchName, string BranchId, string UserName, string CompanyId)
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_NPP}/{Route.ROUTE_NPP}/checkrapindo?ChassisNo={ChassisNo}&BranchName={BranchName}&BranchId={BranchId}&UserName={UserName}&CompanyId={CompanyId}", Method.Post);
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

        #region Masters

        public async Task<APIResponse> CheckBillingReceivedDate(string Day)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_NPP}/{Route.ROUTE_NPP}/check-billing-date?Day={Day}", Method.Post);
                var data = this.converter.JsonToObject<ValidationBillingDate>(dataResponse.data);

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

        public async Task<APIResponse> CheckChassisCode(string creditId, string chassisCode)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_NPP}/{Route.ROUTE_NPP}/check-chassis-code?creditId={creditId}&chassisCode={chassisCode}", Method.Post);
                var data = this.converter.JsonToObject<ValidationChassisCodeModel>(dataResponse.data);

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

        public async Task<APIResponse> GetDealerJobTitleList()
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/dealer-job-title/all", Method.Get);
                var data = this.converter.JsonToList<MsDealerJobTitle>(dataResponse.data);

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

        public async Task<APIResponse> GetDealerPersonnelListByJobTitle(int jobTitleId)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/dealer-personnel/byJobTitleId/{jobTitleId}", Method.Get);
                var data = this.converter.JsonToList<MsDealerPersonnel>(dataResponse.data);

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

        public async Task<APIResponse> GetDisbursalTypeUMCListByBranchId(string branchId)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/disbursal-type-umc/byBranchId/{branchId}", Method.Get);
                var data = this.converter.JsonToList<MsDisbursalTypeUmc>(dataResponse.data);

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

        public async Task<APIResponse> GetPaginationDealerBankRef(string branchId, string SearchTerm, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/npp/dealer-bank-ref/pagination?SearchTerm={SearchTerm}&BranchId={branchId}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationDealerBankRef>>(dataResponse.data);

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

        public async Task<APIResponse> GetPaginationLookupNppProcessCredit(string branchId, string SearchTerm, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/npp/lookup-process-credit/pagination?searchTerm={SearchTerm}&branchId={branchId}&pageIndex={PageIndex}&pageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationLookupNppProcessCredit>>(dataResponse.data);

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

        public async Task<APIResponse> GetApprovalReasonListByReasonId(string reasonId)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/approval-reason/byType/{reasonId}", Method.Get);
                var data = this.converter.JsonToList<MsApprovalReason>(dataResponse.data);

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

        #endregion Masters

        #region Reports

        /// <summary>
        /// Dokumen Perjanjian Pembiayaan
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetReportMemorandumOfUnderstanding(string creditId)
        {
            try
            {
                APIResponse dataResponse = await this.GetAPIResponseAsync($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/acquisition/npp/memorandum-of-understanding/{creditId}", Method.Get);

                PrintMemorandumOfUnderstanding data = this.converter.JsonToObject<PrintMemorandumOfUnderstanding>(dataResponse.data);

                if (dataResponse.data is not null && data is null)
                    return UnprocessableResponse();

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
                    data = new { }
                };
            }
        }

        /// <summary>
        /// Dokumen Pemberitahuan Penting
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>

        public async Task<APIResponse> GetImportantNotice(string creditId)
        {
            try
            {
                APIResponse dataResponse = await this.GetAPIResponseAsync($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/acquisition/npp/important-notice/{creditId}", Method.Get);

                var data = this.converter.JsonToObject<PrintImportantNotice>(dataResponse.data);

                if (dataResponse.data is not null && data is null)
                    return UnprocessableResponse();

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
                    data = new { }
                };
            }
        }

        /// <summary>
        /// Dokumen Membebankan Fidusia
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetReportPowerOfAttorneyFidusia(string creditId)
        {
            try
            {
                var dataResponse = await this.GetAPIResponseAsync($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/acquisition/npp/power-of-attorney-fidusia/{creditId}", Method.Get);

                var data = this.converter.JsonToObject<PrintPowerOfAttorneyFidusia>(dataResponse.data);

                if (dataResponse.data is not null && data is null)
                    return UnprocessableResponse();

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
                    data = new { }
                };
            }
        }

        /// <summary>
        /// Dokumen Surat Kuasa
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetReportPowerOfAttorney(string creditId)
        {
            try
            {
                var dataResponse = await this.GetAPIResponseAsync($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/acquisition/npp/power-of-attorney/{creditId}", Method.Get);

                var data = this.converter.JsonToObject<PrintPowerOfAttorney>(dataResponse.data);

                if (dataResponse.data is not null && data is null)
                    return UnprocessableResponse();

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
                    data = new { }
                };
            }
        }

        /// <summary>
        /// Surat Pernyataan
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetReportStatementLetter(string creditId)
        {
            try
            {
                var dataResponse = await this.GetAPIResponseAsync($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/acquisition/npp/statement-letter/{creditId}", Method.Get);

                var data = this.converter.JsonToObject<PrintStatementLetter>(dataResponse.data);

                if (dataResponse.data is not null && data is null)
                    return UnprocessableResponse();

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
                    data = { }
                };
            }
        }

        /// <summary>
        /// Surat Pernyataan Halaman 2
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetReportSecondStatementLetter(string creditId)
        {
            try
            {
                var dataResponse = await this.GetAPIResponseAsync($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/acquisition/npp/statement-letter-second/{creditId}", Method.Get);

                var data = this.converter.JsonToObject<PrintSecondStatementLetter>(dataResponse.data);

                if (dataResponse.data is not null && data is null)
                    return UnprocessableResponse();

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
                    data = new { }
                };
            }
        }

        /// <summary>
        /// Surat Persetujuan
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetApprovalLetter(string creditId)
        {
            try
            {
                var dataResponse = await this.GetAPIResponseAsync($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/acquisition/npp/approval-letter/{creditId}", Method.Get);

                var data = this.converter.JsonToObject<PrintApprovalLetter>(dataResponse.data);

                if (dataResponse.data is not null && data is null)
                    return UnprocessableResponse();

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
                    data = new { }
                };
            }
        }

        /// <summary>
        /// Surat pernyataan dokumen
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetReportConsumentStatementLetter(string creditId)
        {
            try
            {
                var dataResponse = await this.GetAPIResponseAsync($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/acquisition/npp/consument-statement-letter/{creditId}", Method.Get);

                var data = this.converter.JsonToObject<PrintConsumentStatementLetter>(dataResponse.data);

                if (dataResponse.data is not null && data is null)
                    return UnprocessableResponse();

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
                    data = new { }
                };
            }
        }

        /// <summary>
        /// Surat Pernyataan Balik Nama BPKB
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetReportNameChangeStatementLetter(string creditId)
        {
            try
            {
                var dataResponse = await this.GetAPIResponseAsync($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/acquisition/npp/name-change-statement-letter/{creditId}", Method.Get);

                var data = this.converter.JsonToObject<PrintNameChangeStatementLetter>(dataResponse.data);

                if (dataResponse.data is not null && data is null)
                    return UnprocessableResponse();

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
                    data = new { }
                };
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetReportNameChangeReceipt(string creditId)
        {
            try
            {
                var dataResponse = await this.GetAPIResponseAsync($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/acquisition/npp/name-change-receipt/{creditId}", Method.Get);
                var data = this.converter.JsonToObject<PrintNameChangeReceipt>(dataResponse.data);

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
                    data = new { }
                };
            }
        }

        /// <summary>
        /// Save log when user download/ print document.
        /// if user make download/ print one spesific document more than one, counter will be increased by 1 (handle by SP)
        /// </summary>
        /// <param name="logParam"></param>
        /// <returns></returns>
        public APIResponse InsertReportLog(TrNppPrintRequest logParam)
        {
            try
            {
                string endpointUrl = $"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/acquisition/npp/nppprintlog";

                APIResponse dataResponse = this.GetAPIResponse(endpointUrl, Method.Post, new
                {
                    creditId = logParam.CreditId,
                    userName = logParam.UserName,
                    isPrintPK = logParam.IsPrintPK,
                    isPrintFidusia = logParam.IsPrintFidusia,
                    isPrintAkad = logParam.IsPrintAkad
                });

                return new APIResponse
                {
                    code = dataResponse.code,
                    data = new { },
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
                    data = new { }
                };
            }
        }

        #endregion Reports

        #region Function

        private static TrNppRequest ConvertDateRequestModel(TrNppRequest npp)
        {
            npp.BillReceivedDate = DateTime.Parse(npp.strBillReceivedDate
                                        , CultureInfo.CreateSpecificCulture("fr-FR")
                                        , DateTimeStyles.NoCurrentDateDefault);
            npp.BillReceiptDate = DateTime.Parse(npp.strBillReceiptDate
                                        , CultureInfo.CreateSpecificCulture("fr-FR")
                                        , DateTimeStyles.NoCurrentDateDefault);
            npp.DownPaymentReceiptDate = DateTime.Parse(npp.strDownPaymentReceiptDate
                                        , CultureInfo.CreateSpecificCulture("fr-FR")
                                        , DateTimeStyles.NoCurrentDateDefault);
            npp.ReceiptDate = DateTime.Parse(npp.strReceiptDate
                                        , CultureInfo.CreateSpecificCulture("fr-FR")
                                        , DateTimeStyles.NoCurrentDateDefault);
            npp.BpkbLetterDate = DateTime.Parse(npp.strBpkbLetterDate
                                        , CultureInfo.CreateSpecificCulture("fr-FR")
                                        , DateTimeStyles.NoCurrentDateDefault);
            npp.BastDate = DateTime.Parse(npp.strBastDate
                                        , CultureInfo.CreateSpecificCulture("fr-FR")
                                        , DateTimeStyles.NoCurrentDateDefault);
            npp.InstallmentDate = DateTime.Parse(npp.strInstallmentDate
                                        , CultureInfo.CreateSpecificCulture("fr-FR")
                                        , DateTimeStyles.NoCurrentDateDefault);
            npp.ConsumenInstallmentDate = npp.InstallmentDate;

            return npp;
        }

        private static APIResponse UnprocessableResponse()
        {
            return new APIResponse
            {
                code = (int)HttpStatusCode.UnprocessableEntity,
                data = new { },
                status = HttpStatusCode.UnprocessableEntity.ToString()
            };
        }

        #endregion Function
    }
}