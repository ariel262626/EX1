using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    /// <summary>
    /// class of the controller..
    /// </summary>
    class Controller
    {
        /// <summary>
        /// dictionary of command
        /// </summary>
        private Dictionary<string, ICommand> commands;
        /// <summary>
        /// model
        /// </summary>
        private IModel model;
        /// <summary>
        /// constructor
        /// </summary>
        public Controller()
        {
            this.model = new Modell();
            //add the commands to the dictionary
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(model));
            commands.Add("solve", new SolveCommand(model));
            commands.Add("start", new StartCommand(model));
            commands.Add("list", new ListCommand(model));
            commands.Add("join", new JoinCommand(model));
            commands.Add("play", new PlayCommand(model));
            commands.Add("close", new CloseCommand(model));
        }
        /// <summary>
        /// implement execute
        /// </summary>
        /// <param name="commandLine">string of command</param>
        /// <param name="client">client</param>
        /// <returns></returns>
        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            string[]  args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }
    }
}
