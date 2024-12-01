using System.Text.RegularExpressions;
using static AdventOfCode.Solutions.Puzzles.Year2023.Day15.OneStarSolution;

namespace AdventOfCode.Solutions.Puzzles.Year2023.Day15;

public partial class TwoStarSolution : ISolution
{
    ValueTask<object> ISolution.Solve(string input, CancellationToken ct)
    {

        var instructions = input
            .Split(Environment.NewLine)
            .SelectMany(l => l.Split(',', StringSplitOptions.TrimEntries))
            .Select(ParseInstruction);

        var hashMap = new HashMap();
        foreach (var instruction in instructions)
        {
            hashMap.Execute(instruction);
        }

        var sum = CalculateTotalFocusingPower(hashMap.Boxes);
        return ValueTask.FromResult<object>(sum);
    }

    private static int CalculateTotalFocusingPower(IReadOnlyDictionary<int, IEnumerable<Lens>> boxes)
        => boxes.Sum(kv => CalculateTotalFocusingPower(kv.Key, kv.Value));

    private static int CalculateTotalFocusingPower(int box, IEnumerable<Lens> lenses)
    {
        var boxPoints = box + 1;
        var sum = lenses
            .Select((l, i) => boxPoints * (i + 1) * l.FocalLength)
            .Sum();
        return sum;
    }

    public readonly record struct Instruction(int BoxId, Operation Operation, Lens Lens);
    public readonly record struct Lens(string Label, int FocalLength);
    public enum Operation
    {
        Remove,
        Place
    }

    [GeneratedRegex(@"[-=]")]
    public static partial Regex OperationRegex();
    private static Instruction ParseInstruction(string s)
    {
        var operationMatch = OperationRegex().Match(s);
        var label = s[..operationMatch.Index];
        var boxId = HolidayAsciiStringHelperAlgorithm.Hash(label);
        var operation = operationMatch.Value switch
        {
            "-" => Operation.Remove,
            "=" => Operation.Place,
            _ => throw new NotImplementedException($"{operationMatch.Value} is not a supported operation")
        };

        var focalLengthStr = s[(operationMatch.Index + 1)..];
        var focalLength = string.IsNullOrEmpty(focalLengthStr) ? 0 : int.Parse(focalLengthStr);
        var lens = new Lens(label, focalLength);
        var instruction = new Instruction(boxId, operation, lens);
        return instruction;
    }

    public class HashMap
    {
        private readonly Dictionary<int, IEnumerable<Lens>> _boxes = [];

        public IReadOnlyDictionary<int, IEnumerable<Lens>> Boxes => _boxes;
        public void Execute(Instruction instruction)
        {
            var box = _boxes.GetValueOrDefault(instruction.BoxId, []);
            var newBox = instruction.Operation switch
            {
                Operation.Remove => Remove(box, instruction.Lens),
                Operation.Place => Place(box, instruction.Lens),
                _ => throw new NotImplementedException($"{instruction.Operation} is not a supported operation")
            };

            _boxes[instruction.BoxId] = newBox;
        }

        private static IEnumerable<Lens> Place(IEnumerable<Lens> box, Lens lens)
        {
            var b = box.ToList();
            if (!b.Exists(l => l.Label == lens.Label))
                return box.Append(lens);

            var existingLensIndex = b.FindIndex(l => l.Label == lens.Label);
            b[existingLensIndex] = lens;
            return b;
        }

        private static IEnumerable<Lens> Remove(IEnumerable<Lens> box, Lens lens)
        {
            return box.Where(l => l.Label != lens.Label);
        }
    }
}
