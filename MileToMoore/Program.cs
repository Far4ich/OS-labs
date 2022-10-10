using States;

int k, m;
var reader = new StreamReader("../../../input.txt");
var writer = new StreamWriter("../../../output.txt");

string[] input = reader.ReadLine().Split(" ");
k = Convert.ToInt32(input[0]);
m = Convert.ToInt32(input[1]);

List<MileState> mileStates = new();

for(int i=0; i < k; i++)
{
    MileState mileState = new MileState(reader.ReadLine());
    mileStates.Add(mileState);
}

List<MooreState> mooreStates = new();

AddMooreStates(m, mileStates, mooreStates);
FillTransitions(m, mileStates, mooreStates);

foreach (var state in mooreStates)
{
    writer.WriteLine(state.ToString());
}

reader.Close();
writer.Close();

static void AddMooreStates(int m, List<MileState> mileStates, List<MooreState> mooreStates)
{
    for (int i = 0; i < mileStates.Count; i++)
    {
        for (int j = 1; j <= m; j++)
        {
            if(mileStates.Find(x => 
                x.Transitions.Exists(x => 
                    x != null && x.Item1 == i && x.Item2 == j)) != null)
            {
                mooreStates.Add(new(i, j));
            }
        }
    }
}

static void FillTransitions(int m, List<MileState> mileStates, List<MooreState> mooreStates)
{
    foreach (var mooreState in mooreStates)
    {
        foreach(var trans in mileStates[mooreState.PreviousStateId].Transitions)
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