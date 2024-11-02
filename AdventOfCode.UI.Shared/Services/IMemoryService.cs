namespace AdventOfCode.UI.Shared.Services;

public interface IMemoryService
{
    ValueTask<MemoryUsageResult> GetMemoryUsage();
    ValueTask ClearMemory();
}

public record struct MemoryUsageResult(long UsedHeapSize, long TotalHeapSize, long HeapSizeLimit);
