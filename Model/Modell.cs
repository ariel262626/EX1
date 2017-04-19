using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
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
    public class Modell : IModel
    {
        private Dictionary<string, Maze> poolMaze;
        private Dictionary<string, Solution<Position>> solutionCache;
        private Dictionary<string, MultiPlayerGame> poolGameToJoin;
        private Dictionary<string, MultiPlayerGame> allActivePoolGame;
        // private bool partnerReady = false;
        public Modell()
        {
            poolMaze = new Dictionary<string, Maze>();
            solutionCache = new Dictionary<string, Solution<Position>>();
            poolGameToJoin = new Dictionary<string, MultiPlayerGame>();
            allActivePoolGame = new Dictionary<string, MultiPlayerGame>();
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



        public string startGame(TcpClient host, string name, int rows, int cols)
        {
            Maze maze;
            // check if we have exist maze, if not -create new one
            if (poolMaze.ContainsKey(name))
            {
                maze = poolMaze[name];
            }
            else
            {
                maze = generateMaze(name, rows, cols);
            }
            MultiPlayerGame multiGame = new MultiPlayerGame(host, maze);
            poolGameToJoin.Add(maze.Name, multiGame);
            multiGame.waitForGuest();
            return maze.ToJSON();
        }

        public List<string> listGame()
        {
            List<string> gameList = new List<string>(this.poolGameToJoin.Keys);
            return gameList;
        }

        public string joinToGame(TcpClient guest, string name)
        {
            if (poolGameToJoin.ContainsKey(name))
            {
                MultiPlayerGame multiGame = poolGameToJoin[name];
                multiGame.setGuest(guest);
                string stringJoinJson = poolGameToJoin[name].getMaze().ToJSON();
                allActivePoolGame.Add(name, multiGame);
                poolGameToJoin.Remove(name);
                return stringJoinJson;
            }
            // else
            Console.WriteLine("server - there is no free player to play");
            return "client - there is no free player to play";
        }

        public MultiPlayerGame playGame(TcpClient client)
        {
            foreach (KeyValuePair<string, MultiPlayerGame> tuple in allActivePoolGame)
            {
                if (tuple.Value.getHost() == client)
                {
                    return tuple.Value;
                }
                if (tuple.Value.getGuest() == client)
                {
                    return tuple.Value;
                }
            }
            Console.WriteLine("MultiPlayerGame not found 404");
            return null;
        }
    }
}
