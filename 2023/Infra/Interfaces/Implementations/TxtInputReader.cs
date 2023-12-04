

namespace Infra.Interfaces.Implementations;

internal sealed class TxtInputReader(string filePath) : IInputReader
{
    public IEnumerable<string> LinesAsEnumerable()
    {
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var file = new StreamReader(fileStream, System.Text.Encoding.UTF8, true, 128);

        string? line;
        while ((line = file.ReadLine()) is not null)
        {
            yield return line;
        }
    }

    public IEnumerable<T> ParseLines<T>(Func<string, T> parser) => ParseLines((line, _) => parser(line));
    public IEnumerable<T> ParseLines<T>(Func<string, int, T> parser)
    {
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var file = new StreamReader(fileStream, System.Text.Encoding.UTF8, true, 128);

        var result = Enumerable.Empty<T>();
        string? line;
        var index = 0;
        while ((line = file.ReadLine()) is not null)
        {
            var parsedValue = parser(line, index++);
            result = result.Append(parsedValue);
        }
        return result;
    }
}
