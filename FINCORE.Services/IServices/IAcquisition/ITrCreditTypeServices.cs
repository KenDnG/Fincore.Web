using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IAcquisition
{
    public interface ITrCreditTypeServices
    {
        Task<APIResponse> GetList();
    }
}