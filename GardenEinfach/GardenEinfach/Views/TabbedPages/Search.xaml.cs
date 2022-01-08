using ExcelDataReader;
using GardenEinfach.Model;
using GardenEinfach.ViewModels;
using GemBox.Spreadsheet;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GardenEinfach.Views.TabbedPages
{
    public partial class Search : ContentPage
    {
        public List<string> cls;
        public Search()
        {
            
            InitializeComponent();
            BindingContext = new SearchViewModel();

            cls = new List<string>()
            {
                "Red","","Magenta","Blue","Green"
            };
            //listview.IsVisible = false;
           
        }

        protected  override void OnAppearing()
        {
            base.OnAppearing();

           


        }

        

        //private List<PostZahl> GetJsonData()
        //{
        //   // string jsonFileName = "contacts.json";
            


        //   // var assembly = typeof(Search).GetTypeInfo().Assembly;
        //   // Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
        //   // using (var reader = new System.IO.StreamReader(stream))
        //   // {
        //   //     var jsonString = reader.ReadToEnd();

        //   //     //Converting JSON Array Objects into generic list    
        //   ////     ObjContactList = JsonConvert.DeserializeObject<ContactList>(jsonString);
        //   // }

        //   // return ObjContactList.contacts;
        //}
        private void searchBar_SearchButtonPressed(object sender, System.EventArgs e)
        {
          
        }


        

        private void sb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = sb.Text;
            var suggestions = cls.Where(c => c.ToLower().Contains(keyword.ToLower()));
            listview.IsVisible = true;
            if (sb.Text.Length == 0)
            {
                suggestions = Enumerable.Empty<string>();
               // listview.IsVisible = false;
            }

          //  listview.ItemsSource = suggestions;
            
        }
    }
}
