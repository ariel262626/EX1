using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// interface
    /// </summary>
    /// <typeparam name="T">T type</typeparam>
    public interface ISearcher<T>
    {
        /// <summary>
        /// the search method
        /// </summary>
        /// <param name="searchable">serachable</param>
        /// <returns>solution</returns>
        Solution<T> search(ISearchable<T> searchable);
        /// <summary>
        ///  get how many nodes were evaluated by the algorithm
        /// </summary>
        /// <returns>int </returns>
        int getNumberOfNodesEvaluated();
    }
}
