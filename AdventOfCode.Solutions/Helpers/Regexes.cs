using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Helpers;
public static partial class Regexes
{

    [GeneratedRegex(@"\d+")]
    public static partial Regex Number();

    [GeneratedRegex(@"\w+")]
    public static partial Regex Word();
}
