using Ionic.Zlib;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TerrariaProtocol.DataStructures;

namespace TerrariaProtocol
{
    class TClient
    {
        public TcpClient client;
        private static bool nomap = true;

        public byte PlayerSlot { get; private set; }
        public string CurRelease = "Terraria194";
        public bool IsPlaying { get; private set; }
        public WorldMap Map { get; private set; }

        public readonly Player[] Players;

        public Player MyPlayer
        {
            get => Players[PlayerSlot];
            set => Players[PlayerSlot] = value;
        }

        public delegate void OnSpawnCallback(TClient self);
        public event OnSpawnCallback OnSpawn;
        public delegate void OnTogglePvpCallback(TClient self, byte slot);
        public event OnTogglePvpCallback OnTogglePvp;
        public delegate void OnPlayerUpdateCallback(TClient self, byte slot);
        public event OnPlayerUpdateCallback OnPlayerUpdate;
        public delegate void OnTileSendSectionCallback(TClient self, int x, int y, short w, short h);
        public event OnTileSendSectionCallback OnTileSendSection;
        public delegate void OnChatCallback(TClient self, string text, Color color);
        public event OnChatCallback OnChat;
        public delegate void OnSendPlayerCallback(TClient self, ref Player player);
        public event OnSendPlayerCallback OnSendPlayer;
        public delegate void OnStatusBarCallback(TClient self, string text);
        public event OnStatusBarCallback OnStatusBar;

        public TClient()
        {
            client = new TcpClient();
            Players = new Player[256];

            for (int i = 0; i < Players.Length; ++i)
                Players[i] = new Player();
        }

        public void Connect(IPEndPoint server, IPEndPoint proxy = null)
        {
            if (proxy == null)
            {
                client.Connect(server);
                return;
            }

            client.Connect(proxy);

            //Console.WriteLine("Proxy connected to " + proxy.ToString());
            var encoding = new UTF8Encoding(false, true);
            using (var sw = new StreamWriter(client.GetStream(), encoding, 4096, true) { NewLine = "\r\n" })
            using (var sr = new StreamReader(client.GetStream(), encoding, false, 4096, true))
            {
                sw.WriteLine($"CONNECT {server.ToString()} HTTP/1.1");
                sw.WriteLine("User-Agent: Java/1.8.0_192");
                sw.WriteLine($"Host: {server.ToString()}");
                sw.WriteLine("Accept: text/html, image/gif, image/jpeg, *; q=.2, */*; q=.2");
                sw.WriteLine("Proxy-Connection: keep-alive");
                sw.WriteLine();
                sw.Flush();

                var resp = sr.ReadLine();
                Console.WriteLine("Proxy connection; " + resp);
                if (!resp.StartsWith("HTTP/1.1 200")) throw new Exception();

                while (true)
                {
                    resp = sr.ReadLine();
                    if (string.IsNullOrEmpty(resp)) break;
                }
            }
        }

        public void KillServer()
        {
            client.GetStream().Write(new byte[] { 0, 0 }, 0, 2);
        }
        public Packet Receive()
        {
            NetworkStream stream = client.GetStream();
            int size = stream.ReadByte();
            if (size == -1) return null;
            Packet packet = new Packet();
            size = size + (stream.ReadByte() << 8);
            byte[] buffer = new byte[size - 3];
            packet.Type = (PacketTypes)stream.ReadByte();
            int start = 0, remain = size - 3;
            while (remain > 0)
            {
                int t = stream.Read(buffer, start, remain);
                start += t;
                remain -= t;
            }
            packet.Writer.Write(buffer);
            packet.Reader.BaseStream.Seek(0, SeekOrigin.Begin);
            return packet;
        }
        public void Send(Packet packet)
        {
            byte[] raw = packet.ToArray();
            client.GetStream().Write(raw, 0, raw.Length);
        }
        public void Hello(string message)
        {
            Packet hello = new Packet();
            hello.Type = PacketTypes.ConnectRequest;
            hello.Writer.Write(message);
            Send(hello);
        }
        
        public void HurtPlayer(byte playerSlot, PlayerDeathReason reason, short damage, byte dir, DamageType type)
        {
            Packet hurt = new Packet(PacketTypes.PlayerHurtV2, playerSlot);
            hurt.Writer.Write(reason);
            hurt.Writer.Write(damage);
            hurt.Writer.Write(dir);
            hurt.Writer.Write((byte) type);
            Send(hurt);
        }
        public void SendPassword(string message)
        {
            Packet password = new Packet();
            password.Type = PacketTypes.PasswordSend;
            password.Writer.Write(message);
            Send(password);
        }
        
        public void TogglePvp(bool isPvp)
        {
            Packet pvp = new Packet(PacketTypes.TogglePvp, PlayerSlot);
            pvp.Writer.Write(isPvp);
            Send(pvp);
        }
        public void SendPlayerInfo(Player player)
        {
            Packet playerInfo = new Packet(PacketTypes.PlayerInfo, PlayerSlot);
            PlayerInfo PlayerInfo = player.playerInfo;
            playerInfo.Writer.Write(PlayerInfo.SkinVariant);
            playerInfo.Writer.Write(PlayerInfo.Hair);
            playerInfo.Writer.Write(PlayerInfo.Name);
            playerInfo.Writer.Write(PlayerInfo.HairDye);
            playerInfo.Writer.Write(PlayerInfo.HideVis1);
            playerInfo.Writer.Write(PlayerInfo.HideVis2);
            playerInfo.Writer.Write(PlayerInfo.HideMisc);
            PlayerInfo.HairColor.WriteTo(playerInfo.Writer);
            PlayerInfo.SkinColor.WriteTo(playerInfo.Writer);
            PlayerInfo.EyeColor.WriteTo(playerInfo.Writer);
            PlayerInfo.ShirtColor.WriteTo(playerInfo.Writer);
            PlayerInfo.UndershirtColor.WriteTo(playerInfo.Writer);
            PlayerInfo.PantsColor.WriteTo(playerInfo.Writer);
            PlayerInfo.ShoeColor.WriteTo(playerInfo.Writer);
            playerInfo.Writer.Write((byte)PlayerInfo.difficulty);
            playerInfo.Writer.Write(0);
            Send(playerInfo);
        }

        public void UpdatePlayerHp(Player player)
        {
            Packet life = new Packet(PacketTypes.PlayerHp, PlayerSlot);
            life.Writer.Write(player.Life);
            life.Writer.Write(player.LifeMax);
            Send(life);
        }
        public void UpdatePlayerMana(Player player)
        {
            Packet mana = new Packet(PacketTypes.PlayerMana, PlayerSlot);
            mana.Writer.Write(player.Mana);
            mana.Writer.Write(player.ManaMax);
            Send(mana);
        }
        public void UpdatePlayerBuff(Player player)
        {
            Packet buff = new Packet(PacketTypes.PlayerBuff, PlayerSlot);
            buff.Writer.Write(player.Buff);
            Send(buff);
        }
        public void UpdatePlayerSlot(Player player, byte itemSlot)
        {
            Packet slot = new Packet(PacketTypes.PlayerSlot, PlayerSlot);
            Item item = player.Inventory[itemSlot];
            slot.Writer.Write(itemSlot);
            slot.Writer.Write(item.Stack);
            slot.Writer.Write(item.prefix);
            slot.Writer.Write(item.ID);
            Send(slot);
        }

        public void TileGetSection(int x, int y)
        {
            Packet request = new Packet(PacketTypes.TileGetSection);
            request.Writer.Write(x);
            request.Writer.Write(y);
            Send(request);
        }
        public void Spawn(int x, int y)
        {
            Packet request = new Packet(PacketTypes.PlayerSpawn, PlayerSlot);
            request.Writer.Write((short)x);
            request.Writer.Write((short)y);
            request.Writer.Write(0);
            request.Writer.Write((byte)1);
            Send(request);
        }
        public void SendPlayer(Player player)
        {
            SendPlayerInfo(player);
            var uuid = new Packet(PacketTypes.ClientUUID);
            uuid.Writer.Write(Guid.NewGuid().ToString());
            Send(uuid);
            UpdatePlayerHp(player);
            UpdatePlayerMana(player);
            UpdatePlayerBuff(player);
            for (byte i = 0; i < 73; ++i)
                UpdatePlayerSlot(player, i);
        }

        public void UpdateProjectile(short identity, Vector2 position, Vector2 velocity, float knockback, short damage, short type, byte[] extra)
        {
            Packet projectile = new Packet(PacketTypes.ProjectileNew);
            projectile.Writer.Write(identity);
            projectile.Writer.Write(position);
            projectile.Writer.Write(velocity);
            projectile.Writer.Write(knockback);
            projectile.Writer.Write(damage);
            projectile.Writer.Write(PlayerSlot);
            projectile.Writer.Write(type);
            projectile.Writer.Write(extra);
            Send(projectile);
        }

        public void ChatText(string message)
        {
            Packet chat = new Packet(PacketTypes.LoadNetModule);
            chat.Writer.Write(new byte[] { 0x01, 0x00 }); //Module ID for Chat Text
            chat.Writer.Write("Say");
            chat.Writer.Write(message);
            Send(chat);
        }

        public void GiveItem(short slot, short type, byte prefix, short stack)
        {
            Packet item = new Packet(PacketTypes.PlayerSlot, PlayerSlot);
            item.Writer.Write(slot);
            item.Writer.Write(stack);
            item.Writer.Write(prefix);
            item.Writer.Write(type);
            Send(item);
        }

        public void ChangeItem(byte itemslot)
        {
            PlayerUpdate(0, itemslot, MyPlayer.position, MyPlayer.velocity);
        }

        public void PlayerUpdate(byte control, byte itemslot, Vector2 position, Vector2 velocity, byte misc1=0, byte misc2 = 0, byte misc3 = 0)
        {
            MyPlayer.position = position;
            MyPlayer.velocity = velocity;
            Packet player = new Packet(PacketTypes.PlayerUpdate, PlayerSlot);
            player.Writer.Write(control);
            player.Writer.Write(misc1);
            player.Writer.Write(misc2);
            player.Writer.Write(misc3);
            player.Writer.Write(itemslot);
            player.Writer.Write(position);
            player.Writer.Write(velocity);
            Send(player);
        }
        public void Teleport(Vector2 position)
        {
            PlayerUpdate(0x00, 0x00, position, new Vector2(0f, 0f), 0x00);
        }

        public void Kill(byte slot)
        {
            if (slot == PlayerSlot) return;
            HurtPlayer(slot, PlayerDeathReason.ByCustomReason("123"), 0x497, 0x01, DamageType.Pvp);
        }

        // Terraria.NetMessage
        // Token: 0x06000147 RID: 327 RVA: 0x0003AD08 File Offset: 0x00038F08
        public void DecompressTileBlock_Inner(BinaryReader reader, int xStart, int yStart, int width, int height)
        {
            Tile tile = null;
            int num = 0;
            for (int i = yStart; i < yStart + height; i++)
            {
                for (int j = xStart; j < xStart + width; j++)
                {
                    if (num != 0)
                    {
                        num--;
                        if (Map.Tiles[j, i] == null)
                        {
                            Map.Tiles[j, i] = new Tile(tile);
                        }
                        else
                        {
                            Map.Tiles[j, i].CopyFrom(tile);
                        }
                    }
                    else
                    {
                        byte b2;
                        byte b = b2 = 0;
                        tile = Map.Tiles[j, i];
                        if (tile == null)
                        {
                            tile = new Tile();
                            Map.Tiles[j, i] = tile;
                        }
                        else
                        {
                            tile.ClearEverything();
                        }
                        byte b3 = reader.ReadByte();
                        if ((b3 & 1) == 1)
                        {
                            b2 = reader.ReadByte();
                            if ((b2 & 1) == 1)
                            {
                                b = reader.ReadByte();
                            }
                        }
                        bool flag = tile.active();
                        byte b4;
                        if ((b3 & 2) == 2)
                        {
                            tile.active(true);
                            ushort type = tile.type;
                            int num2;
                            if ((b3 & 32) == 32)
                            {
                                b4 = reader.ReadByte();
                                num2 = (int)reader.ReadByte();
                                num2 = (num2 << 8 | (int)b4);
                            }
                            else
                            {
                                num2 = (int)reader.ReadByte();
                            }
                            tile.type = (ushort)num2;
                            if (Constants.tileFrameImportant[num2])
                            {
                                tile.frameX = reader.ReadInt16();
                                tile.frameY = reader.ReadInt16();
                            }
                            else if (!flag || tile.type != type)
                            {
                                tile.frameX = -1;
                                tile.frameY = -1;
                            }
                            if ((b & 8) == 8)
                            {
                                tile.color(reader.ReadByte());
                            }
                        }
                        if ((b3 & 4) == 4)
                        {
                            tile.wall = reader.ReadByte();
                            if ((b & 16) == 16)
                            {
                                tile.wallColor(reader.ReadByte());
                            }
                        }
                        b4 = (byte)((b3 & 24) >> 3);
                        if (b4 != 0)
                        {
                            tile.liquid = reader.ReadByte();
                            if (b4 > 1)
                            {
                                if (b4 == 2)
                                {
                                    tile.lava(true);
                                }
                                else
                                {
                                    tile.honey(true);
                                }
                            }
                        }
                        if (b2 > 1)
                        {
                            if ((b2 & 2) == 2)
                            {
                                tile.wire(true);
                            }
                            if ((b2 & 4) == 4)
                            {
                                tile.wire2(true);
                            }
                            if ((b2 & 8) == 8)
                            {
                                tile.wire3(true);
                            }
                            b4 = (byte)((b2 & 112) >> 4);
                            if (b4 != 0 && Constants.tileSolid[(int)tile.type])
                            {
                                if (b4 == 1)
                                {
                                    tile.halfBrick(true);
                                }
                                else
                                {
                                    tile.slope((byte)(b4 - 1));
                                }
                            }
                        }
                        if (b > 0)
                        {
                            if ((b & 2) == 2)
                            {
                                tile.actuator(true);
                            }
                            if ((b & 4) == 4)
                            {
                                tile.inActive(true);
                            }
                            if ((b & 32) == 32)
                            {
                                tile.wire4(true);
                            }
                        }
                        b4 = (byte)((b3 & 192) >> 6);
                        if (b4 == 0)
                        {
                            num = 0;
                        }
                        else if (b4 == 1)
                        {
                            num = (int)reader.ReadByte();
                        }
                        else
                        {
                            num = (int)reader.ReadInt16();
                        }
                    }
                }
            }

            short num3 = reader.ReadInt16();
            for (int k = 0; k < (int)num3; k++)
            {
                short num4 = reader.ReadInt16();
                short x = reader.ReadInt16();
                short y = reader.ReadInt16();
                string name = reader.ReadString();
                if (num4 >= 0 && num4 < 1000)
                {
                    if (Map.Chests[(int)num4] == null)
                    {
                        Map.Chests[(int)num4] = new Chest(false);
                    }
                    Map.Chests[(int)num4].name = name;
                    Map.Chests[(int)num4].x = (int)x;
                    Map.Chests[(int)num4].y = (int)y;
                }
            }
            num3 = reader.ReadInt16();
            for (int l = 0; l < (int)num3; l++)
            {
                short num5 = reader.ReadInt16();
                short x2 = reader.ReadInt16();
                short y2 = reader.ReadInt16();
                string text = reader.ReadString();
                if (num5 >= 0 && num5 < 1000)
                {
                    if (Map.Signs[(int)num5] == null)
                    {
                        Map.Signs[(int)num5] = new Sign();
                    }
                    Map.Signs[(int)num5].text = text;
                    Map.Signs[(int)num5].x = (int)x2;
                    Map.Signs[(int)num5].y = (int)y2;
                }
            }
            num3 = reader.ReadInt16();
            for (int m = 0; m < (int)num3; m++)
            {
               // TileEntity tileEntity = TileEntity.Read(reader, false);
                //TileEntity.ByID[tileEntity.ID] = tileEntity;
                //TileEntity.ByPosition[tileEntity.Position] = tileEntity;
            }
        }

        private void DecompressTileBlock(byte[] buffer, int bufferStart, int bufferLength)
        {
            /*
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(buffer, bufferStart, bufferLength);
                memoryStream.Position = 0L;
                MemoryStream memoryStream3;
                if (memoryStream.ReadByte() != 0)
                {
                    MemoryStream memoryStream2 = new MemoryStream();
                    using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress, true))
                    {
                        deflateStream.CopyTo(memoryStream2);
                        deflateStream.Close();
                    }
                    memoryStream3 = memoryStream2;
                    memoryStream3.Position = 0L;
                }
                else
                {
                    memoryStream3 = memoryStream;
                    memoryStream3.Position = 1L;
                }
                using (BinaryReader binaryReader = new BinaryReader(memoryStream3))
                {
                    int xStart = binaryReader.ReadInt32();
                    int yStart = binaryReader.ReadInt32();
                    short width = binaryReader.ReadInt16();
                    short height = binaryReader.ReadInt16();
                    OnTileSendSection?.Invoke(this, xStart, yStart, width, height);
                    for (int x = xStart / 100; x < (xStart + width) / 100; ++x)
                        for (int y = yStart / 100; y < (yStart + height) / 100; ++y)
                            Map.SectionReceived[x, y] = true;
                    DecompressTileBlock_Inner(binaryReader, xStart, yStart, (int)width, (int)height);
                }
            }*/
            Console.WriteLine($"Tile Received Successfully. ({bufferLength} bytes in total)");
        }

        public void PlaceTile(short x, short y, short type, byte style = 0)
        {
            Packet tile = new Packet(PacketTypes.Tile);
            tile.Writer.Write((byte)1); //placetile
            tile.Writer.Write(x);
            tile.Writer.Write(y);
            tile.Writer.Write(type);
            tile.Writer.Write(style);
            Send(tile);
        }

        public void StackOverflow(int x, int y)
        {

        }

        public void BypassSSC()
        {
            Packet itemowner = new Packet(PacketTypes.ItemOwner);
            itemowner.Writer.Write((short)400);
            itemowner.Writer.Write((byte)255);
            Send(itemowner);
        }

        public void GameLoop(IPEndPoint endPoint, string password, IPEndPoint proxy = null)
        {
            Connect(endPoint, proxy);
            Console.WriteLine("Sending Client Hello...");
            Hello(CurRelease);

            /*TcpClient verify = new TcpClient();
            byte[] raw = Encoding.ASCII.GetBytes("-1551487326");
            verify.Connect(new IPEndPoint(endPoint.Address, 7980));
            verify.GetStream().Write(raw, 0, raw.Length);
            verify.Close();*/

                    bool connected = true;
            while (connected)
            {
                Packet packet = Receive();
                if (packet == null)
                {
                    Thread.Sleep(10);
                    continue;
                }
                try
                {
                    //lock (Console.Out) Console.WriteLine($"received type {packet.Type}");
                    switch (packet.Type)
                    {
                        case PacketTypes.Status:
                            packet.Reader.ReadInt32();
                            packet.Reader.ReadByte();
                            OnStatusBar?.Invoke(this, packet.Reader.ReadString());
                            break;
                        case PacketTypes.LoadNetModule:
                            if (packet.Reader.ReadInt16() == 0x01)
                            {
                                packet.Reader.ReadInt16();
                                OnChat?.Invoke(this, packet.Reader.ReadString(), packet.Reader.ReadColor());
                            }
                            break;
                        case PacketTypes.SmartTextMessage:
                            Color color = packet.Reader.ReadColor();
                            ++packet.Reader.BaseStream.Position;
                            OnChat?.Invoke(this, packet.Reader.ReadString(), color);
                            break;
                        case PacketTypes.Disconnect:
                            Console.WriteLine("Kicked : " + Encoding.UTF8.GetString(packet.Reader.ReadBytes((int)packet.Reader.BaseStream.Length)));
                            connected = false;
                            break;
                        case PacketTypes.ContinueConnecting:
                            PlayerSlot = packet.Reader.ReadByte();
                            OnSendPlayer?.Invoke(this, ref Players[PlayerSlot]);
                            SendPlayer(MyPlayer);
                            Send(new Packet(PacketTypes.ContinueConnecting2));
                            Console.WriteLine("Requesting World Info...");
                            break;
                        case PacketTypes.PasswordRequired:
                            Console.WriteLine("Sending password...");
                            SendPassword(password);
                            break;
                        case PacketTypes.WorldInfo:
                            Packet worldInfo = packet;
                            if (Map == null)
                                Map = new WorldMap(packet.Reader, nomap);

                            if (!IsPlaying)
                            {
                                TileGetSection(Map.spawnTileX, Map.spawnTileY);
                                IsPlaying = true;
                            }
                            break;
                        case PacketTypes.TileSendSection:
                            byte[] buffer = packet.ToArray();
                            DecompressTileBlock(buffer, 0x03, buffer.Length - 3);
                            break;
                        case PacketTypes.PlayerSpawnSelf:
                            Console.WriteLine("Spawning player...");
                            Spawn(Map.spawnTileX, Map.spawnTileY);
                            MyPlayer.position = new Vector2(Map.spawnTileX * 16, Map.spawnTileY * 16);
                            BypassSSC();
                            OnSpawn?.Invoke(this);
                            break;
                        case PacketTypes.TogglePvp:
                            {
                                byte slot = packet.Reader.ReadByte();
                                Players[slot].hostile = packet.Reader.ReadBoolean();
                                if (slot != PlayerSlot)
                                    OnTogglePvp?.Invoke(this, slot);
                                break;
                            }
                        case PacketTypes.PlayerUpdate:
                            {
                                byte slot = packet.Reader.ReadByte();
                                packet.Reader.BaseStream.Position += 5;
                                Players[slot].position = packet.Reader.ReadVec2();
                                if (slot != PlayerSlot)
                                    OnPlayerUpdate?.Invoke(this, slot);
                                break;
                            }
                        case PacketTypes.Teleport:
                            {
                                packet.Reader.ReadByte();
                                short slot = packet.Reader.ReadInt16();
                                Players[slot].position = packet.Reader.ReadVec2();
                                Console.WriteLine($"[INFO] player#{slot} teleported to {Players[slot].position.X}, {Players[slot].position.Y}");
                                break;
                            }
                        case PacketTypes.SyncMods:
                            Send(packet);
                            break;
                        case PacketTypes.ItemOwner:
                            break;
                        case PacketTypes.NpcAddBuff:
                            break;
                        case PacketTypes.NpcUpdateBuff:
                            break;
                        case PacketTypes.NpcUpdate:
                            break;
                        case PacketTypes.ItemDrop:
                            break;
                        case PacketTypes.ProjectileNew:
                            break;
                        case PacketTypes.ProjectileDestroy:
                            break;
                        default:
                            //Console.ForegroundColor = ConsoleColor.Red;
                            //Console.WriteLine($"[Warning] unknown packet type {packet.Type}");
                            //Console.ResetColor();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Exception caught when trying to parse packet {packet.Type}");
                    Console.WriteLine(e);
                    Console.ResetColor();
                }
            }
        }
    }
}
