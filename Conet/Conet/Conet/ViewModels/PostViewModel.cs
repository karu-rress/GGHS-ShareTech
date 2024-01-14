using Conet.Models;
using Conet.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Conet.ViewModels
{
    public class GiveHelpPostsVM : GiveHelpPostBaseVM
    {
        private GiveHelpPost _post;

        public ObservableCollection<GiveHelpPost> Posts { get; }
        public Command LoadPostsCommand { get; }
        public Command WritePostsCommand { get; }
        public Command<GiveHelpPost> PostTapped { get; }

        public GiveHelpPostsVM()
        {
            Posts = new();
            LoadPostsCommand = new(async () => await ExecuteLoadItemsCommand());
            PostTapped = new(OnPostSelected);
            WritePostsCommand = new(OnWritePost);
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Posts.Clear();
                var posts = await DataStore.GetItemsAsync(true);
                foreach (var post in posts)
                {
                    Posts.Add(post);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedPost = null;
        }

        public GiveHelpPost SelectedPost
        {
            get => _post;
            set
            {
                SetProperty(ref _post, value);
                OnPostSelected(value);
            }
        }

        private async void OnWritePost(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewGiveHelpPostPage));
        }

        async void OnPostSelected(GiveHelpPost post)
        {
            if (post is null)
                return;

            string value = $"{nameof(GiveHelpPostDetailsPage)}?{nameof(GiveHelpPostDetailVM.PostId)}={post.Id}";
            //

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync(value);
        }
    }

    public class NeedHelpPostsVM : NeedHelpPostBaseVM
    {
        private NeedHelpPost _post;

        public ObservableCollection<NeedHelpPost> Posts { get; }
        public Command LoadPostsCommand { get; }
        public Command WritePostsCommand { get; }
        public Command<NeedHelpPost> PostTapped { get; }

        public NeedHelpPostsVM()
        {
            Posts = new();
            LoadPostsCommand = new(async () => await ExecuteLoadItemsCommand());
            PostTapped = new(OnPostSelected);
            WritePostsCommand = new(OnWritePost);
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Posts.Clear();
                var posts = await DataStore.GetItemsAsync(true);
                foreach (var post in posts)
                {
                    Posts.Add(post);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedPost = null;
        }

        public NeedHelpPost SelectedPost
        {
            get => _post;
            set
            {
                SetProperty(ref _post, value);
                OnPostSelected(value);
            }
        }

        private async void OnWritePost(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewNeedHelpPostPage));
        }

        async void OnPostSelected(NeedHelpPost post)
        {
            if (post is null)
                return;

            string value = $"{nameof(NeedHelpPostDetailsPage)}?{nameof(NeedHelpPostDetailVM.PostId)}={post.Id}";
            //

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync(value);
        }
    }
}
