using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace Controller
{
    /// <summary>
    /// class main of controller
    /// </summary>
    class Program
    {
        /// <summary>
        /// main of server
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IClientHandler ch = new ClientHandler();
            int port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            Server server = new Server(port, ch);
            server.Start();
            Console.ReadKey();
        }
    }
}
