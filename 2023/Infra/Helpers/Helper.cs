
using Infra.Interfaces;
using Infra.Interfaces.Implementations;
using System.Globalization;

namespace Infra.Helpers;

public static class Helper
{
    public static IInputReader GetInputReader(string filePath) => new TxtInputReader(filePath);
    public static string GetInputPath(string partPath) =>
        $"{partPath}{Path.DirectorySeparatorChar}Inputs{Path.DirectorySeparatorChar}Input.txt";
    public static IRunner GetRunner(params IPuzzlePart[] parts) => new Runner(parts);
    public static int ParseInt(ReadOnlySpan<char> input) => int.Parse(input, NumberFormatInfo.InvariantInfo);
}
