using Microsoft.Reporting.NETCore;
using System.Data;
using System.Reflection;

namespace FINCORE.WEB.Helpers
{
    public class Report
    {
        public static void Load(LocalReport report, object data, string ReportName)
        {
            using Stream? rs = Assembly.GetExecutingAssembly().GetManifestResourceStream(ReportName);

            if (rs is null)
                throw new Exception("RDLC File not visible to caller, try change build action to embeded resource");

            report.LoadReportDefinition(rs);
            DataTable dt = new DataTable();
            foreach (var p in data.GetType().GetProperties())
            {
                dt.Columns.Add(p.Name, p.PropertyType);
            }
            DataRow drow = dt.NewRow();
            dt.Rows.Add(drow);
            foreach (PropertyInfo p in data.GetType().GetProperties())
            {
                var val = p.GetValue(data);
                dt.Rows[0][p.Name] = val;
            }
            report.DataSources.Add(new ReportDataSource("Items", dt));
        }
    }

    public struct FileExtensions
    {
        public static readonly string PDF = "pdf";
        public static readonly string EXCEL_XLSX = "xlsx";
        public static readonly string EXCEL_XLS = "xls";
        public static readonly string DOC = "doc";
        public static readonly string DOCX = "docx";
        public static readonly string TEXT = "txt";
        public static readonly string CSV = "csv";
        public static readonly string JPG = "jpg";
        public static readonly string JPEG = "jpeg";
    }

    public struct MimeTypes
    {
        public static readonly string PDF = "application/pdf";
        public static readonly string XLSX = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public static readonly string XLS = "application/vnd.ms-excel";
        public static readonly string DOC = "application/msword";
        public static readonly string DOCX = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public static readonly string TEXT = "text/plain";
        public static readonly string CSV = "text/csv";
        public static readonly string JPEG_JPG = "image/jpeg";
    }
}