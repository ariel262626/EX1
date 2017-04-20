using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Client client = new Client();
            client.startGame();
            //keep the console open
            Console.ReadKey();
        }
    }
}
