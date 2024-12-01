using AdventOfCode.Shared.Attributes;
using AdventOfCode.Shared.Helpers;
using AdventOfCode.Shared.Solutions;
using System.Diagnostics;

namespace AdventOfCode.UI.Shared.Services.Implementations;

[Singleton(typeof(ISolutionRunnerService))]
public class SolutionRunnerService(IMemoryService memoryService) : ISolutionRunnerService
{
    private static readonly SemaphoreSlim _semaphore = new(1, 1);
    public async ValueTask<SolutionResult> Run(ISolution solution, string input, CancellationToken ct)
    {
        using var _ = await _semaphore.WaitDisposable();

        await memoryService.ClearMemory();

        var startMemory = await memoryService.GetMemoryUsage();

        var startTime = Stopwatch.GetTimestamp();
        var response = await solution.Solve(input, ct);
        var runTime = Stopwatch.GetElapsedTime(startTime);

        var endMemory = await memoryService.GetMemoryUsage();


        var usedHeapSizeDiff = endMemory.UsedHeapSize - startMemory.UsedHeapSize;
        var totalHeapSizeDiff = endMemory.TotalHeapSize - startMemory.TotalHeapSize;
        var heapSizeLimitDiff = endMemory.HeapSizeLimit - startMemory.HeapSizeLimit;

        var result = new SolutionResult(response, runTime, usedHeapSizeDiff, totalHeapSizeDiff, heapSizeLimitDiff);
        return result;
    }
}
