using States;

int k, m;
var reader = new StreamReader("../../../input.txt");
var writer = new StreamWriter("../../../output.txt");

string[] input = reader.ReadLine()!.Split(" ");
k = Convert.ToInt32(input[0]);
m = Convert.ToInt32(input[1]);

List<MooreState> mooreStates = new();

for (var i = 0; i < k; i++)
{
    MooreState mooreState = new(reader.ReadLine()!);
    mooreStates.Add(mooreState);
}

List<MealyState> mileStates = new();

foreach(MooreState mooreState in mooreStates)
{
    MealyState mileState = new(mooreState, mooreStates);
    mileStates.Add(mileState);
}

foreach(MealyState mileState in mileStates)
{
    writer.WriteLine(mileState.ToString());
}

reader.Close();
writer.Close();