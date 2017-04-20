using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// abstract class sercher implement isercher
    /// </summary>
    /// <typeparam name="T"> T kind </typeparam>
    public abstract class Searcher<T> : ISearcher<T>
    {
        /// <summary>
        /// priority queue
        /// </summary>
        private Priority_Queue.SimplePriorityQueue<State<T>> openList;
        /// <summary>
        /// evaluatednodes value
        /// </summary>
        private int evaluatedNodes;
        /// <summary>
        /// constructor
        /// </summary>
        public Searcher()
        {
            openList = new Priority_Queue.SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }
        /// <summary>
        /// pop from open list
        /// </summary>
        /// <returns> the state that poped</returns>
        protected State<T> popOpenList()
        {
            evaluatedNodes++;
            //remove the head ofthe queue and returns it.
            return openList.Dequeue();
        }
        /// <summary>
        /// a property of openList
        /// </summary>
        public int OpenListSize
        {
            // it is a read-only property :)
            get { return openList.Count; }
        }
        /// <summary>
        /// ISearcher’smethods:
        /// </summary>
        /// <returns> the value of nodesevaluated</returns>
        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
        /// <summary>
        /// increase evaluated value
        /// </summary>
        public void increaseEvaluatedNodes()
        {
            evaluatedNodes++;
        }

        /// <summary>
        /// add to open list
        /// </summary>
        /// <param name="s"></param>
        public void addToOpenList(State<T> s)
        {
            openList.Enqueue(s, s.Cost);
        }
        /// <summary>
        /// remove from openlist
        /// </summary>
        /// <param name="s"> who we want to delete</param>
        public void removeFromOpenList(State<T> s)
        {
            openList.Remove(s);
        }
        /// <summary>
        /// check if contain
        /// </summary>
        /// <param name="other"> other </param>
        /// <returns> bool</returns>
        public bool openContaines(State<T> other)
        {
            foreach (State<T> s in openList)
            {
                if (s.Equals(other))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// get the path from the last one to the start
        /// </summary>
        /// <typeparam name="T">T </typeparam>
        /// <param name="state">the last</param>
        /// <returns>solution</returns>
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
        /// <summary>
        /// abstract methos
        /// </summary>
        /// <param name="searchable"> serchabale</param>
        /// <returns>solution</returns>
        public abstract Solution<T> search(ISearchable<T> searchable);
    }
}
