﻿using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    class JoinCommand: ICommand
    {
        private IModel model;
        public JoinCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            string messageToClient = model.joinToGame(client, name);
            return messageToClient;
        }
    }
}