
using Infra.Interfaces;
using Infra.Interfaces.Implementations;
using System.Globalization;

namespace Infra.Helpers;

public static class Helper
{
    public static IInputReader GetInputReader(string filePath) => new TxtInputReader(filePath);
    public static string GetInputPath(string partPath, int part = 0)
    {
        if (part == 0)
            return $"{partPath}{Path.DirectorySeparatorChar}Inputs{Path.DirectorySeparatorChar}Input.txt";
        return $"{partPath}{Path.DirectorySeparatorChar}Inputs{Path.DirectorySeparatorChar}SmallInput{part}.txt";
    }

    public static IRunner GetRunner(params Action[] parts) => new Runner(parts);
    public static int ParseInt(ReadOnlySpan<char> input) => int.Parse(input, NumberFormatInfo.InvariantInfo);
}
