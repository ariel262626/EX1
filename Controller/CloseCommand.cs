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
    public class CloseCommand : ICommand
    {
        private IModel model;
        public CloseCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            MultiPlayerGame gameToClose = model.closeGame(client, name);
            NetworkStream stream = gameToClose.getOtherClient(client).GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(ToJson(name));
            
            NetworkStream stream1 = client.GetStream();
            StreamReader reader1 = new StreamReader(stream1);
            StreamWriter writer1 = new StreamWriter(stream1);
            writer1.WriteLine(ToJson(name));
            writer1.Flush();
            writer.Flush();
            return null;
        }

        public string ToJson(string name)
        {
            dynamic playJson = new JObject();
            return playJson.ToString();
        }
    }
}
