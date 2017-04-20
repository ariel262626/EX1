using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// bfs implement searcher
    /// </summary>
    /// <typeparam name="T">T type</typeparam>
    public class Bfs<T> : Searcher<T>
    {
        /// <summary>
        /// search for solution
        /// </summary>
        /// <param name="searchable"> T type</param>
        /// <returns>solution</returns>
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
                            // remove and add it, in order the priority queue will sort it
                            removeFromOpenList(s);
                            addToOpenList(s);
                        }
                    }
                }
                // if we had wrong in the input, we will arrive here
                return backTrace(searchable.getGoalState());
            }
        }
    }
