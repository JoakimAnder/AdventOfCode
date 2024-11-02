using static AdventOfCode.Solutions.Puzzles.Year2023.Day04.OneStarSolution;

namespace AdventOfCode.Solutions.Puzzles.Year2023.Day04;

public class TwoStarSolution : ISolution
{
    private const int OriginalCardCount = 1;

    ValueTask<object> ISolution.Solve(string input, CancellationToken ct)
    {
        var cardCopies = new Dictionary<int, int>();
        var totalCardCount = 0;
        foreach (var line in input.AsSpan().EnumerateLines())
        {
            var card = ParseCard(line.ToString());
            cardCopies.TryGetValue(card.Id, out var copiesCount);
            copiesCount += OriginalCardCount;
            totalCardCount += copiesCount;

            var wonCardsCount = card.CalculateMatchingNumbersCount();
            for (var i = 1; i <= wonCardsCount; i++)
            {
                var wonId = card.Id + i;
                if (!cardCopies.TryAdd(wonId, copiesCount))
                    cardCopies[wonId] += copiesCount;
            }

            cardCopies.Remove(card.Id);
        }

        return ValueTask.FromResult<object>(totalCardCount);
    }
}
