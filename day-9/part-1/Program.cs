// var fileName = "example.txt";
var fileName = "input.txt";

var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName))
    .ToList();

var total = 0L;

lines.ForEach(line =>
{
    var history = line.Split(" ").Select(long.Parse).ToList();

    var sequences = new List<List<long>>();

    var currentSequence = history;

    sequences.Add(currentSequence);

    while (currentSequence.Any(l => l != 0))
    {
        var newSequence = new List<long>();

        for (var i = 0; i < currentSequence.Count - 1; i++)
        {
            newSequence.Add(currentSequence[i + 1] - currentSequence[i]);
        }

        currentSequence = newSequence;
        sequences.Add(currentSequence);
    }

    var nextData = sequences.Sum(l => l.Last());

    total += nextData;
});

Console.WriteLine(total);