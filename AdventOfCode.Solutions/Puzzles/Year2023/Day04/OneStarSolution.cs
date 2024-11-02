using AdventOfCode.Solutions.Helpers;

namespace AdventOfCode.Solutions.Puzzles.Year2023.Day04;

public class OneStarSolution : ISolution
{
    ValueTask<object> ISolution.Solve(string input, CancellationToken ct)
    {
        var sum = 0;

        foreach (var line in input.AsSpan().EnumerateLines())
        {
            var card = ParseCard(line.ToString());
            var points = card.CalculatePoints();
            sum += points;
        }

        return ValueTask.FromResult<object>(sum);
    }

    public static ScratchCard ParseCard(string line)
    {
        //Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        var r = Regexes.Number();
        var id = int.Parse(r.Match(line.ToString()).ValueSpan);

        var numbersInput = line.Split(':')[1];
        var winningNumbersInput = numbersInput.Split("|")[0];
        var scratchedNumbersInput = numbersInput.Split("|")[1];

        var winningNumbers = r.Matches(winningNumbersInput)
            .Select(m => int.Parse(m.ValueSpan))
            .ToArray();
        var scratchedNumbers = r.Matches(scratchedNumbersInput)
            .Select(m => int.Parse(m.ValueSpan))
            .ToArray();

        return new ScratchCard(id, winningNumbers, scratchedNumbers);
    }

    public readonly struct ScratchCard(
        int id,
        IEnumerable<int> winningNumbers,
        IEnumerable<int> scratchedNumbers)
    {
        public readonly int Id { get; } = id;

        public readonly int CalculatePoints()
            => (int)Math.Pow(2, CalculateMatchingNumbersCount() - 1);

        public readonly int CalculateMatchingNumbersCount()
            => winningNumbers
                .Intersect(scratchedNumbers)
                .Count();
    }

}
