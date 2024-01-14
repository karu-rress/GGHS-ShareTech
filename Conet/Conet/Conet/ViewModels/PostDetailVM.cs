using Conet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Conet.ViewModels
{
    [QueryProperty(nameof(PostId), nameof(PostId))]
    public class GiveHelpPostDetailVM : GiveHelpPostBaseVM
    {
        private string postId;
        private string title;
        private string description;
        private string author;
        private Region region;
        public string Id { get; set; } // Needed?

        public string Title { get => title; set => SetProperty(ref title, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public string Author { get => author; set => SetProperty(ref author, value); }
        public Region Region { get => region; set => SetProperty(ref region, value); }
        public string PostId { get => postId; set { postId = value; LoadItemId(value); } }
        public async void LoadItemId(string itemId)
        {
            GiveHelpPost item = await DataStore.GetItemAsync(itemId);
            Id = item.Id;
            Title = item.Title;
            Description = item.Description;
            Author = item.Author;
            Region = item.Region;
        }
    }

    [QueryProperty(nameof(PostId), nameof(PostId))]
    public class NeedHelpPostDetailVM : NeedHelpPostBaseVM
    {
        private string postId;
        private string title;
        private string description;
        private string author;
        private Region region;
        public string Id { get; set; } // Needed?

        public string Title { get => title; set => SetProperty(ref title, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public string Author { get => author; set => SetProperty(ref author, value); }
        public Region Region { get => region; set => SetProperty(ref region, value); }
        public string PostId { get => postId; set { postId = value; LoadItemId(value); } }
        public async void LoadItemId(string itemId)
        {
            NeedHelpPost item = await DataStore.GetItemAsync(itemId);
            Id = item.Id;
            Title = item.Title;
            Description = item.Description;
            Author = item.Author;
            Region = item.Region;
        }
    }
}