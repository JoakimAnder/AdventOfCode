namespace Models;

public readonly record struct History(IEnumerable<int> Readings)
{
    public readonly int ExtrapolatedValue()
    {
        var values = new List<int[]> { Readings.ToArray() };

        while (!Array.TrueForAll(values[0], v => v == 0))
        {
            var current = values[0];
            var differences = new List<int>();
            for (var i = 0; i < current.Length - 1; i++)
            {
                var diff = current[i + 1] - current[i];
                differences.Add(diff);
            }
            values.Insert(0, [.. differences]);
        }


        //var extrapolatedValue = values.Where(v => v.Length > 0).Sum(v => v[^1]);
        var extrapolatedValue = 0;

        for (int i = 1; i < values.Count; i++)
        {
            var current = values[i];
            extrapolatedValue += current[^1];

        }

        return extrapolatedValue;
    }
}
