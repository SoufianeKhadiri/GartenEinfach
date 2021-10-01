using System;
using System.Collections.Generic;
using System.Text;

namespace GardenEinfach.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public MainPageViewModel()
        {
            Name = "Test";
        }

    }
}
