namespace FINCORE.Services.Helpers.Domain
{
    public static class EndpointAPI
    {
        public struct Endpoint
        {
            // BPKB
            public static readonly string bpkbURL = "http://localhost:1901";

            public static readonly string bpkbURLIIS = "http://localhost:1901";

            /// <summary>
            /// Domain LDAP
            /// </summary>

            #region Domain LDAP

            public static readonly string LDAP_URL = "http://172.16.91.62:99/getuser";

            #endregion Domain LDAP

            #region Third-party Domain
            
            //RapindoUAT
            public static readonly string RAPINDOUATURL = "https://api.mcf.co.id:5011/";

            //RAPINDOLIVE
            public static readonly string RAPINDOLIVEURL = "https://api.mcf.co.id:5010/";

            // Dukcapil FCL Recognition
            public static readonly string DOMAIN_DUKCAPIL = "https://api.mcf.co.id:5530";

            #endregion


            /// <summary>
            /// Domain Acquisition Services
            /// </summary>

            #region Domain Acquisition Services

            public static readonly string DOMAIN_TRCAS = "http://localhost:1301";
            public static readonly string DOMAIN_CM = "http://localhost:1401";
            public static readonly string DOMAIN_CA = "http://localhost:1501";
            public static readonly string DOMAIN_NPP = "http://localhost:1601";
            public static readonly string DOMAIN_VERTEL = "http://localhost:1802";
            public static readonly string DOMAIN_OMA = "http://localhost:2001";

            #endregion Domain Acquisition Services

            /// <summary>
            /// Domain Mini-API for Pagination
            /// </summary>

            #region Domain Mini-API
            public static readonly string DOMAIN_MINIAPI = "http://localhost:1201";
            public static readonly string DOMAIN_MINIAPI_APPROVAL = "http://localhost:1201";
            #endregion

            /// <summary>
            /// Domain Masters
            /// </summary>

            #region Domain Masters
            public static readonly string DOMAIN_MASTERS = "http://localhost:1101";
            #endregion

            /// <summary>
            /// Domain Reports
            /// </summary>

            #region Domain Reports
            public static readonly string DOMAIN_REPORTS = "http://localhost:1701";
            #endregion

            public static readonly string DOMAIN_CS_SESSION = "http://localhost:2101";

            /// <summary>
            /// Domain Payment
            /// </summary>

            #region Domain Payment

            public static readonly string DOMAIN_PAYMENT = "http://localhost:2201";


            #endregion Domain Payment

        }

        public struct Route
        {
            /// <summary>
            /// Route for Services
            /// </summary>

            #region Route Services

            public static readonly string ROUTE_ACQUISITION = "api/v1/services/acquisition/credits";
            public static readonly string ROUTE_TRCAS       = "api/v1/services/acquisition/cas";
            public static readonly string ROUTE_CM          = "api/v0/services/acquisition/cm/2w/ef";
            public static readonly string ROUTE_CM_Car      = "api/v0/services/acquisition/cm/4w/ef";
            public static readonly string ROUTE_CA_Car      = "api/v0/services/acquisition/cacar/ef";
            public static readonly string ROUTE_VERTEL      = "api/v1/services/acquisition/vertel";
            public static readonly string ROUTE_NPP         = "api/v1/services/acquisition/npp";
            public static readonly string ROUTE_BPKB        = "api/v0/services/acquisition/BPKB/EF";
            public static readonly string ROUTE_NEO_SCORE   = "api/v0/services/acquisition/neoscore/ef";

            #endregion Route Services

            /// <summary>
            /// Route for Mini-API
            /// </summary>

            #region Route Mini-API

            //public static readonly string ROUTE_MINIAPI                 = "api/v1/services/mini";
            public static readonly string ROUTE_MINIAPI_ACQUISITION     = "api/v1/services/mini/acquisition";
            public static readonly string ROUTE_MINIAPI_APPROVAL        = "api/v1/services/mini/approval";
            public static readonly string ROUTE_MiniAPI_CM              = "api/v1/services/mini/acquisition/cm/ef";
            public static readonly string ROUTE_MiniAPI_CSV             = "api/v1/services/mini/payment/cashier-session-verify";

            #endregion Route Mini-API

            /// <summary>
            /// Route for Masters
            /// </summary>

            #region Route Masters

            public static readonly string ROUTE_MASTERS = "api/v1/services/master";

            #endregion Route Masters

            /// <summary>
            /// Route for Reports
            /// </summary>

            #region Route Reports

            public static readonly string ROUTE_REPORTS = "api/v1/services/report";

            #endregion Route Reports

            #region OMA Path
            public static readonly string OMA_UPLOAD_PATH = @"\\172.16.100.195\Upload\CAS_NewZoom\";
            #endregion

            public static readonly string ROUTE_CS_SESSION = "api/v1/services/payment/cashiersession";
            public static readonly string ROUTE_PAYMENT = "api/v1/services/payment";

            #region Third-party Domain
            public static readonly string ROUTE_DUKCAPIL = "Dukcapil/fcl_recognition/";
            #endregion
        }

        public struct Token
        {
            //rapindouat
            public static readonly string TokenCodeRapindoMAFUAT = "73feef94a47465e2e7e5cd523d9f10f1";

            public static readonly string TokenCodeRapindoMCFUAT = "1bbdf6068b904455253259268669b922";

            //rapindolive
            public static readonly string TokenCodeRapindoMAFLive = "f5545b6e20a5b2af74a92506b286f62b";

            public static readonly string TokenCodeRapindoMCFLive = "33a8ff3f84c8741d75e9dacaefb71f97";
        }
    }
}
