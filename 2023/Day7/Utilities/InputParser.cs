using Infra.Helpers;
using Models;

namespace Puzzle;

public static class InputParser
{
    private static readonly Dictionary<char, Card> CardLookup = new()
    {
        { '2', Card.Two },
        { '3', Card.Three },
        { '4', Card.Four },
        { '5', Card.Five },
        { '6', Card.Six },
        { '7', Card.Seven },
        { '8', Card.Eight },
        { '9', Card.Nine },
        { 'T', Card.Ten },
        { 'J', Card.Jack },
        { 'Q', Card.Queen },
        { 'K', Card.King },
        { 'A', Card.Ace },
    };

    public static Hand Parse(string line)
    {
        // 32T3K 765
        var splitInput = line.Split(' ');

        var cards = splitInput[0].Select(ParseCard).ToArray();
        var bid = Helper.ParseInt(splitInput[1]);

        return new Hand(cards, bid);
    }

    private static Card ParseCard(char c)
    {
        return CardLookup[c];
    }
}
