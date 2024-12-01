namespace AdventOfCode.UI.Shared.Models;

public record RunSolutionRequest(string Input);
public record SolutionResult(
    object Result,
    TimeSpan RunTime,
    long UsedHeapSize,
    long TotalHeapSize,
    long HeapSizeLimit);
