namespace Models;

public class HashMap
{
    private readonly Dictionary<int, IEnumerable<Lens>> _boxes = new();

    public IReadOnlyDictionary<int, IEnumerable<Lens>> Boxes => _boxes;
    public void Execute(Instruction instruction)
    {
        var box = _boxes.GetValueOrDefault(instruction.BoxId, Enumerable.Empty<Lens>());
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
