using Models;

namespace Utilities;

public class HandComparer2 : IComparer<Hand>
{
    public int Compare(Hand x, Hand y)
    {
        var xHandType = x.GetHandType();
        var yHandType = y.GetHandType();

        var handTypeComparison = xHandType.CompareTo(yHandType);
        if (handTypeComparison != 0)
        {
            return handTypeComparison;
        }

        var sameHandComparison = CompareSameHandType(x, y, xHandType);
        if (sameHandComparison != 0)
        {
            return sameHandComparison;
        }

        var highCardResult = CompareHighestCardFromStart(x, y);
        if (highCardResult == 0)
            throw new NotSupportedException();
        return highCardResult;
    }

    private static int CompareSameHandType(Hand x, Hand y, HandType handType)
    {
        if (handType == HandType.HighCard)
        {
            var highCardResult = x.Cards.OrderDescending().First().CompareTo(y.Cards.OrderDescending().First());
            if (highCardResult != 0)
                return highCardResult;
        }
        return 0;
    }

    private static int CompareHighestCardFromStart(Hand x, Hand y)
    {
        var xCards = x.Cards.ToArray();
        var yCards = y.Cards.ToArray();

        for (int i = 0; i < xCards.Length; i++)
        {
            var xCard = xCards[i];
            var yCard = yCards[i];
            var cardComparison = xCard.CompareTo(yCard);
            if (cardComparison != 0)
                return cardComparison;
        }

        return 0;

    }


}
