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
        public Client() { }
        public void startGame()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            TcpClient client = new TcpClient();
            try
            {
                client.Connect(ep);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Error in socket");
            }
            Console.WriteLine("You are connected");
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            while (true)
            {
                // Send data to server
                //Console.Write("Please enter a mission:!!!! ");
                string mission = Console.ReadLine();
                string[] arr = mission.Split(' ');
                if (arr[0].Equals("start") || arr[0].Equals("join"))
                {
                    // to know if we need new thread
                    isMultiPlayer = true;
                }
                if (mission == "close")
                {
                    break;
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
                    Task listenTask = new Task(() =>
                    {
                        while (true)
                        {
                        // Send data to server
                        //Console.Write("Please enter a mission: ");
                        mission = Console.ReadLine();
                            if (mission == "close")
                            {
                                break;
                            }

                            writer.WriteLine(mission);
                            writer.Flush();
                        // Get result from server
                        // string result = reader.ReadLine();
                    }

                    });
                  
                    Task sendingTask = new Task(() =>
                    {
                        while (true)
                        {
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
                        }
                    });
                    listenTask.Start();
                    sendingTask.Start();
                   //listenTask.Wait();
                    //sendingTask.Wait();
                }
            }
        }
    }
}