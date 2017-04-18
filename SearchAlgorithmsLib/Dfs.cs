using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
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
        }
    }
}
