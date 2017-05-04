using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using View;
namespace Model
{
    /// <summary>
    /// imodel interface
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// generate action
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="rows">rows</param>
        /// <param name="cols">cols</param>
        /// <returns>new maze</returns>
        Maze generateMaze(string name, int rows, int cols);
        /// <summary>
        /// solve action
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="algoChoose">algo</param>
        /// <returns>solution</returns>
        Solution<Position> solveMaze(string name, int algoChoose);
        /// <summary>
        /// start action
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="name">name</param>
        /// <param name="rows">rows</param>
        /// <param name="cols">cols</param>
        /// <returns>string</returns>
        string startGame(TcpClient client, string name, int rows, int cols);
        /// <summary>
        /// list action
        /// </summary>
        /// <returns>list of game</returns>
        List<string> listGame();
        /// <summary>
        /// join action
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="name">name of game</param>
        /// <returns>string</returns>
        string joinToGame(TcpClient client, string name);
        /// <summary>
        /// play action
        /// </summary>
        /// <param name="client">client</param>
        /// <returns>multiGame</returns>
        MultiPlayerGame playGame(TcpClient client);
        /// <summary>
        /// close action
        /// </summary>
        /// <param name="tc">client</param>
        /// <param name="nameToClose">name to close</param>
        /// <returns></returns>
        MultiPlayerGame closeGame(TcpClient tc, string nameToClose);

    }
}
