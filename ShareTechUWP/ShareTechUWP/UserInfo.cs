using System.ComponentModel;

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class IsExternalInit { }
}

namespace ShareTechUWP
{
    public record UserInfo(string NickName, string Id, string Password, RegionInfo Region)
    {
        public UserInfo() : this(default, default, default, default) { }
    }

    public record RegionInfo()
    {
        public override string ToString()
        {
            return "확인되지 않은 지역";
        }
    }


    public enum UserStatus
    {
        Normal,
        NotRegistered,
        Unknown
    }
}
