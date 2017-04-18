using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace Controller
{
    class Program
    {
        static void Main(string[] args)
        {
            IClientHandler ch = new ClientHandler();
            Server server = new Server(5556, ch);
            server.Start();
            Console.ReadKey();
        }
    }
}
