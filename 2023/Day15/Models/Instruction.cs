namespace Models;

public readonly record struct Instruction(int BoxId, Operation Operation, Lens Lens)
{
}
