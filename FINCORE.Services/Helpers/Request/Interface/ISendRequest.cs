using RestSharp;

namespace FINCORE.Services.Helpers.Request.Interface
{
    public interface ISendRequest<T>
    {
        T GetAPIResponse(string url, Method method);

        T GetAPIResponse(string url, Method method, object reqBody, string token);

        T GetAPIResponseLDAP(string userDomain, string password, string companyId);

        T GetAPIResponse(string url, Method method, object reqBody);

        Task<T> GetAPIResponseAsync(string url, Method method);
    }
}