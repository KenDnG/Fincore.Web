using FINCORE.Models.Helpers;

namespace FINCORE.Models.Models.Acquisition.CAS
{
    public class TrCasFinancialModels
    {
        public string created_by { get; set; }
        public DateTime? created_on { get; set; } = ModelHelpers.SetDefaultDate();
        public string last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; } = ModelHelpers.SetDefaultDate();
        public string credit_id { get; set; }
        public int primary_income { get; set; }
        public int other_income { get; set; } = 0;
        public string other_income_desc { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string office_name { get; set; }
        public string office_address { get; set; }
        public int office_location_id { get; set; }
        public string office_telephone_number { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string office_fax { get; set; } = ModelHelpers.SetDefaultEmptyString();
        public string position { get; set; }
        public int industry_type_id { get; set; }
        public string commodity_type { get; set; }
        public decimal years_of_work_experience { get; set; }
        public string profession_id { get; set; }
        public int household_expenses { get; set; } = 0;
        public int education_expenses { get; set; } = 0;
        public int health_expenses { get; set; } = 0;
        public int number_of_dependents { get; set; }
        public int monthly_other_installment { get; set; } = 0;


        //Helper Variable
        public string txtPrimaryIncome { get; set; }
        public string txtOther_income { get; set; }
        public string txtHousehold_expenses { get; set; }
        public string txtEducation_expenses { get; set; }
        public string txtHealth_expenses { get; set; }
        public string txtMonthly_other_installment { get; set; }


    }
}