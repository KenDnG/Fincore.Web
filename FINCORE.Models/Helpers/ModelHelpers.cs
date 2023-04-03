namespace FINCORE.Models.Helpers
{
    public class ModelHelpers
    {
        /// <summary>
        /// 01/01/1900
        /// </summary>
        /// <returns></returns>
        public static DateTime SetDefaultDate()
        {
            return Convert.ToDateTime("01/01/1900");
        }

        public static string SetDefaultEmptyString()
        {
            return String.Empty;
        }
    }
}