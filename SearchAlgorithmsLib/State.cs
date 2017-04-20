using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{ 
    public class State<T>
    {
        private T state; // the state represented by a STRING 
        private float cost; // cost to reach this state (set by a setter)
        private State<T> cameFrom;
        public State(T state) {
            this.state = state;
            this.cost = 0;
            this.cameFrom = null;
        }
        public override bool Equals(object obj)
        {
            bool a = state.Equals((obj as State<T>).state);
            return state.Equals((obj as State<T>).state);
        }
        //get the cost - priority
        public float Cost
        {
            get; set;
        }

        public State<T> CameFrom
        {
            get; set;
        }

        public T getState()
        {
            return state;
        }

        public override int GetHashCode()
        {
            return state.ToString().GetHashCode();
        }
    }

    

    

    
}