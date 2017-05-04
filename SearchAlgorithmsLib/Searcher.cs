using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
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
}
