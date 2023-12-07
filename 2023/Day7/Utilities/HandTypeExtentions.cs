using Models;

namespace Utilities;

public static class HandTypeExtentions
{
    public static HandType CalculateHandType(this Hand hand)
    {
        var cardsAsDict = hand.CardsAsDict;
        var highestKindCount = HighestKindCount(cardsAsDict);
        return highestKindCount switch
        {
            5 => HandType.FiveOfAKind,
            4 => HandType.FourOfAKind,
            3 => FindHighest3KindType(cardsAsDict),
            2 => FindHighest2KindType(cardsAsDict),
            1 => HandType.HighCard,
            _ => throw new NotSupportedException("Hand has no match")
        };
    }
    private static int HighestKindCount(IDictionary<Card, int> hand) => hand.Select(c => c.Value)
            .OrderDescending()
            .First();

    private static HandType FindHighest3KindType(IDictionary<Card, int> hand) => hand.Any(c => c.Value == 2) ? HandType.FullHouse : HandType.ThreeOfAKind;

    private static HandType FindHighest2KindType(IDictionary<Card, int> hand) => hand.Count(c => c.Value == 2) == 2 ? HandType.TwoPair : HandType.OnePair;
}
