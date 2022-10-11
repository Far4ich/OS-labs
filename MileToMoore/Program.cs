using States;

int k, m;
var reader = new StreamReader("../../../input.txt");
var writer = new StreamWriter("../../../output.txt");

string[] input = reader.ReadLine()!.Split(" ");
k = Convert.ToInt32(input[0]);
m = Convert.ToInt32(input[1]);

List<MealyState> mealyStates = new();

for(int i=0; i < k; i++)
{
    MealyState mealyState = new MealyState(reader.ReadLine()!);
    mealyStates.Add(mealyState);
}

List<MooreState> mooreStates = new();

AddMooreStates(mealyStates, mooreStates);
FillTransitions(mealyStates, mooreStates);

foreach (var state in mooreStates)
{
    writer.WriteLine(state.ToString());
}

reader.Close();
writer.Close();

static void AddMooreStates(List<MealyState> mealyStates, List<MooreState> mooreStates)
{
    int maxSignalId = GetMaxSignalId(mealyStates);
    for (int i = 0; i < mealyStates.Count; i++)
    {
        for (int j = 1; j <= maxSignalId; j++)
        {
            if(mealyStates.Find(x => 
                x.Transitions.Exists(x => 
                    x != null && x.Item1 == i && x.Item2 == j)) != null)
            {
                mooreStates.Add(new(i, j));
            }
        }
    }
}

static int GetMaxSignalId(List<MealyState> mealyStates)
{
    int result = 0;
    foreach(var state in mealyStates)
    {
        int maxSignal = state.Transitions.Max(x =>
            {
                if (x != null)
                {
                    return x.Item2;
                }
                return 0;
            });
        result = maxSignal > result ? maxSignal : result;
    }
    return result;
}

static void FillTransitions(List<MealyState> mealyStates, List<MooreState> mooreStates)
{
    foreach (var mooreState in mooreStates)
    {
        foreach(var trans in mealyStates[mooreState.PreviousStateId].Transitions)
        {
            if(trans != null)
            {
                mooreState.Transitions.Add(
                    mooreStates.IndexOf(
                        mooreStates.First(
                            x => x.PreviousStateId == trans.Item1 && x.Signal == trans.Item2)));
            }
            else
            {
                mooreState.Transitions.Add(-1);
            }
        }
    }
}