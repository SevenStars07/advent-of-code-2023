// See https://aka.ms/new-console-template for more information

/*
    * Your calculation isn't quite right.
    * It looks like some of the digits are actually spelled out with letters: one, two, three, four, five,
    * six, seven, eight, and nine also count as valid "digits".
    *
    * Equipped with this new information, you now need to find the real first and last digit on each line. For example:
        * two1nine
        * eightwothree
        * abcone2threexyz
        * xtwone3four
        * 4nineeightseven2
        * zoneight234
        * 7pqrstsixteen
    *
    * In this example, the calibration values are 29, 83, 13, 24, 42, 14, and 76. Adding these together produces 281.
    * What is the sum of all of the calibration values?
*/

var possibleDigits = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

var letterToNumber = new Dictionary<string, string>
{
    { "one", "1" },
    { "two", "2" },
    { "three", "3" },
    { "four", "4" },
    { "five", "5" },
    { "six", "6" },
    { "seven", "7" },
    { "eight", "8" },
    { "nine", "9" },
};

// var fileName = "example.txt";
var fileName = "input.txt";

var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName));

var totalSum = 0;

foreach (var line in input)
{
    var firstDigit = "";
    var lastDigit = "";

    var prevFirstDigitIndex = int.MaxValue;
    var prevLastDigitIndex = int.MinValue;

    foreach (var digit in possibleDigits)
    {
        var firstDigitIndex = line.IndexOf(digit, StringComparison.Ordinal);

        if (firstDigitIndex != -1 && firstDigitIndex < prevFirstDigitIndex)
        {
            prevFirstDigitIndex = firstDigitIndex;
            firstDigit = letterToNumber[digit];
        }

        var lastDigitIndex = line.LastIndexOf(digit, StringComparison.Ordinal);

        if (lastDigitIndex != -1 && lastDigitIndex > prevLastDigitIndex)
        {
            prevLastDigitIndex = lastDigitIndex;
            lastDigit = letterToNumber[digit];
        }
    }

    var firstIntDigit = line.FirstOrDefault(x => int.TryParse(x.ToString(), out _));
    if (firstIntDigit != default(char))
    {
        var firstIntDigitIndex = line.IndexOf(firstIntDigit);

        if (firstIntDigitIndex < prevFirstDigitIndex)
        {
            firstDigit = firstIntDigit.ToString();
        }
    }

    var lastIntDigit = line.LastOrDefault(x => int.TryParse(x.ToString(), out _));
    if (lastIntDigit != default(char))
    {
        var lastIntDigitIndex = line.LastIndexOf(lastIntDigit);

        if (lastIntDigitIndex > prevLastDigitIndex)
        {
            lastDigit = lastIntDigit.ToString();
        }
    }

    var result = int.TryParse(firstDigit + lastDigit, out var calibrationValue);

    if (!result)
    {
        Console.WriteLine("Something went wrong!");
        return;
    }

    totalSum += calibrationValue;
}

Console.WriteLine(totalSum);