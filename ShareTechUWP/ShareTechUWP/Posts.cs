#nullable enable

using System;

namespace ShareTechUWP
{
    public record PostLikes
    {
        public PostLikes() : this(0, 0) { }
        public PostLikes(uint likes, uint dislikes)
        {
            Likes = likes;
            Dislikes = dislikes;
        }
        public uint Likes { get; set; }
        public uint Dislikes { get; set; }

        public int TotalLikes => (int)(Likes - Dislikes);
        public void DoLike(uint Count = 1) => Likes += Count;
        public void DoDislike(uint Count = 1) => Dislikes += Count;
    }

    public class PostData
    {
        public PostData(in UserInfo userInfo, in DateTime date, string title, string body, in PostLikes likes)
        {
            User = userInfo;
            WrittenDate = date;
            Title = title;
            Body = body;
            Likes = likes;
        }

        public UserInfo User { get; init; }
        public DateTime WrittenDate { get; init; }
        public string Title { get; set; }
        public string Body { get; set; }
        public PostLikes Likes { get; set; }
    }
}
