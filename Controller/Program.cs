using System;
using System.Collections.Generic;
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
            Server server = new Server(5555, ch);
            server.Start();
            Console.ReadKey();
        }
    }
}
