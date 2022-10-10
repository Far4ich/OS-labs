namespace States
{
    public class MooreState
    {
        public MooreState(int id, int signal)
        {
            PreviousStateId = id;
            Signal = signal;
        }
        public MooreState(string str)
        {
            string[] parseStr = str.Split(" ");

            Signal = Convert.ToInt32(parseStr[0].Trim('Y'));

            for (int i = 1; i < parseStr.Length; i++)
            {
                if (parseStr[i].Contains('q'))
                {
                    Transitions.Add(Convert.ToInt32(parseStr[i].Trim('q')));
                }
                if (parseStr[i].Contains('-'))
                {
                    Transitions.Add(-1);
                }
            }
        }

        public int PreviousStateId { get; set; }
        public int Signal { get; set; }
        public List<int> Transitions { get; set; } = new();

        public string ToString()
        {
            string result = "Y" + Signal + " ";
            foreach (var t in Transitions)
            {
                if (t != -1)
                {
                    result += "q" + t + " ";
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
