using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        private List<State<T>> myPath;
        private int evaluatedNodes = 0;

        public Solution(List<State<T>> path)
        {
            myPath = new List<State<T>>();
           for (int i = path.Count - 1; i >= 0; i--)
            {
                myPath.Add(path[i]);
            }
           // this.myPath = path;

        }
        public List<State<T>> getList()
        {
            return myPath;
        }
        public void setEvaluatedNodes(int evaluatedNodesFromAlgo)
        {
            this.evaluatedNodes = evaluatedNodesFromAlgo;
        }
        
        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
    }
}
