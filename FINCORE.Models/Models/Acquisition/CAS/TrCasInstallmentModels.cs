using FINCORE.Models.Helpers;

namespace FINCORE.Models.Models.Acquisition.CAS
{
    public class TrCasInstallmentModels
    {
        public string created_by { get; set; }
        public DateTime created_on { get; set; } = ModelHelpers.SetDefaultDate();
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; } = ModelHelpers.SetDefaultDate();
        public string credit_id { get; set; }
        public string[] monthly_other_installment_id { get; set; } = new string[0];
    }
}