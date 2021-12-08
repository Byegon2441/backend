using Newtonsoft.Json;

namespace backend.Models
{
    public class Message
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}