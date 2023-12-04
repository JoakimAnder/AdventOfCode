using Infra.Helpers;
using System.Text.RegularExpressions;

namespace Puzzle;

public static partial class InputParser
{

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();

    public static ScratchCard ParseCard(string input)
    {
        //Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        var r = NumberRegex();
        var id = Helper.ParseInt(r.Match(input).ValueSpan);

        var numbersInput = input.Split(':')[1];
        var winningNumbersInput = numbersInput.Split("|")[0];
        var scratchedNumbersInput = numbersInput.Split("|")[1];

        var winningNumbers = r.Matches(winningNumbersInput)
            .Select(m => Helper.ParseInt(m.ValueSpan))
            .ToArray();
        var scratchedNumbers = r.Matches(scratchedNumbersInput)
            .Select(m => Helper.ParseInt(m.ValueSpan))
            .ToArray();

        return new ScratchCard(id, winningNumbers, scratchedNumbers);

    }
}
