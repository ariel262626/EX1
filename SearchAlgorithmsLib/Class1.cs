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
        public State(T state) {
            this.state = state;
            this.cost = 0;
            this.cameFrom = null;
        }
        public override bool Equals(object obj)
        {
            bool a = state.Equals((obj as State<T>).state);
            return state.Equals((obj as State<T>).state);
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

        public T getState()
        {
            return state;
        }

        public override int GetHashCode()
        {
            return state.ToString().GetHashCode();
        }
    }

    public interface ISearchable<T> {
        State<T> getInitialState();
        State<T> getGoalState();
        List<State<T>> getAllPossibleStates(State<T> s);
    }

    public interface ISearcher<T> {
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

        public void increaseEvaluatedNodes()
        {
            evaluatedNodes++;
        }

        public void addToOpenList(State<T> s)
        {
            openList.Enqueue(s, s.Cost);
        }

        public void removeFromOpenList(State<T> s)
        {
            openList.Remove(s);
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

        public Solution<T> backTrace<T>(State<T> state)
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

        public abstract Solution<T> search(ISearchable<T> searchable);
    }

    public class Solution<T>
    {
        private List<State<T>> path;

        public Solution(List<State<T>> path)
        {
            this.path = path;
        }
        public List<State<T>> getList()
        {
            return path;
        }
    }
}