// const string fileName = "example.txt";
const string fileName = "input.txt";

var games = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName));

var totalPower = 0;

foreach (var game in games)
{
    var (red, green, blue) = GetMaxNumberOfCubes(game);

    var power = GetPowerOfGame(red, green, blue);

    totalPower += power;
}

Console.WriteLine(totalPower);

(int minRed, int minGreen, int minBlue) GetMaxNumberOfCubes(string game)
{
    var maxRed = int.MinValue;
    var maxGreen = int.MinValue;
    var maxBlue = int.MinValue;

    var sets = game.Split(":")[1].Split(";");

    foreach (var set in sets)
    {
        var cubes = set.Split(",").Select(x => x.Trim()).ToList();

        var red = int.Parse(cubes.FirstOrDefault(x => x.Contains("red"))?.Split(" ")[0] ?? "0");
        var green = int.Parse(cubes.FirstOrDefault(x => x.Contains("green"))?.Split(" ")[0] ?? "0");
        var blue = int.Parse(cubes.FirstOrDefault(x => x.Contains("blue"))?.Split(" ")[0] ?? "0");

        maxRed = red > maxRed ? red : maxRed;
        maxGreen = green > maxGreen ? green : maxGreen;
        maxBlue = blue > maxBlue ? blue : maxBlue;
    }

    return (maxRed, maxGreen, maxBlue);
}

int GetPowerOfGame(int red, int green, int blue)
{
    return red * green * blue;
}