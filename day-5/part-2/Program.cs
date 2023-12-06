using System.Collections.Concurrent;
using part_2;

// var fileName = "example.txt";
var fileName = "input.txt";

var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName))
    .ToList();

var seedsLine = lines[0];
var seeds = GetSeeds();
// var seeds = seedsLine.Split(":")[1].Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(double.Parse).ToList();

lines.RemoveRange(0, 2);

var seedToSoil = GetMap("seed-to-soil", "soil-to-fertilizer");
var soilToFertilizer = GetMap("soil-to-fertilizer", "fertilizer-to-water");
var fertilizerToWater = GetMap("fertilizer-to-water", "water-to-light");
var waterToLight = GetMap("water-to-light", "light-to-temperature");
var lightToTemperature = GetMap("light-to-temperature", "temperature-to-humidity");
var temperatureToHumidity = GetMap("temperature-to-humidity", "humidity-to-location");
var humidityToLocation = GetMap("humidity-to-location");

var closestLocation = double.MaxValue;

// var final = new BlockingCollection<double>();
// Parallel.ForEach(seeds, seed =>
// {
//     Console.WriteLine($"starting {seed.StartIndex} with range {seed.Range}");
//     var seedClosestLocation = double.MaxValue;
//     for (var i = 0; i < seed.Range; i++)
//     {
//         var soil = GetValueForMapping(seedToSoil, seed.StartIndex+i);
//         var fertilizer = GetValueForMapping(soilToFertilizer, soil);
//         var water = GetValueForMapping(fertilizerToWater, fertilizer);
//         var light = GetValueForMapping(waterToLight, water);
//         var temperature = GetValueForMapping(lightToTemperature, light);
//         var humidity = GetValueForMapping(temperatureToHumidity, temperature);
//         var location = GetValueForMapping(humidityToLocation, humidity);
//
//         if (location < seedClosestLocation)
//         {
//             seedClosestLocation = location;
//         }
//     }
//     Console.WriteLine($"finished {seed.StartIndex} with range {seed.Range} found {seedClosestLocation}");
//     final.Add(seedClosestLocation);
// });

// Console.WriteLine(final.Min());

foreach (var seed in seeds)
{
    Console.WriteLine($"starting {seed.StartIndex} with range {seed.Range}");
    for (var i = 0; i < seed.Range; i++)
    {
        var soil = GetValueForMapping(seedToSoil, seed.StartIndex + i);
        var fertilizer = GetValueForMapping(soilToFertilizer, soil);
        var water = GetValueForMapping(fertilizerToWater, fertilizer);
        var light = GetValueForMapping(waterToLight, water);
        var temperature = GetValueForMapping(lightToTemperature, light);
        var humidity = GetValueForMapping(temperatureToHumidity, temperature);
        var location = GetValueForMapping(humidityToLocation, humidity);

        if (location < closestLocation)
        {
            closestLocation = location;
            Console.WriteLine($"Found closer location {closestLocation}");
        }
    }
    Console.WriteLine($"finished {seed.StartIndex}");
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
    // return map.OrderBy(x => x.StartKeyIndex).ToList();
}

double GetValueForMapping(List<MapEntry> mapEntries, double key)
{
    var soilEntry = mapEntries.Find(x => x.StartKeyIndex <= key && key <= x.StartKeyIndex + x.Range);
    return soilEntry is not null ? soilEntry.StartValueIndex + key - soilEntry.StartKeyIndex : key;

    // var nextSoilEntryIndex = mapEntries.FindIndex(x => x.StartKeyIndex > key);
    //
    // if (nextSoilEntryIndex == -1)
    // {
    //
    // }
    //
    // var prevSoilEntry = mapEntries[nextSoilEntryIndex - 1];
    // var soilEntry2 = key < prevSoilEntry.StartKeyIndex + prevSoilEntry.Range ? prevSoilEntry : null;
    // return soilEntry2 is not null ? soilEntry2.StartValueIndex + key - soilEntry2.StartKeyIndex : key;
}

double GetKeyForValue(List<MapEntry> mapEntries, double value)
{
    // mapEntries.Find(x=>)
    return 0;
}

List<SeedEntry> GetSeeds()
{
    var seedsMap = new List<SeedEntry>();

    var seedsSplit = seedsLine.Split(": ")[1].Split(" ");

    for (var i = 0; i < seedsSplit.Length; i += 2)
    {
        seedsMap.Add(new SeedEntry
        {
            StartIndex = double.Parse(seedsSplit[i]),
            Range = double.Parse(seedsSplit[i + 1])
        });
    }

    return seedsMap;
}