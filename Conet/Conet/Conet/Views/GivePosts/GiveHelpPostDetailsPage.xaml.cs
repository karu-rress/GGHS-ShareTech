using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conet.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Conet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GiveHelpPostDetailsPage : ContentPage
    {
        public GiveHelpPostDetailsPage()
        {
            InitializeComponent();
            BindingContext = new GiveHelpPostDetailVM();
        }
    }
}