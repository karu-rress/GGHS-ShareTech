using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using muxc = Microsoft.UI.Xaml.Controls;
using ShareTechUWP.SubPages;

namespace ShareTechUWP
{
    public enum NavigationDeferralType
    {
        None,
        WritingPost,
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class NavigationPage : Page
    {
        public static int? NavigateToIndex { get; set; } = null;
        public static NavigationDeferralType DeferralMode { get; set; } = NavigationDeferralType.None;

        public static Frame NavigationFrame { get; private set; }

        public NavigationPage()
        {
            InitializeComponent();

            NavigationFrame = Frame;
            if (NavigateToIndex is not null)
            {
                ContentFrame.Navigate(
                    NavigateToIndex switch
                    {
                        0 => typeof(DeliverPage),
                        1 => typeof(GamePartyPage),
                        2 => typeof(OttPage),
                        3 => typeof(TripMatePage),
                        4 => typeof(FoodPage),
                        5 => typeof(GoodsPage),
                        _ => throw new Exception("NavigationPage: Got invalid value from MainPage.")
                    }, null, new DrillInNavigationTransitionInfo());
                Navigation.SelectedItem = Navigation.MenuItems[NavigateToIndex.Value];
                NavigateToIndex = null;
            }
        }

        private void Navigation_ItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked is true)
            {
                NavigateTo("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (Navigation.SelectedItem is muxc.NavigationViewItem ItemContent)
            {
                NavigateTo(ItemContent.Tag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavigateTo(object tag, NavigationTransitionInfo transition)
        {
            Type page = tag switch
            {
                "settings" => typeof(SettingsPage),
                "DeliverPage" => typeof(DeliverPage),
                "GamePartyPage" => typeof(GamePartyPage),
                "OttPage" => typeof(OttPage),
                "TripMatePage" => typeof(TripMatePage),
                "FoodPage" => typeof(FoodPage),
                "GoodsPage" => typeof(GoodsPage),
                _ => throw new Exception("Navigation item value error")
            };
            ContentFrame.Navigate(page, null, transition);
        }

        private void Navigation_BackRequested(muxc.NavigationView sender, muxc.NavigationViewBackRequestedEventArgs args)
        {
            // var page = ContentFrame.SourcePageType;
            Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
        }
    }
}