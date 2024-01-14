#nullable enable

using System;
using System.Collections.Generic;

namespace Conet
{
    public readonly struct GroupProperty
    {
        private readonly string data;
        public GroupProperty(string param) => data = param;
        public static implicit operator string(GroupProperty param) => param.data;
    }

    public class Group
    {
        public GroupProperty Data { get; set; }
        public Group(GroupProperty group) => Data = group;
        public static readonly GroupProperty NA = new("N/A");
    }

    public class Club
    {
        public static readonly GroupProperty Cepa = new("세파");
        public static readonly GroupProperty Flip = new("플립");
        public static readonly GroupProperty Largo = new("라르고");
        public static readonly GroupProperty Regen = new("반올림");
        public static readonly GroupProperty Vank = new("반크");
        public static readonly GroupProperty Guitar = new("기타");
    }

    public class Class
    {
    }
}