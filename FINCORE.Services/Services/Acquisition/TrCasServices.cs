using FINCORE.Models.Models;
using FINCORE.Models.Models.Acquisition.CAS;
using FINCORE.Models.Models.Acquisition.CAS.ModelHelper;
using FINCORE.Models.Models.Acquisition.CAS.Paginations;
using FINCORE.Models.Models.Acquisition.CAS.References;
using FINCORE.Models.Models.Acquisition.CM;
using FINCORE.Models.Models.Acquisition.Masters;
using FINCORE.Models.Models.Acquisition.Reports;
using FINCORE.Models.Models.Masters;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IAcquisition;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Acquisition
{
    public class TrCasServices : SendRequest<APIResponse>, ITrCasServices
    {
        private ObjectConverter converter = new ObjectConverter();

        #region Masters Data

        public async Task<APIResponse> GetRepeatOrderReason()
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/get/ROReasons?", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<MsROReasonsModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> GetMsNationality()
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/nationality/get", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsNationalityModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetCreditEvaluation()
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/evaluation/getcreditevaluations", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsEvaluationModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetCreditSource()
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/creditsource/getcreditsource", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsCreditSourceModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetCustomerSource()
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/customersource/getcustomersource", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsCustomerSourceModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetCustomerType()
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/customertype/getcustomertype", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsCustomerTypeModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetIdentityType()
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/identity/getidentitytype", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsIdentityTypeModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetIndustryType()
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/industrytype/getindustrytype", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsIndustryTypeModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetMaritalStatus()
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/marital/getmarital", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsMaritalModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetMonthlyOtherInstallment()
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/otherinstallment/get", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsMonthlyOtherInstallmentModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetMsROApplicantRelation()
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/applicantrelation/get", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsROApplicantRelationModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetMsROCategory()
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/rocategory/get", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsROCategoryModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetMsRODecision()
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/rodecision/get", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsRODecisionModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetMsROReferenceSource()
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/roreferencesource/get", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsROReferenceSourceModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetOwnershipProof()
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/ownershipproof/get", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsOwnershipProofModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetProfession()
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/profession/getprofession", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsProfessionModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetRelationship()
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/relationship/ef/getrelationship", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsRelationshipModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetResidence()
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/residence/get", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsResidenceStatusModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetSource()
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/mssource/getsource", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsSourceModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetMsPaymentPoint(int paymentTypeId)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/paymentpoint/ef/get?paymentType={paymentTypeId}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsPaymentPointModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetMsReferenceTypeName()
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/referencetype/getrefname", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsReferenceTypeModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetMsLocationById(int locationId)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/location/getlocationbyid?locationId={locationId}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsLocationDetailModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetMsLocation()
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/location/getlocation", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsLocationDetailModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetIdentityType(string customerType)
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/identity/getidentitytypebycustomertype?customerType={customerType}", Method.Get);
                data = new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsIdentityTypeModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetBankMaster()
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/get/bank", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsBankMasterModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        #endregion Masters Data

        public async Task<APIResponse> InsertTrCasMotor(TrCasModels trCasModels)
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/insert/motor", Method.Post, trCasModels);
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

        public async Task<APIResponse> UpdateTrCasMotor(TrCasModels trCasModels)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/update/motor", Method.Post, trCasModels);
                return new APIResponse
                {
                    code = response.code,
                    data = response.data,
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> InsertTrCasMobil(TrCasModels trCasModels)
        {
            var data = new APIResponse();
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/insert/car", Method.Post, trCasModels);
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

        public async Task<APIResponse> UpdateTrCasMobil(TrCasModels trCasModels)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/update/car", Method.Post, trCasModels);
                return new APIResponse
                {
                    code = response.code,
                    data = response.data,
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetTrCasByCreditId(string creditId)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/ef/get/trcas?creditid={creditId}", Method.Get);
                var d = this.converter.JsonToList<TrCasModels>(response.data).ToList();
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<TrCasModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> CheckBlacklistKTP(string ktpNo)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/validasi/checkblacklistktp?ktpNo={ktpNo}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<CheckBlacklistModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> CheckApuppt(ApupptParamModels apupptParam)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/validasi/checkapuppt", Method.Post, apupptParam);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<CheckApupptModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetDataPoolingOrder(string orderId)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/get/poolingorder?orderId={orderId}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<PoolingOrderModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetCreditIdByOrderId(string orderId)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/get/findcreditidbyorder?orderId={orderId}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = response.data,
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetNppLamaRO(string ktpNo)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/validasi/agreementold?ktpNo={ktpNo}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<PaginationAgreementNumberOldModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GenerateCreditId(string branchId)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/ef/get/generatecode?branchId={branchId}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<CreditIDResultsModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetMailToSource()
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_MASTERS}/{Route.ROUTE_MASTERS}/mailtosource/get/all", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<MsMailToSourceModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetDataReferensiSlik(string nik)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/get/datareferensi_slik?nik={nik}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToObject<FclResultSlikModels>(response.data),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetPathKTPKonsumen(string creditId)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/get/path_photo_ktp_konsumen?creditId={creditId}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<DataPhotoModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetPathKartuKeluargaKonsumen(string creditId)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/get/path_photo_kartukeluarga_konsumen?creditId={creditId}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<DataPhotoModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetPathSlipGajiKonsumen(string creditId)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/get/path_photo_slip_gaji?creditId={creditId}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<DataPhotoModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetPathKTPPasngan(string creditId)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/get/path_photo_ktp_pasangan?creditId={creditId}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<DataPhotoModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> GetPathKepemilikanRumah(string creditId)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_TRCAS}/{Route.ROUTE_TRCAS}/get/path_photo_kepemilikan_rumah?creditId={creditId}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<DataPhotoModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        public async Task<APIResponse> PrintLAHSCas(string creditId)
        {
            try
            {
                var response = this.GetAPIResponse($"{Endpoint.DOMAIN_REPORTS}/{Route.ROUTE_REPORTS}/trcas/print-lahs?creditId={creditId}", Method.Get);
                return new APIResponse
                {
                    code = response.code,
                    data = this.converter.JsonToList<PrintLAHSModels>(response.data).ToList(),
                    status = response.status,
                    message = response.message
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

        #region Pagination & Lookup

        public async Task<APIResponse> GetPaginationMsLocation(string SearchTerm, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/cas/ef/pagination/location?SearchTerm={SearchTerm}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationLocationModels>>(dataResponse.data);
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

        public async Task<APIResponse> GetPaginationMsBank(string SearchTerm, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/cas/ef/pagination/msbank?SearchTerm={SearchTerm}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationBankModels>>(dataResponse.data);
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

        public async Task<APIResponse> GetPaginationPoolingOrder(string SearchTerm, string tipeOrder, string branchId, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/cas/ef/pagination/poolingorder?SearchTerm={SearchTerm}&branchId={branchId}&tipeOrder={tipeOrder}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationPoolingOrderModels>>(dataResponse.data);
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

        public async Task<APIResponse> GetPaginationAgreementOld(string SearchTerm, string lesseeId, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/cas/ef/pagination/agreementold?SearchTerm={SearchTerm}&lesseeId={lesseeId}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationAgreementNumberOldModels>>(dataResponse.data);
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

        public async Task<APIResponse> GetPaginationNikKonsumen(string employeeName, string branchId, bool isKonsol, bool includePos, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MINIAPI_ACQUISITION}/credits/pagination/nik_konsumen?employeeName={employeeName}&branchId={branchId}&isKonsol={isKonsol}&includePos={includePos}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationNikKonsumenModels>>(dataResponse.data);
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

        #endregion Pagination & Lookup
    }
}