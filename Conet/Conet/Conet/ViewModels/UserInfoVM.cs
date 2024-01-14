#nullable enable

using Conet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Conet.ViewModels
{
    public class UserInfoVM : BaseViewModel
    {
        private string? nickName;
        private string? iD;
        private string? password;
        private Region? region;
        private Egg egg;
        private bool isLoggedIn;

        public string? NickName { get=> nickName; set=>SetProperty(ref nickName, value); }
        public string? ID { get=> iD; set=>SetProperty(ref iD, value); }
        public string? Password { get => password; set=>SetProperty(ref password, value); }
        public string? Region { get; set; }
        public Egg Egg { get => egg; set=>SetProperty(ref egg, value); }
        public bool IsLoggedIn { get => IsLoggedIn; set => SetProperty(ref isLoggedIn, value); }

        public UserInfoVM()
        {
            if (App.UserInfo is not null)
            {
                NickName = App.UserInfo.NickName;
                Password = App.UserInfo.Password;
                ID = App.UserInfo.ID;
                region = App.UserInfo.Region;
                Egg = App.UserInfo.Egg;
                IsLoggedIn = true;
            }
            SaveCommand = new Command(OnSave, ValidateSave);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(NickName)
                && !String.IsNullOrWhiteSpace(Region);
        }
        public Command SaveCommand { get; }
        private async void OnSave()
        {
            App.UserInfo = new(NickName, ID)
            {
                Password = Password is null ? App.UserInfo.Password : Password,
                Region = new(),
                IsLoggedIn = true,
            };
            await Shell.Current.GoToAsync("..");
        }
    }
}