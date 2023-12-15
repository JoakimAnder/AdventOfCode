using Infra.Helpers;
using Infra.Interfaces;
using Models;
using System.Text.RegularExpressions;

namespace Puzzle;

public static partial class InputParser
{
    public static string[] Parse(IInputReader reader)
    {
        return reader.ParseLines(l => l.Split(','))
            .SelectMany(l => l)
            .Select(l => l.Trim())
            .ToArray();
    }

    public static Instruction[] ParseInstructions(IInputReader reader)
    {
        return reader.ParseLines(l => l.Split(','))
            .SelectMany(l => l)
            .Select(l => l.Trim())
            .Select(ParseInstruction)
            .ToArray();
    }


    [GeneratedRegex(@"[-=]")]
    public static partial Regex OperationRegex();
    private static Instruction ParseInstruction(string s)
    {
        var operationMatch = OperationRegex().Match(s);
        var label = s[..operationMatch.Index];
        var boxId = HolidayAsciiStringHelperAlgorithm.Hash(label);
        var operation = operationMatch.Value switch
        {
            "-" => Operation.Remove,
            "=" => Operation.Place,
            _ => throw new NotImplementedException($"{operationMatch.Value} is not a supported operation")
        };

        var focalLengthStr = s[(operationMatch.Index + 1)..];
        var focalLength = string.IsNullOrEmpty(focalLengthStr) ? 0 : Helper.ParseInt(focalLengthStr);
        var lens = new Lens(label, focalLength);
        var instruction = new Instruction(boxId, operation, lens);
        return instruction;
    }
}
