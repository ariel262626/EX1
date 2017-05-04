using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    /// <summary>
    /// join command implement Icommand
    /// </summary>
    class JoinCommand: ICommand
    {
        /// <summary>
        ///my model
        /// </summary>
        private IModel model;
        public JoinCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// implement execute
        /// </summary>
        /// <param name="args">args of action</param>
        /// <param name="client">client</param>
        /// <returns>string</returns>
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            string messageToClient = model.joinToGame(client, name);
            return messageToClient;
        }
    }
}
