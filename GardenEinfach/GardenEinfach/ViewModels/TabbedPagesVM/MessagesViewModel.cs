
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

        //private ObservableCollection<> _name;
        //public ObservableCollection<> name
        //{
        //    get { return _name; }
        //    set { SetProperty(ref _name, value); }
        //}
        public MessagesViewModel()
        {
            _ = getData();


            //  SnapshotListener();
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
            //if (!string.IsNullOrEmpty(PostTitel))
            //{
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
                                          case DocumentChangeType.Added:

                                              var msg = documentChange.Document;

                                              //if (msg.FromUser == Preferences.Get("FirstName", ""))
                                              //{

                                              //    msg.Sender = true;
                                              //    msg.Receiver = false;
                                              //}
                                              //else
                                              //{
                                              //    msg.Sender = false;
                                              //    msg.Receiver = true;
                                              //}
                                              string test = "";
                                              //Messages.Add(msg);

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
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
            // }
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
