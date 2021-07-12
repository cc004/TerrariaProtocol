using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaProtocol.DataStructures
{
    [Flags]
    public enum Difficulty
    {
        Normal = 0,
        MediumCore = 1,
        HardCore = 2,
        ExtraAcc = 4,
    }
}
