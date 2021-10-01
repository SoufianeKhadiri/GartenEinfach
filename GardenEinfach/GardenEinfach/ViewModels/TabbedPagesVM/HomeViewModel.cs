
using GardenEinfach.Views.PageDetails;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {



        private DelegateCommand _test;
        public DelegateCommand Test =>
        _test ?? (_test = new DelegateCommand(CommandMthode));

        async void CommandMthode()
        {
            await Navigation.PushAsync(new PostDetail());
        }

        INavigation Navigation;
        public HomeViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
    }
}
