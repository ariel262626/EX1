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
        }
        public bool Equals(State<T> s) // we overload Object's Equals method
        {
            return state.Equals(s.state);
        }
        //get the cost - priority
        public float getCost()
        {
            return cost;
        }
        public void setCost(float newCost)
        {
            this.cost = newCost;
        }
    }

    public interface ISearchable<T> {
        State<T> getInitialState();
        State<T> getGoalState();
        List<State<T>> getAllPossibleStates(State<T> s);
    }

    public interface ISearcher<T> {
        // the search method
        Solution search(ISearchable<T> searchable);
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
            openList.Enqueue(s, s.getCost());
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

    public abstract class Solution<T>
    {
        List<State<T>> list;
        public Solution(List<State<T>> otherList)
        {
            this.list = otherList;
        }
    }

    public class Bfs<T> : Searcher<T>
    {
        public override Solution<T> search(ISearchable<T> searchable)
        {
            addToOpenList(searchable.getInitialState()); // inherited from Searcher
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> n = popOpenList(); // inherited from Searcher, removes the best state
                closed.Add(n);
                if (n.Equals(searchable.getGoalState()))
                    return backTrace(); // private method, back traces through the parents
                                        // calling the delegated method, returns a list of states with n as a parent
                List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !openContaines(s))
                    {
                        // s.setCameFrom(n); // already done by getSuccessors
                        addToOpenList(s);
                    }
                    else
                    {
                    if (openContaines(s) && s.getCost< n.getCost + distance from the goal state)
                            {
                            // update it's cost  because we have new way
                            
                        }
                    }
                }
            }
        }
        public Solution<T> backTrace()
        {

        }
    }

    public class Dfs<T> : Searcher<T>
    { 
        public override Solution<T> search(ISearchable<T> searchable)
        {
            Stack<State<T>> stack = new Stack<State<T>>();
            stack.Push(searchable.getInitialState());
            while (stack.Count != 0)
            {
            State<T> state = stack.Pop();
            if v is not labeled as discovered:
            label v as discovered
            for all edges from v to w in G.adjacentEdges(v) do
           stack.Push(w)
        }

}
