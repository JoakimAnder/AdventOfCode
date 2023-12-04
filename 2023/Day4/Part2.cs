
using Day4.Classes;
using Infra.Helpers;

namespace Day4;

public static class Part2
{
    private const int OriginalCardCount = 1;
    public static void Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(Environment.CurrentDirectory));

        var cardCopies = new Dictionary<int, int>();
        var totalCardCount = 0;
        foreach (var line in input.LinesAsEnumerable())
        {
            var card = InputParser.ParseCard(line);
            var copiesCount = cardCopies.TryGetValue(card.Id, out var c) ? c : 0;
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
    }

    private static int CalculateCardCount(int index, ScratchCard[] scratchCards)
    {
        if (index >= scratchCards.Length)
            return 0; // Or 1, it's unclear in the given example.

        var currentCard = scratchCards[index];
        var wonCardsCount = currentCard.CalculateMatchingNumbersCount();

        var totalCopyCount = 1;
        for (int i = 0; i < wonCardsCount; i++)
        {
            var copies = CalculateCardCount(index + i + 1, scratchCards);
            totalCopyCount += copies;
        }

        return totalCopyCount;
    }

}
