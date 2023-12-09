using Infra.Helpers;
using Models;

namespace Puzzle;

public static class InputParser
{
    private struct MutableNode(string id, Dictionary<Direction, string> strElements)
    {
        public readonly string Id => id;
        public readonly Dictionary<Direction, string> StrElements => strElements;

    }
    public static (Direction[] directions, Node[] network) Parse(IEnumerable<string> lines)
    {
        Direction[]? directions = null;
        var mutableNetwork = new List<MutableNode>();
        foreach (var line in lines)
        {
            if (directions is null)
            {
                directions = ParseDirections(line);
                continue;
            }

            if (string.IsNullOrEmpty(line))
                continue;

            var node = ParseNode(line);
            mutableNetwork.Add(node);
        }

        var network = GatherNetwork(mutableNetwork);

        return (directions!, network);
    }

    private static Node[] GatherNetwork(List<MutableNode> mutableNetwork)
    {
        var network = mutableNetwork.Select(n => new Node(n.Id))
            .ToArray();

        foreach (var node in network)
        {
            var mutable = mutableNetwork.First(n => n.Id == node.Id);
            foreach (var (direction, elementId) in mutable.StrElements)
            {
                var element = network.First(e => e.Id == elementId);
                node.AddDirection(direction, element);
            }

            mutableNetwork.Remove(mutable);
        }


        return network;
    }

    private static Direction[] ParseDirections(string line)
    {
        return line.Select(c => c switch
        {
            'L' => Direction.Left,
            'R' => Direction.Right,
            _ => throw new NotImplementedException($"{c} as a direction is not supported"),
        }).ToArray();
    }

    private static MutableNode ParseNode(string line)
    {
        //AAA = (BBB, CCC)
        var regex = Helper.WordRegex();
        var words = regex.Matches(line)
            .Select(m => m.Value)
            .ToArray();

        var id = words[0];
        var left = words[1];
        var right = words[2];

        var elements = new Dictionary<Direction, string>();
        if (left != id)
            elements.Add(Direction.Left, left);
        if (right != id)
            elements.Add(Direction.Right, right);

        return new MutableNode(id, elements);
    }

}
