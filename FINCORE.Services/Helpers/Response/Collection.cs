namespace FINCORE.Services.Helpers.Response
{
    public static class Collection
    {
        public struct Codes
        {
            public static readonly int SUCCESS = 200;
            public static readonly int CREATED = 201;
            public static readonly int NOT_CONTENT = 204;

            public static readonly int BAD_REQUEST = 400;
            public static readonly int UNAUTHORIZED = 401;
            public static readonly int FORBIDDEN = 403;
            public static readonly int NOT_FOUND = 404;
            public static readonly int METHOD_NOT_ALLOWED = 405;
            public static readonly int REQUEST_TIMEOUT = 408;
            public static readonly int UNSUPPORTED_MEDIA_TYPE = 415;
            public static readonly int LOCKED = 423;
            public static readonly int TO_MANY_REQUESTS = 429;

            public static readonly int INTERNAL_SERVER_ERROR = 500;
            public static readonly int BAD_GATEWAY = 502;
            public static readonly int SERVICE_UNAVAILABLE = 503;
            public static readonly int GATEWAY_TIMEOUT = 504;
        }

        public struct Messages
        {
            public static readonly string SUCCESS = "OK";
            public static readonly string CREATED = "Created";
            public static readonly string NO_CONTENT = "No Content";

            public static readonly string BAD_REQUEST = "Bad Request";
            public static readonly string UNAUTHORIZED = "Unauthorized";
            public static readonly string FORBIDDEN = "Forbidden";
            public static readonly string NOT_FOUND = "Not Found";
            public static readonly string METHOD_NOT_ALLOWED = "Method Not Allowed";
            public static readonly string REQUEST_TIMEOUT = "Request Timeout";
            public static readonly string UNSUPPORTED_MEDIA_TYPE = "Unsupported Media Type";
            public static readonly string LOCKED = "Locked";
            public static readonly string TO_MANY_REQUESTS = "To Many Request";

            public static readonly string INTERNAL_SERVER_ERROR = "Internak Server Error";
            public static readonly string BAD_GATEWAY = "Bad Gateway";
            public static readonly string SERVICE_UNAVAILABLE = "Service Unavailable";
            public static readonly string GATEWAY_TIMEOUT = "Gateway Timeout";
        }

        public struct Status
        {
            public static readonly string SUCCESS = "Success";
            public static readonly string FAILED = "Failed";
            public static readonly string WARNING = "Warning";
            public static readonly string INFO = "Information"; 
            public static readonly string ERROR_THEN_ROUTE = "ErrorThenRoute";
            public static readonly string WARNING_THEN_ROUTE = "WarningThenRoute";

        }

        public struct ErrorState
        {
            public static readonly int NOT_ERROR = 0;
            public static readonly int ERROR = 1;
        }

        public struct SessionIdentity
        {
            public static readonly string ISEDIT_KEY_NAME = "IsEdit";
            public static readonly string LDAP_KEY_NAME = "SessionUser";
            public static readonly string BRANCH_NAME = "branch_name";
            public static readonly string BRANCH_ID = "branch_id";
            public static readonly string USER_MENU_ACCESS = "access";
            public static readonly string COMPANY_CREDENTIAL = "CompanyCredentials";
            public static readonly string USER_CREDENTIAL = "UserCredentials";
            public static readonly string PROMOTION_LINE_TEXT = "PromotionLineText";
            public static readonly string CREDIT_ID = "credit_id";
            public static readonly string ISVIEW = "IsView";
            public static readonly string LOCATION_DATA_TEMP = "DataLocationTemp";
            public static readonly string AGREEMENT_OLD_DATA_TEMP = "DataAggreementOldTemp";
            public static readonly string EXPIRED_SESSION_STATUS = "EXP_SESSION_STATUS";
            public static readonly string EXPIRED_SESSION_MESSAGES = "EXP_SESSION_MESSAGES";
        }

        public struct CustomerTypes
        {
            public static readonly string CORPORATE_TYPE_CODE = "C";
            public static readonly string INDIVIDUAL_TYPE_CODE = "P";
        }

        public struct ItemId
        {
            public static readonly string MOTOR_CODE = "001";
            public static readonly string MOBIL_CODE = "002";
            public static readonly string MOTOR_NAME = "Motor";
            public static readonly string MOBIL_NAME = "Mobil";
        }

        public struct SendingStatus
        {
            public static readonly string STATUS_SENDED = "Sended";
            public static readonly string STATUS_NOT_SENDED = "NotSended";
        }

        public struct CompanyId
        {
            public static readonly string MCF = "3";
            public static readonly string MAF = "2";
            public static readonly string COMPANY_ID = Environment.GetEnvironmentVariable("COMPANY_ID_ENVIRONMENT"); 
            public static readonly string COMPANY_NAME = Environment.GetEnvironmentVariable("COMPANY_NAME_ENVIRONMENT"); 
        }

        public struct StatusCredits
        {
            public struct Code
            {
                public static readonly string APPROVE = "A";
                public static readonly string DRAFT = "D";
                public static readonly string RFA = "0";
                public static readonly string REJECT = "R";
                public static readonly string CORRECTION = "C";
            }

            public static readonly string APPROVE = "Approve";
            public static readonly string DRAFT = "Draft";
            public static readonly string RFA = "RFA";
            public static readonly string REJECT = "Reject";
            public static readonly string CORRECTION = "Correction";
        }

        public struct PhotoTypeName
        {
            public static readonly string KTP = "KTP";
            public static readonly string KARTU_KELUARGA = "Kartu Keluarga";
            public static readonly string SLIP_GAJI = "Slip Gaji";
            public static readonly string KTP_PASANGAN = "KTP Pasangan";
            public static readonly string KTP_PENJAMIN = "KTP Penjamin";
            public static readonly string KEPEMILIKAN_RUMAH = "Dokumen Kepemilikan Rumah";
        }

        public struct ReferencesCollection
        {
            public static readonly List<int> REFERENCE_ID = new() { 3, 4, 5 };
        }
    }
}