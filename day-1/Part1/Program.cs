/*
    * The newly-improved calibration document consists of lines of text; each line originally contained
    * a specific calibration value that the Elves now need to recover.
    * On each line, the calibration value can be found by combining the first digit and the last digit
    * (in that order) to form a single two-digit number.
    *
    * For example:
        * 1abc2
        * pqr3stu8vwx
        * a1b2c3d4e5f
        * treb7uchet
    *
    * In this example, the calibration values of these four lines are 12, 38, 15, and 77.
    * Adding these together produces 142.
    * Consider your entire calibration document. What is the sum of all of the calibration values?
*/

// var fileName = "example.txt";
var fileName = "input.txt";

var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName));

var totalSum = 0;

foreach (var line in input)
{
    var firstDigit = line.First(x => int.TryParse(x.ToString(), out _)).ToString();
    var lastDigit = line.Last(x => int.TryParse(x.ToString(), out _)).ToString();

    var result = int.TryParse(firstDigit + lastDigit, out var calibrationValue);

    if (!result)
    {
        Console.WriteLine("Something went wrong!");
        return;
    }

    totalSum += calibrationValue;
}

Console.WriteLine(totalSum);