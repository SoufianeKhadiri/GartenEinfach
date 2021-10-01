
using System;
using System.Collections.Generic;
using System.Linq;

namespace GardenEinfach.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }
        public SearchViewModel()
        {
            Name = "test";
        }
    }
}
