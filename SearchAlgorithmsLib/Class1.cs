using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Class1
    {
    }

    public class State<T>
    {
        private T state; // the state represented by a STRING 
        private float cost; // cost to reach this state (set by a setter)
        private State<T> cameFrom;
        public State(T state)
        {
            this.state = state;
            this.cameFrom = null;
        }
        public bool Equals(State<T> s) // we overload Object's Equals method
        {
            return state.Equals(s.state);
        }
        //get the cost - priority
        public float Cost
        {
            get; set;
        }

        public State<T> CameFrom
        {
            get; set;
        }
    }

    public interface ISearchable<T>
    {
        State<T> getInitialState();
        State<T> getGoalState();
        List<State<T>> getAllPossibleStates(State<T> s);
    }

    public interface ISearcher<T>
    {
        // the search method
        Solution<T> search(ISearchable<T> searchable);
        // get how many nodes were evaluated by the algorithm
        int getNumberOfNodesEvaluated();
    }

    public abstract class Searcher<T> : ISearcher<T>
    {
        private Priority_Queue.SimplePriorityQueue<State<T>> openList;
        private int evaluatedNodes;
        public Searcher()
        {
            openList = new Priority_Queue.SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }

        protected State<T> popOpenList()
        {
            evaluatedNodes++;
            //remove the head ofthe queue and returns it.
            return openList.Dequeue();
        }

        // a property of openList
        public int OpenListSize
        {
            // it is a read-only property :)
            get { return openList.Count; }
        }

        // ISearcher’smethods:
        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        public void addToOpenList(State<T> s)
        {
            openList.Enqueue(s, s.Cost);
        }

        public bool openContaines(State<T> other)
        {
            foreach (State<T> s in openList)
            {
                if (s.Equals(other))
                    return true;
            }
            return false;
        }
        public abstract Solution<T> search(ISearchable<T> searchable);
    }

    public class Solution<T>
    {
        // private List<State<T>> list;
        private List<State<T>> path;

        public Solution(List<State<T>> path)
        {
            this.path = path;
        }

        /* public Solution(List<State<T>> otherList)
         {
             this.list = otherList;
         }*/
    }

    public class Bfs<T> : Searcher<T>
    {
        public override Solution<T> search(ISearchable<T> searchable)
        {
            addToOpenList(searchable.getInitialState()); // inherited from Searcher
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> currentState = popOpenList(); // inherited from Searcher, removes the best state
                closed.Add(currentState);
                if (currentState.Equals(searchable.getGoalState()))
                    return backTrace(currentState); // private method, back traces through the parents
                                                    // calling the delegated method, returns a list of states with n as a parent
                List<State<T>> succerssors = searchable.getAllPossibleStates(currentState);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !openContaines(s))
                    {
                        // s.setCameFrom(n); // already done by getSuccessors
                        s.Cost += 1; // update cost of neigbour before we put it in open list  
                        addToOpenList(s);
                        // Check!! mabye we should check if each neigbour is the foal state
                    }
                    else if (!openContaines(s) && (s.Cost > (currentState.Cost + 1)))
                    {
                        // update it's cost  because we have new way
                        s.Cost = currentState.Cost + 1;
                        s.CameFrom = currentState;
                    }
                }
            }
            return null;
        }

        public Solution<T> backTrace(State<T> state)
        {
            List<State<T>> path = new List<State<T>>();
            // add the goal state 
            path.Add(state);
            // add each father of the chain from the goal state to initial state
            while (state.CameFrom != null)
            {
                path.Add(state.CameFrom);
                state = state.CameFrom;
            }
            Solution<T> sol = new Solution<T>(path);
            return sol;
        }
    }
}
