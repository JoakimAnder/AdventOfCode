using Infra.Helpers;
using Infra.Interfaces;

namespace Puzzle;

public class Part2 : IPuzzlePart
{
    private const int OriginalCardCount = 1;

    public object? ExpectedResult => 10425665;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(nameof(Part2)));

        var cardCopies = new Dictionary<int, int>();
        var totalCardCount = 0;
        foreach (var line in input.LinesAsEnumerable())
        {
            var card = InputParser.ParseCard(line);
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

        Console.WriteLine("You end up with {0} total scratchcards.", totalCardCount);
        return totalCardCount;
    }

}
