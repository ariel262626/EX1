using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        private List<State<T>> path;
        private int evaluatedNodes = 0;

        public Solution(List<State<T>> path)
        {
            this.path = path;
        }
        public List<State<T>> getList()
        {
            return path;
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
