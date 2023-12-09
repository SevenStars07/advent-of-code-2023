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

// nodes.ForEach(Console.WriteLine);

var root = nodes.First(x => x.Name == "AAA");

var currentNode = root;
var currentStepIndex = 0;
var numberOfSteps = 0;

while (currentNode.Name != "ZZZ")
{
    currentNode = currentNode.Move(steps[currentStepIndex]);
    currentStepIndex = (currentStepIndex + 1) % steps.Count;
    numberOfSteps++;
}

Console.WriteLine(currentNode.Name);
Console.WriteLine(numberOfSteps);



