using FINCORE.Models.Models.Acquisition.NPP;
using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IAcquisition;

public interface INPPServices
{
    Task<APIResponse> GetPaginationNPP(string SearchTerm, string BranchId, int PageIndex, int PageSize);

    Task<APIResponse> GetNPPViewById(string creditId, string userId);

    Task<APIResponse> GetNPPEditById(string creditId);

    Task<APIResponse> GetOrderSourceTACByCreditId(string creditId);

    Task<APIResponse> GetOrderSourceTPByCreditId(string creditId);

    Task<APIResponse> InsertNPPMotorcyle(TrNppRequest nppRequest);

    Task<APIResponse> InsertNPPCar(TrNppRequest nppRequest);

    Task<APIResponse> UpdateNPPMotorcyle(TrNppRequest nppRequest);

    Task<APIResponse> UpdateNPPCar(TrNppRequest nppRequest);

    Task<APIResponse> GetDataProcessCMByCreditId(string creditId);

    //Lookup
    Task<APIResponse> GetPaginationLookupNppProcessCredit(string branchId, string SearchTerm, int PageIndex, int PageSize);
    Task<APIResponse> GetPaginationDealerBankRef(string branchId, string SearchTerm, int PageIndex, int PageSize);

    //Masters
    Task<APIResponse> GetDealerJobTitleList();

    Task<APIResponse> GetDealerPersonnelListByJobTitle(int jobTitleId);


    //Report Generation
    Task<APIResponse> GetReportMemorandumOfUnderstanding(string creditId);

    Task<APIResponse> GetImportantNotice(string creditId);

    Task<APIResponse> GetReportPowerOfAttorneyFidusia(string creditId);

    Task<APIResponse> GetReportPowerOfAttorney(string creditId);

    Task<APIResponse> GetReportStatementLetter(string creditId);

    Task<APIResponse> GetReportSecondStatementLetter(string creditId);

    Task<APIResponse> GetApprovalLetter(string creditId);

    Task<APIResponse> GetReportConsumentStatementLetter(string creditId);

    Task<APIResponse> GetReportNameChangeStatementLetter(string creditId);

    Task<APIResponse> CheckRapindo(string ChassisNo, string BranchName, string BranchId, string UserName, string CompanyId);
}