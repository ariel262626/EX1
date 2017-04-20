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
    /// <summary>
    /// the client class 
    /// </summary>
    public class Client
    {
        /// <summary>
        ///  if we in multiplayer make it true
        /// </summary>
        private bool isMultiPlayer = false;
        private bool isStop = false;

        /// <summary>
        /// constructor of client
        /// </summary>
        public Client() { }

        /// <summary>
        /// start game method acting the single and the multi game
        /// </summary>
        public void startGame()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            TcpClient client = new TcpClient();

            NetworkStream stream = null;
            StreamWriter writer = null;
            StreamReader reader = null;
            //the main while loop keep ask for the next mission
            while (true)
            {
                //first usr give his choice
                Console.Write("Please enter a mission:");
                //read from user
                string mission = Console.ReadLine();
                //if client is off
                if (!client.Connected)
                {
                    try
                    {
                        //open and connect
                        client = new TcpClient();
                        client.Connect(ep);
                        Console.WriteLine("now im connect");
                    }
                    catch (SocketException e)
                    {
                        Console.WriteLine("Error in socket at client");
                    }
                    Console.WriteLine("You are connected");
                     stream = client.GetStream();
                     reader = new StreamReader(stream);
                     writer = new StreamWriter(stream);
                }
                // Send data to server
                //split the input and check
                string[] arr = mission.Split(' ');
                if (arr[0].Equals("start") || arr[0].Equals("join"))
                {
                    // to know if we need multiplayer platform
                    isMultiPlayer = true;
                }
                //send the mission to handle
                writer.WriteLine(mission);
                writer.Flush();
                //while for reading all the result
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
                //clean
                reader.DiscardBufferedData();
                //if we need multiplayer mode open 2 tasks one to read and one more to write at same time
                if (isMultiPlayer)
                {
                    Task sendingTask = new Task(() =>
                    {
                        //to keep the task open
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
                        }
                        Console.WriteLine("hh");
                    });
                    //start the tasks and wait for them
                    sendingTask.Start();
                    listenTask.Start();
                    sendingTask.Wait();
                    listenTask.Wait();
                } else
                {
                    //if single player close connection
                    client.Close();
                    stream.Dispose();
                    writer.Dispose();
                    reader.Dispose();
                }
            }
        }
    }
}