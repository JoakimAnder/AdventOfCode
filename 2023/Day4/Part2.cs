
using Day4.Classes;
using Infra.Helpers;
using Infra.Interfaces;

namespace Day4;

public class Part2 : IPuzzlePart
{
    public object? ExpectedResult => null;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(Environment.CurrentDirectory));

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
        return sum;
    }

    private const int OriginalCardCount = 1;
}
