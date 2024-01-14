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
    public partial class NeedHelpPostsPage : ContentPage
    {
        NeedHelpPostsVM viewModel;
        public NeedHelpPostsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}