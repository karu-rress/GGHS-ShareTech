using Conet.Models;
using Conet.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Conet.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class PostBaseVM : BaseViewModel
    {
        bool isBusy = false;
        public bool IsBusy { get => isBusy; set => SetProperty(ref isBusy, value); }
    }
    public class GiveHelpPostBaseVM : PostBaseVM
    {
        public IDataStore<GiveHelpPost> DataStore => DependencyService.Get<IDataStore<GiveHelpPost>>();
    }

    public class NeedHelpPostBaseVM : PostBaseVM
    {
        public IDataStore<NeedHelpPost> DataStore => DependencyService.Get<IDataStore<NeedHelpPost>>();
    }
}
