using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// isearchable interface
    /// </summary>
    /// <typeparam name="T">T type</typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// get start state
        /// </summary>
        /// <returns>start state</returns>
        State<T> getInitialState();
        /// <summary>
        /// get goal state
        /// </summary>
        /// <returns>end state</returns>
        State<T> getGoalState();
        /// <summary>
        /// all neighbours of s
        /// </summary>
        /// <param name="s"> find s neighbours</param>
        /// <returns> list of neighbours</returns>
        List<State<T>> getAllPossibleStates(State<T> s);
    }
}
