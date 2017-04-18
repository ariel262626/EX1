using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class Client
    {
    public Client(){ }
        public void startGame()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            TcpClient client = new TcpClient();
            try {
                client.Connect(ep);
            } catch (SocketException e)
            {
                Console.Write("Error in socket");
            }
            Console.WriteLine("You are connected");
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                while (true) {
                    // Send data to server
                    Console.Write("Please enter a mission: ");
                    string mission = Console.ReadLine();
                    if (mission == "close")
                    {
                        break;
                    }
                    writer.WriteLine(mission);
                    writer.Flush();
                    // Get result from server
                    string result = reader.ReadLine();
                    Console.Write("{0}", result);
                }
            }
            client.Close();
        }
    }
}
