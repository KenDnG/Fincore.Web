namespace FINCORE.Models.Helpers
{
    public static class FTP
    {
        public struct AcquisitionDoc
        {
            public static readonly string FTP_HOST = "macf-file2";
            public static readonly string FTP_USER_NAME = "ftp.fincore";
            public static readonly string FTP_PASSWORD = "PasswordS3";
            public static readonly int FTP_PORT = 6621;
            public static readonly string FTP_PATH_ACQUISITION_DOCUMENT = "/AcquisitionDocument";
            public static readonly string FTP_PATH_PO_DOCUMENT = "/PODocument";
        }

        public struct AcquisitionDoc_NewZoom
        {
            public static readonly string FTP_HOST_NEW_ZOOM = "macf-file2";
            public static readonly string FTP_USER_NAME_NEW_ZOOM = "ftp.fincore";
            public static readonly string FTP_PASSWORD_NEW_ZOOM = "PasswordS3";
            public static readonly int FTP_PORT_NEW_ZOOM = 4546;
            public static readonly string FTP_PATH_ACQUISITION_DOCUMENT_NEW_ZOOM = "/";
        }
    }
}