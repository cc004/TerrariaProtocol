using System;
using System.IO;
using TerrariaProtocol.DataStructures;

namespace TerrariaProtocol
{
    public class Packet
    {
        private MemoryStream stream;
        public BinaryWriter Writer { get; private set; }
        public BinaryReader Reader { get; private set; }
        public PacketTypes Type;
        public Packet()
        {
            stream = new MemoryStream();
            Writer = new BinaryWriter(stream);
            Reader = new BinaryReader(stream);
        }
        public Packet(PacketTypes type)
        {
            stream = new MemoryStream();
            Writer = new BinaryWriter(stream);
            Reader = new BinaryReader(stream);
            Type = type;
        }
        public Packet(PacketTypes type, byte playerSlot)
        {
            stream = new MemoryStream();
            Writer = new BinaryWriter(stream);
            Reader = new BinaryReader(stream);
            Type = type;
            Writer.Write(playerSlot);
        }
        public byte[] ToArray()
        {
            byte[] data = stream.ToArray();
            byte[] result = new byte[data.Length + 3];
            result[0] = (byte)result.Length;
            result[1] = (byte)(result.Length >> 8);
            result[2] = (byte)Type;
            Buffer.BlockCopy(data, 0, result, 3, data.Length);
            return result;
        }
    }
}
