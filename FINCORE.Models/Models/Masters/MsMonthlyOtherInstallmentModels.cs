namespace FINCORE.Models.Models.Masters
{
    public class MsMonthlyOtherInstallmentModels
    {
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string? last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
        public string monthly_other_installment_id { get; set; }
        public string monthly_other_installment_desc { get; set; }
        public bool is_active { get; set; }
    }
}