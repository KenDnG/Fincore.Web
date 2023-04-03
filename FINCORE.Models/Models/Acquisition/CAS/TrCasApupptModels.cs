using FINCORE.Models.Helpers;

namespace FINCORE.Models.Models.Acquisition.CAS
{
    public class TrCasApupptModels
    {
        public string created_by { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public DateTime? created_on { get; set; } = ModelHelpers.SetDefaultDate();
        public string? credit_id { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public int[] question_id { get; set; } = new int[0];
        public string[] answer { get; set; } = new string[0];
        public string question_flag { get; set; } = ModelHelpers.SetDefaultEmptyString();
    }
}