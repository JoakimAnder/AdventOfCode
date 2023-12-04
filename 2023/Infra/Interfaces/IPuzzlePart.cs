namespace Infra.Interfaces;

public interface IPuzzlePart
{
    object? ExpectedResult { get; }
    object Run();
}