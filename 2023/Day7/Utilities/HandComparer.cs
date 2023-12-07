using Models;

namespace Utilities;

public class HandComparer : IComparer<Hand>
{
    public int Compare(Hand x, Hand y)
    {
        var xHandType = x.GetHandType();
        var yHandType = y.GetHandType();

        var handTypeComparison = xHandType.CompareTo(yHandType);
        if (handTypeComparison != 0)
        {
            //Console.WriteLine("Comparison between [{0}] and [{1}] resulted in {2}",
            //    string.Join(" , ", x.CardsAsDict.Select(cardCount => $"({(int)cardCount.Key}:{cardCount.Value})")),
            //    string.Join(" , ", y.CardsAsDict.Select(cardCount => $"({(int)cardCount.Key}:{cardCount.Value})")),
            //    handTypeComparison);
            return handTypeComparison;
        }

        var xAsDict = x.CardsAsDict;
        var yAsDict = y.CardsAsDict;

        var sameHandResult = CompareSameHand(xAsDict, yAsDict, xHandType);

        if (sameHandResult != 0)
        {
            //Console.WriteLine("Comparison between {0} and {1} resulted in {2}",
            //        string.Join(" , ", xAsDict.OrderByDescending(h => h.Value).ThenByDescending(h => h.Key).Select(cardCount => $"({(int)cardCount.Key}:{cardCount.Value})")),
            //        string.Join(" , ", yAsDict.OrderByDescending(h => h.Value).ThenByDescending(h => h.Key).Select(cardCount => $"({(int)cardCount.Key}:{cardCount.Value})")),
            //        sameHandResult);
            return sameHandResult;
        }

        var xArr = x.Cards.ToArray();
        var yArr = y.Cards.ToArray();
        for (var i = 0; i < xArr.Length; i++)
        {
            var cardResult = xArr[i].CompareTo(yArr[i]);
            if (cardResult != 0)
                return cardResult;
        }

        var result = CompareHighestKind(xAsDict, yAsDict, 1);
        if (result == 0)
        {
            Console.WriteLine("Comparison between {0} and {1} resulted in {2}",
                        string.Join(" , ", xAsDict.OrderByDescending(h => h.Value).ThenByDescending(h => h.Key).Select(cardCount => $"({(int)cardCount.Key}:{cardCount.Value})")),
                        string.Join(" , ", yAsDict.OrderByDescending(h => h.Value).ThenByDescending(h => h.Key).Select(cardCount => $"({(int)cardCount.Key}:{cardCount.Value})")),
                        result);
        }
        return result;
    }

    private static int CompareSameHand(IDictionary<Card, int> x, IDictionary<Card, int> y, HandType handType)
    {
        return handType switch
        {
            HandType.HighCard => CompareHighestKind(x, y, 1),
            HandType.OnePair => CompareHighestKind(x, y, 2),
            HandType.TwoPair => CompareHighestKind(x, y, 2),
            HandType.ThreeOfAKind => CompareHighestKind(x, y, 3),
            HandType.FullHouse => CompareFullHouse(x, y),
            HandType.FourOfAKind => CompareHighestKind(x, y, 4),
            HandType.FiveOfAKind => CompareHighestKind(x, y, 5),
            _ => throw new NotImplementedException(),
        };
    }

    private static int CompareHighestKind(IDictionary<Card, int> x, IDictionary<Card, int> y, int kindCount)
    {
        var xCards = x.Where(cardCount => cardCount.Value == kindCount)
            .Select(cardCount => cardCount.Key)
            .OrderDescending()
            .ToArray();

        var yCards = y.Where(cardCount => cardCount.Value == kindCount)
            .Select(cardCount => cardCount.Key)
            .OrderDescending()
            .ToArray();

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

    private static int CompareFullHouse(IDictionary<Card, int> x, IDictionary<Card, int> y)
    {
        var tripletComparison = CompareHighestKind(x, y, 3);
        if (tripletComparison != 0)
            return tripletComparison;
        return CompareHighestKind(x, y, 2);
    }


}
