using System;
using System.IO;

namespace TerrariaProtocol.DataStructures
{
    public struct Color
    {
        public byte R, G, B, A;

        public Color(byte R, byte G, byte B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            A = 0xff;
        }
        public Color(byte R, byte G, byte B, byte A)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }
        public Color(int R, int G, int B, int A)
        {
            this.R = (byte)R;
            this.G = (byte)G;
            this.B = (byte)B;
            this.A = (byte)A;
        }
        public static Color RandomColor()
        {
            byte[] buffer = new byte[3];
            new Random().NextBytes(buffer);
            return new Color(buffer[0], buffer[1], buffer[2]);
        }
        public void WriteTo(BinaryWriter bw)
        {
            bw.Write(R);
            bw.Write(G);
            bw.Write(B);
        }
    }
}
