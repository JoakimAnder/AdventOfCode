
using AdventOfCode.Solutions.Helpers;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Puzzles.Year2024.Day03;

public partial class OneStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var instructions = ParseInput(input);

        var sum = instructions.Sum(i => i.Result);
        return ValueTask.FromResult<object>(sum);
    }

    private static IReadOnlyCollection<MultiplicationInstruction> ParseInput(ReadOnlySpan<char> input)
    {
        List<MultiplicationInstruction> instructions = [];
        foreach (var match in MultiplicationInstruction.Regex.EnumerateMatches(input))
        {
            var range = match.ToRange();
            var numbers = input[range].GetIntegers();
            var x = numbers.ElementAt(0);
            var y = numbers.ElementAt(1);
            instructions.Add(new MultiplicationInstruction(x, y));
        }
        return instructions;
    }

    private readonly partial record struct MultiplicationInstruction(int X, int Y)
    {
        public static readonly Regex Regex = GetRegex();
        public readonly int Result { get; } = X * Y;

        [GeneratedRegex("mul\\(\\d{1,3},\\d{1,3}\\)")]
        private static partial Regex GetRegex();
    }
}
