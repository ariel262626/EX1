using MazeLib;
using Model;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class SolveCommand: ICommand
    {
        private IModel model;
        //private Searcher<Position> searcher;
        public SolveCommand (IModel model)
        {
            this.model = model;
        }


        public string ToJson(string name, Solution<Position> sol)
        {
            dynamic solveJson = new JObject();
            solveJson.Name = name;
            solveJson.Solution = calculateSteps(sol);
            solveJson.NodesEvaluated = sol.getNumberOfNodesEvaluated().ToString();
            return solveJson.ToString();
        }

        public string calculateSteps(Solution<Position> sol)
        {
            // string object for adding string each time
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < sol.getList().Count - 1; i++)
            {
                // i move left
                if (sol.getList()[i].getState().Col > sol.getList()[i + 1].getState().Col)
                {
                    sb.Append("0");
                    continue;
                }
                // i move right
                if (sol.getList()[i].getState().Col < sol.getList()[i + 1].getState().Col)
                {
                    sb.Append("1");
                    continue;
                }
                // i move up
                if (sol.getList()[i].getState().Row > sol.getList()[i + 1].getState().Row)
                {
                    sb.Append("2");
                    continue;
                }
                // i move down
                if (sol.getList()[i].getState().Row < sol.getList()[i + 1].getState().Row)
                {
                    sb.Append("3");
                    continue;
                }
            }
            return sb.ToString();
        }

        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            Solution<Position> sol = model.solveMaze(name, algorithm);
            return this.ToJson(name, sol);
        }

        
    }
}
