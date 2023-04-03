using FINCORE.Services.Helpers.Response.Interface;
using Newtonsoft.Json;

namespace FINCORE.Services.Helpers.Response
{
    public class ObjectConverter : IObjectConverter
    {
        /// <summary>
        /// Convert Json Data Response into your List data object models
        /// </summary>
        /// <typeparam name="T">Your Model</typeparam>
        /// <param name="content">Response JSON Data</param>
        /// <returns></returns>
        public IList<TModel> JsonToList<TModel>(object content)
        {
            var dataResult = new List<TModel>();
            try
            {
                dataResult = JsonConvert.DeserializeObject<List<TModel>>(JsonConvert.SerializeObject(content));
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return dataResult.ToList();
        }

        public T JsonToObject<T>(object content)
        {
            try
            {
                var d = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(content));
                return d;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        //public async T JsonToObjectModel<T>(object content)
        //{
        //    return await System.Text.Json.JsonSerializer.DeserializeAsync<T>(await )
        //}
    }
}