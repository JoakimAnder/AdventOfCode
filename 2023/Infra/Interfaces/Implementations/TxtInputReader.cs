

namespace Infra.Interfaces.Implementations;

public class TxtInputReader(string filePath) : IInputReader
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

    public async Task<IEnumerable<T>> ParseLines<T>(Func<string, T> parser)
    {
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var file = new StreamReader(fileStream, System.Text.Encoding.UTF8, true, 128);

        var result = Enumerable.Empty<T>();
        string? line;
        while ((line = await file.ReadLineAsync()) is not null)
        {
            var parsedValue = parser(line);
            result = result.Prepend(parsedValue);
        }
        return result;
    }

}
