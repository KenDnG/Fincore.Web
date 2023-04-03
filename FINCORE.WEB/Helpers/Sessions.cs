using Newtonsoft.Json;

namespace FINCORE.WEB.Helpers
{
    public static class Sessions
    {
        /// <summary>
        /// Set and Save Session as object you need
        /// </summary>
        /// <param name="session">HttpContext.Session</param>
        /// <param name="key">Uniq key for identify Session value</param>
        /// <param name="value">Value of Session</param>
        public static void SetSessionAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// Get and Assign Session into object
        /// </summary>
        /// <typeparam name="T">Catch your Session into object as you need</typeparam>
        /// <param name="session">HttpContext.Session</param>
        /// <param name="key">Key Session Identity</param>
        /// <returns></returns>
        public static TObject GetSessionFromJson<TObject>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(TObject) : JsonConvert.DeserializeObject<TObject>(value);
        }

        /// <summary>
        /// Remove Session by Uniq Key
        /// </summary>
        /// <param name="session">HttpContext.Session</param>
        /// <param name="key">your Session key</param>
        public static void RemoveSessionByKey(this ISession session, string key)
        {
            session.Remove(key);
        }

        /// <summary>
        /// Remove All Session
        /// </summary>
        /// <param name="session">HttpContext.Session</param>
        public static void RemoveAllSession(this ISession session)
        {
            session.Clear();
        }
    }
}