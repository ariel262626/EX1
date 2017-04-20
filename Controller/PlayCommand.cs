using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    class PlayCommand : ICommand
    {
        private IModel model;
        public PlayCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string move = args[0];
            MultiPlayerGame multiPlayerGame = model.playGame(client);
            NetworkStream stream = multiPlayerGame.getOtherClient(client).GetStream();
         //   StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(ToJson(multiPlayerGame, move));
            writer.Flush();
            return null;
        }

        public string ToJson(MultiPlayerGame multiPlayerGame, string direction)
        {
            dynamic playJson = new JObject();
            playJson.Name = multiPlayerGame.getMaze().Name;
            playJson.Direction = direction;
            return playJson.ToString();
        }
    }
}
