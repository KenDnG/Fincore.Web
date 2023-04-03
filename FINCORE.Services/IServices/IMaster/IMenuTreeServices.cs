using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IMaster
{
    public interface IMenuTreeServices
    {
        Task<APIResponse> GetMenuTree(string nik);
    }
}