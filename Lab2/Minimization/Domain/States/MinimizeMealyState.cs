namespace Domain.States
{
    public class MinimizeMealyState
    {
        public List<MealyState> SameStates = new();
        public List<int> Transitions = new();
        public readonly List<int> Signals;

        public MinimizeMealyState(List<int> signalList, MealyState state, List<int> transitions = null!)
        {
            Signals = signalList;
            Transitions = transitions;
            SameStates.Add(state);
        }
    }
}
