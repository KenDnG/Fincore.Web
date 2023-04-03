namespace FINCORE.Services.Helpers.Request
{
    public class Validator
    {
        public bool IsModelNullOrEmpty<T>(T items)
        {
            var data = new List<object>();
            data.Add(items);

            return true;
        }
    }
}