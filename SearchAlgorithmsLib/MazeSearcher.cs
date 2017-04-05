using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
namespace SearchAlgorithmsLib
{
    /* Adapter class for the Maze */
    public class MazeSearcher: ISearchable<Position>
    {
        private Maze myMaze;
        private State<Position> myStart, myGoal; 
        public MazeSearcher (Maze maze)
        {
            myMaze = maze;
           // create start state
            myStart = new State<Position>(myMaze.InitialPos);
            // create goal state
            myGoal = new State<Position>(myMaze.GoalPos);
        } 

        public State<Position> getInitialState()
        {
            return myStart;
        }

        public State<Position> getGoalState()
        {
            return myGoal;
        }

        public List<State<Position>> getAllPossibleStates(State<Position> s)
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
                Position downPosition = new Position(s.getState().Row + 1, s.getState().Col);
                State<Position> down = new State<Position>(downPosition);
                down.CameFrom = s;
                myNeigbours.Add(down);
            }
            //left
            if ((s.getState().Col - 1 >= 0) && (myMaze[s.getState().Row, s.getState().Col - 1] == 0))
            {
                Position leftPosition = new Position(s.getState().Row, s.getState().Col - 1);
                State<Position> left = new State<Position>(leftPosition);
                left.CameFrom = s;
                myNeigbours.Add(left);
            }
            return myNeigbours;
        }
    }
}
