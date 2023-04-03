using System.Text.Json.Serialization;

namespace FINCORE.Services.Helpers.Response
{
    public class APIResponse
    {
        public APIResponse()
        {
        }

        public APIResponse(int? code, string status, string message, object data)
        {
            this.code = code;
            this.status = status;
            this.message = message;
            this.data = data;
        }

        [JsonPropertyName("code")]
        public int? code { get; set; }

        [JsonPropertyName("status")]
        public string status { get; set; }

        [JsonPropertyName("message")]
        public string message { get; set; }

        [JsonPropertyName("data")]
        public object data { get; set; }
    }
}