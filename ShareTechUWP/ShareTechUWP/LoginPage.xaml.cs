#nullable enable 

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.System;

using RollingRess.Security;
using static RollingRess.StaticClass;

namespace ShareTechUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
#nullable restore
        public static UserInfo CurrentUser { get; private set; }
#nullable enable

        // Make static?
        private static string ConnectionString => ConfigurationManager.ConnectionStrings["ConetUsers"].ConnectionString;

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            switch (0)
            {
                case var _ when AreNullOrEmpty(NickNameRegBox.Text, IdRegBox.Text, PasswordRegBox.Password, PasswordReRegBox.Password, RegionBox.Text):
                    RegisterText.Text = "모든 항목을 입력해주세요.";
                    return;

                case var _ when NickNameRegBox.Text.Any(ch => !char.IsLetterOrDigit(ch)) || NickNameRegBox.Text.Length > 10:
                    RegisterText.Text = "닉네임은 한글, 숫자, 영문으로 10자 이내 제한입니다.";
                    return;

                case var _ when !Regex.IsMatch(IdRegBox.Text, @"^[a-zA-Z0-9]+$") || IdRegBox.Text.Length > 20:
                    RegisterText.Text = "아이디는 영문, 숫자만 가능하며 20자 이내여야 합니다.";
                    return;

                case var _ when PasswordRegBox.Password != PasswordReRegBox.Password:
                    RegisterText.Text = "비밀번호가 일치하지 않습니다.";
                    return;
            }

            RegisterProgress.Visibility = Visibility.Visible;

            using SqlConnection sql = new();
            sql.ConnectionString = ConnectionString;
            await sql.OpenAsync();

            UserInfoDAC dac = new(sql);
            if (await dac.IDExistsAsync(IdRegBox.Text))
            {
                RegisterProgress.Visibility = Visibility.Collapsed;
                RegisterText.Text = "ID가 이미 존재합니다.";
                return;
            }

            if (await dac.NickNameExistsAsync(NickNameRegBox.Text))
            {
                RegisterProgress.Visibility = Visibility.Collapsed;
                RegisterText.Text = "닉네임이 이미 존재합니다.";
                return;
            }

            string encryptedpw = EncryptPassword(PasswordRegBox.Password);
            UserInfo user = new(NickNameRegBox.Text, IdRegBox.Text, encryptedpw, new());
            await dac.InsertAsync(user);
            CurrentUser = user;

            RegisterProgress.Visibility = Visibility.Collapsed; 
            ContentDialog content = new()
            { 
                Title = "회원가입 성공",
                Content = $"{user.NickName}님, {user.Id} 아이디로 Co-net에 가입하셨습니다. 환영합니다.",
                CloseButtonText = "확인",
            };
            await content.ShowAsync();
            Frame.Navigate(typeof(MainPage));
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (AreNullOrEmpty(IdBox.Text, PasswordBox.Password))
            {
                LoginText.Text = "모든 항목을 입력해주세요.";
                return;
            }

            LoginProgress.Visibility = Visibility.Visible;

            using SqlConnection sql = new();
            sql.ConnectionString = ConnectionString;
            await sql.OpenAsync();

            UserInfoDAC dac = new(sql);
            if (await dac.IDExistsAsync(IdBox.Text) is false)
            {
                LoginProgress.Visibility = Visibility.Collapsed;
                LoginText.Text = "ID가 존재하지 않습니다.";
                return;
            }

            UserInfo user = await dac.GetUserAsync(IdBox.Text);
            if (EncryptPassword(PasswordBox.Password) != user.Password)
            {
                LoginProgress.Visibility = Visibility.Collapsed;
                LoginText.Text = "비밀번호가 일치하지 않습니다.";
                return;
            }

            ContentDialog content = new()
            {
                Title = "로그인 성공",
                Content = $"{user.NickName}님, Co-net에 다시 오신 걸 환영합니다.",
                CloseButtonText = "확인",
            };
            LoginProgress.Visibility = Visibility.Collapsed;
            await content.ShowAsync();

            CurrentUser = user;
            Frame.Navigate(typeof(MainPage));
        }

        private string EncryptPassword(string input) => Encryptor.SHA256Encrypt(input);

        private void Box_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            switch (sender)
            {
                case TextBox when e.Key is VirtualKey.Enter: RegisterButton_Click(RegisterButton, new()); return;
                case PasswordBox _ when e.Key is VirtualKey.Enter: LoginButton_Click(LoginButton, new()); return;
            }
        }
    }
}
