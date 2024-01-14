global using MoneyType = System.UInt32;
using System;


namespace Conet
{
    public readonly partial struct Won
    {
        private readonly MoneyType _won;
        public Won(MoneyType won) => _won = won;
        public Won(Egg egg) => _won = egg.Value * 1000;
        public MoneyType Value => _won;
        public static explicit operator Egg(Won won) => new(won);
        public override string ToString() => $"{_won}원";
    }

    public readonly partial struct Egg
    {
        private readonly MoneyType _egg;
        public Egg(MoneyType egg) => _egg = egg;
        public Egg(Won won) => _egg = won.Value / 1000;

        public static explicit operator Won(Egg egg) => new(egg);
        public MoneyType Value => _egg;
        public override string ToString() => $"{_egg} 에그";
    }

    [Flags]
    public enum PointRules : uint
    {
        DailyCheck = 1,
        PostUpload = 2,
        // 4
        // 8
         // 16
         // 32
         // 64
         // 128
         // 256
    }
    public class UserPoint
    {
        public UserPoint() { }

        public uint Point { get; private set; } = 0;

        public void Modify(PointRules rules)
        {
            if (rules.HasFlag(PointRules.DailyCheck))
                Point += 1;
        }

        public void SetAs(uint value)
        {
            Point = value;
        }

        public override string ToString()
        {
            return $"{Point} 에그";
        }
    }

    public class Region
    {
        public override string ToString()
        {
            return base.ToString();
        }
    }
}