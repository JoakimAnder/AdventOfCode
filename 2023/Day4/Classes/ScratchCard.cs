namespace Day4.Classes;

public readonly record struct ScratchCard(
    int Id,
    IEnumerable<int> WinningNumbers,
    IEnumerable<int> ScratchedNumbers
    )
{
    public readonly int CalculatePoints() => (int)Math.Pow(2, CalculateMatchingNumbersCount() - 1);
    public readonly int CalculateMatchingNumbersCount() => WinningNumbers
            .Intersect(ScratchedNumbers)
            .Count();

}
