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
    /// <summary>
    /// class cllientHandler implement icleintHandler
    /// </summary>
    public class ClientHandler: IClientHandler
    {
        /// <summary>
        /// my controller
        /// </summary>
        private Controller controller;
        /// <summary>
        /// bool is command of single player
        /// </summary>
        private bool isClosedCommand; 
        /// <summary>
        /// constructor
        /// </summary>
        public ClientHandler ()
        {
            controller = new Controller();
        }
        /// <summary>
        /// handle the client with new task
        /// </summary>
        /// <param name="client"></param>
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
                    //read command
                    commandLine = reader.ReadLine();
                    isClosedCommand = false;
                    Console.WriteLine("Got command: {0}", commandLine);
                    if (commandLine != null)
                    {
                        arr = commandLine.Split(' ');
                    }
                    //update the bool if is single player action
                        if (arr[0].Equals("generate") || arr[0].Equals("solve") || arr[0].Equals("list") || commandLine == null || arr[0].Equals("close"))
                        {
                            isClosedCommand = true;
                        }
                        string result = null;
                        result = controller.ExecuteCommand(commandLine, client);
                        //for write all
                        result += '\n';
                        result += '@';
                        if (result != "\n@")
                        {
                            writer.Write(result);
                            writer.Flush();
                        }
                        //close connection
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
