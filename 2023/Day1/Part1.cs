
using Shared.Helpers;

namespace Day1;

public static class Part1
{
    public static void Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(Environment.CurrentDirectory));
        
        var sum = 0;
        foreach (var line in input.LinesAsEnumerable())
        {
            var calibrationValue = ParseCalibrationValue(line);
            sum += calibrationValue;
        }

        Console.WriteLine("The sum of all of the calibration values is {0}", sum);
    }

    private static int ParseCalibrationValue(string line)
    {
        var firstNum = line.First(char.IsNumber);
        var lastNum = line.Last(char.IsNumber);

        return int.Parse($"{firstNum}{lastNum}");
    }
}
