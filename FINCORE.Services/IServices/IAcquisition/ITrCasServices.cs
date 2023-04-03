using FINCORE.Models.Models.Acquisition.CAS;
using FINCORE.Models.Models.Acquisition.CAS.ModelHelper;
using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IAcquisition
{
    public interface ITrCasServices
    {
        Task<APIResponse> InsertTrCasMobil(TrCasModels trCasModels);

        Task<APIResponse> UpdateTrCasMobil(TrCasModels trCasModels);

        Task<APIResponse> InsertTrCasMotor(TrCasModels trCasModels);

        Task<APIResponse> UpdateTrCasMotor(TrCasModels trCasModels);

        #region Masters

        Task<APIResponse> GetBankMaster();

        Task<APIResponse> GetCreditSource();

        Task<APIResponse> GetCreditEvaluation();

        /// <summary>
        /// Get Data Narasumber
        /// </summary>
        /// <returns></returns>
        Task<APIResponse> GetSource();

        Task<APIResponse> GetMaritalStatus();

        Task<APIResponse> GetProfession();

        Task<APIResponse> GetIndustryType();

        Task<APIResponse> GetIdentityType();

        Task<APIResponse> GetOwnershipProof();

        Task<APIResponse> GetRelationship();

        Task<APIResponse> GetResidence();

        Task<APIResponse> GetCustomerType();

        Task<APIResponse> GetCustomerSource();

        Task<APIResponse> GetMonthlyOtherInstallment();

        Task<APIResponse> GetMsROApplicantRelation();

        Task<APIResponse> GetMsROCategory();

        Task<APIResponse> GetMsRODecision();

        Task<APIResponse> GetMsROReferenceSource();

        Task<APIResponse> GetMsNationality();

        Task<APIResponse> GetMsPaymentPoint(int paymentTypeId);

        Task<APIResponse> GetMsLocationById(int locationId);

        Task<APIResponse> GetMsLocation();

        Task<APIResponse> GetIdentityType(string customerType);

        Task<APIResponse> GetMailToSource();

        #endregion Masters

        Task<APIResponse> GetPaginationMsLocation(string SearchTerm, int PageIndex, int PageSize);

        Task<APIResponse> GetPaginationMsBank(string SearchTerm, int PageIndex, int PageSize);

        Task<APIResponse> GetPaginationPoolingOrder(string SearchTerm, string tipeOrder, string branchId, int PageIndex, int PageSize);

        Task<APIResponse> GetPaginationAgreementOld(string SearchTerm, string lesseeId, int PageIndex, int PageSize);

        Task<APIResponse> GetTrCasByCreditId(string creditId);

        Task<APIResponse> CheckBlacklistKTP(string ktpNo);

        Task<APIResponse> CheckApuppt(ApupptParamModels apupptParam);

        Task<APIResponse> GetCreditIdByOrderId(string orderId);

        Task<APIResponse> GetNppLamaRO(string ktpNo);

        Task<APIResponse> GenerateCreditId(string branchId);

        Task<APIResponse> GetDataReferensiSlik(string nik);

        Task<APIResponse> GetPaginationNikKonsumen(string employeeName, string branchId, bool isKonsol, bool includePos, int PageIndex, int PageSize);

        Task<APIResponse> GetPathKTPKonsumen(string creditId);

        Task<APIResponse> GetPathKartuKeluargaKonsumen(string creditId);

        Task<APIResponse> GetPathSlipGajiKonsumen(string creditId);

        Task<APIResponse> GetPathKTPPasngan(string creditId);

        Task<APIResponse> PrintLAHSCas(string creditId);

        Task<APIResponse> GetPathKepemilikanRumah(string creditId);
    }
}