using System;
using System.Collections.Generic;
using System.Text;
using Conet.Views;
using Conet.ViewModels;
using Conet.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Conet.Services
{
    public class GiveHelpPostDataStore : IDataStore<GiveHelpPost>
    {
        readonly List<GiveHelpPost> posts;

        public GiveHelpPostDataStore()
        {
            posts = new()
            {
                new GiveHelpPost()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "파이썬 코딩 대신 해드립니다",
                    Author = "카루",
                    Description = "창문해 만점 쌉가능",
                    Egg = new(2),
                    Region = new(),
                    WrittenTime = DateTime.Now
                },
            };
        }

        public async Task<bool> AddItemAsync(GiveHelpPost item)
        {
            posts.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(GiveHelpPost item)
        {
            var oldPost = posts.Where((GiveHelpPost arg) => arg == item).FirstOrDefault();
            posts.Remove(oldPost);
            posts.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldPost = posts.Where((GiveHelpPost arg) => arg.Id == id).FirstOrDefault();
            posts.Remove(oldPost);

            return await Task.FromResult(true);
        }

        public async Task<GiveHelpPost> GetItemAsync(string id) => await Task.FromResult(posts.FirstOrDefault(s => s.Id == id));

        public async Task<IEnumerable<GiveHelpPost>> GetItemsAsync(bool forceRefresh = false) => await Task.FromResult(posts);
    }

    public class NeedHelpPostDataStore : IDataStore<NeedHelpPost>
    {
        readonly List<NeedHelpPost> posts;

        public NeedHelpPostDataStore()
        {
            posts = new()
            {
                new NeedHelpPost()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "꼬넷 좀 대신 만들어주세요",
                    Author = "카루",
                    Description = "이러다 나 죽어",
                    Egg = new(2),
                    Region = new(),
                    WrittenTime = DateTime.Now
                },
                new NeedHelpPost()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "1일 FLIP 부장 위임합니다",
                    Author = "카루",
                    Description = "효숙쌤한테 갈궈져보실?",
                    Egg = new(3),
                    Region = new(),
                    WrittenTime = DateTime.Now
                },
                new NeedHelpPost()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "솔크부대 모집합니다",
                    Author = "카루",
                    Description = "엉엉",
                    Egg = new(20),
                    Region = new(),
                    WrittenTime = DateTime.Now
                },
                new NeedHelpPost()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "수능 대리시험 구해요",
                    Author = "카루",
                    Description = "1 안 뜨면 고발할거임",
                    Egg = new(300),
                    Region = new(),
                    WrittenTime = DateTime.Now
                }
            };
        }

        public async Task<bool> AddItemAsync(NeedHelpPost item)
        {
            posts.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(NeedHelpPost item)
        {
            var oldPost = posts.Where((NeedHelpPost arg) => arg == item).FirstOrDefault();
            posts.Remove(oldPost);
            posts.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldPost = posts.Where((NeedHelpPost arg) => arg.Id == id).FirstOrDefault();
            posts.Remove(oldPost);

            return await Task.FromResult(true);
        }

        public async Task<NeedHelpPost> GetItemAsync(string id) => await Task.FromResult(posts.FirstOrDefault(s => s.Id == id));

        public async Task<IEnumerable<NeedHelpPost>> GetItemsAsync(bool forceRefresh = false) => await Task.FromResult(posts);
    }

}
