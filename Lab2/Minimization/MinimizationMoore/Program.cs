using Domain.States;

List<MooreState> mooreStates = new List<MooreState>();

var reader = new StreamReader("../../../input.txt");
var writer = new StreamWriter("../../../output.txt");

ReadMooreFromFile(mooreStates, reader);

List<MinimizeMooreState> minimizeStates = PrepareToMinimize(mooreStates);

List<MinimizeMooreState> result = Minimization(minimizeStates);

mooreStates = ConvertMinimizeToMoore(result);

foreach (var state in mooreStates)
{
    writer.WriteLine(state.ToString());
}

reader.Close();
writer.Close();

static List<MinimizeMooreState> Minimization(List<MinimizeMooreState> minimizeStates)
{
    List<MinimizeMooreState> result = new();
    foreach (var states in minimizeStates)
    {
        List<MinimizeMooreState> resultStates = new();
        foreach (var state in states.SameStates)
        {
            List<int> findTransition = new();
            foreach (var trans in state.Transitions)
            {
                if (trans != -1)
                {
                    findTransition.Add(minimizeStates.IndexOf(minimizeStates
                    .FirstOrDefault(x => x.SameStates
                        .FirstOrDefault(x => x.Id == trans) != null)!));
                }
                else
                {
                    findTransition.Add(-1);
                }
            }

            MinimizeMooreState findSameStates = resultStates.FirstOrDefault(x => Enumerable.SequenceEqual(x.Transitions, findTransition))!;
            if (findSameStates != null)
            {
                findSameStates.SameStates.Add(state);
            }
            else
            {
                resultStates.Add(new MinimizeMooreState(states.Signal, state, findTransition));
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

static List<MooreState> ConvertMinimizeToMoore(List<MinimizeMooreState> minimizeMooreStates)
{
    List<MooreState> result = new();
    for (int i = 0; i < minimizeMooreStates.Count; i++)
    {
        MooreState state = new(i, minimizeMooreStates[i].Signal);
        state.Transitions = minimizeMooreStates[i].Transitions;
        result.Add(state);
    }
    return result;
}

static List<MinimizeMooreState> PrepareToMinimize(List<MooreState> mooreStates)
{
    List<MinimizeMooreState> result = new();
    foreach (var state in mooreStates)
    {
        MinimizeMooreState findSameStates = result.FirstOrDefault(x => x.Signal == state.Signal)!;
        if (findSameStates != null)
        {
            findSameStates.SameStates.Add(state);
        }
        else
        {
            result.Add(new MinimizeMooreState(state.Signal, state));
        }
    }
    return result;
}

static void ReadMooreFromFile(List<MooreState> mooreStates, StreamReader reader)
{
    int k, m;

    string[] input = reader.ReadLine()!.Split(" ");
    k = Convert.ToInt32(input[0]);
    m = Convert.ToInt32(input[1]);

    for (int i = 0; i < k; i++)
    {
        input = reader.ReadLine()!.Split(" ");
        MooreState mooreState = new MooreState(i, Convert.ToInt32(input[0]));
        for (int j = 1; j < m + 1; j++)
        {
            if (input[j] != "-")
            {
                mooreState.Transitions.Add(Convert.ToInt32(input[j]));
            }
            else
            {
                mooreState.Transitions.Add(-1);
            }
        }
        mooreStates.Add(mooreState);
    }
}