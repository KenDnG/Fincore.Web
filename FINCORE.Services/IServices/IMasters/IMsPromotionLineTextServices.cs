using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IMasters
{
    public interface IMsPromotionLineTextServices
    {
        Task<APIResponse> GetPromotionLineTextList();
    }
}