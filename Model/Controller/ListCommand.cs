using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    /// <summary>
    /// list command implement Icommand
    /// </summary>
    public class ListCommand : ICommand
    {
        /// <summary>
        /// model
        /// </summary>
        private IModel model;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="model">model</param>
        public ListCommand(IModel model)
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
            List <string> gameList = model.listGame();
            JArray jsonConvert = new JArray(gameList);
            return jsonConvert.ToString();
        }
    }
}
