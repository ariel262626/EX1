﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Dfs <T>: Searcher<T>
    {
        public override Solution<T> search(ISearchable<T> searchable)
        {
            Stack<State<T>> stack = new Stack<State<T>>();
            HashSet<State<T>> visitedStates = new HashSet<State<T>>();
            stack.Push(searchable.getInitialState());
            while (stack.Count != 0)
            {

                State<T> state = stack.Pop();
                increaseEvaluatedNodes();

                if (state.Equals(searchable.getGoalState()))
                {
                    int x = 7;
                    return backTrace(state);
                }
                if (!visitedStates.Contains(state))
                {
                    visitedStates.Add(state);
                }
                List<State<T>> succerssors = searchable.getAllPossibleStates(state);
                //for every neighbor add hin to the stack
                foreach (State<T> neig in succerssors)
                {      
                    if (!visitedStates.Contains(neig))
                        stack.Push(neig);
                }
            }
            // if we had wrong in the input, we will arrive here
            return backTrace(searchable.getGoalState());
        }
    }
}
