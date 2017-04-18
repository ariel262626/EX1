using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Model
{
    public class Modell : IModel
    {
        private Dictionary<string, Maze> poolMaze;
        private Dictionary<string, Solution<Position>> solutionCache;
        private Dictionary<string, Maze> poolGameToJoin;
        private bool partnerReady = false; 
        public Modell()
        {
            poolMaze = new Dictionary<string, Maze>();
            solutionCache = new Dictionary<string, Solution<Position>>();
            poolGameToJoin = new Dictionary<string, Maze>();
        }
        public Maze generateMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator mazeCreator = new DFSMazeGenerator();
            Maze maze = mazeCreator.Generate(rows, cols);
            maze.Name = name;
            poolMaze.Add(maze.Name, maze);
            return maze;
        }

        public Dictionary<string, Solution<Position>> getSolutionCache()
        {
            return solutionCache;
        }


        public Solution<Position> solveMaze(string name, int algoChoose)
        {
            // check if the solution is already exist..
            if (solutionCache.ContainsKey(name))
            {
                Solution<Position> sol = solutionCache[name];
                return sol;
            }
            // check if we need caculste dfs or bfs
            if (algoChoose == 1)
            {
                // dfs
                if (poolMaze.ContainsKey(name))
                {
                    Maze maze = poolMaze[name];
                    MazeSearcher mazeSearchable = new MazeSearcher(maze);
                    Searcher<Position> dfsAlgo = new Dfs<Position>();
                    Solution<Position> sol1 = dfsAlgo.search(mazeSearchable);
                    sol1.setEvaluatedNodes(dfsAlgo.getNumberOfNodesEvaluated());
                    solutionCache.Add(name, sol1);
                    return sol1;
                }
                else
                {
                    Console.WriteLine("Error in poolMaze request");
                    return null;
                }
            }
            else
            {
                // bfs
                if (poolMaze.ContainsKey(name))
                {
                    Maze maze = poolMaze[name];
                    MazeSearcher mazeSearchable = new MazeSearcher(maze);
                    Searcher<Position> bfsAlgo = new Bfs<Position>();
                    Solution<Position> sol2 = bfsAlgo.search(mazeSearchable);
                    sol2.setEvaluatedNodes(bfsAlgo.getNumberOfNodesEvaluated());
                    solutionCache.Add(name, sol2);
                    return sol2;
                }
                else
                {
                    Console.WriteLine("Error in poolMaze request");
                    return null;
                }
            }
        }



        public void startGame(string name, int rows, int cols)
        {
            // check if we have exist maze, if not -create new one
            if (poolMaze.ContainsKey(name))
            {
                Maze maze = poolMaze[name];
            }
            else
            {
                Maze maze = generateMaze(name, rows, cols);
                poolGameToJoin.Add(maze.Name, maze);
            }

            while (!partnerReady)
            {
                Thread.Sleep(100);
            }
        }

        public List<string> listGame()
        {
            List<string> gameList = new List<string>(this.poolGameToJoin.Keys);
            return gameList;
        }

        public string joinToGame(string name)
        {
            if (poolGameToJoin.ContainsKey(name))
            {
                partnerReady = true;
                return poolGameToJoin[name].ToJSON();
            }
            // else
            Console.WriteLine("there is no free player to play");
            return null;
            
        }
    }
}
