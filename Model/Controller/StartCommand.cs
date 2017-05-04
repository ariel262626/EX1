using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    /// <summary>
    /// class start command implement Icommand
    /// </summary>
    class StartCommand: ICommand
    {
        /// <summary>
        /// model
        /// </summary>
        private IModel model;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="model">model</param>
        public StartCommand (IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// implement execute
        /// </summary>
        /// <param name="args">args of command</param>
        /// <param name="host">host</param>
        /// <returns>string</returns>
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
