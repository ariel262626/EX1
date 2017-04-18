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
    public interface IModel
    {
        Maze generateMaze(string name, int rows, int cols);
        Solution<Position> solveMaze(string name, int algoChoose);
        string startGame(TcpClient client, string name, int rows, int cols);
        List<string> listGame();
        string joinToGame(TcpClient client, string name);
        MultiPlayerGame playGame(TcpClient client);

    }
}
