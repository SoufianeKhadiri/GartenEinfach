
using GardenEinfach.Model;
using Plugin.CloudFirestore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GardenEinfach.ViewModels
{
    public class MessagesViewModel : BaseViewModel
    {
        public MessagesViewModel()
        {

            Messenger messenger = new Messenger();

            Messages = new ObservableCollection<Messenger>();
            GetChat();
            //testc();

        }


        private ObservableCollection<Messenger> _Messages;
        public ObservableCollection<Messenger> Messages
        {
            get { return _Messages; }
            set { SetProperty(ref _Messages, value); }
        }


        private string _Buyyer;
        public string Buyer
        {
            get { return _Buyyer; }
            set { SetProperty(ref _Buyyer, value); }
        }

        private string _Seller;
        public string Seller
        {
            get { return _Seller; }
            set { SetProperty(ref _Seller, value); }
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
            await CrossCloudFirestore.Current
                         .Instance
                         .Collection("Messages")
                         .Document("Laptop").Collection("Chat")
                         .AddAsync
                         (new Messenger()
                         {
                             FromUser = Buyer,
                             ToUser = Seller,
                             DateSent = DateTime.Now,
                             Message = Message,
                             Status = Status
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
