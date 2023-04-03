using FINCORE.Services.Helpers.Models.LDAP;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using FINCORE.Services.IServices.ILDAP;

namespace FINCORE.Services.Services.LDAP
{
    public class LDAPServices : SendRequest<LDAPResponse>, ILDAPServices
    {
        public async Task<LDAPResponse> GetUserLDAP(string userDomain, string password, string companyId)
        {
            var data = new LDAPResponse();
            try
            {
                var dataResponse = this.GetAPIResponseLDAP(userDomain, password, companyId);
                if (dataResponse.data == null)
                {
                    data = new LDAPResponse
                    {
                        err_code = Collection.Codes.NOT_FOUND,
                        err_message = dataResponse.err_message,
                        status = Collection.Status.FAILED,
                        data = null
                    };
                }
                else if (String.IsNullOrEmpty(dataResponse.data.EmployeeId))
                {
                    data = new LDAPResponse
                    {
                        err_code = Collection.Codes.NOT_FOUND,
                        err_message = "NIK tidak ditemukan.",
                        status = Collection.Status.FAILED,
                        data = null
                    };
                }
                else
                {
                    data = dataResponse;
                }
            }
            catch (Exception ex)
            {
                data = new LDAPResponse
                {
                    err_code = Collection.Codes.INTERNAL_SERVER_ERROR,
                    err_message = ex.Message,
                    status = Collection.Status.FAILED,
                    data = null
                };
            }
            return data;
        }
    }
}