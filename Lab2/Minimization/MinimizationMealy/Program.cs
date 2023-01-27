using Domain.States;

List<MealyState> mealyStates = new List<MealyState>();

var reader = new StreamReader("../../../input.txt");
var writer = new StreamWriter("../../../output.txt");

ReadMealyFromFile(mealyStates, reader);

List<MinimizeMealyState> minimizeStates = PrepareToMinimize(mealyStates);

List<MinimizeMealyState> result = Minimization(minimizeStates);

mealyStates = ConvertMinimizeToMealy(result);

foreach (var state in mealyStates)
{
    writer.WriteLine(state.ToString());
}

reader.Close();
writer.Close();

static List<MinimizeMealyState> Minimization(List<MinimizeMealyState> minimizeStates)
{
    List<MinimizeMealyState> result = new();
    foreach (var states in minimizeStates)
    {
        List<MinimizeMealyState> resultStates = new();
        foreach (var state in states.SameStates)
        {
            List<int> findTransition = new();
            foreach (var trans in state.Transitions)
            {
                if (trans != null)
                {
                    findTransition.Add(minimizeStates.IndexOf(minimizeStates
                    .FirstOrDefault(x => x.SameStates
                        .FirstOrDefault(x => x.Id == trans.Item1) != null)!));
                }
                else
                {
                    findTransition.Add(-1);
                }
            }

            MinimizeMealyState findSameStates = resultStates.FirstOrDefault(x => Enumerable.SequenceEqual(x.Transitions, findTransition))!;
            if (findSameStates != null)
            {
                findSameStates.SameStates.Add(state);
            }
            else
            {
                resultStates.Add(new MinimizeMealyState(states.Signals, state, findTransition));
            }
        }
        result.AddRange(resultStates);
    }
    if (minimizeStates.Count == result.Count)
    {
        return result;
    }
    else
    {
        return Minimization(result);
    }
}

static List<MealyState> ConvertMinimizeToMealy(List<MinimizeMealyState> minimizeMealyStates)
{
    List<MealyState> result = new();
    for (int i = 0; i < minimizeMealyStates.Count; i++)
    {
        MealyState state = new(i);
        for (int j = 0; j < minimizeMealyStates[i].Transitions.Count; j++)
        {
            state.Transitions.Add(new Tuple<int, int>(minimizeMealyStates[i].Transitions[j], minimizeMealyStates[i].Signals[j]));
        }
        result.Add(state);
    }
    return result;
}

static List<MinimizeMealyState> PrepareToMinimize(List<MealyState> mealyStates)
{
    List<MinimizeMealyState> result = new();
    foreach (var state in mealyStates)
    {
        List<int> findSignals = new();
        foreach (var trans in state.Transitions)
        {
            if (trans != null)
            {
                findSignals.Add(trans.Item2);
            }
            else
            {
                findSignals.Add(-1);
            }
        }
        MinimizeMealyState findSameStates = result.FirstOrDefault(x => Enumerable.SequenceEqual(x.Signals, findSignals))!;
        if (findSameStates != null)
        {
            findSameStates.SameStates.Add(state);
        }
        else
        {
            result.Add(new MinimizeMealyState(findSignals, state));
        }
    }
    return result;
}

static void ReadMealyFromFile(List<MealyState> mealyStates, StreamReader reader)
{
    int k, m;

    string[] input = reader.ReadLine()!.Split(" ");
    k = Convert.ToInt32(input[0]);
    m = Convert.ToInt32(input[1]);

    for (int i = 0; i < k; i++)
    {
        input = reader.ReadLine()!.Split(" ");
        MealyState mealyState = new MealyState(i);
        for (int j = 0; j < m; j++)
        {
            if(input[j * 2] != "-")
            {
                mealyState.Transitions.Add(new Tuple<int, int>(
                Convert.ToInt32(input[j * 2]),
                Convert.ToInt32(input[j * 2 + 1])));
            }
            else
            {
                mealyState.Transitions.Add(null!);
            }
        }
        mealyStates.Add(mealyState);
    }
}