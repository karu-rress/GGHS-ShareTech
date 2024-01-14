using Conet.Models;
using Conet.Services;
using Conet.Views;
using Plugin.LocalNotification;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Conet
{
    public partial class App : Application
    {

        public static UserInfo UserInfo { get; set; } = null;
        public App()
        {
            InitializeComponent();

            DependencyService.Register<GiveHelpPostDataStore>();
            DependencyService.Register<NeedHelpPostDataStore>();

            UserInfo = new("카루", "karu1338");
            UserInfo.Egg = new(1000);
            MainPage = new AppShell();
        }

        protected async override void OnStart()
        {
            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Title = "[Conet] 새로운 도움 요청이 들어왔어요!",
                Description = @"""사진 찍어주실 분 구합니다!""",
                ReturningData = "Dummy data", // Returning data when tapped on notification.
                Schedule =
                {
                    NotifyTime = DateTime.Now.AddSeconds(3) // Used for Scheduling local notification, if not specified notification will show immediately.
                }
            };
            await NotificationCenter.Current.Show(notification);
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {
            // ReadFile
        }
    }
}
