
using AdventOfCode.Shared.Attributes;
using AdventOfCode.Shared.Solutions;
using AdventOfCode.Solutions.Helpers;
using AdventOfCode.UI.Shared.Helpers;
using AdventOfCode.UI.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

const string BACK_COMMAND = "b";
const string EXIT_COMMAND = "e";

var services = new ServiceCollection()
    .AddSolutions()
    .AddAdventOfCodeServices()
    .AddAttributedDependencies(Assembly.GetExecutingAssembly())
    .BuildServiceProvider();

var solutionRunner = services.GetRequiredService<ISolutionRunnerService>();

Console.WriteLine("""
    Hello!

    These are my solutions for Advent of Code.

    Enter 'b' at any time to go back one step, 'e' to exit the program.
    If a solution is running and you want to cancel it, either close the console or press CTRL + C.
    
    
    """);


while (true)
{
    Console.WriteLine("Which year are you interested in?");
    var year = Console.ReadLine();

    if (year == BACK_COMMAND || year == EXIT_COMMAND)
        break;

    if (string.IsNullOrEmpty(year))
        continue;

    while (true)
    {
        Console.WriteLine("Which day are you interested in?");
        var day = Console.ReadLine();

        if (day == EXIT_COMMAND)
            goto EXIT;

        if (day == BACK_COMMAND)
            break;

        if (string.IsNullOrEmpty(day))
            continue;

        while (true)
        {
            Console.WriteLine("Which part are you interested in? (1 or 2)");
            var partNumber = Console.ReadLine();

            if (partNumber == EXIT_COMMAND)
                goto EXIT;

            if (partNumber == BACK_COMMAND)
                break;

            if (string.IsNullOrEmpty(partNumber))
                continue;

            var solution = services.GetKeyedService<ISolution>($"{year}.{day}.{partNumber}");
            if (solution is null)
            {
                Console.WriteLine("The solution for this puzzle has not yet been implemented.");
                continue;
            }

            Console.WriteLine("What's the input? (End input by entering '_end_')");
            var lines = new List<string>();

            while (true)
            {
                var currentLine = Console.ReadLine();

                if (currentLine == "_end_")
                    break;

                lines.Add(currentLine ?? string.Empty);
            }

            var input = string.Join(Environment.NewLine, lines);

            if (input == EXIT_COMMAND)
                goto EXIT;

            if (input == BACK_COMMAND)
                break;

            if (string.IsNullOrEmpty(input))
                continue;

            Console.WriteLine("Running the solution...");
            var result = await solutionRunner.Run(solution, input, default);

            Console.WriteLine($"""
                -----------------------------------------------------
                Result: {result.Result}

                Duration: {result.RunTime}

                Memory (doesn't work):
                    Used: {result.UsedHeapSize}b
                    Total: {result.TotalHeapSize}b
                    Memory: {result.HeapSizeLimit}b
                -----------------------------------------------------
                """);
        }

    }
}

EXIT:
Console.WriteLine("Thank you for your time!");
