// var fileName = "example.txt";
var fileName = "input.txt";

var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName));

var time = lines[0].Split(":")[1].Trim().Split(" ").Where(x=>!string.IsNullOrWhiteSpace(x)).Aggregate((a,b)=>a+b);
var distance =  lines[1].Split(":")[1].Trim().Split(" ").Where(x=>!string.IsNullOrWhiteSpace(x)).Aggregate((a,b)=>a+b);

var numberOfWaysToWinRace = GetNumberOfSecondsToHoldForWin((double.Parse(time), double.Parse(distance)));

Console.WriteLine(numberOfWaysToWinRace);

double GetNumberOfSecondsToHoldForWin((double, double) race)
{
    var numberOfWins = 0;

    var raceDistanceToWin = race.Item2;

    for (var i = 1; i < race.Item1; i++)
    {
        var distanceThisRace = (race.Item1 - i) * i;

        if (distanceThisRace > raceDistanceToWin)
            numberOfWins++;
    }

    return numberOfWins;
}