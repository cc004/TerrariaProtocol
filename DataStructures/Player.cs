
namespace TerrariaProtocol.DataStructures
{
    public class Player
    {
        public short LifeMax, Life, ManaMax, Mana;
        public byte[] Buff;
        public Item[] Inventory;
        public PlayerInfo playerInfo;
        public Vector2 position, velocity;
        public bool hostile;
        public Player()
        {
            playerInfo = new PlayerInfo();
            Buff = new byte[10];
            Inventory = new Item[73];
            LifeMax = 100;
            Life = 100;
            ManaMax = 20;
            Mana = 20;
            hostile = false;
        }
    }


}
