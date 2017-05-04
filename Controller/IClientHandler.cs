using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    /// <summary>
    /// interface IcleintHandler
    /// </summary>
    interface IClientHandler
    {
        /// <summary>
        /// handle client method
        /// </summary>
        /// <param name="client">the client</param>
        void HandleClient(TcpClient client);
    }
}
