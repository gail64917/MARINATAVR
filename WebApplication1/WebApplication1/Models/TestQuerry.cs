using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TestQuerry
    {
        public int group_id { get; set; }
        public string type { get; set; }
        public MessageObject @object { get; set; }
        public string secret { get; set; }
    }

    public class MessageObject
    {
        public int date { get; set; }
        public int from_id { get; set; }
        public int id { get; set; }
        public int @out { get; set; }
        public int peer_id { get; set; }
        public string text { get; set; }
        public string conversation_message_id { get; set; }
        public ForwardMessage[] fwd_messages { get; set; }
        public bool important { get; set; }
        public int random_id { get; set; }
        public Attachments[] attachments { get; set; }
        public bool is_hidden { get; set; }        
    }

    public class ForwardMessage
    {

    }

    public class Attachments
    {

    }
}
