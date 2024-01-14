using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using ShareTechUWP.SubPages;
using System.Threading.Tasks;
using RollingRess;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ShareTechUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        RecordingViewModel ViewModel = new();
        private static PackageVersion version => Package.Current.Id.Version;
        public static string Version => $"{version.Major}.{version.Minor}.{version.Build}";
        public MainPage()
        {
            InitializeComponent();
        }

        public static async void HandleException(object _, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            await HandleException(e.Exception);
        }

        public static async Task HandleException(Exception exception)
        {
            string errorMsg = "에러가 발생했습니다.\n\n";
            errorMsg += exception.ToString();

            RollingMailSender mail = new()
            {
                Subject = $"ShareTech Co-net Exception Occured in V{Version}",
                Body = errorMsg
            };

            try
            {
                await mail.Send();
            }
            finally
            {
                MessageDialog messageDialog = new(errorMsg, "An error has occured.");
                await messageDialog.ShowAsync();
            }
        }

        private void GridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationPage.NavigateToIndex = CategoryGrid.SelectedIndex;
            Frame.Navigate(typeof(NavigationPage), null, new DrillInNavigationTransitionInfo());
        }
    }

}
