using Infra.Helpers;
using Infra.Interfaces;
using System.Numerics;

namespace Puzzle;

public class Part2 : IPuzzlePart
{
    public object? ExpectedResult => null;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(GetType().Name));
        const char startChar = 'A';
        const char endChar = 'Z';
        var (directions, network) = InputParser.Parse(input.LinesAsEnumerable());
        var startNodes = network.Where(n => n.Id.EndsWith(startChar)).ToArray();

        //var maxLoopOffset = startNodes.Max(node => node.FindLoopOffset(directions));
        //var x = startNodes.SelectMany(n => n.FindHitSteps(directions, endChar, maxLoopOffset * startNodes.Length * 2)).ToArray();
        //var sharedHitSteps = x
        //    .GroupBy(steps => steps)
        //    .Where(g => g.Count() == startNodes.Length)
        //    .Select(g => g.Key)
        //    .ToArray();
        //
        //
        var loops = startNodes.Select(n => n.FindLoop(directions)).ToArray();
        var lastLoop = loops.MaxBy(l => l.Offset + l.Length);
        var maxLoopOffset2 = lastLoop.Offset;
        var maxLoopLength = loops.Max(l => l.Length);
        //var minLoopLength = loops.Min(l => l.Length);

        var shiftedLoops = loops.Where(l => l != lastLoop)
            .Select(l => l.Shift(maxLoopOffset2 - l.Offset))
            .Append(lastLoop)
            .ToArray();

        //var x1 = shiftedLoops.SelectMany(l => l.FindHitSteps(endChar, maxLoopLength * startNodes.Length)).ToArray();
        //var sharedHitSteps2 = x1
        //    .GroupBy(steps => steps)
        //    .Where(g => g.Count() == startNodes.Length)
        //    .Select(g => g.Key)
        //    .ToArray();

        //var maxLoopOffset = loops.Max(l => l.Offset);
        //var sharedHitSteps = startNodes.SelectMany(n => n.FindHitSteps(directions, endChar, maxLoopOffset + 1))
        //    .GroupBy(steps => steps)
        //    .Where(g => g.Count() == startNodes.Length)
        //    .Select(g => g.Key)
        //    .ToArray();
        //if (sharedHitSteps.Length == 0)
        //{
        //    var maxSteps = loops.Max(l => l.EndSteps.Count());
        //    for (var i = 0; i <= maxSteps; i++)
        //    {
        //        var x = loops.Select(l => l.EndSteps.Select(s => s * i + l.Offset)).ToArray();



        //        var a1 = x.SelectMany(s => s).ToArray();
        //        var b = a1.GroupBy(steps => steps).ToArray();
        //        var c = b.Where(g => g.Count() == startNodes.Length).ToArray();
        //        var d = c.Select(g => g.Key).ToArray();
        //        sharedHitSteps = d.ToArray();

        //        if (sharedHitSteps.Length != 0)
        //            break;
        //    }

        //}

        shiftedLoops = shiftedLoops.ToArray();
        /*
        Console.WriteLine(shiftedLoops.Select(l => l.Length).Aggregate((l1, l2) => l1 * l2));

        var loopMatches2 = shiftedLoops.SelectMany(l => l.FindHitSteps(endChar, maxLoopLength))
            .Select(l => l * 1L)
            .Aggregate((l1, l2) => l1 * l2);
        Console.WriteLine(loopMatches2);
        Console.WriteLine(long.MaxValue);
        Console.WriteLine(int.MaxValue);

        long step = 0;
        var enumerators = shiftedLoops.Select(l => l.NextHit(endChar, int.MaxValue).GetEnumerator()).ToArray();
        foreach (var e in enumerators)
        {
            e.MoveNext();
        }
        while (enumerators.Max(e => e.Current) != enumerators.Min(e => e.Current))
        {
            var enumerator = enumerators.MinBy(e => e.Current);
            enumerator!.MoveNext();
            step++;
        }

        */
        var steps = shiftedLoops.SelectMany(l => l.FindHitSteps(endChar, maxLoopLength)).ToArray();
        var step = CalcLCM(steps);

        foreach (var s in shiftedLoops)
        {
            var st = s.FindHitSteps(endChar, s.Length)[0] * decimal.Parse("1", System.Globalization.NumberStyles.Integer, null);
            Console.WriteLine(st);
        }

        step += maxLoopOffset2;

        Console.WriteLine("It takes {0} steps before you're only on nodes that end with Z", step);
        //Console.WriteLine(loopMatches);
        return step;
    }

    private static BigInteger CalcLCM(IEnumerable<long> nums)
    {
        return nums
            .Select(n => new BigInteger(n))
            .Aggregate(CalcLCM);
    }
    private static BigInteger CalcLCM(BigInteger num1, BigInteger num2)
    {
        var gcd = BigInteger.GreatestCommonDivisor(num1, num2);
        var lcm = (num1 * num2) / gcd;
        return lcm;
    }
}
