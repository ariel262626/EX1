using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;

namespace View
{
    class Program
    {
        /// <summary>
        /// the main of the client
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //int port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            //IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            Client client = new Client();
            client.startGame();
            //keep the console open
            Console.ReadKey();
        }
    }
}
