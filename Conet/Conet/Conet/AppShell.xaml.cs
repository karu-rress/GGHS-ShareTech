using Conet.ViewModels;
using Conet.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Conet
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(GiveHelpPostDetailsPage), typeof(GiveHelpPostDetailsPage));
            Routing.RegisterRoute(nameof(NewGiveHelpPostPage), typeof(NewGiveHelpPostPage));
            
            Routing.RegisterRoute(nameof(NeedHelpPostDetailsPage), typeof(NeedHelpPostDetailsPage));
            Routing.RegisterRoute(nameof(NewNeedHelpPostPage), typeof(NewNeedHelpPostPage));
            mainTabBar.CurrentItem = mainTabBar_home;
        }

    }
}
