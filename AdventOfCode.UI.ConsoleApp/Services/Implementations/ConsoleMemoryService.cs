using AdventOfCode.Shared.Attributes;
using AdventOfCode.UI.Shared.Services;
using System.Diagnostics;

namespace AdventOfCode.UI.ConsoleApp.Services.Implementations;

[Transient(typeof(IMemoryService))]
public class ConsoleMemoryService : IMemoryService
{
    public ValueTask ClearMemory()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        return ValueTask.CompletedTask;
    }

    public ValueTask<MemoryUsageResult> GetMemoryUsage()
    {
        var process = Process.GetCurrentProcess();
        var used = process.VirtualMemorySize64;
        var total = process.PeakVirtualMemorySize64;
        var limit = process.PeakWorkingSet64;
        return ValueTask.FromResult(new MemoryUsageResult(used, total, limit));
    }
}
