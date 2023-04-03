using Microsoft.Reporting.NETCore;
using System.Data;
using System.Reflection;

namespace FINCORE.WEB.Views.Acquisition.BPKB.Reports
{
    public class Report
    {
        public static void Load(LocalReport report, object data, string ReportName)
        {
            using var rs = Assembly.GetExecutingAssembly().GetManifestResourceStream(ReportName);
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
}