#nullable enable

using Conet.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conet.Models
{
    public class UserInfo
    {
        [SyncWithSQL]
        public string NickName { get; set; }

        [SyncWithSQL]
        public string ID { get; } = string.Empty; // ID is read-only

        [SyncWithSQL]
        public string? Password { get; set; } // Once logged-in, don't need to hold password

        [SyncWithSQL]
        public Region? Region { get; set; }

        [SyncWithSQL]
        public List<Group> Groups { get; set; } = new();

        [LocalSyncWithFile]
        public Egg Egg { get; [Developer, BetaTester] set; }


        public List<Review> WrittenReviews { get; set; } = new();
        public List<Review> GottenReviews { get; set; } = new();

        public bool IsLoggedIn { get; set; } = false;
        public const string FileName = "ConetUser";
        
        public UserInfo(string nickname, string id) : this(nickname, id, null) { }
        public UserInfo(string nickname, string id, string? password)
        {
            NickName = nickname;
            ID = id;
            Password = password;
        }  
        public UserInfo(UserInfoVM vm)
        {
            NickName = vm.NickName ?? "(비로그인)";
            ID = vm.ID ?? string.Empty;
            Password = vm.Password;
            Region = new();
            Egg = vm.Egg;
            IsLoggedIn = vm.IsLoggedIn;
        }

        /*
         * Picture Picture { get; set; }
List<Review> WrittenReviews { get; set; }
List<Review> GottenReviews { get; set; }
COORD Region { get; set; }

UserPoint => uint32 기반. 적립 / 차감 메서드를 통해
readonly staticDictionary와 연계하여 적립 및 차감

UserDAC는 필요 없음. 로컬 파일로만 저장할것.
         */
    }
}
