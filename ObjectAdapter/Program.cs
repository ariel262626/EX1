using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;

namespace ObjectAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.compareSolvers();
        }
        public void compareSolvers()
        {
            DFSMazeGenerator mazeCreator = new DFSMazeGenerator();
            Maze maze = mazeCreator.Generate(5, 5);
            // convert maze to string before the print
            String mazeString = maze.ToString();
            Searcher<Position> bfsAlgo = new Bfs<Position>();
            MazeSearcher mazeSearchable = new MazeSearcher(maze);
             Solution<Position> sol = bfsAlgo.search(mazeSearchable);
            Console.WriteLine(bfsAlgo.getNumberOfNodesEvaluated());
            List<State<Position>> path = sol.getList();
            foreach (State<Position> s in path)
            {
                int x = s.getState().Row;
                int y = s.getState().Col;
                
                Console.Write("( {0} , {1} ), ", x, y);
            }
            Console.WriteLine();
            Console.WriteLine(mazeString);
            Console.ReadKey();
        }
    }
}
