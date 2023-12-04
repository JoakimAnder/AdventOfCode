
using Infra.Helpers;
using Infra.Interfaces;

namespace Day1;

public class Part1 : IPuzzlePart
{
    public object? ExpectedResult => 55090;

    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(Environment.CurrentDirectory));

        var sum = 0;
        foreach (var line in input.LinesAsEnumerable())
        {
            var calibrationValue = ParseCalibrationValue(line);
            sum += calibrationValue;
        }

        Console.WriteLine("The sum of all of the calibration values is {0}", sum);
        return sum;
    }

    private static int ParseCalibrationValue(string line)
    {
        var firstNum = line.First(char.IsNumber);
        var lastNum = line.Last(char.IsNumber);

        return Helper.ParseInt($"{firstNum}{lastNum}");
    }
}
