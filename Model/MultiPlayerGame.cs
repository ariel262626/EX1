using MazeLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using View;

namespace Model
{
    /// <summary>
    /// multiplayer class
    /// </summary>
    public class MultiPlayerGame
    {
        /// <summary>
        /// host client
        /// </summary>
        private TcpClient host;
        /// <summary>
        /// guest client
        /// </summary>
        private TcpClient guest;
        /// <summary>
        /// my maze
        /// </summary>
        private Maze maze;
        /// <summary>
        /// check if partner is ready yet
        /// </summary>
        private bool partnerReady = false;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="myHost">with host</param>
        /// <param name="maze">my maze</param>
        public MultiPlayerGame(TcpClient myHost, Maze maze) {
            this.host = myHost;
            this.maze = maze;
        }
        /// <summary>
        /// wait for guest to connect
        /// </summary>
        public void waitForGuest()
        {
            while(!partnerReady)
            {
                Thread.Sleep(100);
            }
        }
        /// <summary>
        /// set the guest
        /// </summary>
        /// <param name="joinClient"> the guest</param>
        public void setGuest(TcpClient joinClient)
        {
            guest = joinClient;
            partnerReady = true;
        }
        /// <summary>
        /// set partner is ready to true
        /// </summary>
        public void setPartnerIsReady()
        {
            partnerReady = true;
        }
        /// <summary>
        /// get maze
        /// </summary>
        /// <returns>my maze</returns>
        public Maze getMaze()
        {
            return maze;
        }
        /// <summary>
        /// get host
        /// </summary>
        /// <returns>host</returns>
        public TcpClient getHost()
        {
            return host;
        }
        /// <summary>
        /// get guest
        /// </summary>
        /// <returns>guest</returns>
        public TcpClient getGuest()
        {
            return guest;
        }
        /// <summary>
        /// find the other client
        /// </summary>
        /// <param name="client">self</param>
        /// <returns>my partner</returns>
        public TcpClient getOtherClient(TcpClient client)
        {
            if (host == client)
                return guest;
            return host;
        }
    }
}