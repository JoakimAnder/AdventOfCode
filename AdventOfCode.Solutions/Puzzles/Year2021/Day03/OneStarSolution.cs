
namespace AdventOfCode.Solutions.Puzzles.Year2021.Day03;

public class OneStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var values = input.Split(Environment.NewLine);
        var valueCount = values.Length;
        var binaryGammaRate = "";
        for (int i = 0; i < values.First().Length; i++)
        {
            var oneCount = values.Count(v => v[i] == '1'); // Todo: refactor out all of the enumerations.
            binaryGammaRate += oneCount > valueCount / 2 ? '1' : 0;
        }

        var gammaRate = 0;
        var epsilonRate = 0;
        var reversedBinaryGammaRate = binaryGammaRate.Reverse().ToArray();
        for (int i = 0; i < binaryGammaRate.Length; i++)
        {
            var value = (int)Math.Pow(2, i);
            if (reversedBinaryGammaRate[i] == '1')
                gammaRate += value;
            else
                epsilonRate += value;
        }

        return ValueTask.FromResult<object>(new { GammaRate = gammaRate, EpsilonRate = epsilonRate, PowerConsumption = gammaRate * epsilonRate });
    }

}
