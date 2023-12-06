using part_1;

var fileName = "example.txt";
// var fileName = "input.txt";

var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName))
    .ToList();

var seedsLine = lines[0];
var seeds = seedsLine.Split(":")[1].Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(double.Parse).ToList();

lines.RemoveAt(0);
lines.RemoveAt(0);

var seedToSoil = GetMap("seed-to-soil", "soil-to-fertilizer");
var soilToFertilizer = GetMap("soil-to-fertilizer", "fertilizer-to-water");
var fertilizerToWater = GetMap("fertilizer-to-water", "water-to-light");
var waterToLight = GetMap("water-to-light", "light-to-temperature");
var lightToTemperature = GetMap("light-to-temperature", "temperature-to-humidity");
var temperatureToHumidity = GetMap("temperature-to-humidity", "humidity-to-location");
var humidityToLocation = GetMap("humidity-to-location");

var closestLocation = double.MaxValue;

foreach (var seed in seeds)
{
    var soil = GetValueForMapping(seedToSoil, seed);
    var fertilizer = GetValueForMapping(soilToFertilizer, soil);
    var water = GetValueForMapping(fertilizerToWater, fertilizer);
    var light = GetValueForMapping(waterToLight, water);
    var temperature = GetValueForMapping(lightToTemperature, light);
    var humidity = GetValueForMapping(temperatureToHumidity, temperature);
    var location = GetValueForMapping(humidityToLocation, humidity);

    if (location < closestLocation)
    {
        closestLocation = location;
    }
}

Console.WriteLine(closestLocation);

List<MapEntry> GetMap(string startString, string? endString = null)
{
    var map = new List<MapEntry>();

    var startIndex = lines.FindIndex(x => x.Contains(startString)) + 1;

    var stopIndex = !string.IsNullOrEmpty(endString)
        ? lines.FindIndex(x => x.Contains(endString)) - 2
        : lines.Count - 1;

    for (var i = startIndex; i <= stopIndex; i++)
    {
        var split = lines[i].Split(" ");
        var destination = double.Parse(split[0]);
        var source = double.Parse(split[1]);
        var range = double.Parse(split[2]);

        map.Add(new MapEntry
        {
            StartKeyIndex = source,
            StartValueIndex = destination,
            Range = range
        });
    }

    return map;
}

double GetValueForMapping(List<MapEntry> mapEntries, double key)
{
    var soilEntry = mapEntries.Find(x => x.StartKeyIndex <= key && key <= x.StartKeyIndex + x.Range);
    return soilEntry is not null ? soilEntry.StartValueIndex + key - soilEntry.StartKeyIndex : key;
}