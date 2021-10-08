using GardenEinfach.Views.PageDetails;
using GardenEinfach.Views.TabbedPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GardenEinfach
{

    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PostDetail), typeof(PostDetail));
            Routing.RegisterRoute(nameof(Posts), typeof(Posts));
            Routing.RegisterRoute(nameof(MyPosts), typeof(MyPosts));

        }
    }
}