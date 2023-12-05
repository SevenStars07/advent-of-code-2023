// var fileName = "example.txt";
var fileName = "input.txt";

var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName));

var totalNumberOfCards = Enumerable.Repeat(1, lines.Length).ToList();

for (var i = 0; i < lines.Length; i++)
{
    var line = lines[i];

    var splitNumbers = line.Split(":")[1].Split("|");

    var winningNumbers = splitNumbers[0].Split(" ").Where(x=>!string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x.Trim()));
    var myNumbers = splitNumbers[1].Split(" ").Where(x=>!string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x.Trim()));

    var matchedNumbers = myNumbers.Count(x => winningNumbers.Contains(x));

    for (var j = 1; j <= matchedNumbers; j++)
    {
        totalNumberOfCards[i + j] += totalNumberOfCards[i];
    }
}

Console.WriteLine(totalNumberOfCards.Sum());