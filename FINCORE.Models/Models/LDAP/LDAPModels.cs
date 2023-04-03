using FINCORE.Models.Abstract;

namespace FINCORE.Models.Models.LDAP
{
    public class LDAPModels : BaseClass
    {
        public LDAPModels()
        {
            this.isError = 0;
            this.errorMessage = "";
        }

        public LDAPModels(int isError, string errorMessage)
        {
            this.isError = isError;
            this.errorMessage = errorMessage;
        }

        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string GivenName { get; set; }
        public string SamAccountName { get; set; }
        public string Sid { get; set; }
        public string UserPrincipalName { get; set; }
        public DistinguishedNameList DistinguishedNameList { get; set; }
        public string EmailAddress { get; set; }
        public string EmployeeId { get; set; }
    }

    public class DistinguishedNameList
    {
        public string[] CN { get; set; }
        public string[] OU { get; set; }
        public string[] DC { get; set; }
    }
}