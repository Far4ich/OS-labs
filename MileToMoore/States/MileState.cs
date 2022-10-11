using System.Security.Cryptography;

namespace States
{
    public class MealyState
    {
        public MealyState(string str)
        {
            string[] transitions = str.Split(", ");
            foreach (string transition in transitions)
            {
                if (transition != "-")
                {
                    string[] transitionParameters = transition.Split(" ");
                    Transitions.Add(new Tuple<int, int>(
                        Convert.ToInt32(transitionParameters[0].TrimStart('S')),
                        Convert.ToInt32(transitionParameters[1].TrimStart('Y')))
                    );
                }
                else
                {
                    Transitions.Add(null!);
                }
            }
        }
        public MealyState(MooreState currentMooreState, List<MooreState> mooreStates)
        {
            foreach (var t in currentMooreState.Transitions)
            {
                if (t != -1)
                {
                    Transitions.Add(new Tuple<int, int>(
                        t,
                        mooreStates[t].Signal));
                }
                else
                {
                    Transitions.Add(null!);
                }
            }
        }
        public List<Tuple<int, int>> Transitions { get; set; } = new();
        public override string ToString()
        {
            string result = string.Empty;
            foreach(var t in Transitions)
            {
                if(t != null)
                {
                    result += "S" + t.Item1 + " Y" + t.Item2 + " ";
                }
                else
                {
                    result += "- ";
                }
            }
            return result; 
        }
    }
}
