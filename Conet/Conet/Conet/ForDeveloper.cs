#define DEVELOPER
#define BETA_TESTER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Conet
{
#if DEVELOPER || BETA_TESTER
    public readonly partial struct Won
    {
        [Developer, BetaTester]
        public static void Assign(ref Won won, MoneyType value) => won = new(value);
    }

    public readonly partial struct Egg
    {
        [Developer, BetaTester]
        public static void Assign(ref Egg egg, MoneyType value) => egg = new(value);
    }
#endif
}