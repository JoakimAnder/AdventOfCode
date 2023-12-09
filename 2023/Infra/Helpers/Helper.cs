
using Infra.Interfaces;
using Infra.Interfaces.Implementations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Infra.Helpers;

public static partial class Helper
{
    [GeneratedRegex(@"\d+")]
    public static partial Regex NumberRegex();
    [GeneratedRegex(@"\w+")]
    public static partial Regex WordRegex();
    public static IInputReader GetInputReader(string filePath) => new TxtInputReader(filePath);
    public static string GetInputPath(string className) =>
        $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{className}{Path.DirectorySeparatorChar}Inputs{Path.DirectorySeparatorChar}Input.txt";
    public static IRunner GetRunner(params IPuzzlePart[] parts) => new Runner(parts);
    public static int ParseInt(ReadOnlySpan<char> input) => int.Parse(input, NumberFormatInfo.InvariantInfo);
    public static long ParseLong(ReadOnlySpan<char> input) => long.Parse(input, NumberFormatInfo.InvariantInfo);
}
