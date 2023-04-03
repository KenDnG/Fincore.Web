using FINCORE.Models.Models.Vertel;
using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IAcquisition
{
    internal interface IVertel
    {
        Task<APIResponse> InsertVertel(PagingVertelModels item);

        Task<APIResponse> GetDataVertel();

        Task<APIResponse> GetPriceAwal(string creditId);

        Task<APIResponse> UpdateCRUD(PagingVertelModels item);

        Task<APIResponse> GetPagingList(string SearchTerm, string BranchId, int PageIndex, int PageSize);

        Task<APIResponse> InsertVertel(VertelModels item);

        Task<APIResponse> UpdateVertel(VertelModels item);

        Task<APIResponse> GetVertelLookupNPP(string SearchTerm, int PageIndex, int PageSize, string branchId);

        Task<APIResponse> GetGetDocField(string CMNo);

        Task<APIResponse> GetDataVertifikasiKonsumen(string verifikasiNo, string employeeId);

        #region Print

        Task<APIResponse> PrintVerificationCustomer(string transId, string employeeId, string sessBranchId);

        Task<APIResponse> PrintVerificationCustomerDokumen(string transId);

        #endregion Print

        #region Approval Proccess

        Task<APIResponse> GetDataApproverType(string typeApproval);

        Task<APIResponse> ApprovalProccess(VertelApprovalModels data);

        #endregion Approval Proccess
    }
}