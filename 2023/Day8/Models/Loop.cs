namespace Models;

public readonly record struct NodeLoop(int Offset, Node Start, int Length, IEnumerable<Direction> Directions)
{
    public long[] FindHitSteps(char endChar, int maxSteps) => Start.FindHitSteps(Directions.ToArray(), endChar, maxSteps);

    public readonly NodeLoop Shift(int steps)
    {
        var currentNode = Start;
        var ds = Directions.ToArray();

        for (int i = 0; i < steps; i++)
        {
            var directionIndex = i % Directions.Count();
            var direction = ds[directionIndex];
            currentNode = currentNode.Navigate(direction);
        }

        var offset = Offset + steps;
        var di = steps % Directions.Count();
        var directions = ds[di..].Concat(ds[..di]).ToArray();
        return new NodeLoop(offset, currentNode, Length, directions);
    }

    public readonly IEnumerable<long> NextHit(char endChar, long max)
    {
        var endSteps = FindHitSteps(endChar, Length);
        var i = 0;
        while (i < max)
        {
            var result = endSteps[i++ % endSteps.Length] + i * Length;
            yield return result;
        }

    }
}
