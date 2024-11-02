namespace AdventOfCode.Solutions.Puzzles.Year2023.Day15;

public class OneStarSolution : ISolution
{
    ValueTask<object> ISolution.Solve(string input, CancellationToken ct)
    {
        var strings = input
            .Split(Environment.NewLine)
            .SelectMany(l => l.Split(',', StringSplitOptions.TrimEntries));

        var sum = strings.Select(s => HolidayAsciiStringHelperAlgorithm.Hash(s))
            .Sum();

        return ValueTask.FromResult<object>(sum);
    }

    public static class HolidayAsciiStringHelperAlgorithm
    {
        public static int Hash(ReadOnlySpan<char> input)
        {
            var currentValue = 0;
            foreach (var c in input)
            {
                //Determine the ASCII code for the current character of the string.
                var asciiCode = (int)c;

                //Increase the current value by the ASCII code you just determined.
                currentValue += asciiCode;

                //Set the current value to itself multiplied by 17.
                currentValue *= 17;

                //Set the current value to the remainder of dividing itself by 256.
                currentValue %= 256;
            }

            return currentValue;
        }
    }

}
