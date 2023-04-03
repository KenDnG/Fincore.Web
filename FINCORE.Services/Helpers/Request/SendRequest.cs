using FINCORE.Services.Helpers.Models.LDAP;
using FINCORE.Services.Helpers.Request.Interface;
using Newtonsoft.Json;
using RestSharp;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Helpers.Request
{
    public class SendRequest<T> : ISendRequest<T>
    {
        public T GetAPIResponse(string url, Method method)
        {
            var client = new RestClient();
            var request = new RestRequest(url, method);
            var response = client.Execute(request);
            var dataResponse = JsonConvert.DeserializeObject<T>(response.Content.ToString());

            return dataResponse;
        }

        public async Task<T> GetAPIResponseAsync(string url, Method method)
        {
            var client = new RestClient();
            var request = new RestRequest(url, method);
            var response = await client.ExecuteAsync(request);
            var dataResponse = JsonConvert.DeserializeObject<T>(response.Content.ToString());

            return dataResponse;
        }

        public T GetAPIResponse(string url, Method method, object reqBody, string token)
        {
            var client = new RestClient();
            var request = new RestRequest(url, method);

            request.AddHeader(BaseRequest.Headers.AUTH_TYPE, token);
            request.AddParameter(BaseRequest.Headers.CONTENT_TYPE, JsonConvert.SerializeObject(reqBody), ParameterType.RequestBody);
            var response = client.Execute(request);
            var dataResponse = JsonConvert.DeserializeObject<T>(response.Content.ToString());

            return dataResponse;
        }

        public T GetAPIResponse(string url, Method method, object reqBody)
        {
            var client = new RestClient();
            var request = new RestRequest(url, method);

            request.AddParameter(BaseRequest.Headers.CONTENT_TYPE, JsonConvert.SerializeObject(reqBody), ParameterType.RequestBody);
            var response = client.Execute(request);
            var dataResponse = JsonConvert.DeserializeObject<T>(response.Content.ToString());

            return dataResponse;
        }

        public T GetAPIResponse(string url, Method method, object reqBody, RapindoHeaderRequest RapindoHeader)
        {
            var client = new RestClient();
            var request = new RestRequest(url, method);

            request.AddParameter(BaseRequest.Headers.CONTENT_TYPE, JsonConvert.SerializeObject(reqBody), ParameterType.RequestBody);
            request.AddHeader("token_code", RapindoHeader.token_code);
            request.AddHeader("x_tag_user", RapindoHeader.x_tag_user);
            request.AddHeader("nama_cabang", RapindoHeader.nama_cabang);
            request.AddHeader("kode_cabang", RapindoHeader.kode_cabang);

            var response = client.Execute(request);
            var dataResponse = JsonConvert.DeserializeObject<T>(response.Content.ToString());

            return dataResponse;
        }

        /// <summary>
        /// LDAP Login
        /// </summary>
        /// <param name="userDomain">Computer domain or Employee NIK</param>
        /// <param name="password">Computer Password</param>
        /// <param name="companyId">Your companyID: MCF or MAF</param>
        /// <returns></returns>
        public T GetAPIResponseLDAP(string userDomain, string password, string companyId)
        {
            var reqBody = new LDAPBodyRequest { userdomain = userDomain, password = password, company = companyId };
            var client = new RestClient();
            var request = new RestRequest(Endpoint.LDAP_URL, Method.Post);
            request.AddHeader(BaseRequest.Headers.AUTH, BaseRequest.Headers.LDAP_AUTH_VALUE);
            request.AddHeader(BaseRequest.Headers.LDAP_CLIENT_KEY, BaseRequest.Headers.LDAP_CLIENT_VALUE);
            request.AddParameter(BaseRequest.Headers.CONTENT_TYPE, JsonConvert.SerializeObject(reqBody), ParameterType.RequestBody);
            var response = client.Execute(request);
            var dataResponse = JsonConvert.DeserializeObject<T>(response.Content.ToString());

            return dataResponse;
        }
    }
}