/*
    * As you walk, the Elf shows you a small bag and some cubes which are either red, green, or blue.
    * Each time you play this game, he will hide a secret number of cubes of each color in the bag,
    * and your goal is to figure out information about the number of cubes.
    *
    * To get information, once a bag has been loaded with cubes, the Elf will reach into the bag,
    * grab a handful of random cubes, show them to you, and then put them back in the bag.
    * He'll do this a few times per game.
    *
    * You play several games and record the information from each game (your puzzle input).
    * Each game is listed with its ID number (like the 11 in Game 11: ...) followed by a semicolon-separated
    * list of subsets of cubes that were revealed from the bag (like 3 red, 5 green, 4 blue).
*/

const int maxRed = 12;
const int maxGreen = 13;
const int maxBlue = 14;

// const string fileName = "example.txt";
const string fileName = "input.txt";

var games = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName));


var gameIdsSum = 0;

foreach (var game in games)
{
    var gameId = GetGameId(game);

    var isGamePossible =  IsGamePossible(game);

    if (isGamePossible)
    {
        gameIdsSum += gameId;
    }
}

Console.WriteLine(gameIdsSum);

bool IsGamePossible(string game)
{
    var sets = game.Split(":")[1].Split(";");

    foreach (var set in sets)
    {
        var cubes = set.Split(",").Select(x => x.Trim()).ToList();

        var red = int.Parse(cubes.FirstOrDefault(x => x.Contains("red"))?.Split(" ")[0] ?? "0");
        var green = int.Parse(cubes.FirstOrDefault(x => x.Contains("green"))?.Split(" ")[0] ?? "0");
        var blue = int.Parse(cubes.FirstOrDefault(x => x.Contains("blue"))?.Split(" ")[0] ?? "0");

        if (red > maxRed || green > maxGreen || blue > maxBlue)
            return false;
    }

    return true;
}

int GetGameId(string s)
{
    var firstSplit = s.Split(":");

    var gameSplit = firstSplit[0].Split(" ");

    return int.Parse(gameSplit[1]);
}