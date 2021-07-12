using System;
using System.IO;

namespace TerrariaProtocol.DataStructures
{
    // Token: 0x0200009E RID: 158
    public enum FileType : byte
    {
        // Token: 0x04000E0D RID: 3597
        None,
        // Token: 0x04000E0E RID: 3598
        Map,
        // Token: 0x04000E0F RID: 3599
        World,
        // Token: 0x04000E10 RID: 3600
        Player
    }

    // Token: 0x0200009D RID: 157
    public class FileMetadata
    {
        // Token: 0x060009ED RID: 2541 RVA: 0x00377E74 File Offset: 0x00376074
        public void Write(BinaryWriter writer)
        {
            writer.Write(27981915666277746UL | (ulong)this.Type << 56);
            writer.Write(this.Revision);
            writer.Write((ulong)((long)((this.IsFavorite ? 1 : 0) & 1)));
        }

        // Token: 0x060009EE RID: 2542 RVA: 0x00377EB0 File Offset: 0x003760B0
        public void IncrementAndWrite(BinaryWriter writer)
        {
            this.Revision += 1u;
            this.Write(writer);
        }

        // Token: 0x060009EF RID: 2543 RVA: 0x00377EC8 File Offset: 0x003760C8
        public static FileMetadata FromCurrentSettings(FileType type)
        {
            return new FileMetadata
            {
                Type = type,
                Revision = 0u,
                IsFavorite = false
            };
        }

        // Token: 0x060009F0 RID: 2544 RVA: 0x00377EF4 File Offset: 0x003760F4
        public static FileMetadata Read(BinaryReader reader, FileType expectedType)
        {
            FileMetadata fileMetadata = new FileMetadata();
            fileMetadata.Read(reader);
            if (fileMetadata.Type != expectedType)
            {
                throw new FileFormatException(string.Concat(new string[]
                {
                    "Expected type \"",
                    Enum.GetName(typeof(FileType), expectedType),
                    "\" but found \"",
                    Enum.GetName(typeof(FileType), fileMetadata.Type),
                    "\"."
                }));
            }
            return fileMetadata;
        }

        // Token: 0x060009F1 RID: 2545 RVA: 0x00377F78 File Offset: 0x00376178
        public void Read(BinaryReader reader)
        {
            ulong num = reader.ReadUInt64();
            if ((num & 72057594037927935UL) != 27981915666277746UL)
            {
                throw new FileFormatException("Expected Re-Logic file format.");
            }
            byte b = (byte)(num >> 56 & 255UL);
            FileType fileType = FileType.None;
            FileType[] array = (FileType[])Enum.GetValues(typeof(FileType));
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == (FileType)b)
                {
                    fileType = array[i];
                    break;
                }
            }
            if (fileType == FileType.None)
            {
                throw new FileFormatException("Found invalid file type.");
            }
            this.Type = fileType;
            this.Revision = reader.ReadUInt32();
            ulong num2 = reader.ReadUInt64();
            this.IsFavorite = ((num2 & 1UL) == 1UL);
        }

        // Token: 0x060009F2 RID: 2546 RVA: 0x00003362 File Offset: 0x00001562
        public FileMetadata()
        {
        }

        // Token: 0x04000E07 RID: 3591
        public const ulong MAGIC_NUMBER = 27981915666277746UL;

        // Token: 0x04000E08 RID: 3592
        public const int SIZE = 20;

        // Token: 0x04000E09 RID: 3593
        public FileType Type;

        // Token: 0x04000E0A RID: 3594
        public uint Revision;

        // Token: 0x04000E0B RID: 3595
        public bool IsFavorite;
    }
}
