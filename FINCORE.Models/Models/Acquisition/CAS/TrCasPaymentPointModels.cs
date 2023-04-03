using FINCORE.Models.Helpers;
using FINCORE.Models.Models.Acquisition.CAS.ModelHelper;

namespace FINCORE.Models.Models.Acquisition.CAS
{
    public class TrCasPaymentPointModels
    {
        public string created_by { get; set; }
        public DateTime created_on { get; set; } = ModelHelpers.SetDefaultDate();
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; } = ModelHelpers.SetDefaultDate();
        public string credit_id { get; set; }
        public int payment_type_ids { get; set; }
        public int payment_point_ids { get; set; }
        public int[] payment_type_id { get; set; }
        public int[] payment_point_id { get; set; }
        public List<PaymentLocationPlansModels> PaymentLocationPlans { get; set; }
    }
}