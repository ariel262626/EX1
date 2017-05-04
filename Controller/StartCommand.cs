using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    class StartCommand: ICommand
    {
        private IModel model;
        public StartCommand (IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient host)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            string startJsonString = model.startGame(host, name, rows, cols);
            return startJsonString;
        }
    }
}
