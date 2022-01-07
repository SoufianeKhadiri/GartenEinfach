using GardenEinfach.Model;
using GardenEinfach.Service;
using Plugin.CloudFirestore;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    [QueryProperty(nameof(Key), nameof(Key))]
    public class ChatViewModel : BaseViewModel
    {
        List<Message> msgs = new List<Message>();

        private string _Key;
        public string Key
        {
            get { return _Key; }
            set
            {
                SetProperty(ref _Key, value);
                LoadItemId(value);
            }
        }

        private string _PostTitel;
        public string PostTitel
        {
            get { return _PostTitel; }
            set { SetProperty(ref _PostTitel, value); }
        }

        private string _PostOwnerid;
        public string PostOwnerId
        {
            get { return _PostOwnerid; }
            set
            {
                SetProperty(ref _PostOwnerid, value);

               // PostTitel = PostOwnerId;
            }
        }

        private string _PostOwner;
        public string PostOwner
        {
            get { return _PostOwner; }
            set { SetProperty(ref _PostOwner, value); }
        }
        private string _postImage;
        public string PostImage
        {
            get { return _postImage; }
            set { SetProperty(ref _postImage, value); }
        }

        private ObservableCollection<Message> _Messages;
        public ObservableCollection<Message> Messages
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
        //ctor
        public ChatViewModel()
        {
            Messages = new ObservableCollection<Message>();
            msgs = new List<Message>();
           
            SnapshotListener();

        }



        private void SnapshotListener()
        {
            if(msgs != null)
            {
                msgs.Clear();
            }
            if (!string.IsNullOrEmpty(PostTitel))
            {
                try
                {
                    CrossCloudFirestore.Current
                              .Instance
                              .Collection("Messages")
                              .Document(PostTitel)
                              .AddSnapshotListener((snapshot, error) =>
                              {
                                  if (snapshot != null)
                                  {

                                      var msg = snapshot.ToObject<Messenger>();

                                      if (msg!= null && msg.Messages != null)
                                      {
                                          Messages.Clear();

                                          foreach (var m in msg.Messages)
                                          {
                                              if (m.FromUser == Preferences.Get("FirstName", ""))
                                              {

                                                  m.Sender = true;
                                                  m.Receiver = false;
                                              }
                                              else
                                              {
                                                  m.Sender = false;
                                                  m.Receiver = true;
                                              }
                                             // msgs.Add(m);
                                              Messages.Add(m);
                                              

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
        }

       
        public async void LoadItemId(string key)
        {
            try
            {

                var AllPosts = await postService.GetItemsAsync();
                var post = await Task.FromResult(AllPosts.FirstOrDefault(p => p.Key == key));
               
                FillPostData(post);
                SnapshotListener();

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }


        }
        private void FillPostData(Post PostDetail)
        {
            PostOwnerId = PostDetail.User.Key;
            PostTitel = PostDetail.Titel;
            PostOwner = PostDetail.User.FirstName;
            PostImage = PostDetail.Images[0];

        }


        private DelegateCommand _Send;
        public DelegateCommand Send =>
        _Send ?? (_Send = new DelegateCommand(SendM));

        private async void SendM()
        {           

            Messenger msg = new Messenger();
            Message message = new Message()
            {
                content = Message,
                createdAt = DateTime.Now.AddHours(1),
                FromUser = FirstName,
                ToUser = PostOwner
                

            };
        
            Messages.Add(message);
          //  msgs.Add(message);


            msg.FromUser = UserKey;
            msg.ToUser = PostOwnerId;
            msg.DateSent = DateTime.Now.AddHours(1);
            msg.LastMsg = Message;
            msg.PostImage = PostImage;
            msg.Messages = Messages;
            msg.PostTitel = PostTitel;
            msg.Key = Key;


            await CrossCloudFirestore.Current
                         .Instance
                         .Collection("Messages")
                         .Document(PostTitel).SetAsync(msg);



            Message = "";

        }

    }
}

