using System.IO;

namespace TerrariaProtocol.DataStructures
{
    // Token: 0x020002D2 RID: 722
    public class PlayerDeathReason
    {
        // Token: 0x06001777 RID: 6007 RVA: 0x00439FA0 File Offset: 0x004381A0
        public PlayerDeathReason()
        {
        }

        // Token: 0x06001778 RID: 6008 RVA: 0x00439FC4 File Offset: 0x004381C4
        public static PlayerDeathReason LegacyEmpty()
        {
            return new PlayerDeathReason
            {
                SourceOtherIndex = 254
            };
        }

        // Token: 0x06001779 RID: 6009 RVA: 0x00439FD8 File Offset: 0x004381D8
        public static PlayerDeathReason LegacyDefault()
        {
            return new PlayerDeathReason
            {
                SourceOtherIndex = 255
            };
        }

        // Token: 0x0600177A RID: 6010 RVA: 0x00439FEC File Offset: 0x004381EC
        public static PlayerDeathReason ByNPC(int index)
        {
            return new PlayerDeathReason
            {
                SourceNPCIndex = index
            };
        }

        // Token: 0x0600177B RID: 6011 RVA: 0x00439FFC File Offset: 0x004381FC
        public static PlayerDeathReason ByCustomReason(string reasonInEnglish)
        {
            return new PlayerDeathReason
            {
                SourceCustomReason = reasonInEnglish
            };
        }

        // Token: 0x0600177D RID: 6013 RVA: 0x0043A070 File Offset: 0x00438270
        public static PlayerDeathReason ByOther(int type)
        {
            return new PlayerDeathReason
            {
                SourceOtherIndex = type
            };
        }

        // Token: 0x06001780 RID: 6016 RVA: 0x0043A148 File Offset: 0x00438348
        public void WriteSelfTo(BinaryWriter writer)
        {
            BitsByte bb = 0;
            bb[0] = (this.SourcePlayerIndex != -1);
            bb[1] = (this.SourceNPCIndex != -1);
            bb[2] = (this.SourceProjectileIndex != -1);
            bb[3] = (this.SourceOtherIndex != -1);
            bb[4] = (this.SourceProjectileType != 0);
            bb[5] = (this.SourceItemType != 0);
            bb[6] = (this.SourceItemPrefix != 0);
            bb[7] = (this.SourceCustomReason != null);
            writer.Write(bb);
            if (bb[0])
            {
                writer.Write((short)this.SourcePlayerIndex);
            }
            if (bb[1])
            {
                writer.Write((short)this.SourceNPCIndex);
            }
            if (bb[2])
            {
                writer.Write((short)this.SourceProjectileIndex);
            }
            if (bb[3])
            {
                writer.Write((byte)this.SourceOtherIndex);
            }
            if (bb[4])
            {
                writer.Write((short)this.SourceProjectileType);
            }
            if (bb[5])
            {
                writer.Write((short)this.SourceItemType);
            }
            if (bb[6])
            {
                writer.Write((byte)this.SourceItemPrefix);
            }
            if (bb[7])
            {
                writer.Write(this.SourceCustomReason);
            }
        }

        // Token: 0x06001781 RID: 6017 RVA: 0x0043A2B4 File Offset: 0x004384B4
        public static PlayerDeathReason FromReader(BinaryReader reader)
        {
            PlayerDeathReason playerDeathReason = new PlayerDeathReason();
            BitsByte bitsByte = reader.ReadByte();
            if (bitsByte[0])
            {
                playerDeathReason.SourcePlayerIndex = (int)reader.ReadInt16();
            }
            if (bitsByte[1])
            {
                playerDeathReason.SourceNPCIndex = (int)reader.ReadInt16();
            }
            if (bitsByte[2])
            {
                playerDeathReason.SourceProjectileIndex = (int)reader.ReadInt16();
            }
            if (bitsByte[3])
            {
                playerDeathReason.SourceOtherIndex = (int)reader.ReadByte();
            }
            if (bitsByte[4])
            {
                playerDeathReason.SourceProjectileType = (int)reader.ReadInt16();
            }
            if (bitsByte[5])
            {
                playerDeathReason.SourceItemType = (int)reader.ReadInt16();
            }
            if (bitsByte[6])
            {
                playerDeathReason.SourceItemPrefix = (int)reader.ReadByte();
            }
            if (bitsByte[7])
            {
                playerDeathReason.SourceCustomReason = reader.ReadString();
            }
            return playerDeathReason;
        }

        // Token: 0x04003C2E RID: 15406
        public int SourcePlayerIndex = -1;

        // Token: 0x04003C2F RID: 15407
        public int SourceNPCIndex = -1;

        // Token: 0x04003C30 RID: 15408
        public int SourceProjectileIndex = -1;

        // Token: 0x04003C31 RID: 15409
        public int SourceOtherIndex = -1;

        // Token: 0x04003C32 RID: 15410
        public int SourceProjectileType;

        // Token: 0x04003C33 RID: 15411
        public int SourceItemType;

        // Token: 0x04003C34 RID: 15412
        public int SourceItemPrefix;

        // Token: 0x04003C35 RID: 15413
        public string SourceCustomReason;
    }
}
