using FINCORE.Models.Models.Acquisition.PO;
using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IAcquisition
{
    public interface IAcquisitionServices
    {
        Task<APIResponse> GetPaginationAcquisitionMobil(string branch_id, string SearchTerm, int PageIndex, int PageSize);

        Task<APIResponse> GetPaginationAcquisitionMotor(string branch_id, string SearchTerm, string SearchTerm1, int PageIndex, int PageSize);

        Task<APIResponse> PrintPOMobil(string poNo, string branchId, string printBy);

        Task<APIResponse> PrintPOMotor(string poNo, string branchId, string printBy);

        Task<APIResponse> GetPoNoByCreditId(string creditId);

        Task<APIResponse> InsertTrPoSendEmail(TrPoSendToEmailModels poSendToEmailModels);

        Task<APIResponse> SendEmailPrintPO(string poNo);

        Task<APIResponse> UpdateSumPrintPO(string poNo, string printBy);

        Task<APIResponse> OpenCM(string poNo, string creditId);

        Task<APIResponse> CheckUserPositionPrintPO(string employeeId);
    }
}