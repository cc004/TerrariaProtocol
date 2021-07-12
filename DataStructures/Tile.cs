using System;

namespace TerrariaProtocol.DataStructures
{
    // Token: 0x02000049 RID: 73
    public class Tile
    {
        // Token: 0x06000495 RID: 1173 RVA: 0x002A538C File Offset: 0x002A358C
        public Tile()
        {
            this.type = 0;
            this.wall = 0;
            this.liquid = 0;
            this.sTileHeader = 0;
            this.bTileHeader = 0;
            this.bTileHeader2 = 0;
            this.bTileHeader3 = 0;
            this.frameX = 0;
            this.frameY = 0;
        }

        // Token: 0x06000496 RID: 1174 RVA: 0x002A53E0 File Offset: 0x002A35E0
        public Tile(Tile copy)
        {
            if (copy == null)
            {
                this.type = 0;
                this.wall = 0;
                this.liquid = 0;
                this.sTileHeader = 0;
                this.bTileHeader = 0;
                this.bTileHeader2 = 0;
                this.bTileHeader3 = 0;
                this.frameX = 0;
                this.frameY = 0;
                return;
            }
            this.type = copy.type;
            this.wall = copy.wall;
            this.liquid = copy.liquid;
            this.sTileHeader = copy.sTileHeader;
            this.bTileHeader = copy.bTileHeader;
            this.bTileHeader2 = copy.bTileHeader2;
            this.bTileHeader3 = copy.bTileHeader3;
            this.frameX = copy.frameX;
            this.frameY = copy.frameY;
        }

        // Token: 0x06000497 RID: 1175 RVA: 0x0000665F File Offset: 0x0000485F
        public object Clone()
        {
            return base.MemberwiseClone();
        }

        // Token: 0x06000498 RID: 1176 RVA: 0x002A54A4 File Offset: 0x002A36A4
        public void ClearEverything()
        {
            this.type = 0;
            this.wall = 0;
            this.liquid = 0;
            this.sTileHeader = 0;
            this.bTileHeader = 0;
            this.bTileHeader2 = 0;
            this.bTileHeader3 = 0;
            this.frameX = 0;
            this.frameY = 0;
        }

        // Token: 0x06000499 RID: 1177 RVA: 0x00008519 File Offset: 0x00006719
        public void ClearTile()
        {
            this.slope(0);
            this.halfBrick(false);
            this.active(false);
        }

        // Token: 0x0600049A RID: 1178 RVA: 0x002A54F0 File Offset: 0x002A36F0
        public void CopyFrom(Tile from)
        {
            this.type = from.type;
            this.wall = from.wall;
            this.liquid = from.liquid;
            this.sTileHeader = from.sTileHeader;
            this.bTileHeader = from.bTileHeader;
            this.bTileHeader2 = from.bTileHeader2;
            this.bTileHeader3 = from.bTileHeader3;
            this.frameX = from.frameX;
            this.frameY = from.frameY;
        }

        // Token: 0x0600049D RID: 1181 RVA: 0x002A5680 File Offset: 0x002A3880
        public int blockType()
        {
            if (this.halfBrick())
            {
                return 1;
            }
            int num = (int)this.slope();
            if (num > 0)
            {
                num++;
            }
            return num;
        }

        // Token: 0x0600049E RID: 1182 RVA: 0x00008530 File Offset: 0x00006730
        public void liquidType(int liquidType)
        {
            if (liquidType == 0)
            {
                this.bTileHeader &= 159;
                return;
            }
            if (liquidType == 1)
            {
                this.lava(true);
                return;
            }
            if (liquidType == 2)
            {
                this.honey(true);
            }
        }
        // Terraria.Tile
        // Token: 0x06000489 RID: 1161 RVA: 0x002FA140 File Offset: 0x002F8340
        public virtual bool isTheSameAs(Tile compTile)
        {
            if (compTile == null)
            {
                return false;
            }
            if (this.sTileHeader != compTile.sTileHeader)
            {
                return false;
            }
            if (this.active())
            {
                if (this.type != compTile.type)
                {
                    return false;
                }
                if (Constants.tileFrameImportant[(int)this.type] && (this.frameX != compTile.frameX || this.frameY != compTile.frameY))
                {
                    return false;
                }
            }
            if (this.wall != compTile.wall || this.liquid != compTile.liquid)
            {
                return false;
            }
            if (compTile.liquid == 0)
            {
                if (this.wallColor() != compTile.wallColor())
                {
                    return false;
                }
            }
            else if (this.bTileHeader != compTile.bTileHeader)
            {
                return false;
            }
            return true;
        }

        // Token: 0x0600049F RID: 1183 RVA: 0x00008560 File Offset: 0x00006760
        public byte liquidType()
        {
            return (byte)((this.bTileHeader & 96) >> 5);
        }

        // Token: 0x060004A0 RID: 1184 RVA: 0x0000856E File Offset: 0x0000676E
        public bool nactive()
        {
            return (this.sTileHeader & 96) == 32;
        }

        // Token: 0x060004A1 RID: 1185 RVA: 0x00008580 File Offset: 0x00006780
        public void ResetToType(ushort type)
        {
            this.liquid = 0;
            this.sTileHeader = 32;
            this.bTileHeader = 0;
            this.bTileHeader2 = 0;
            this.bTileHeader3 = 0;
            this.frameX = 0;
            this.frameY = 0;
            this.type = type;
        }

        // Token: 0x060004A2 RID: 1186 RVA: 0x000085BB File Offset: 0x000067BB
        internal void ClearMetadata()
        {
            this.liquid = 0;
            this.sTileHeader = 0;
            this.bTileHeader = 0;
            this.bTileHeader2 = 0;
            this.bTileHeader3 = 0;
            this.frameX = 0;
            this.frameY = 0;
        }

        // Token: 0x060004A3 RID: 1187 RVA: 0x002A56A8 File Offset: 0x002A38A8
        public Color actColor(Color oldColor)
        {
            if (!this.inActive())
            {
                return oldColor;
            }
            double num = 0.4;
            return new Color((int)((byte)(num * (double)oldColor.R)), (int)((byte)(num * (double)oldColor.G)), (int)((byte)(num * (double)oldColor.B)), (int)oldColor.A);
        }

        // Token: 0x060004A4 RID: 1188 RVA: 0x002A56F8 File Offset: 0x002A38F8
        public bool topSlope()
        {
            byte b = this.slope();
            return b == 1 || b == 2;
        }

        // Token: 0x060004A5 RID: 1189 RVA: 0x002A5718 File Offset: 0x002A3918
        public bool bottomSlope()
        {
            byte b = this.slope();
            return b == 3 || b == 4;
        }

        // Token: 0x060004A6 RID: 1190 RVA: 0x002A5738 File Offset: 0x002A3938
        public bool leftSlope()
        {
            byte b = this.slope();
            return b == 2 || b == 4;
        }

        // Token: 0x060004A7 RID: 1191 RVA: 0x002A5758 File Offset: 0x002A3958
        public bool rightSlope()
        {
            byte b = this.slope();
            return b == 1 || b == 3;
        }

        // Token: 0x060004A8 RID: 1192 RVA: 0x000085EE File Offset: 0x000067EE
        public bool HasSameSlope(Tile tile)
        {
            return (this.sTileHeader & 29696) == (tile.sTileHeader & 29696);
        }

        // Token: 0x060004A9 RID: 1193 RVA: 0x0000860A File Offset: 0x0000680A
        public byte wallColor()
        {
            return (byte)(this.bTileHeader & 31);
        }

        // Token: 0x060004AA RID: 1194 RVA: 0x00008616 File Offset: 0x00006816
        public void wallColor(byte wallColor)
        {
            if (wallColor > 30)
            {
                wallColor = 30;
            }
            this.bTileHeader = (byte)((this.bTileHeader & 224) | wallColor);
        }

        // Token: 0x060004AB RID: 1195 RVA: 0x00008636 File Offset: 0x00006836
        public bool lava()
        {
            return (this.bTileHeader & 32) == 32;
        }

        // Token: 0x060004AC RID: 1196 RVA: 0x00008645 File Offset: 0x00006845
        public void lava(bool lava)
        {
            if (lava)
            {
                this.bTileHeader = (byte)((this.bTileHeader & 159) | 32);
                return;
            }
            this.bTileHeader &= 223;
        }

        // Token: 0x060004AD RID: 1197 RVA: 0x00008674 File Offset: 0x00006874
        public bool honey()
        {
            return (this.bTileHeader & 64) == 64;
        }

        // Token: 0x060004AE RID: 1198 RVA: 0x00008683 File Offset: 0x00006883
        public void honey(bool honey)
        {
            if (honey)
            {
                this.bTileHeader = (byte)((this.bTileHeader & 159) | 64);
                return;
            }
            this.bTileHeader &= 191;
        }

        // Token: 0x060004AF RID: 1199 RVA: 0x000086B2 File Offset: 0x000068B2
        public bool wire4()
        {
            return (this.bTileHeader & 128) == 128;
        }

        // Token: 0x060004B0 RID: 1200 RVA: 0x000086C7 File Offset: 0x000068C7
        public void wire4(bool wire4)
        {
            if (wire4)
            {
                this.bTileHeader |= 128;
                return;
            }
            this.bTileHeader &= 127;
        }

        // Token: 0x060004B1 RID: 1201 RVA: 0x000086F0 File Offset: 0x000068F0
        public int wallFrameX()
        {
            return (int)((this.bTileHeader2 & 15) * 36);
        }

        // Token: 0x060004B2 RID: 1202 RVA: 0x000086FE File Offset: 0x000068FE
        public void wallFrameX(int wallFrameX)
        {
            this.bTileHeader2 = (byte)((int)(this.bTileHeader2 & 240) | (wallFrameX / 36 & 15));
        }

        // Token: 0x060004B3 RID: 1203 RVA: 0x0000871B File Offset: 0x0000691B
        public byte frameNumber()
        {
            return (byte)((this.bTileHeader2 & 48) >> 4);
        }

        // Token: 0x060004B4 RID: 1204 RVA: 0x00008729 File Offset: 0x00006929
        public void frameNumber(byte frameNumber)
        {
            this.bTileHeader2 = (byte)((int)(this.bTileHeader2 & 207) | (int)(frameNumber & 3) << 4);
        }

        // Token: 0x060004B5 RID: 1205 RVA: 0x00008744 File Offset: 0x00006944
        public byte wallFrameNumber()
        {
            return (byte)((this.bTileHeader2 & 192) >> 6);
        }

        // Token: 0x060004B6 RID: 1206 RVA: 0x00008755 File Offset: 0x00006955
        public void wallFrameNumber(byte wallFrameNumber)
        {
            this.bTileHeader2 = (byte)((int)(this.bTileHeader2 & 63) | (int)(wallFrameNumber & 3) << 6);
        }

        // Token: 0x060004B7 RID: 1207 RVA: 0x0000876D File Offset: 0x0000696D
        public int wallFrameY()
        {
            return (int)((this.bTileHeader3 & 7) * 36);
        }

        // Token: 0x060004B8 RID: 1208 RVA: 0x0000877A File Offset: 0x0000697A
        public void wallFrameY(int wallFrameY)
        {
            this.bTileHeader3 = (byte)((int)(this.bTileHeader3 & 248) | (wallFrameY / 36 & 7));
        }

        // Token: 0x060004B9 RID: 1209 RVA: 0x00008796 File Offset: 0x00006996
        public bool checkingLiquid()
        {
            return (this.bTileHeader3 & 8) == 8;
        }

        // Token: 0x060004BA RID: 1210 RVA: 0x000087A3 File Offset: 0x000069A3
        public void checkingLiquid(bool checkingLiquid)
        {
            if (checkingLiquid)
            {
                this.bTileHeader3 |= 8;
                return;
            }
            this.bTileHeader3 &= 247;
        }

        // Token: 0x060004BB RID: 1211 RVA: 0x000087CB File Offset: 0x000069CB
        public bool skipLiquid()
        {
            return (this.bTileHeader3 & 16) == 16;
        }

        // Token: 0x060004BC RID: 1212 RVA: 0x000087DA File Offset: 0x000069DA
        public void skipLiquid(bool skipLiquid)
        {
            if (skipLiquid)
            {
                this.bTileHeader3 |= 16;
                return;
            }
            this.bTileHeader3 &= 239;
        }

        // Token: 0x060004BD RID: 1213 RVA: 0x00008803 File Offset: 0x00006A03
        public byte color()
        {
            return (byte)(this.sTileHeader & 31);
        }

        // Token: 0x060004BE RID: 1214 RVA: 0x0000880F File Offset: 0x00006A0F
        public void color(byte color)
        {
            if (color > 30)
            {
                color = 30;
            }
            this.sTileHeader = (short)(((int)this.sTileHeader & 65504) | (int)color);
        }

        // Token: 0x060004BF RID: 1215 RVA: 0x0000882F File Offset: 0x00006A2F
        public bool active()
        {
            return (this.sTileHeader & 32) == 32;
        }

        // Token: 0x060004C0 RID: 1216 RVA: 0x0000883E File Offset: 0x00006A3E
        public void active(bool active)
        {
            if (active)
            {
                this.sTileHeader |= 32;
                return;
            }
            this.sTileHeader = (short)((int)this.sTileHeader & 65503);
        }

        // Token: 0x060004C1 RID: 1217 RVA: 0x00008867 File Offset: 0x00006A67
        public bool inActive()
        {
            return (this.sTileHeader & 64) == 64;
        }

        // Token: 0x060004C2 RID: 1218 RVA: 0x00008876 File Offset: 0x00006A76
        public void inActive(bool inActive)
        {
            if (inActive)
            {
                this.sTileHeader |= 64;
                return;
            }
            this.sTileHeader = (short)((int)this.sTileHeader & 65471);
        }

        // Token: 0x060004C3 RID: 1219 RVA: 0x0000889F File Offset: 0x00006A9F
        public bool wire()
        {
            return (this.sTileHeader & 128) == 128;
        }

        // Token: 0x060004C4 RID: 1220 RVA: 0x000088B4 File Offset: 0x00006AB4
        public void wire(bool wire)
        {
            if (wire)
            {
                this.sTileHeader |= 128;
                return;
            }
            this.sTileHeader = (short)((int)this.sTileHeader & 65407);
        }

        // Token: 0x060004C5 RID: 1221 RVA: 0x000088E0 File Offset: 0x00006AE0
        public bool wire2()
        {
            return (this.sTileHeader & 256) == 256;
        }

        // Token: 0x060004C6 RID: 1222 RVA: 0x000088F5 File Offset: 0x00006AF5
        public void wire2(bool wire2)
        {
            if (wire2)
            {
                this.sTileHeader |= 256;
                return;
            }
            this.sTileHeader = (short)((int)this.sTileHeader & 65279);
        }

        // Token: 0x060004C7 RID: 1223 RVA: 0x00008921 File Offset: 0x00006B21
        public bool wire3()
        {
            return (this.sTileHeader & 512) == 512;
        }

        // Token: 0x060004C8 RID: 1224 RVA: 0x00008936 File Offset: 0x00006B36
        public void wire3(bool wire3)
        {
            if (wire3)
            {
                this.sTileHeader |= 512;
                return;
            }
            this.sTileHeader = (short)((int)this.sTileHeader & 65023);
        }

        // Token: 0x060004C9 RID: 1225 RVA: 0x00008962 File Offset: 0x00006B62
        public bool halfBrick()
        {
            return (this.sTileHeader & 1024) == 1024;
        }

        // Token: 0x060004CA RID: 1226 RVA: 0x00008977 File Offset: 0x00006B77
        public void halfBrick(bool halfBrick)
        {
            if (halfBrick)
            {
                this.sTileHeader |= 1024;
                return;
            }
            this.sTileHeader = (short)((int)this.sTileHeader & 64511);
        }

        // Token: 0x060004CB RID: 1227 RVA: 0x000089A3 File Offset: 0x00006BA3
        public bool actuator()
        {
            return (this.sTileHeader & 2048) == 2048;
        }

        // Token: 0x060004CC RID: 1228 RVA: 0x000089B8 File Offset: 0x00006BB8
        public void actuator(bool actuator)
        {
            if (actuator)
            {
                this.sTileHeader |= 2048;
                return;
            }
            this.sTileHeader = (short)((int)this.sTileHeader & 63487);
        }

        // Token: 0x060004CD RID: 1229 RVA: 0x000089E4 File Offset: 0x00006BE4
        public byte slope()
        {
            return (byte)((this.sTileHeader & 28672) >> 12);
        }

        // Token: 0x060004CE RID: 1230 RVA: 0x000089F6 File Offset: 0x00006BF6
        public void slope(byte slope)
        {
            this.sTileHeader = (short)(((int)this.sTileHeader & 36863) | (int)(slope & 7) << 12);
        }

        // Token: 0x060004CF RID: 1231 RVA: 0x002A5778 File Offset: 0x002A3978
        // Token: 0x060004D0 RID: 1232 RVA: 0x002A58B4 File Offset: 0x002A3AB4
        public override string ToString()
        {
            return string.Concat(new object[]
            {
                "Tile Type:",
                this.type,
                " Active:",
                this.active().ToString(),
                " Wall:",
                this.wall,
                " Slope:",
                this.slope(),
                " fX:",
                this.frameX,
                " fY:",
                this.frameY
            });
        }

        // Token: 0x0400075D RID: 1885
        public ushort type;

        // Token: 0x0400075E RID: 1886
        public byte wall;

        // Token: 0x0400075F RID: 1887
        public byte liquid;

        // Token: 0x04000760 RID: 1888
        public short sTileHeader;

        // Token: 0x04000761 RID: 1889
        public byte bTileHeader;

        // Token: 0x04000762 RID: 1890
        public byte bTileHeader2;

        // Token: 0x04000763 RID: 1891
        public byte bTileHeader3;

        // Token: 0x04000764 RID: 1892
        public short frameX;

        // Token: 0x04000765 RID: 1893
        public short frameY;

        // Token: 0x04000766 RID: 1894
        public const int Type_Solid = 0;

        // Token: 0x04000767 RID: 1895
        public const int Type_Halfbrick = 1;

        // Token: 0x04000768 RID: 1896
        public const int Type_SlopeDownRight = 2;

        // Token: 0x04000769 RID: 1897
        public const int Type_SlopeDownLeft = 3;

        // Token: 0x0400076A RID: 1898
        public const int Type_SlopeUpRight = 4;

        // Token: 0x0400076B RID: 1899
        public const int Type_SlopeUpLeft = 5;

        // Token: 0x0400076C RID: 1900
        public const int Liquid_Water = 0;

        // Token: 0x0400076D RID: 1901
        public const int Liquid_Lava = 1;

        // Token: 0x0400076E RID: 1902
        public const int Liquid_Honey = 2;
    }
}
