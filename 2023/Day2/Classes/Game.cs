

namespace Day2.Classes;

public class Game(int id, IEnumerable<IDictionary<CubeColor, int>> draws)
{
    public int CalculatePoints(IDictionary<CubeColor, int> constraints) => IsPossible(constraints) ? id : 0;
    private bool IsPossible(IDictionary<CubeColor, int> constraints)
    {
        foreach (var draw in draws)
        {
            foreach (var kv in draw)
            {
                constraints.TryGetValue(kv.Key, out var maxCubes);
                if (maxCubes < kv.Value)
                    return false;
            }
        }
        return true;
    }

    public int CalculatePower()
    {
        var minimum = GetMinimumPossible();
        var power = 1;
        foreach (var num in minimum.Values)
        {
            power *= num;
        }
        return power;
    }

    private Dictionary<CubeColor, int> GetMinimumPossible()
    {
        return draws
            .SelectMany(d => d)
            .GroupBy(d => d.Key, (color, draws) => (
                color,
                draws.Select(d => d.Value).Max()
            ))
            .ToDictionary();
    }
}
