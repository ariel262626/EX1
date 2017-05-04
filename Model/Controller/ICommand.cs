using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    /// <summary>
    /// interface Icommand
    /// </summary>
    interface ICommand
    {
        /// <summary>
        /// need to implement
        /// </summary>
        /// <param name="args">args of action</param>
        /// <param name="client">client</param>
        /// <returns>string</returns>
        string Execute(string[] args, TcpClient client = null);
    }
}
