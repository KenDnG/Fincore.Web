namespace FINCORE.Services.Helpers.Request
{
    public static class BaseRequest
    {
        public struct Headers
        {
            public static readonly string CONTENT_TYPE = "application/json";
            public static readonly string AUTH_TYPE = "Bearer ";
            public static readonly string AUTH = "Authorization";
            public static readonly string LDAP_CLIENT_KEY = "client_id";
            public static readonly string LDAP_CLIENT_VALUE = "FINCORE";
            public static readonly string LDAP_AUTH_VALUE = "18789673-E4AA-4F0C-A781-4D7FC3DB1562";
        }
    }
}