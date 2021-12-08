using System.Collections.Generic;
using Newtonsoft.Json;

namespace backend.Models
{
    public class Events
    {
        [JsonProperty("events")]
        public List<EventsList> EventList { get; set; }
    }
}
