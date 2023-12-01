
namespace Shared.Interfaces;

public interface IInputReader
{
    Task<IEnumerable<T>> ParseLines<T>(Func<string, T> parser);
    IEnumerable<string> LinesAsEnumerable();
}
