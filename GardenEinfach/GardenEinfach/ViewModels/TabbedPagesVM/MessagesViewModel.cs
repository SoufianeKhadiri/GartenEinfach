
using GardenEinfach.Model;
using NodaTime;
using Plugin.CloudFirestore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    [QueryProperty(nameof(Poster), nameof(Poster))]
    public class MessagesViewModel : BaseViewModel
    {

        private string _Poster;
        public string Poster
        {
            get { return _Poster; }
            set
            {
                SetProperty(ref _Poster, value);

            }
        }

        //ctor
        public MessagesViewModel()
        {


            Messenger messenger = new Messenger();

            Messages = new ObservableCollection<Messenger>();
            //GetChat();
            //testc();
            SnapshotListener();
        }

        private void SnapshotListener()
        {
            CrossCloudFirestore.Current
                   .Instance
                   .Collection("Messages")
                   .Document("Laptop").Collection("Chat").OrderBy("DateSent")
                   .AddSnapshotListener((snapshot, error) =>
                   {
                       if (snapshot != null)
                       {
                           foreach (var documentChange in snapshot.DocumentChanges)
                           {
                               switch (documentChange.Type)
                               {
                                   case DocumentChangeType.Added:

                                       var msg = documentChange.Document.ToObject<Messenger>();

                                       if (msg.FromUser == "sarah")
                                       {
                                           msg.IsOwnerMessage = true;
                                       }
                                       else
                                       {
                                           msg.IsOwnerMessage = false;
                                       }

                                       Messages.Add(msg);

                                       // Document Added
                                       break;
                                   case DocumentChangeType.Modified:
                                       // Document Modified
                                       break;
                                   case DocumentChangeType.Removed:
                                       // Document Removed
                                       break;
                               }
                           }
                       }
                   });
        }

        private ObservableCollection<Messenger> _Messages;
        public ObservableCollection<Messenger> Messages
        {
            get { return _Messages; }
            set { SetProperty(ref _Messages, value); }
        }




        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }

        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { SetProperty(ref _Status, value); }
        }

        private DelegateCommand _Send;
        public DelegateCommand Send =>
        _Send ?? (_Send = new DelegateCommand(SendM));

        void SendM()
        {
            send();
            GetChat();
        }

        private async void send()
        {

            var timeZone = DateTimeZoneProviders.Tzdb["Europe/Vienna"];
            var zonedDateTime = Instant.FromDateTimeUtc(DateTime.UtcNow).InZone(timeZone);
            DateTime localDT = zonedDateTime.ToDateTimeUnspecified();


            await CrossCloudFirestore.Current
                         .Instance
                         .Collection("Messages")
                         .Document("Laptop").Collection("Chat")
                         .AddAsync
                         (new Messenger()
                         {
                             FromUser = FirstName,
                             ToUser = Poster,
                             DateSent = localDT,
                             Message = Message,
                             Status = "SENT"
                         });


        }

        private async void GetChat()
        {
            if (Messages.Count > 1)
            {
                Messages.Clear();
            }



            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("Messages")
                                        .Document("Laptop").Collection("Chat")
                                        .OrderBy("DateSent")
                                        .LimitTo(100)
                                        .GetAsync();

            var msgs = document.ToObjects<Messenger>();


            foreach (var item in msgs)
            {
                Messenger msg = new Messenger();
                msg.FromUser = item.FromUser;
                msg.ToUser = item.ToUser;
                msg.Status = item.Status;
                msg.Message = item.Message;
                msg.DateSent = item.DateSent;
                Messages.Add(msg);
            }

        }
    }
}
