using Utilities;

namespace Models;

public record struct Hand(IEnumerable<Card> Cards, int Bid)
{
    public readonly HandType GetHandType()
    {
        return this.CalculateHandType();
    }

    public readonly IDictionary<Card, int> CardsAsDict => Cards.GroupBy(c => c)
        .Select(g => (g.Key, count: g.Count()))
        .ToDictionary(t => t.Key, t => t.count);

}
