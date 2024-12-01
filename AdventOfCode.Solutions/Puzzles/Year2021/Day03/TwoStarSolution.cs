
namespace AdventOfCode.Solutions.Puzzles.Year2021.Day03;

public class TwoStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var values = input
            .Split(Environment.NewLine)
            .Select(l => l.Select(c => c == '1').ToArray())
            .ToArray();

        var oxygenGeneratorRatingBinaries = FindRatingBinaries(values, true, false);
        var co2ScrubberRatingBinaries = FindRatingBinaries(values, false, true);
        var oxygenGeneratorRating = CalculateBinaries(oxygenGeneratorRatingBinaries);
        var co2ScrubberRating = CalculateBinaries(co2ScrubberRatingBinaries);

        return ValueTask.FromResult<object>(new
        {
            OxygenGeneratorRating = oxygenGeneratorRating,
            Co2ScrubberRating = co2ScrubberRating,
            LifeSupportRating = oxygenGeneratorRating * co2ScrubberRating
        });
    }

    private static bool? MostCommonValue(bool[][] binaries, int position)
    {
        var halfCount = binaries.Length / 2;
        var oneCount = binaries.Count(x => x[position]);

        if (oneCount == halfCount)
            return null;
        return oneCount > halfCount;
    }

    private static bool[] FindRatingBinaries(bool[][] binaries, bool defaultMostCommon, bool reverseMostCommon)
    {
        for (int i = 0; i < binaries[0].Length; i++)
        {
            if (binaries.Length == 1)
                break;

            var mostCommon = MostCommonValue(binaries, i);
            if (mostCommon is null)
                mostCommon = defaultMostCommon;
            else if (reverseMostCommon)
                mostCommon = !mostCommon;

            binaries = binaries.Where(x => x[i] == mostCommon).ToArray();
        }

        return binaries[0];
    }

    private static int CalculateBinaries(bool[] binaries)
    {
        var totalValue = 0;
        var reversedBinaries = binaries.Reverse().ToArray();

        for (int i = 0; i < binaries.Length; i++)
        {
            var currentValue = (int)Math.Pow(2, i);
            if (reversedBinaries[i])
                totalValue += currentValue;
        }

        return totalValue;
    }
}
