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
        private bool isClosedCommand; 
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
                    string[] arr = null;
                    string commandLine = null;

                    commandLine = reader.ReadLine();
                    isClosedCommand = false;
                    Console.WriteLine("Got command: {0}", commandLine);
                    if (commandLine != null)
                    {
                        arr = commandLine.Split(' ');
                    }

                        if (arr[0].Equals("generate") || arr[0].Equals("solve") || arr[0].Equals("list") || commandLine == null || arr[0].Equals("close"))
                        {
                            isClosedCommand = true;
                        }
                        string result = null;
                        result = controller.ExecuteCommand(commandLine, client);

                        result += '\n';
                        result += '@';
                        if (result != "\n@")
                        {
                            writer.Write(result);
                            writer.Flush();
                        }
                        if (isClosedCommand)
                        {

                            stream.Dispose();
                            reader.Dispose();
                            writer.Dispose();
                            client.Close();
                        }
                        if (isClosedCommand)
                        {
                            isClosedCommand = false;
                            break;
                        }
                    }
              
                   
            }).Start();
        }
    }
}
