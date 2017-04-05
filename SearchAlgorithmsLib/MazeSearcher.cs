using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /* Adapter class for the Maze */
    class MazeSearcher<T>: ISearchable<T>
    {
        private MazeLib.Maze myMaze;
        private MazeLib.CellType cell = new MazeLib.CellType();
        private State<T> myStart, myGoal; 
        public MazeSearcher (MazeLib.Maze maze)
        {
            myMaze = maze;
           // create start state
            State<MazeLib.Position> start = new State<MazeLib.Position>(myMaze.InitialPos);
            // create goal state
            State<MazeLib.Position> goal = new State<MazeLib.Position>(myMaze.GoalPos);
        } 

        public State<T> getInitialState()
        {
            return myStart;
        }

        public State<T> getGoalState()
        {
            return myGoal;
        }

        public List<State<MazeLib.Position>> getAllPossibleStates(State<MazeLib.Position> s)
        {
            List<State<MazeLib.Position>> myNeigbours = new List<State<MazeLib.Position>>();
            // check his all relevant niegbours
            // up
            if ((s.getState().Row - 1 >= 0) && (myMaze[s.getState().Row - 1 , s.getState().Col] == 0))
            {
                MazeLib.Position upPosition = new MazeLib.Position(s.getState().Row - 1, s.getState().Col);
                State<MazeLib.Position> up = new State<MazeLib.Position>(upPosition);
                up.CameFrom = s;
                myNeigbours.Add(up);
            }
            //right
            if ((s.getState().Col + 1 < myMaze.Cols) && (myMaze[s.getState().Row, s.getState().Col + 1] == 0))
            {
                MazeLib.Position rightPosition = new MazeLib.Position(s.getState().Row, s.getState().Col + 1);
                State<MazeLib.Position> right = new State<MazeLib.Position>(rightPosition);
                right.CameFrom = s;
                myNeigbours.Add(right);
            }
            // down
            if ((s.getState().Row + 1 < myMaze.Rows) && (myMaze[s.getState().Row + 1, s.getState().Col] == 0))
            {
                MazeLib.Position downPosition = new MazeLib.Position(s.getState().Row + 1, s.getState().Col);
                State<MazeLib.Position> down = new State<MazeLib.Position>(downPosition);
                down.CameFrom = s;
                myNeigbours.Add(down);
            }
            //left
            if ((s.getState().Col - 1 >= 0) && (myMaze[s.getState().Row, s.getState().Col - 1] == 0))
            {
                MazeLib.Position leftPosition = new MazeLib.Position(s.getState().Row, s.getState().Col - 1);
                State<MazeLib.Position> left = new State<MazeLib.Position>(leftPosition);
                left.CameFrom = s;
                myNeigbours.Add(left);
            }
            return myNeigbours;
        }
    }
}
