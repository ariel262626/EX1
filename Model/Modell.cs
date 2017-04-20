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
    /// <summary>
    /// class model implement imodel
    /// </summary>
    public class Modell : IModel
    {
        /// <summary>
        /// pool of regular maze
        /// </summary>
        private Dictionary<string, Maze> poolMaze;
        /// <summary>
        /// solution cache 
        /// </summary>
        private Dictionary<string, Solution<Position>> solutionCache;
        /// <summary>
        /// pool of game we can to join them
        /// </summary>
        private Dictionary<string, MultiPlayerGame> poolGameToJoin;
        /// <summary>
        /// all active game
        /// </summary>
        private Dictionary<string, MultiPlayerGame> allActivePoolGame;
        /// <summary>
        /// constructor
        /// </summary>
        public Modell()
        {
            poolMaze = new Dictionary<string, Maze>();
            solutionCache = new Dictionary<string, Solution<Position>>();
            poolGameToJoin = new Dictionary<string, MultiPlayerGame>();
            allActivePoolGame = new Dictionary<string, MultiPlayerGame>();
        }
        /// <summary>
        /// generate maze
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="rows">rows</param>
        /// <param name="cols">cols</param>
        /// <returns></returns>
        public Maze generateMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator mazeCreator = new DFSMazeGenerator();
            Maze maze = mazeCreator.Generate(rows, cols);
            maze.Name = name;
            poolMaze.Add(maze.Name, maze);
            return maze;
        }
        /// <summary>
        /// get solution cache
        /// </summary>
        /// <returns>dictionary</returns>
        public Dictionary<string, Solution<Position>> getSolutionCache()
        {
            return solutionCache;
        }

        /// <summary>
        /// solve maze
        /// </summary>
        /// <param name="name">name to solve</param>
        /// <param name="algoChoose">solve with...</param>
        /// <returns></returns>
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


        /// <summary>
        /// for start action
        /// </summary>
        /// <param name="host">the host</param>
        /// <param name="name">name of the game</param>
        /// <param name="rows">rows</param>
        /// <param name="cols">col</param>
        /// <returns></returns>
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
            //make a multi game
            MultiPlayerGame multiGame = new MultiPlayerGame(host, maze);
            poolGameToJoin.Add(maze.Name, multiGame);
            //wait
            multiGame.waitForGuest();
            return maze.ToJSON();
        }
        /// <summary>
        /// list action
        /// </summary>
        /// <returns>list of game to join</returns>
        public List<string> listGame()
        {
            List<string> gameList = new List<string>(this.poolGameToJoin.Keys);
            return gameList;
        }
        /// <summary>
        /// join action
        /// </summary>
        /// <param name="guest">guest</param>
        /// <param name="name">name of game to join</param>
        /// <returns></returns>
        public string joinToGame(TcpClient guest, string name)
        {
            //find
            if (poolGameToJoin.ContainsKey(name))
            {
                //get
                MultiPlayerGame multiGame = poolGameToJoin[name];
                //set
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
        /// <summary>
        /// play action
        /// </summary>
        /// <param name="client">client move</param>
        /// <returns>the game he playes at</returns>
        public MultiPlayerGame playGame(TcpClient client)
        {
            //find the game
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
        /// <summary>
        /// close action
        /// </summary>
        /// <param name="client">current client</param>
        /// <param name="nameToClose">game to close</param>
        /// <returns>the game</returns>
        public MultiPlayerGame closeGame(TcpClient client, string nameToClose)
        {
            MultiPlayerGame multiGameToDelete;
            //find
            foreach (KeyValuePair<string, MultiPlayerGame> tuple in allActivePoolGame)
            {
                if (tuple.Value.getHost() == client)
                {
                    multiGameToDelete = tuple.Value;
                    allActivePoolGame.Remove(tuple.Key);
                    return multiGameToDelete;
                }
                if (tuple.Value.getGuest() == client)
                {
                    multiGameToDelete = tuple.Value;
                    allActivePoolGame.Remove(tuple.Key);
                    return multiGameToDelete;
                }
            }
            Console.WriteLine("MultiPlayerGame not found 404");
            return null;
        }
    }
}
