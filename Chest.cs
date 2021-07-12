using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaProtocol
{
    public class Chest
    {
        public string name;
        public bool bankChest;
        public int x, y;
        public Chest(bool bank = false)
        {
            this.bankChest = bank;
            this.name = string.Empty;
        }
    }
}
