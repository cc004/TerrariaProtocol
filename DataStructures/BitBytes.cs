using System;
using System.Collections.Generic;
using System.IO;

namespace TerrariaProtocol.DataStructures
{
    // Token: 0x02000006 RID: 6
    public struct BitsByte
    {
        // Token: 0x06000031 RID: 49 RVA: 0x000035DC File Offset: 0x000017DC
        public BitsByte(bool b1 = false, bool b2 = false, bool b3 = false, bool b4 = false, bool b5 = false, bool b6 = false, bool b7 = false, bool b8 = false)
        {
            this.value = 0;
            this[0] = b1;
            this[1] = b2;
            this[2] = b3;
            this[3] = b4;
            this[4] = b5;
            this[5] = b6;
            this[6] = b7;
            this[7] = b8;
        }

        // Token: 0x06000032 RID: 50 RVA: 0x00003638 File Offset: 0x00001838
        public void ClearAll()
        {
            this.value = 0;
        }

        // Token: 0x06000033 RID: 51 RVA: 0x00003644 File Offset: 0x00001844
        public void SetAll()
        {
            this.value = byte.MaxValue;
        }

        // Token: 0x1700001F RID: 31
        public bool this[int key]
        {
            get
            {
                return ((int)this.value & 1 << key) != 0;
            }
            set
            {
                if (value)
                {
                    this.value |= (byte)(1 << key);
                    return;
                }
                this.value &= (byte)(~(byte)(1 << key));
            }
        }

        // Token: 0x06000036 RID: 54 RVA: 0x0000369C File Offset: 0x0000189C
        public void Retrieve(ref bool b0)
        {
            this.Retrieve(ref b0, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
        }

        // Token: 0x06000037 RID: 55 RVA: 0x000036D4 File Offset: 0x000018D4
        public void Retrieve(ref bool b0, ref bool b1)
        {
            this.Retrieve(ref b0, ref b1, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
        }

        // Token: 0x06000038 RID: 56 RVA: 0x00003708 File Offset: 0x00001908
        public void Retrieve(ref bool b0, ref bool b1, ref bool b2)
        {
            this.Retrieve(ref b0, ref b1, ref b2, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
        }

        // Token: 0x06000039 RID: 57 RVA: 0x00003738 File Offset: 0x00001938
        public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3)
        {
            this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
        }

        // Token: 0x0600003A RID: 58 RVA: 0x00003764 File Offset: 0x00001964
        public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4)
        {
            this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
        }

        // Token: 0x0600003B RID: 59 RVA: 0x00003790 File Offset: 0x00001990
        public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4, ref bool b5)
        {
            this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref BitsByte.Null, ref BitsByte.Null);
        }

        // Token: 0x0600003C RID: 60 RVA: 0x000037B8 File Offset: 0x000019B8
        public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4, ref bool b5, ref bool b6)
        {
            this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref BitsByte.Null);
        }

        // Token: 0x0600003D RID: 61 RVA: 0x000037DC File Offset: 0x000019DC
        public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4, ref bool b5, ref bool b6, ref bool b7)
        {
            b0 = this[0];
            b1 = this[1];
            b2 = this[2];
            b3 = this[3];
            b4 = this[4];
            b5 = this[5];
            b6 = this[6];
            b7 = this[7];
        }

        // Token: 0x0600003E RID: 62 RVA: 0x00003838 File Offset: 0x00001A38
        public static implicit operator byte(BitsByte bb)
        {
            return bb.value;
        }

        // Token: 0x0600003F RID: 63 RVA: 0x00003840 File Offset: 0x00001A40
        public static implicit operator BitsByte(byte b)
        {
            return new BitsByte
            {
                value = b
            };
        }

        // Token: 0x06000040 RID: 64 RVA: 0x00003860 File Offset: 0x00001A60
        public static BitsByte[] ComposeBitsBytesChain(bool optimizeLength, params bool[] flags)
        {
            int i = flags.Length;
            int num = 0;
            while (i > 0)
            {
                num++;
                i -= 7;
            }
            BitsByte[] array = new BitsByte[num];
            int num2 = 0;
            int num3 = 0;
            for (int j = 0; j < flags.Length; j++)
            {
                array[num3][num2] = flags[j];
                num2++;
                if (num2 == 7 && num3 < num - 1)
                {
                    array[num3][num2] = true;
                    num2 = 0;
                    num3++;
                }
            }
            if (optimizeLength)
            {
                int num4 = array.Length - 1;
                while (array[num4] == 0 && num4 > 0)
                {
                    array[num4 - 1][7] = false;
                    num4--;
                }
                Array.Resize<BitsByte>(ref array, num4 + 1);
            }
            return array;
        }

        // Token: 0x06000041 RID: 65 RVA: 0x0000391C File Offset: 0x00001B1C
        public static BitsByte[] DecomposeBitsBytesChain(BinaryReader reader)
        {
            List<BitsByte> list = new List<BitsByte>();
            BitsByte item;
            do
            {
                item = reader.ReadByte();
                list.Add(item);
            }
            while (item[7]);
            return list.ToArray();
        }

        // Token: 0x0400002F RID: 47
        public static bool Null;

        // Token: 0x04000030 RID: 48
        public byte value;
    }
}
