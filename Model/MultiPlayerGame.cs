﻿using MazeLib;
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
    public class MultiPlayerGame
    {
        private TcpClient host;
        private TcpClient guest;
        private Maze maze;
        private bool partnerReady = false;

        public MultiPlayerGame(TcpClient myHost, Maze maze) {
            this.host = myHost;
            this.maze = maze;
        }

        public void waitForGuest()
        {
            while(!partnerReady)
            {
                Thread.Sleep(100);
            }
          /*  

            NetworkStream stream = guest.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);*/
        }

        public void setGuest(TcpClient joinClient)
        {
            guest = joinClient;
        }

        public void setPartnerIsReady()
        {
            partnerReady = true;
        }

        public Maze getMaze()
        {
            return maze;
        }
        public TcpClient getHost()
        {
            return host;
        }
        public TcpClient getGuest()
        {
            return guest;
        }
    }
}
