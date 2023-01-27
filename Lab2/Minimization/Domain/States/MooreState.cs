namespace Domain.States
{
    public class MooreState
    {
        public int Id { get; set; }
        public int Signal { get; set; }
        public List<int> Transitions { get; set; } = new();

        public MooreState(int id, int signal)
        {
            Id = id;
            Signal = signal;
        }

        public override string ToString()
        {
            string result = Signal + " ";
            foreach (var t in Transitions)
            {
                if (t != -1)
                {
                    result += t + " ";
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
