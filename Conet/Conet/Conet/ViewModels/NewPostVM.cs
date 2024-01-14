#nullable enable

using Conet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Conet.ViewModels
{
    public class NewGiveHelpPostVM : GiveHelpPostBaseVM
    {
        private string title;
        private string description;
        private Egg egg;

        public string Title { get => title; set => SetProperty(ref title, value); }
        public string Description { get => description; set => SetProperty(ref description, value);}
        public string Egg { get; set; }
        
        public NewGiveHelpPostVM()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(title)
                && !String.IsNullOrWhiteSpace(description)
                && !String.IsNullOrWhiteSpace(Egg);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
        private async void OnSave()
        {
            egg = new(Convert.ToUInt32(Egg));
            GiveHelpPost newPost = new()
            {
                Id = Guid.NewGuid().ToString(),
                Title = Title,
                Description = Description,
                Egg = egg,
                Author = App.UserInfo.ID,
                Region = App.UserInfo.Region,
                WrittenTime = DateTime.Now,
            };

            await DataStore.AddItemAsync(newPost);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }

    public class NewNeedHelpPostVM : NeedHelpPostBaseVM
    {
        private string title;
        private string description;
        private Egg egg;

        public string Title { get => title; set => SetProperty(ref title, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public string Egg { get; set; }

        public NewNeedHelpPostVM()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(title)
                && !String.IsNullOrWhiteSpace(description)
                && !String.IsNullOrWhiteSpace(Egg);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
        private async void OnSave()
        {
            egg = new(Convert.ToUInt32(Egg));
            NeedHelpPost newPost = new()
            {
                Id = Guid.NewGuid().ToString(),
                Title = Title,
                Description = Description,
                Egg = egg,
                Author = App.UserInfo.ID,
                Region = App.UserInfo.Region,
                WrittenTime = DateTime.Now,
            };

            await DataStore.AddItemAsync(newPost);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }

    public class NumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {

            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isValid = args.NewTextValue.ToCharArray().All(x => char.IsDigit(x)); //Make sure all characters are numbers

                ((Entry)sender).Text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            }
        }


    }
}
