using FINCORE.Models.Models.Acquisition;
using FINCORE.Models.Models.Acquisition.CM;
using FINCORE.Models.Models.Acquisition.CM.Paginations;
using FINCORE.Services.Helpers.Models.Pagination;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.IAcquisition;
using RestSharp;
using Endpoint = FINCORE.Services.Helpers.Domain.EndpointAPI.Endpoint;
using Route = FINCORE.Services.Helpers.Domain.EndpointAPI.Route;

namespace FINCORE.Services.Services.Acquisition
{
    public class CMServices : SendRequest<APIResponse>, ICMServices
    {
        private ObjectConverter converter = new ObjectConverter();

        #region Save

        public async Task<APIResponse> InsertCM(ViewModelCM item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/insert-cm", Method.Post, item);

                List<ResultSaveModelCM> data2 = this.converter.JsonToList<ResultSaveModelCM>(dataResponse.data).ToList();
                
                if (data2[0].error_number == 1)
                {
                    data = new APIResponse()
                    {
                        code = Collection.Codes.INTERNAL_SERVER_ERROR,
                        message = data2[0].error_message,
                        status = Collection.Status.FAILED,
                        data = null
                    };
                }
                else
                {
                    data = new APIResponse()
                    {
                        code = dataResponse.code,
                        data = this.converter.JsonToList<ResultSaveModelCM>(dataResponse.data).ToList(),
                        status = dataResponse.status,
                        message = dataResponse.message,
                    };
                }
                

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

        public async Task<APIResponse> InsertCMPhotoDetail(ViewModelCMPhotoType item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/insert-cm_photo_detail", Method.Post, item);
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

        public async Task<APIResponse> RFACM(ViewModelCMPhotoType item)
        {

            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/rfa-cm", Method.Post, item);

				List<ResultSaveModelCM> data2 = this.converter.JsonToList<ResultSaveModelCM>(dataResponse.data).ToList();

				if (data2[0].error_number == 1)
				{
					data = new APIResponse()
					{
						code = Collection.Codes.INTERNAL_SERVER_ERROR,
						message = data2[0].error_message,
						status = Collection.Status.FAILED,
						data = null
					};
				}
				else
				{
					data = new APIResponse()
					{
						code = dataResponse.code,
						data = this.converter.JsonToList<ResultSaveModelCM>(dataResponse.data).ToList(),
						status = dataResponse.status,
						message = dataResponse.message,
					};
				}

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

        public async Task<APIResponse> ApproveCM(CMModels item)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/approve-cm", Method.Post, item);
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

        public async Task<APIResponse> SaveSkalaResiko(RiskScaleResultModels RiskScaleResult)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/save-risk-scale", Method.Post, RiskScaleResult);
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

        #endregion Save

        #region GetData
        public async Task<APIResponse> Get_STNK_status()
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-STNK_status?", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<STNKStatusModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_plat_prefix(string branch_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-plat-prefix?branch_id={branch_id}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<branchprefixplatModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_HistoryApproval(string credit_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-history_approval?credit_id={credit_id}", Method.Get);
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

        public async Task<APIResponse> Get_ApprovalReason(string UserId, string reason_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-approval_reason?UserId={UserId}&reason_id={reason_id}", Method.Get);
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

        public async Task<APIResponse> Get_Tr_CM(string credit_id, string CreatedBy)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-Tr_CM?credit_id={credit_id}&CreatedBy={CreatedBy}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<CMModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_asset_kind(string asset_kind_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_asset_kind?asset_kind_id={asset_kind_id}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_asset_kindModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_Installment(string asset_kind_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM_Car}/get-installment?asset_kind_id={asset_kind_id}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<InstallmentModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_tipe_perantara()
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-tipe_perantara?", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<tipe_perantaraModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_account_owner()
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-account_owner?", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<account_ownerModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_application_type()
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_application_type?", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_application_typeModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_product_finance()
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_product_finance?", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_product_financeModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_item_brand(string item_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_item_brand?item_id={item_id}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_item_brandModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_asset_kind_class(string item_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_asset_kind_class?item_id={item_id}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_asset_kind_classModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_year(string item_id, string Item_Brand_Id, string asset_kind_class_id, string asset_type_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-year?item_id={item_id}&Item_Brand_Id={Item_Brand_Id}&asset_kind_class_id={asset_kind_class_id}&asset_type_id={asset_type_id}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<CMYearModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_product(string item_id)
        {
            var data = new APIResponse();
            try
            {
                string where = "-";

                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_product?where={where}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_productModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_product_marketing(string company_id, string asset_kind_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_product_marketing?company_id={company_id}&asset_kind_id={asset_kind_id}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_product_marketingModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_STNK_name(string where)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_STNK_name?where={where}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_stnk_nameModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_provenance_purchase_order(string where)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_provenance_purchase_order?where={where}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_provenance_purchase_orderModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_usage_type(string where)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_usage_type?where={where}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_usage_typeModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_ar(string where)
        {
            var data = new APIResponse();
            try
            {
                where = "-";
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_ar?where={where}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_arModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_insurance_cover_type(string where)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_insurance_cover_type?where=001", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_insurance_cover_typeModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_insurance_type(string where)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_insurance_type?where={where}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_insurance_typeModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ms_interest_rate_type(string where)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_interest_rate_type?where={where}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_interest_rate_typeModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_InsuranceFee(string asset_kind_id, string OTR, string CompanyId, string BranchId, string Tenor)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-InsuranceFee?asset_kind_id={asset_kind_id}&OTR={OTR}&CompanyId={CompanyId}&BranchId={BranchId}&Tenor={Tenor}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<InsuranceFeeModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_MarketPrice(string asset_kind_id, string CompanyId, string BranchId, string asset_type_id, string Year, string credit_id, string tipe_kerja_sama, string status_stnk_id, string Faktur_BPKB, string destination_bank_account_id_umc)
        {
            var data = new APIResponse();
            try
            {
                if (tipe_kerja_sama is null)
                {
                    tipe_kerja_sama = "0";
                }

                if (destination_bank_account_id_umc is null)
                {
                    destination_bank_account_id_umc = "0";
                }

                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-MarketPrice?asset_kind_id={asset_kind_id}&CompanyId={CompanyId}&BranchId={BranchId}&asset_type_id={asset_type_id}&Year={Year}&credit_id={credit_id}&tipe_kerja_sama={tipe_kerja_sama}&status_stnk_id={status_stnk_id}&Faktur_BPKB={Faktur_BPKB}&destination_bank_account_id_umc={destination_bank_account_id_umc}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<MarketPriceModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_ChasisCode(string year)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ChasisCode?year={year}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToObject<ChasisCodeModels>(dataResponse.data),
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

        public async Task<APIResponse> Get_ms_general_parameter(string parameter)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_general_parameter?parameter={parameter}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<ms_general_parameterModels>(dataResponse.data).ToList(),
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
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-Tr_CM_photo_detail?credit_id={credit_id}&photo_type_id={photo_type_id}&photo_id={photo_id}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<CMPhotoTypeModels>(dataResponse.data).ToList(),
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
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-ms_photo_type?credit_id={credit_id}", Method.Get);
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

        public async Task<APIResponse> Get_PO_No(string credit_id)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-po_no?credit_id={credit_id}", Method.Get);

                data = new APIResponse
                {
                    code = Collection.Codes.SUCCESS,
                    message = Collection.Messages.SUCCESS,
                    status = Collection.Status.SUCCESS,
                    data = this.converter.JsonToObject<POModels>(dataResponse.data)
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

        public async Task<APIResponse> Get_SkalaResiko(string EmployeeId)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-RiskScale?EmployeeId={EmployeeId}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<RiskScaleModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> Get_SkalaResikoValue(string where)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-RiskScaleValue?where={where}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<RiskScaleValueModels>(dataResponse.data).ToList(),
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

        public async Task<APIResponse> GetBankNameDestination(string destination_bank_account_id_umc)
        {
            var data = new APIResponse();
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_CM}/{Route.ROUTE_CM}/get-bank_name_destination?destination_bank_account_id_umc={destination_bank_account_id_umc}", Method.Get);
                data = new APIResponse()
                {
                    code = dataResponse.code,
                    data = this.converter.JsonToList<BankNameDestinationModels>(dataResponse.data).ToList(),                    
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

        #endregion GetData

        #region Pagination & Lookup

        public async Task<APIResponse> GetPaginationItem(string SearchTerm, string item_id, string item_brand_id, string asset_kind_class_id, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MiniAPI_CM}/get-paginationitem?SearchTerm={SearchTerm}&item_id={item_id}&item_brand_id={item_brand_id}&asset_kind_class_id={asset_kind_class_id}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationItemModels>>(dataResponse.data);
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

        public async Task<APIResponse> GetPaginationDealer(string SearchTerm, string item_id, string is_item_new, string branch_id, string item_merk, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MiniAPI_CM}/get-paginationdealer?SearchTerm={SearchTerm}&item_id={item_id}&is_item_new={is_item_new}&branch_id={branch_id}&item_merk={item_merk}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationDealerModels>>(dataResponse.data);
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

        public async Task<APIResponse> GetPaginationSurveyor(string SearchTerm, string item_id, string branch_id, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MiniAPI_CM}/get-paginationsurveyor?SearchTerm={SearchTerm}&item_id={item_id}&branch_id={branch_id}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationSurveyorModels>>(dataResponse.data);
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

        public async Task<APIResponse> GetPaginationMarketingHead(string SearchTerm, string item_id, string branch_id, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MiniAPI_CM}/get-paginationmarketinghead?SearchTerm={SearchTerm}&item_id={item_id}&branch_id={branch_id}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationMarketingHeadModels>>(dataResponse.data);
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

        public async Task<APIResponse> GetPaginationCGSNo(string SearchTerm, string BranchId, string CompanyId, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MiniAPI_CM}/get-paginationCGSNo?SearchTerm={SearchTerm}&BranchId={BranchId}&CompanyId={CompanyId}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationCGSNoModels>>(dataResponse.data);
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

        public async Task<APIResponse> GetPaginationOldNPP(string SearchTerm, string BranchId, string CompanyId, string ItemMerkTypeID, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MiniAPI_CM}/get-paginationOldNPP?SearchTerm={SearchTerm}&BranchId={BranchId}&CompanyId={CompanyId}&ItemMerkTypeID={ItemMerkTypeID}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationOldNPPModels>>(dataResponse.data);
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

        public async Task<APIResponse> GetPaginationPerantara(string SearchTerm, string BranchId, string CompanyId, string TipePerantara, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MiniAPI_CM}/get-paginationPerantara?SearchTerm={SearchTerm}&BranchId={BranchId}&CompanyId={CompanyId}&TipePerantara={TipePerantara}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationPerantaraModels>>(dataResponse.data);
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

        public async Task<APIResponse> GetPaginationBankName(string SearchTerm, string BranchId, string CompanyId, string PemilikRekening, int PageIndex, int PageSize)
        {
            try
            {
                var dataResponse = this.GetAPIResponse($"{Endpoint.DOMAIN_MINIAPI}/{Route.ROUTE_MiniAPI_CM}/get-paginationBankName?SearchTerm={SearchTerm}&BranchId={BranchId}&CompanyId={CompanyId}&PemilikRekening={PemilikRekening}&PageIndex={PageIndex}&PageSize={PageSize}", Method.Get);
                var dataPaging = this.converter.JsonToObject<PaginationModels<PaginationBankNameModels>>(dataResponse.data);
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