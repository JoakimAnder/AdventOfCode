using AdventOfCode.Solutions.Helpers;

namespace AdventOfCode.Solutions.Puzzles.Year2023.Day06;

public class OneStarSolution : ISolution
{
    ValueTask<object> ISolution.Solve(string input, CancellationToken ct)
    {
        var product = 1L;
        var races = Parse(input);
        foreach (var race in races)
        {
            var (min, max) = race.FindWinningChargeTimes();
            var winCount = max - min + 1;
            if (winCount > 0)
                product *= winCount;
        }

        return ValueTask.FromResult<object>(product);
    }


    public static IEnumerable<Race> Parse(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var timeLine = lines[0];
        var distanceLine = lines[1];

        var numberRegex = Regexes.Number();

        var times = numberRegex
            .Matches(timeLine)
            .Select((m, i) => (id: i, time: long.Parse(m.ValueSpan)));

        var distances = numberRegex
            .Matches(distanceLine)
            .Select((m, i) => (id: i, distance: long.Parse(m.ValueSpan)));

        var races = times
            .Join(distances, t => t.id, d => d.id, (t, d) => (t.time, d.distance, t.id))
            .Select(td => new Race(td.id, td.time, td.distance));

        return races;
    }


    public readonly record struct Race(int Id, long Time, long RecordDistance)
    {
        public (long min, long max) FindWinningChargeTimes()
        {
            var wins = FindWinningChargeTimesBetween(1, Time - 1);
            return wins ?? (0, 0);
        }
        private (long min, long max)? FindWinningChargeTimesBetween(long min, long max)
        {
            if (max < min)
                return null;

            long? winningMin = null;
            long? winningMax = null;

            if (IsWinner(min))
                winningMin = min;

            if (IsWinner(max))
                winningMax = max;

            if (winningMin.HasValue && winningMax.HasValue)
                return (winningMin.Value, winningMax.Value);

            var half = ((max - min) / 2) + min;
            if (IsWinner(half))
            {
                if (!winningMin.HasValue)
                    winningMin = FindWinningChargeTimesBetween(min, half - 1)?.min ?? half;
                if (!winningMax.HasValue)
                    winningMax = FindWinningChargeTimesBetween(half + 1, max)?.max ?? half;

                return (winningMin.Value, winningMax.Value);
            }

            return FindWinningChargeTimesBetween(min, half - 1) ?? FindWinningChargeTimesBetween(half + 1, max);

        }

        private bool IsWinner(long chargeTime)
        {
            var speed = chargeTime;
            var runTime = Time - chargeTime;
            var distance = runTime * speed;
            return distance > RecordDistance;
        }
    }

}
