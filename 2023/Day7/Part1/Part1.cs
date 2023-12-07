using Infra.Helpers;
using Infra.Interfaces;
using Utilities;

namespace Puzzle;

public class Part1 : IPuzzlePart
{
    public object? ExpectedResult => 6440;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(nameof(Part1)));

        var hands = input.ParseLines(InputParser.Parse).ToList();


        var comparer = new HandComparer2();
        var totalWinnings = hands
            .OrderBy(g => g, comparer)
            .Select((h, i) => h.Bid * (i + 1))
            .Sum();

        Console.WriteLine("The total winnings is {0}", totalWinnings);
        return totalWinnings;
    }

}
