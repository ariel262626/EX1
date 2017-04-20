using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace View
{
    public class Client
    {
        private bool isMultiPlayer = false;
        private bool isStop = false;
        public Client() { }
        public void startGame()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            TcpClient client = new TcpClient();

            NetworkStream stream = null;
            StreamWriter writer = null;
            StreamReader reader = null;
            while (true)
            {
                Console.Write("Please enter a mission:");
                string mission = Console.ReadLine();
                if (!client.Connected)
                {
                    Console.WriteLine("Im not connect yet");
                    try
                    {
                        client = new TcpClient();
                        client.Connect(ep);
                        Console.WriteLine("now im connect");
                    }
                    catch (SocketException e)
                    {
                        Console.WriteLine("Error in socket");
                    }
                    Console.WriteLine("You are connected");
                     stream = client.GetStream();
                     reader = new StreamReader(stream);
                     writer = new StreamWriter(stream);
                }
                // Send data to server
              
                string[] arr = mission.Split(' ');
                if (arr[0].Equals("start") || arr[0].Equals("join"))
                {
                    // to know if we need new thread
                    isMultiPlayer = true;
                }

                writer.WriteLine(mission);
                writer.Flush();
                while (true)
                {
                    string result = reader.ReadLine();
                    Console.WriteLine("{0}", result);
                    if (reader.Peek() == '@')
                    {
                        result.TrimEnd('\n');
                        break;
                    }
                }
                reader.DiscardBufferedData();
                if (isMultiPlayer)
                {
                    Task sendingTask = new Task(() =>
                    {
                        while (true)
                        {
                        // Send data to server
                        mission = Console.ReadLine();
                            writer.WriteLine(mission);
                            writer.Flush();
                            arr = mission.Split(' ');
                            if (arr[0] == "close")
                            {
                                //Console.WriteLine("close the caller");
                                // client.Close();
                                Console.WriteLine("Im close finally");
                                break;
                            }
                        }
                    });
                    Console.WriteLine("checking");
                    Task listenTask = new Task(() =>
                    {
                        while (!isStop)
                        {
                            while (!isStop)
                            {
                                string result = reader.ReadLine();
                                if (result == "{}")
                                {
                                    Console.WriteLine("close the listener");
                                    isStop = true;
                                    client.Close();
                                    //startGame();
                                }
                                if (isStop)
                                {
                                    break;
                                }
                                Console.WriteLine("{0}", result);
                                if (reader.Peek() == '@')
                                {
                                    result.TrimEnd('\n');
                                    break;
                                }
                            }
                            if (isStop)
                            {
                                isStop = false;
                                break;
                            }
                            reader.DiscardBufferedData();
                            Console.WriteLine("ere");
                        }
                        Console.WriteLine("hh");
                    });
                    sendingTask.Start();
                    sendingTask.Wait();
                    listenTask.Start();
                    listenTask.Wait();
                } else
                {
                    client.Close();
                    stream.Dispose();
                    writer.Dispose();
                    reader.Dispose();
                }
            }
        }
    }
}