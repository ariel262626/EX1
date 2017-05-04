using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// solution class
    /// </summary>
    /// <typeparam name="T"> T kind</typeparam>
    public class Solution<T>
    {
        /// <summary>
        /// the list of states my path
        /// </summary>
        private List<State<T>> myPath;
        /// <summary>
        /// evaluatednodes allready
        /// </summary>
        private int evaluatedNodes = 0;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="path"> get path </param>
        public Solution(List<State<T>> path)
        {
            myPath = new List<State<T>>();
           for (int i = path.Count - 1; i >= 0; i--)
            {
                myPath.Add(path[i]);
            }

        }
        /// <summary>
        /// get list
        /// </summary>
        /// <returns> the list</returns>
        public List<State<T>> getList()
        {
            return myPath;
        }
        /// <summary>
        /// set evalutednodes member
        /// </summary>
        /// <param name="evaluatedNodesFromAlgo"> the value to set</param>
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
