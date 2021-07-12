using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrariaProtocol.DataStructures;

namespace TerrariaProtocol
{
    public class WorldMap
    {
        public const int curRelease = 194;
        public Tile[,] Tiles;
        public bool[,] SectionReceived;
        public FileMetadata WorldFileMetadata;


        public double time, worldSurface, rockLayer;
        public bool downedBoss1, downedBoss2, downedBoss3, hardMode, downedClown, ServerSideCharacter, downedPlantBoss;
        public bool shadowOrbSmashed, downedMechBoss1, downedMechBoss2, downedMechBoss3, downedMechBossAny;
        public bool dayTime, bloodMoon, eclipse, raining;
        public bool downedSlimeKing, downedQueenBee, downedFishron, downedMartians, downedAncientCultist, downedMoonlord;
        public bool downedHalloweenKing, downedHalloweenTree, downedChristmasIceQueen, downedChristmasSantank;
        public int treeBG, corruptBG, jungleBG, snowBG, hallowBG, crimsonBG, desertBG, oceanBG;
        public bool crimson, pumpkinMoon, snowMoon, expertMode, fastForwardTime;
        public bool downedChristmasTree, downedGolemBoss;
        public int moonPhase, maxTilesX, maxTilesY, spawnTileX, spawnTileY, worldID;
        public int iceBackStyle, jungleBackStyle, hellBackStyle, numClouds;
        public float windSpeedSet, cloudBGActive, maxRaining;
        public int moonType;
        public int invasionType;
        public ulong LobbyId;
        public bool slimeRain;
        public string worldName;
        public Guid UniqueId;
        public ulong WorldGeneratorVersion;
        public float IntendedSeverity;
        public bool downedPirates, downedFrost, downedGoblins, SandstormHappening, ManualParty;
        public bool DD2Ongoing, DownedInvasionT1, DownedInvasionT2, DownedInvasionT3;

        public int[] treeX = new int[4];
        public int[] treeStyle = new int[4];
        public int[] caveBackX = new int[4];
        public int[] caveBackStyle = new int[4];

        public readonly Chest[] Chests = new Chest[1000];
        public readonly Sign[] Signs = new Sign[1000];

        // Terraria.IO.WorldFile
        // Token: 0x06000A13 RID: 2579 RVA: 0x00379EAC File Offset: 0x003780AC// Terraria.IO.WorldFile
        // Token: 0x06000C3D RID: 3133 RVA: 0x003E1668 File Offset: 0x003DF868
        private int SaveFileFormatHeader(BinaryWriter writer)
        {
            short num = 470;
            short num2 = 10;
            writer.Write(194);
            WorldFileMetadata.IncrementAndWrite(writer);
            writer.Write(num2);
            for (int i = 0; i < (int)num2; i++)
            {
                writer.Write(0);
            }
            writer.Write(num);
            byte b = 0;
            byte b2 = 1;
            for (int i = 0; i < (int)num; i++)
            {
                if (Constants.tileFrameImportant[i])
                {
                    b |= b2;
                }
                if (b2 == 128)
                {
                    writer.Write(b);
                    b = 0;
                    b2 = 1;
                }
                else
                {
                    b2 = (byte)(b2 << 1);
                }
            }
            if (b2 != 1)
            {
                writer.Write(b);
            }
            return (int)writer.BaseStream.Position;
        }

        // Terraria.IO.WorldFile
        // Token: 0x06000A15 RID: 2581 RVA: 0x00379FBC File Offset: 0x003781BC
        private int SaveWorldHeader(BinaryWriter writer)
        {
            writer.Write(worldName);
            writer.Write(new Random().Next().ToString());
            writer.Write(833223655425UL);
            writer.Write(Guid.NewGuid().ToByteArray());
            writer.Write(worldID);
            writer.Write((int)0);
            writer.Write((int)134400);
            writer.Write((int)0);
            writer.Write((int)38400);
            writer.Write(maxTilesY);
            writer.Write(maxTilesX);
            writer.Write(expertMode);
            writer.Write(DateTime.Now.ToBinary());
            writer.Write((byte)moonType);
            writer.Write(treeX[0]);
            writer.Write(treeX[1]);
            writer.Write(treeX[2]);
            writer.Write(treeStyle[0]);
            writer.Write(treeStyle[1]);
            writer.Write(treeStyle[2]);
            writer.Write(treeStyle[3]);
            writer.Write(caveBackX[0]);
            writer.Write(caveBackX[1]);
            writer.Write(caveBackX[2]);
            writer.Write(caveBackStyle[0]);
            writer.Write(caveBackStyle[1]);
            writer.Write(caveBackStyle[2]);
            writer.Write(caveBackStyle[3]);
            writer.Write(iceBackStyle);
            writer.Write(jungleBackStyle);
            writer.Write(hellBackStyle);
            writer.Write(spawnTileX);
            writer.Write(spawnTileY);
            writer.Write(worldSurface);
            writer.Write(rockLayer);
            writer.Write(time);
            writer.Write(dayTime);
            writer.Write(moonPhase);
            writer.Write(bloodMoon);
            writer.Write(eclipse);
            //writer.Write(dungeonX);
            //writer.Write(dungeonY);
            writer.Write(spawnTileX);
            writer.Write(spawnTileY);
            writer.Write(crimson);
            writer.Write(downedBoss1);
            writer.Write(downedBoss2);
            writer.Write(downedBoss3);
            writer.Write(downedQueenBee);
            writer.Write(downedMechBoss1);
            writer.Write(downedMechBoss2);
            writer.Write(downedMechBoss3);
            writer.Write(downedMechBossAny);
            writer.Write(downedPlantBoss);
            writer.Write(downedGolemBoss);
            writer.Write(downedSlimeKing);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            writer.Write(downedClown);
            writer.Write(false);
            writer.Write(false);
            writer.Write(shadowOrbSmashed);
            writer.Write(false);
            writer.Write((byte)0);
            writer.Write(0);
            writer.Write(hardMode);
            writer.Write(0);
            writer.Write(0);
            writer.Write(invasionType);
            writer.Write(0.0);
            writer.Write(0.0);
            writer.Write((byte)0);
            writer.Write(raining);
            writer.Write(0);
            writer.Write(maxRaining);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write((byte)treeBG);
            writer.Write((byte)corruptBG);
            writer.Write((byte)jungleBG);
            writer.Write((byte)snowBG);
            writer.Write((byte)hallowBG);
            writer.Write((byte)crimsonBG);
            writer.Write((byte)desertBG);
            writer.Write((byte)oceanBG);
            writer.Write((int)cloudBGActive);
            writer.Write((short)numClouds);
            writer.Write(windSpeedSet);
            writer.Write(0);
            writer.Write(false);
            writer.Write(0);
            writer.Write(false);
            writer.Write(false);
            writer.Write(0);
            writer.Write(0);
            writer.Write((short)580);
            for (int j = 0; j < 580; j++)
            {
                writer.Write(0);
            }
            writer.Write(fastForwardTime);
            writer.Write(downedFishron);
            writer.Write(downedMartians);
            writer.Write(downedAncientCultist);
            writer.Write(downedMoonlord);
            writer.Write(downedHalloweenKing);
            writer.Write(downedHalloweenTree);
            writer.Write(downedChristmasIceQueen);
            writer.Write(downedChristmasSantank);
            writer.Write(downedChristmasTree);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            writer.Write(0);
            writer.Write(0);
            writer.Write(false);
            writer.Write(0);
            writer.Write(0f);
            writer.Write(0f);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            return (int)writer.BaseStream.Position;
        }
        public int SaveFooter(BinaryWriter writer)
        {
            writer.Write(true);
            writer.Write(worldName);
            writer.Write(worldID);
            return (int)writer.BaseStream.Position;
        }
        private int SaveHeaderPointers(BinaryWriter writer, int[] pointers)
        {
            writer.BaseStream.Position = 0L;
            writer.Write(curRelease);
            writer.BaseStream.Position += 20L;
            writer.Write((short)pointers.Length);
            for (int i = 0; i < pointers.Length; i++)
            {
                writer.Write(pointers[i]);
            }
            return (int)writer.BaseStream.Position;
        }

        private int SaveNPCs(BinaryWriter writer)
        {
            writer.Write(false);
            writer.Write(false);
            return (int)writer.BaseStream.Position;
        }

        private int SaveTileEntities(BinaryWriter writer)
        {
            writer.Write(0);
            return (int)writer.BaseStream.Position;
        }

        private int SaveSigns(BinaryWriter writer)
        {
            short num = 0;
            for (int i = 0; i < 1000; i++)
            {
                Sign sign = Signs[i];
                if (sign != null && sign.text != null)
                {
                    num += 1;
                }
            }
            writer.Write(num);
            for (int j = 0; j < 1000; j++)
            {
                Sign sign2 = Signs[j];
                if (sign2 != null && sign2.text != null)
                {
                    writer.Write(sign2.text);
                    writer.Write(sign2.x);
                    writer.Write(sign2.y);
                }
            }
            return (int)writer.BaseStream.Position;
        }
        // Terraria.IO.WorldFile
        // Token: 0x06000A16 RID: 2582 RVA: 0x0037A530 File Offset: 0x00378730
        private int SaveWorldTiles(BinaryWriter writer)
        {
            byte[] array = new byte[13];
            for (int i = 0; i < maxTilesX; i++)
            {
                float num = (float)i / (float)maxTilesX;
                for (int j = 0; j < maxTilesY; j++)
                {
                    Tile tile = Tiles[i, j];
                    int num2 = 3;
                    byte b3;
                    byte b2;
                    byte b = b2 = (b3 = 0);
                    bool flag = false;
                    if (tile.active())
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        b2 |= 2;
                        array[num2] = (byte)tile.type;
                        num2++;
                        if (tile.type > 255)
                        {
                            array[num2] = (byte)(tile.type >> 8);
                            num2++;
                            b2 |= 32;
                        }
                        if (Constants.tileFrameImportant[(int)tile.type])
                        {
                            array[num2] = (byte)(tile.frameX & 255);
                            num2++;
                            array[num2] = (byte)(((int)tile.frameX & 65280) >> 8);
                            num2++;
                            array[num2] = (byte)(tile.frameY & 255);
                            num2++;
                            array[num2] = (byte)(((int)tile.frameY & 65280) >> 8);
                            num2++;
                        }
                        if (tile.color() != 0)
                        {
                            b3 |= 8;
                            array[num2] = tile.color();
                            num2++;
                        }
                    }
                    if (tile.wall != 0)
                    {
                        b2 |= 4;
                        array[num2] = tile.wall;
                        num2++;
                        if (tile.wallColor() != 0)
                        {
                            b3 |= 16;
                            array[num2] = tile.wallColor();
                            num2++;
                        }
                    }
                    if (tile.liquid != 0)
                    {
                        if (tile.lava())
                        {
                            b2 |= 16;
                        }
                        else if (tile.honey())
                        {
                            b2 |= 24;
                        }
                        else
                        {
                            b2 |= 8;
                        }
                        array[num2] = tile.liquid;
                        num2++;
                    }
                    if (tile.wire())
                    {
                        b |= 2;
                    }
                    if (tile.wire2())
                    {
                        b |= 4;
                    }
                    if (tile.wire3())
                    {
                        b |= 8;
                    }
                    int num3;
                    if (tile.halfBrick())
                    {
                        num3 = 16;
                    }
                    else if (tile.slope() != 0)
                    {
                        num3 = (int)(tile.slope() + 1) << 4;
                    }
                    else
                    {
                        num3 = 0;
                    }
                    b |= (byte)num3;
                    if (tile.actuator())
                    {
                        b3 |= 2;
                    }
                    if (tile.inActive())
                    {
                        b3 |= 4;
                    }
                    if (tile.wire4())
                    {
                        b3 |= 32;
                    }
                    int num4 = 2;
                    if (b3 != 0)
                    {
                        b |= 1;
                        array[num4] = b3;
                        num4--;
                    }
                    if (b != 0)
                    {
                        b2 |= 1;
                        array[num4] = b;
                        num4--;
                    }
                    short num5 = 0;
                    int num6 = j + 1;
                    int num7 = maxTilesY - j - 1;
                    while (num7 > 0 && tile.isTheSameAs(Tiles[i, num6]))
                    {
                        num5 += 1;
                        num7--;
                        num6++;
                    }
                    j += (int)num5;
                    if (num5 > 0)
                    {
                        array[num2] = (byte)(num5 & 255);
                        num2++;
                        if (num5 > 255)
                        {
                            b2 |= 128;
                            array[num2] = (byte)(((int)num5 & 65280) >> 8);
                            num2++;
                        }
                        else
                        {
                            b2 |= 64;
                        }
                    }
                    array[num4] = b2;
                    writer.Write(array, num4, num2 - num4);
                }
            }
            return (int)writer.BaseStream.Position;
        }

        // Terraria.IO.WorldFile
        // Token: 0x06000A17 RID: 2583 RVA: 0x0037A930 File Offset: 0x00378B30
        private int SaveChests(BinaryWriter writer)
        {
            short num = 0;
            for (int i = 0; i < 1000; i++)
            {
                Chest chest = Chests[i];
                if (chest != null)
                {
                    bool flag = false;
                    for (int j = chest.x; j <= chest.x + 1; j++)
                    {
                        for (int k = chest.y; k <= chest.y + 1; k++)
                        {
                            if (j < 0 || k < 0 || j >= maxTilesX || k >= maxTilesY)
                            {
                                flag = true;
                                break;
                            }
                            Tile tile = Tiles[j, k];
                            if (!tile.active() || !Constants.tileContainer[(int)tile.type])
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (flag)
                    {
                        Chests[i] = null;
                    }
                    else
                    {
                        num += 1;
                    }
                }
            }
            writer.Write((short)num);
            writer.Write((short)40);
            for (int i = 0; i < 1000; i++)
            {
                --num;
                Chest chest = Chests[i];
                if (chest != null)
                {
                    writer.Write(chest.x);
                    writer.Write(chest.y);
                    writer.Write(chest.name);
                    for (int l = 0; l < 40; l++)
                    {
                        writer.Write((short)0);
                    }
                }
            }
            if (num != 0)
            {
                int a;
            }
            return (int)writer.BaseStream.Position;
        }
        private static int SaveWeightedPressurePlates(BinaryWriter writer)
        {
            writer.Write(0);
            return (int)writer.BaseStream.Position;
        }
        private static int SaveTownManager(BinaryWriter writer)
        {
            writer.Write(0);
            return (int)writer.BaseStream.Position;
        }

        public void SaveWorld_Version2(BinaryWriter writer)
        {
            int[] array = new int[10];
            array[0] = SaveFileFormatHeader(writer);
            array[1] = SaveWorldHeader(writer);
            array[2] = SaveWorldTiles(writer);
            array[3] = SaveChests(writer);
            array[4] = SaveSigns(writer);
            array[5] = SaveNPCs(writer);
            array[6] = SaveTileEntities(writer);
            array[7] = SaveWeightedPressurePlates(writer);
            array[8] = SaveTownManager(writer);
            int[] pointers = array;
            SaveFooter(writer);
            SaveHeaderPointers(writer, pointers);
        }
        private void setBG(int bg, int style)
        {
            switch (bg)
            {
                case 0:
                    treeBG = style;
                    return;
                case 1:
                    corruptBG = style;
                    return;
                case 2:
                    jungleBG = style;
                    return;
                case 3:
                    snowBG = style;
                    return;
                case 4:
                    hallowBG = style;
                    return;
                case 5:
                    crimsonBG = style;
                    return;
                case 6:
                    desertBG = style;
                    return;
                case 7:
                    oceanBG = style;
                    return;
                default:
                    return;
            }
        }

        public WorldMap(BinaryReader reader, bool nomap)
        {

            time = (double)reader.ReadInt32();
            BitsByte bitsByte3 = reader.ReadByte();
            dayTime = bitsByte3[0];
            bloodMoon = bitsByte3[1];
            eclipse = bitsByte3[2];
            moonPhase = (int)reader.ReadByte();
            maxTilesX = (int)reader.ReadInt16();
            maxTilesY = (int)reader.ReadInt16();
            spawnTileX = (int)reader.ReadInt16();
            spawnTileY = (int)reader.ReadInt16();
            worldSurface = (double)reader.ReadInt16();
            rockLayer = (double)reader.ReadInt16();
            worldID = reader.ReadInt32();
            worldName = reader.ReadString();
            UniqueId = new Guid(reader.ReadBytes(16));
            WorldGeneratorVersion = reader.ReadUInt64();
            try
            {
                moonType = (int)reader.ReadByte();
                setBG(0, (int)reader.ReadByte());
                setBG(1, (int)reader.ReadByte());
                setBG(2, (int)reader.ReadByte());
                setBG(3, (int)reader.ReadByte());
                setBG(4, (int)reader.ReadByte());
                setBG(5, (int)reader.ReadByte());
                setBG(6, (int)reader.ReadByte());
                setBG(7, (int)reader.ReadByte());
                iceBackStyle = (int)reader.ReadByte();
                jungleBackStyle = (int)reader.ReadByte();
                hellBackStyle = (int)reader.ReadByte();
                windSpeedSet = reader.ReadSingle();
                numClouds = (int)reader.ReadByte();
                for (int num15 = 0; num15 < 3; num15++)
                {
                    treeX[num15] = reader.ReadInt32();
                }
                for (int num16 = 0; num16 < 4; num16++)
                {
                    treeStyle[num16] = (int)reader.ReadByte();
                }
                for (int num17 = 0; num17 < 3; num17++)
                {
                    caveBackX[num17] = reader.ReadInt32();
                }
                for (int num18 = 0; num18 < 4; num18++)
                {
                    caveBackStyle[num18] = (int)reader.ReadByte();
                }
                maxRaining = reader.ReadSingle();
                raining = (maxRaining > 0f);
                BitsByte bitsByte4 = reader.ReadByte();
                shadowOrbSmashed = bitsByte4[0];
                downedBoss1 = bitsByte4[1];
                downedBoss2 = bitsByte4[2];
                downedBoss3 = bitsByte4[3];
                hardMode = bitsByte4[4];
                downedClown = bitsByte4[5];
                ServerSideCharacter = bitsByte4[6];
                downedPlantBoss = bitsByte4[7];
                BitsByte bitsByte5 = reader.ReadByte();
                downedMechBoss1 = bitsByte5[0];
                downedMechBoss2 = bitsByte5[1];
                downedMechBoss3 = bitsByte5[2];
                downedMechBossAny = bitsByte5[3];
                cloudBGActive = (float)(bitsByte5[4] ? 1 : 0);
                crimson = bitsByte5[5];
                pumpkinMoon = bitsByte5[6];
                snowMoon = bitsByte5[7];
                BitsByte bitsByte6 = reader.ReadByte();
                expertMode = bitsByte6[0];
                fastForwardTime = bitsByte6[1];
                bool flag4 = bitsByte6[2];
                downedSlimeKing = bitsByte6[3];
                downedQueenBee = bitsByte6[4];
                downedFishron = bitsByte6[5];
                downedMartians = bitsByte6[6];
                downedAncientCultist = bitsByte6[7];
                BitsByte bitsByte7 = reader.ReadByte();
                downedMoonlord = bitsByte7[0];
                downedHalloweenKing = bitsByte7[1];
                downedHalloweenTree = bitsByte7[2];
                downedChristmasIceQueen = bitsByte7[3];
                downedChristmasSantank = bitsByte7[4];
                downedChristmasTree = bitsByte7[5];
                downedGolemBoss = bitsByte7[6];
                ManualParty = bitsByte7[7];
                BitsByte bitsByte8 = reader.ReadByte();
                downedPirates = bitsByte8[0];
                downedFrost = bitsByte8[1];
                downedGoblins = bitsByte8[2];
                SandstormHappening = bitsByte8[3];
                DD2Ongoing = bitsByte8[4];
                DownedInvasionT1 = bitsByte8[5];
                DownedInvasionT2 = bitsByte8[6];
                DownedInvasionT3 = bitsByte8[7];
                slimeRain = flag4;
                invasionType = (int)reader.ReadSByte();
                LobbyId = reader.ReadUInt64();
                IntendedSeverity = reader.ReadSingle();
            }
            catch { }

            if (nomap) return;

            Tiles = new Tile[maxTilesX, maxTilesY];
            for (int i = 0; i < maxTilesX; ++i)
                for (int j = 0; j < maxTilesY; ++j)
                    Tiles[i, j] = new Tile();

            SectionReceived = new bool[maxTilesX / 100, maxTilesY / 100];

            WorldFileMetadata = FileMetadata.FromCurrentSettings(FileType.World);
        }
    }
}
