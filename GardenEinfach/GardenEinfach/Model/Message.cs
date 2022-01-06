using System;
using System.Collections.Generic;
using System.Text;

namespace GardenEinfach.Model
{
    public class Message
    {
        public string content { get; set; }
        public DateTime createdAt { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string uid { get; set; }
        public bool Sender { get; set; }
        public bool Receiver { get; set; }
    }
}
