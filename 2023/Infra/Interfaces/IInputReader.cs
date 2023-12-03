
namespace Infra.Interfaces;

public interface IInputReader
{
    IEnumerable<T> ParseLines<T>(Func<string, T> parser);
    IEnumerable<T> ParseLines<T>(Func<string, int, T> parser);
    IEnumerable<string> LinesAsEnumerable();
}
