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

        public Solution(List<State<T>> path)
        {
            this.path = path;
        }
        public List<State<T>> getList()
        {
            return path;
        }
    }
}
