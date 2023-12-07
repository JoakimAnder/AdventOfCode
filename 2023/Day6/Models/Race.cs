namespace Models;

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
