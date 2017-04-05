using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;

namespace ObjectAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            DFSMazeGenerator mazeCreator = new DFSMazeGenerator();
            Maze maze = mazeCreator.Generate(10, 10);
            // convert maze to string before the print
            String mazeString = maze.ToString();
            Console.WriteLine("before print");
            Console.WriteLine(mazeString);
        }
    }
}
