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
        private double cost; // cost to reach this state (set by a setter)
        private State<T> cameFrom;
        public State(T state) {
            this.state = state;
        }
        public bool Equals(State<T> s) // we overload Object's Equals method
        {
            return state.Equals(s.state);
        }
    }

    public interface ISearchable<T> {
        State<T> getInitialState();
        State<T> getGoalState();
        List<State<T>> getAllPossibleStates(State<T> s);
}

    public interface ISearcher<T> {
        // the search method
        Solution search (ISearchable<T> searchable); // get how many nodes were evaluated by the algorithm
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
        public abstract Solution search(ISearchable<T> searchable);
    }

    public abstract class Solution
    {
       //TODO
    }

    public class Bfs<T> : ISearcher<T>
    {
        public int getNumberOfNodesEvaluated()
        {
            //TODO
            return 0;
        }

        public Solution search(ISearchable<T> searchable)
        {
            //TOD
            return null;  
        }
    }
}
