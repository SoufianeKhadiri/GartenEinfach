
using GardenEinfach.Model;
using NodaTime;
using Plugin.CloudFirestore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{

    public class MessagesViewModel : BaseViewModel
    {

        private ObservableCollection<Messenger> _Chats;
        public ObservableCollection<Messenger> Chats
        {
            get { return _Chats; }
            set { SetProperty(ref _Chats, value); }
        }

        private Messenger _Chat;
        public Messenger Chat
        {
            get { return _Chat; }
            set { SetProperty(ref _Chat, value);

                GotoChat(Chat.Key);

            }
        }


        async void GotoChat(string key)
        {
            await Shell.Current

            //MessagingCenter.Send(this, "PostDetail", PDetail);
            .GoToAsync($"{nameof(Chat)}?{nameof(ChatViewModel.Key)}={key}");

        }

        private string _OtherUserChat;
        public string OtherUserChat
        {
            get { return _OtherUserChat; }
            set { SetProperty(ref _OtherUserChat, value); }
        }
        public MessagesViewModel()
        {
            _ = getData();

            Chats = new ObservableCollection<Messenger>();
            SnapshotListener();
        }

        private async Task getData()
        {
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("Messages")

                                        .GetAsync();

            var msgs = document.ToObjects<Messenger>();

            foreach (var item in msgs)
            {
                var s = item;
            }
        }


       


        private void SnapshotListener()
        {
            string userId = Preferences.Get("userId", "");
            try
            {
                CrossCloudFirestore.Current
                          .Instance
                          .Collection("Messages")

                          .AddSnapshotListener((snapshot, error) =>
                          {
                              if (snapshot != null)
                              {
                                  foreach (var documentChange in snapshot.DocumentChanges)
                                  {
                                      switch (documentChange.Type)
                                      {
                                          // Document Added
                                          case DocumentChangeType.Added:

                                              var post = documentChange.Document.ToObject<Messenger>();

                                              if (post.FromUser == userId || post.ToUser == userId)
                                              {
                                                 
                                                  post.otherUserChat = OtherUser(post.Messages[0].FromUser ,post.Messages[0].ToUser);
                                                  
                                                  Chats.Add(post);

                                              }
                                              
                                              
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
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
           
           
        }


        private string OtherUser(string user , string otherUser)
        {
            string currentUsr = Preferences.Get("FirstName", "");
            string otherUsr = "";
            if (Chats != null)
            {
               

                otherUsr = currentUsr == user ? otherUser : user;

            }

            return otherUsr;
        }
        //private async void GetChat()
        //{
        //    if (Messages.Count > 1)
        //    {
        //        Messages.Clear();
        //    }



        //    var document = await CrossCloudFirestore.Current
        //                                .Instance
        //                                .Collection("Messages")
        //                                .Document("Laptop").Collection("Chat")
        //                                .OrderBy("DateSent")
        //                                .LimitTo(100)
        //                                .GetAsync();

        //    var msgs = document.ToObjects<Messenger>();


        //    foreach (var item in msgs)
        //    {
        //        Messenger msg = new Messenger();
        //        msg.FromUser = item.FromUser;
        //        msg.ToUser = item.ToUser;
        //        //msg.Status = item.Status;
        //        msg.Message = item.Message;
        //        msg.DateSent = item.DateSent;
        //        Messages.Add(msg);
        //    }

        //}
    }
}
