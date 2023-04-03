using FINCORE.Models.Models.Acquisition.CM;
using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IAcquisition
{
    public interface ICMServices
    {
        #region Save

        Task<APIResponse> InsertCM(ViewModelCM item);

        Task<APIResponse> InsertCMPhotoDetail(ViewModelCMPhotoType item);

        Task<APIResponse> ApproveCM(CMModels item);

        Task<APIResponse> RFACM(ViewModelCMPhotoType item);

        Task<APIResponse> SaveSkalaResiko(RiskScaleResultModels RiskScaleResult);

        #endregion Save

        #region GetData

        Task<APIResponse> Get_ApprovalReason(string UserId, string reason_id);

        Task<APIResponse> Get_HistoryApproval(string credit_id);

        Task<APIResponse> Get_tipe_perantara();

        Task<APIResponse> Get_account_owner();

        Task<APIResponse> Get_Tr_CM(string credit_id, string CreatedBy);

        Task<APIResponse> Get_ms_asset_kind(string asset_kind_id);

        Task<APIResponse> Get_ms_application_type();

        Task<APIResponse> Get_ms_product_finance();

        Task<APIResponse> Get_ms_item_brand(string item_id);

        Task<APIResponse> Get_ms_asset_kind_class(string item_id);

        Task<APIResponse> Get_year(string item_id, string Item_Brand_Id, string asset_kind_class_id, string asset_type_id);

        Task<APIResponse> Get_ms_product(string item_id);

        Task<APIResponse> Get_ms_product_marketing(string company_id, string asset_kind_id);

        Task<APIResponse> Get_ms_STNK_name(string where);

        Task<APIResponse> Get_ms_provenance_purchase_order(string where);

        Task<APIResponse> Get_ms_usage_type(string where);

        Task<APIResponse> Get_ms_ar(string where);

        Task<APIResponse> Get_ms_insurance_cover_type(string where);

        Task<APIResponse> Get_ms_insurance_type(string where);

        Task<APIResponse> Get_ms_interest_rate_type(string where);

        Task<APIResponse> Get_InsuranceFee(string asset_kind_id, string OTR, string CompanyId, string BranchId, string Tenor);

        Task<APIResponse> Get_MarketPrice(string asset_kind_id, string CompanyId, string BranchId, string asset_type_id, string Year, string credit_id, string tipe_kerja_sama, string status_stnk_id, string Faktur_BPKB, string destination_bank_account_id_umc);

        Task<APIResponse> Get_Tr_CM_photo_detail(string credit_id, string photo_type_id, string photo_id);

        Task<APIResponse> Get_ms_photo_type(string credit_id);

        Task<APIResponse> Get_ms_general_parameter(string parameter);

        Task<APIResponse> GetBankNameDestination(string destination_bank_account_id_umc);

        Task<APIResponse> Get_STNK_status();

        #endregion GetData

        #region Get Lookup Data

        Task<APIResponse> GetPaginationItem(string SearchTerm, string item_id, string item_brand_id, string asset_kind_class_id, int PageIndex, int PageSize);

        Task<APIResponse> GetPaginationDealer(string SearchTerm, string item_id, string is_item_new, string branch_id, string item_merk, int PageIndex, int PageSize);

        Task<APIResponse> GetPaginationSurveyor(string SearchTerm, string item_id, string branch_id, int PageIndex, int PageSize);

        Task<APIResponse> GetPaginationMarketingHead(string SearchTerm, string item_id, string branch_id, int PageIndex, int PageSize);

        Task<APIResponse> GetPaginationCGSNo(string SearchTerm, string BranchId, string CompanyId, int PageIndex, int PageSize);

        Task<APIResponse> GetPaginationOldNPP(string SearchTerm, string BranchId, string CompanyId, string ItemMerkTypeID, int PageIndex, int PageSize);

        Task<APIResponse> GetPaginationPerantara(string SearchTerm, string BranchId, string CompanyId, string TipePerantara, int PageIndex, int PageSize);

        Task<APIResponse> GetPaginationBankName(string SearchTerm, string BranchId, string CompanyId, string PemilikRekening, int PageIndex, int PageSize);

        #endregion Get Lookup Data
    }
}