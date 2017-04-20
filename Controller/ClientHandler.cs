using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controller
{
    public class ClientHandler: IClientHandler
    {
        private Controller controller;
        public ClientHandler ()
        {
            controller = new Controller();
        }
        public void HandleClient(TcpClient client)
        {
           new Task(() =>
            {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);                 
                
                    while (true)
                    {
                        string commandLine = null;
                        try {
                             commandLine = reader.ReadLine();
                            Console.WriteLine("Got command: {0}", commandLine);
                        } catch(Exception e)
                        {
                            Console.WriteLine("Got command!!!: {0}", e);
                        }
                        if (commandLine == "terminate")
                        {
                            break;
                        }
                    // PacketStream packet = Newtonsoft.Json.JsonConvert.DeserializeObject<PacketStream> 
                    string result = null;
                     result = controller.ExecuteCommand(commandLine, client);
                        result += '\n';
                        result += '@';
                        writer.Write(result);
                    writer.Flush();
                    }
                Console.WriteLine("client closet to soon");
                client.Close();
            }).Start();
           
            
        }
    }
}
