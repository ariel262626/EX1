using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{ 
    /// <summary>
    /// generic state class
    /// </summary>
    /// <typeparam name="T"> t is the type we send </typeparam>
    public class State<T>
    {
        /// <summary>
        /// the state represented by a STRING
        /// </summary>
        private T state;
        /// <summary>
        ///cost to reach this state (set by a setter)
        /// </summary>      
        private float cost;
        /// <summary>
        /// from who i came from
        /// </summary>
        private State<T> cameFrom;
        /// <summary>
        /// constructor of state class
        /// </summary>
        /// <param name="state"> get T kind</param>
        public State(T state) {
            this.state = state;
            this.cost = 0;
            this.cameFrom = null;
        }
        /// <summary>
        /// override the equals of object for states
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool a = state.Equals((obj as State<T>).state);
            return state.Equals((obj as State<T>).state);
        }
        /// <summary>
        ///get the cost - priority
        /// </summary>
        public float Cost
        {
            get; set;
        }
        /// <summary>
        ///get the camefrom - priority
        /// </summary>
        public State<T> CameFrom
        {
            get; set;
        }

        /// <summary>
        /// get state 
        /// </summary>
        /// <returns> T state</returns>
        public T getState()
        {
            return state;
        }
        /// <summary>
        /// override the hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return state.ToString().GetHashCode();
        }
    }

    

    

    
}