using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.States
{
    public class MinimizeMooreState
    {
        public List<MooreState> SameStates = new();
        public List<int> Transitions = new();
        public readonly int Signal;

        public MinimizeMooreState(int signal, MooreState state, List<int> transitions = null!)
        {
            Signal = signal;
            Transitions = transitions;
            SameStates.Add(state);
        }
    }
}
