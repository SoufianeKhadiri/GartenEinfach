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

            
        }

       
        private void searchBar_SearchButtonPressed(object sender, System.EventArgs e)
        {
          
        }


        

        
    }
}
