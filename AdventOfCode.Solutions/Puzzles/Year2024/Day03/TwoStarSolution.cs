using AdventOfCode.Solutions.Helpers;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Puzzles.Year2024.Day03;

public partial class TwoStarSolution : ISolution
{
    public ValueTask<object> Solve(string input, CancellationToken ct)
    {
        var instructions = ParseInput(input);

        var sum = 0;
        bool isEnabled = true;
        foreach (var instruction in instructions)
        {
            if (instruction is EnableInstruction)
                isEnabled = true;
            if (instruction is DisableInstruction)
                isEnabled = false;
            if (isEnabled && instruction is MultiplicationInstruction i)
                sum += i.Result;
        }

        return ValueTask.FromResult<object>(sum);
    }

    private static IReadOnlyCollection<IInstruction> ParseInput(ReadOnlySpan<char> input)
    {
        List<IInstruction> instructions = [];
        foreach (var match in InstructionRegex().EnumerateMatches(input))
        {
            var range = match.ToRange();

            var value = input[range];
            if (EnableInstruction.Regex().IsMatch(value))
            {
                instructions.Add(new EnableInstruction());
                continue;
            }

            if (DisableInstruction.Regex().IsMatch(value))
            {
                instructions.Add(new DisableInstruction());
                continue;
            }

            if (MultiplicationInstruction.Regex().IsMatch(value))
            {
                var numbers = input[range].GetIntegers();
                var x = numbers.ElementAt(0);
                var y = numbers.ElementAt(1);
                instructions.Add(new MultiplicationInstruction(x, y));
                continue;
            }
        }
        return instructions;
    }

    [GeneratedRegex(EnableInstruction.RegexStr + "|" + DisableInstruction.RegexStr + "|" + MultiplicationInstruction.RegexStr)]
    private static partial Regex InstructionRegex();

    private interface IInstruction { }
    private readonly partial record struct EnableInstruction() : IInstruction
    {
        [GeneratedRegex(RegexStr)]
        public static partial Regex Regex();
        public const string RegexStr = "do\\(\\)";
    }
    private readonly partial record struct DisableInstruction() : IInstruction
    {
        [GeneratedRegex(RegexStr)]
        public static partial Regex Regex();
        public const string RegexStr = "don't\\(\\)";
    }
    private readonly partial record struct MultiplicationInstruction(int X, int Y) : IInstruction
    {
        [GeneratedRegex(RegexStr)]
        public static partial Regex Regex();
        public const string RegexStr = "mul\\(\\d{1,3},\\d{1,3}\\)";
        public readonly int Result { get; } = X * Y;
    }
}
