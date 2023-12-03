using System.Text;
using part_2;

// var fileName = "example.txt";
var fileName = "input.txt";

var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName));

var numberOfLines = lines.Length;
var numberOfColumns = lines[0].Length;

var foundNumbers = new List<(int, int)>();

var gears = new List<Gear>();

var matrix = new string[numberOfLines][];

CreateMatrix(numberOfLines, matrix, numberOfColumns, lines);

for (var i = 0; i < numberOfLines; i++)
{
    for (var j = 0; j < numberOfColumns; j++)
    {
        var character = matrix[i][j];

        if (IsNotSymbol(character))
        {
            continue;
        }

        var gear = character == "*"
            ? new Gear
            {
                X = i, Y = j
            }
            : null;

        for (var k = -1; k <= 1; k++)
        {
            for (var l = -1; l <= 1; l++)
            {
                var neighborY = i + k;
                var neighborX = j + l;
                if (neighborY < 0 || neighborY >= numberOfLines || neighborX < 0 || neighborX >= numberOfColumns)
                {
                    continue;
                }

                var neighborChar = matrix[neighborY][neighborX];

                if (!char.IsDigit(neighborChar, 0))
                {
                    continue;
                }

                var number = GetNumberContainingDigit(neighborY, neighborX);

                if (number.HasValue) gear?.NeighboringNumbers.Add(number.Value);
                // totalSum += number;
            }
        }

        if (gear != null) gears.Add(gear);
    }
}

var totalGearRatio = 0;

foreach (var gear in gears.Where(g=>g.NeighboringNumbers.Count == 2))
{
    var gearRatio = gear.NeighboringNumbers[0] * gear.NeighboringNumbers[1];

    totalGearRatio += gearRatio;
}

Console.WriteLine(totalGearRatio);

int? GetNumberContainingDigit(int lineIndex, int colIndex)
{
    var sb = new StringBuilder();

    var firstCharInThisNumberIndex = GetFirstIndexOfNumber(matrix[lineIndex], colIndex);

    if (foundNumbers.Contains((lineIndex, firstCharInThisNumberIndex)))
    {
        return null;
    }

    foundNumbers.Add((lineIndex, firstCharInThisNumberIndex));

    while (true)
    {
        if (firstCharInThisNumberIndex >= numberOfColumns ||
            !char.IsDigit(matrix[lineIndex][firstCharInThisNumberIndex], 0))
        {
            break;
        }

        sb.Append(matrix[lineIndex][firstCharInThisNumberIndex]);
        firstCharInThisNumberIndex += 1;
    }

    return int.Parse(sb.ToString());
}

int GetFirstIndexOfNumber(string[] line, int colIndex)
{
    var firstCharInThisNumberIndex = colIndex;

    while (true)
    {
        if (firstCharInThisNumberIndex < 0 || !char.IsDigit(line[firstCharInThisNumberIndex], 0))
        {
            return firstCharInThisNumberIndex + 1;
            break;
        }

        firstCharInThisNumberIndex -= 1;
    }
}

void CreateMatrix(int numberOfLines1, string[][] strings, int numberOfColumns1, string[] lines1)
{
    for (var i = 0; i < numberOfLines1; i++)
    {
        strings[i] = new string[numberOfColumns1];
        for (var j = 0; j < numberOfColumns1; j++)
        {
            var character = lines1[i][j];
            strings[i][j] = character.ToString();
        }
    }
}

void PrintMatrix()
{
    for (var i = 0; i < numberOfLines; i++)
    {
        for (var j = 0; j < numberOfColumns; j++)
        {
            Console.Write($"{matrix[i][j]} ");
        }

        Console.WriteLine();
    }
}

bool IsNotSymbol(string s)
{
    return char.IsDigit(s, 0) || s == ".";
}