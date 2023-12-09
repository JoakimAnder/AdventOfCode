namespace Models;

public class Node(string id)
{
    private readonly Dictionary<Direction, Node> _elements = [];
    public string Id => id;
    public IReadOnlyDictionary<Direction, Node> Elements => _elements;


    public void AddDirection(Direction direction, Node node) => _elements[direction] = node;
    public Node Navigate(Direction direction) => Elements.TryGetValue(direction, out var result) ? result : this;

    public long[] FindHitSteps(Direction[] directions, char endChar, int maxSteps)
    {
        var endSteps = new List<long>();
        var currentNode = this;
        for (int i = 0; i < maxSteps; i++)
        {
            currentNode = currentNode.Navigate(directions[i % directions.Length]);
            if (currentNode.Id.EndsWith(endChar))
                endSteps.Add(i + 1);
        }
        return endSteps.ToArray();
    }
    public int FindLoopOffset(Direction[] directions)
    {
        var currentNode = this;
        var steps = 0;

        var hitNodes = new Dictionary<(string nodeId, int directionIndex), int>();
        int directionIndex;
        while (true)
        {
            directionIndex = steps % directions.Length;
            if (hitNodes.ContainsKey((nodeId: currentNode.Id, directionIndex)))
                break;
            hitNodes.Add((currentNode.Id, directionIndex), steps++);
            var direction = directions[directionIndex];
            var nextNode = currentNode.Navigate(direction);

            currentNode = nextNode;
        }

        //var stepsToLoop = hitNodes[(nodeId: currentNode.Id, directionIndex)];

        return steps;
    }

    public NodeLoop FindLoop(Direction[] directions)
    {
        var currentNode = this;
        var steps = 0;

        var hitNodes = new Dictionary<(string nodeId, int directionIndex), int>();
        int directionIndex;
        while (true)
        {
            directionIndex = steps % directions.Length;
            if (hitNodes.ContainsKey((nodeId: currentNode.Id, directionIndex)))
                break;
            hitNodes.Add((currentNode.Id, directionIndex), steps++);
            var direction = directions[directionIndex];
            var nextNode = currentNode.Navigate(direction);
            currentNode = nextNode;
        }

        var stepsToLoop = hitNodes[(nodeId: currentNode.Id, directionIndex)];
        var length = steps - stepsToLoop;
        var loopDirections = directions[directionIndex..].Concat(directions[..directionIndex]).ToArray();
        return new NodeLoop(stepsToLoop, currentNode, length, loopDirections);
    }
}
