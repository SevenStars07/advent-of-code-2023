// var fileName = "example.txt";
var fileName = "input.txt";

var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName));

var times = lines[0].Split(":")[1].Trim().Split(" ").Where(x=>!string.IsNullOrWhiteSpace(x));
var distances =  lines[1].Split(":")[1].Trim().Split(" ").Where(x=>!string.IsNullOrWhiteSpace(x));

var races = times.Zip(distances, (a, b) => (int.Parse(a.Trim()), int.Parse(b.Trim())));

var totalWaysToWin = 1;

foreach (var race in races)
{
    var numberOfWaysToWinRace = GetNumberOfSecondsToHoldForWin(race);

    totalWaysToWin *= numberOfWaysToWinRace;
}

Console.WriteLine(totalWaysToWin);

int GetNumberOfSecondsToHoldForWin((int, int) race)
{
    var wins = new List<int>();

    var time = race.Item1;
    var raceDistanceToWin = race.Item2;

    for (var i = 1; i < time; i++)
    {
        var speed = i;

        var distance = (time - i) * speed;

        if (distance > raceDistanceToWin)
            wins.Add(speed);
    }

    return wins.Count;
}