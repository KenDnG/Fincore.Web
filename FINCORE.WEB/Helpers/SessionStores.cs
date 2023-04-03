using FINCORE.Models.Models.LDAP;
using FINCORE.Models.Models.Masters;
using FINCORE.Models.Models.Users;
using static FINCORE.Services.Helpers.Response.Collection;

namespace FINCORE.WEB.Helpers
{
    public static class SessionStores
    {
        private static readonly IHttpContextAccessor _httpContext;

        public static string GetBranchId()
        {
            return Sessions.GetSessionFromJson<string>(_httpContext.HttpContext.Session, SessionIdentity.BRANCH_ID);
        }

        public static string GetBranchName()
        {
            return Sessions.GetSessionFromJson<string>(_httpContext.HttpContext.Session, SessionIdentity.BRANCH_NAME);
        }

        public static string GetMenuAccess()
        {
            return Sessions.GetSessionFromJson<string>(_httpContext.HttpContext.Session, SessionIdentity.USER_MENU_ACCESS);
        }

        public static LDAPModels GetUserLdap()
        {
            return Sessions.GetSessionFromJson<LDAPModels>(_httpContext.HttpContext.Session, SessionIdentity.LDAP_KEY_NAME);
        }

        public static CompanyBranchResponse GetCompanyCredential()
        {
            var companies = Sessions.GetSessionFromJson<CompanyBranchResponse>(_httpContext.HttpContext.Session, SessionIdentity.COMPANY_CREDENTIAL);
            return companies;
        }

        public static UsersModels GetUserCredential()
        {
            return Sessions.GetSessionFromJson<UsersModels>(_httpContext.HttpContext.Session, SessionIdentity.USER_CREDENTIAL);
        }
    }
}