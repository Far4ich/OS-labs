using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.States
{
    public class MealyState
    {
        public int Id { get; set; }
        public List<Tuple<int, int>> Transitions { get; set; } = new();
        public MealyState(int id)
        {
            Id = id;
        }
        public override string ToString()
        {
            string result = string.Empty;
            foreach (var t in Transitions)
            {
                if (t != null && t.Item1 >=0 && t.Item2 >= 0)
                {
                    result += t.Item1 + " " + t.Item2 + " ";
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
