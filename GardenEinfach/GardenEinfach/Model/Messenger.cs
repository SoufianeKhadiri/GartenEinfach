using NodaTime;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GardenEinfach.Model
{
    public class Messenger
    {
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        public DateTime DateSent { get; set; }
        //public string ImageUrl { get; set; }
        public bool Sender { get; set; }
        public string PostTitel { get; set; }
        public bool Receiver { get; set; }

        //public List<Messenger> GetMessages()
        //{
        //    List<Messenger> messages = new List<Messenger>
        //{
        //    new Messenger {FromUser = "Devlin", ToUser = "Ruby", Message = "Hey Ruby", DateSent = DateTime.Now, Status = "Received"},
        //    new Messenger {FromUser = "Devlin", ToUser = "Ruby", Message = "Hey Ruby",  DateSent = DateTime.Now, Status = "Received"},
        //    new Messenger {FromUser = "Ruby", ToUser = "Devlin", Message = "Hello Dev",  DateSent = DateTime.Now, Status = "Sent"},
        //    new Messenger {FromUser = "Devlin", ToUser = "Ruby", Message = "How are you?",  DateSent = DateTime.Now, Status = "Received"},
        //    new Messenger {FromUser = "Ruby", ToUser = "Devlin", Message = "Not much. Where are you?",  DateSent = DateTime.Now, Status = "Sent"},
        //    new Messenger {FromUser = "Devlin", ToUser = "Ruby", Message = "Here in Reykjavík. Iceland.",  DateSent = DateTime.Now, Status = "Received"},
        //};

        //    return messages;
        //}
    }
}
