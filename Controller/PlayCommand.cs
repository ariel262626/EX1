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
    /// <summary>
    /// play command implement Icommand
    /// </summary>
    class PlayCommand : ICommand
    {
        /// <summary>
        /// model
        /// </summary>
        private IModel model;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="model">model</param>
        public PlayCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// implement execute
        /// </summary>
        /// <param name="args">args of action</param>
        /// <param name="client">client</param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client)
        {
            string move = args[0];
            MultiPlayerGame multiPlayerGame = model.playGame(client);
            NetworkStream stream = multiPlayerGame.getOtherClient(client).GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(ToJson(multiPlayerGame, move));
            writer.Flush();
            return null;
        }
        /// <summary>
        /// tojson
        /// </summary>
        /// <param name="multiPlayerGame">multi game</param>
        /// <param name="direction">direction to move</param>
        /// <returns></returns>
        public string ToJson(MultiPlayerGame multiPlayerGame, string direction)
        {
            dynamic playJson = new JObject();
            playJson.Name = multiPlayerGame.getMaze().Name;
            playJson.Direction = direction;
            return playJson.ToString();
        }
    }
}
