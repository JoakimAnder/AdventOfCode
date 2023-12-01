
using Shared.Interfaces;
using Shared.Interfaces.Implementations;

namespace Shared.Helpers;

public static class Helper
{
    public static IInputReader GetInputReader(string filePath) => new TxtInputReader(filePath);
    public static string GetInputPath(string partPath, int part = 0)
    {
        if (part == 0)
            return $"{partPath}{Path.DirectorySeparatorChar}Inputs{Path.DirectorySeparatorChar}Input.txt";
        return $"{partPath}{Path.DirectorySeparatorChar}Inputs{Path.DirectorySeparatorChar}SmallInput{part}.txt";
    } 
}
