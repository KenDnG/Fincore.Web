using FINCORE.Models.Models.Acquisition.BPKB;
using FINCORE.Models.Models.Users;
using FINCORE.Services.Helpers.Models.ViewModels;
using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IAcquisition
{
    public interface IBPKBServices
    {
        Task<APIResponse> GetDataBPKB(BPKBModels item);

        Task<APIResponse> GetPagingList(string SearchTerm, string BranchID, int PageIndex, int PageSize);

        Task<APIResponse> InsertBPKB(ViewModelBPKB item);

        Task<APIResponse> UpdateBPKB(ViewModelBPKB item);

        Task<APIResponse> DeleteBPKB(BPKBModels item);

        Task<APIResponse> GetBPKBLookupNPP(string SearchTerm, string BranchID, int PageIndex, int PageSize);

        Task<APIResponse> SaveBPKBPinjam(ViewModelBPKB item);

        Task<APIResponse> GetBPKBReceiverLookup(string SearchTerm, int PageIndex, int PageSize);

        Task<APIResponse> ReEntryBPKB(ViewModelBPKB item);

        Task<APIResponse> GetBPKBReasonList();

        Task<APIResponse> OutBPKB(ViewModelBPKB item);

        Task<APIResponse> GetBPKBPagingTypeOfBureau(string SearchTerm, int PageIndex, int PageSize);//unused

        Task<APIResponse> GetBPKBBureauList();

        Task<APIResponse> GetBPKBDealerLookup(string SearchTerm, string BranchID, int PageIndex, int PageSize);

        Task<APIResponse> GetBPKBAsuransiLookup(string SearchTerm, int PageIndex, int PageSize);

        Task<APIResponse> GetBPKBBiroJasaLookup(string SearchTerm, int PageIndex, int PageSize, string BranchId);

        Task<APIResponse> GetBPKBKaryawanLookup(string SearchTerm, int PageIndex, int PageSize, string BranchId);

        Task<APIResponse> GetCM(BPKBModels item);

        Task<APIResponse> GetBranchDetail(string BranchID);

        Task<APIResponse> GetReportData(BPKBModels item);

        Task<APIResponse> GetBPKBSKDetail(BPKBModels item);

        Task<APIResponse> GetBPKBPinjamDetail(BPKBModels item);

        Task<APIResponse> GetBASTInDetail(BPKBModels item);

        Task<APIResponse> GetThirdPartyDetail(BPKBModels item);

        Task<APIResponse> UpdatePrintBPKB(BPKBModels item);

        Task<APIResponse> RapindoBPKBIn(ViewModelBPKB item, UsersModels user, string branchid, string branchname);
    }
}