using System;
using System.IO;

namespace TerrariaProtocol.DataStructures
{

    public static class BinaryExtension
    {
        public static Vector2 ReadVec2(this BinaryReader br)
        {
            Vector2 result;
            result.X = br.ReadSingle();
            result.Y = br.ReadSingle();
            return result;
        }

        public static void Write(this BinaryWriter bw, Vector2 vector)
        {
            bw.Write(vector.X);
            bw.Write(vector.Y);
        }
        public static Color ReadColor(this BinaryReader br)
        {
            Color result;
            result.R = br.ReadByte();
            result.G = br.ReadByte();
            result.B = br.ReadByte();
            result.A = 0xff;
            return result;
        }

        public static void Write(this BinaryWriter bw, Color color)
        {
            bw.Write(color.R);
            bw.Write(color.G);
            bw.Write(color.B);
        }
        public static void Write(this BinaryWriter writer, PlayerDeathReason reason)
        {
            reason.WriteSelfTo(writer);
        }
    }

}
