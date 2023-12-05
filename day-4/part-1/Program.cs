// var fileName = "example.txt";
var fileName = "input.txt";

var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName));

var totalPoints = 0d;

foreach (var line in lines)
{
    var splitNumbers = line.Split(":")[1].Split("|");

    var winningNumbers = splitNumbers[0].Split(" ").Where(x=>!string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x.Trim()));
    var myNumbers = splitNumbers[1].Split(" ").Where(x=>!string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x.Trim()));

    var matchedNumbers = myNumbers.Count(x => winningNumbers.Contains(x));

    totalPoints += GetPoints(matchedNumbers);
}

Console.WriteLine(totalPoints);

double GetPoints(int matchedNumbers)
{
    if (matchedNumbers == 0)
    {
        return 0;
    }
    return Math.Pow(2, matchedNumbers - 1);
}