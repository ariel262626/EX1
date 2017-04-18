using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IModel
    {
        Maze generateMaze(string name, int rows, int cols);
        Solution<Position> solveMaze(string name, int algoChoose);
        void startGame(string name, int rows, int cols);
        List<string> listGame();
        string joinToGame(string name);

    }
}
