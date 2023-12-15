using Infra.Helpers;
using Infra.Interfaces;
using Models;

namespace Puzzle;

public class Part2 : IPuzzlePart
{
    public object? ExpectedResult => null;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(GetType().Name));

        var instructions = InputParser.ParseInstructions(input);
        var hashMap = new HashMap();
        foreach (var instruction in instructions)
        {
            hashMap.Execute(instruction);
        }

        var sum = CalculateTotalFocusingPower(hashMap.Boxes);

        Console.WriteLine("The focusing power of the resulting lens configuration is {0}", sum);
        return sum;
    }

    private static int CalculateTotalFocusingPower(IReadOnlyDictionary<int, IEnumerable<Lens>> boxes)
    {
        return boxes.Select(kv => CalculateTotalFocusingPower(kv.Key, kv.Value))
            .Sum();
    }

    private static int CalculateTotalFocusingPower(int box, IEnumerable<Lens> lenses)
    {
        var boxPoints = box + 1;
        var sum = lenses.Select((l, i) => boxPoints * (i + 1) * l.FocalLength)
            .Sum();
        return sum;
    }
}
