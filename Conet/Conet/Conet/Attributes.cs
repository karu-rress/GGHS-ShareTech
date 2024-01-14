using System;

namespace Conet
{
    // ForDeveloper.cs
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    internal sealed class DeveloperAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    internal sealed class BetaTesterAttribute : Attribute { }

    // UserInfo.cs
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    internal sealed class SyncWithSQLAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    internal sealed class LocalSyncWithFileAttribute : Attribute { }
}