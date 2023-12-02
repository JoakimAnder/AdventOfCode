using System.Diagnostics;

namespace Infra.Interfaces.Implementations;

internal sealed class Runner(Action[] parts) : IRunner
{
    public const double MegaDivisor = 1_000_000.0;
    public static void WriteDivider() => Console.WriteLine("\n---------------------------------------------------------------------\n");

    public void Run()
    {
#pragma warning disable S1215 // "GC.Collect" should not be called
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
#pragma warning restore S1215 // "GC.Collect" should not be called
        var startMemory = Process.GetCurrentProcess().PeakWorkingSet64;

        foreach ((var part, var index) in parts.Select((p, i) => (part: p, index: i)))
        {
            var partNo = index + 1;
            Console.WriteLine("Part {0}\n", partNo);
            Diagnose(part);
            WriteDivider();
        }

        var peakMemory = (Process.GetCurrentProcess().PeakWorkingSet64 - startMemory) / MegaDivisor;
        Console.WriteLine("Peak memory {0:0.00}MB", peakMemory);
        Console.ReadKey();
    }

    private static void Diagnose(Action action)
    {
#pragma warning disable S1215 // "GC.Collect" should not be called
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
#pragma warning restore S1215 // "GC.Collect" should not be called
        var sw = new Stopwatch();

        var memoryBefore = Process.GetCurrentProcess().VirtualMemorySize64;
        sw.Start();

        action();

        sw.Stop();
        var memoryAfter = Process.GetCurrentProcess().VirtualMemorySize64;

        var memorydiff = (memoryAfter - memoryBefore) / MegaDivisor;
        Console.WriteLine("\nDuration: {0}ms", sw.ElapsedMilliseconds);
        Console.WriteLine("Memory: {0:0.00}MB", memorydiff);
    }
}
