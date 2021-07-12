using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaProtocol.DataStructures
{

    [Flags]
    public enum DamageType
    {
        None = 0,
        Crit = 1,
        Pvp = 2,
    }
}
