using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    /// <summary>
    /// class of server
    /// </summary>
    class Server
    {
            /// <summary>
            /// port
            /// </summary>
            private int port;
        /// <summary>
        /// listener
        /// </summary>
            private TcpListener listener;
        /// <summary>
        /// clientHandler
        /// </summary>
            private IClientHandler ch;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="port">my port</param>
        /// <param name="ch">my client handler</param>
            public Server(int port, IClientHandler ch)
            {
                this.port = port;
                this.ch = ch;
            }
        /// <summary>
        /// start the connection
        /// </summary>
            public void Start()
            {
                IPEndPoint ep = new
               IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                listener = new TcpListener(ep);

                listener.Start();
                Console.WriteLine("Waiting for connections...");
            //new task get client and send hom to handle
                Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }
        /// <summary>
        /// stop to listen
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }
    }
}
