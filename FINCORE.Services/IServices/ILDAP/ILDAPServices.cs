using FINCORE.Services.Helpers.Models.LDAP;

namespace FINCORE.Services.IServices.ILDAP
{
    public interface ILDAPServices
    {
        Task<LDAPResponse> GetUserLDAP(string userDomain, string password, string companyId);
    }
}