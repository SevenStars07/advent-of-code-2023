using part_1;

// var fileName = "example.txt";
var fileName = "input.txt";

var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName))
    .ToList();

var steps = lines[0].ToCharArray().Select(x => x.ToString()).ToList();

lines.RemoveRange(0, 2);

var nodes = lines.Select(x =>
{
    var parts = x.Split(" = ");

    var codes = parts[1].TrimStart('(').TrimEnd(')').Split(", ");
    
    return new Node
    {
        Name = parts[0],
        Left = codes[0],
        Right = codes[1]
    };
}).ToList();

foreach (var node in nodes)
{
    node.LeftNode = nodes.First(x => x.Name == node.Left);
    node.RightNode = nodes.First(x => x.Name == node.Right);
}

var currentNodes = nodes.Where(x => x.Name.EndsWith("A")).ToList();
var numberOfStepsToReachEndList = new List<(string, double, string)>();

foreach (var root in currentNodes)
{
    var currentNode = root;
    var currentStepIndex = 0;
    var numberOfSteps = 0;

    while (!currentNode.Name.EndsWith("Z"))
    {
        currentNode = currentNode.Move(steps[currentStepIndex]);
        currentStepIndex = (currentStepIndex + 1) % steps.Count;
        numberOfSteps++;
    }
    
    numberOfStepsToReachEndList.Add((root.Name, numberOfSteps, currentNode.Name));
}

numberOfStepsToReachEndList.ForEach(x=>Console.WriteLine($"{x.Item1} {x.Item2} {x.Item3}"));

var lcm = numberOfStepsToReachEndList.Select(x => x.Item2).Aggregate(LeastCommonMultiple);

Console.WriteLine(lcm);

double GreatestCommonDenominator(double a, double b)
{
    while (true)
    {
        if (b == 0) return a;
        var a1 = a;
        a = b;
        b = a1 % b;
    }
}

double LeastCommonMultiple(double a, double b) => a * b / GreatestCommonDenominator(a, b);