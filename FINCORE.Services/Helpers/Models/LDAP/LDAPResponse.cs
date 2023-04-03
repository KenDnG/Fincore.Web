using FINCORE.Models.Models.LDAP;

namespace FINCORE.Services.Helpers.Models.LDAP
{
    public class LDAPResponse
    {
        public int err_code { get; set; }
        public string err_message { get; set; }
        public string status { get; set; }
        public LDAPModels data { get; set; }
    }

    public class LDAPBodyRequest
    {
        public string userdomain { get; set; }
        public string password { get; set; }
        public string company { get; set; }
    }
}