using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class DataFromUser
    {

        public string destination { get; set; }
        public List<Event> events { get; set; }
        public class Message
        {
            public string type { get; set; }
            public string id { get; set; }
            public string text { get; set; }
        }

        public class Source
        {
            public string type { get; set; }
            public string userId { get; set; }
        }

        public class Event
        {
            public string type { get; set; }
            public Message message { get; set; }
            public long timestamp { get; set; }
            public Source source { get; set; }
            public string replyToken { get; set; }
            public string mode { get; set; }
        }




    }
}