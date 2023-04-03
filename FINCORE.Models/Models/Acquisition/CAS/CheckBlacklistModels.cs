namespace FINCORE.Models.Models.Acquisition.CAS
{
    public class CheckBlacklistModels
    {
        public int? reason_id { get; set; }
        public string message_error { get; set; }
        public bool? is_blacklist { get; set; }
        public bool is_allow { get; set; }
    }
}