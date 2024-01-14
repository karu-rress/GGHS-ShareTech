using System;
using System.Collections.Generic;
using System.Text;

namespace Conet.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public Egg Egg { get; set; }
        public string Description { get; set; }
        public Region Region { get; set; }
        public string Author { get; set; }
        public DateTime WrittenTime { get; set; }

        public bool IsSameWith(string author, in DateTime time) => Author == author && WrittenTime == time;
        public static bool operator ==(in Post post1, in Post post2) => post1.Author == post2.Author && post1.WrittenTime == post2.WrittenTime;
        public static bool operator !=(in Post post1, in Post post2) => !(post1.Author == post2.Author && post1.WrittenTime == post2.WrittenTime);
        public override bool Equals(object obj)
        {
            Post post = obj as Post;
            return Author == post.Author && WrittenTime == post.WrittenTime;
        }
        public override int GetHashCode() => HashCode.Combine(Author, WrittenTime);
    }
    public class GiveHelpPost : Post { }
    public class NeedHelpPost : Post { }

    [Obsolete("To do")]
    public class Review
    {

    }
}
