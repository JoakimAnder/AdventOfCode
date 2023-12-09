using Infra.Helpers;
using Infra.Interfaces;

namespace Puzzle;

public class Part1 : IPuzzlePart
{
    public object? ExpectedResult => 6;
    public object Run()
    {
        var input = Helper.GetInputReader(Helper.GetInputPath(nameof(Part1)));
        const string startNodeId = "AAA";
        const string endNodeId = "ZZZ";
        var steps = 0;
        var (directions, network) = InputParser.Parse(input.LinesAsEnumerable());
        var currentNode = network.First(n => n.Id == startNodeId);

        while (currentNode.Id != endNodeId)
        {
            var instruction = directions[steps % directions.Length];
            var nextNode = currentNode.Navigate(instruction);
            steps++;
            //Console.WriteLine("{0}: {1} - {2}", steps, currentNode.Id, nextNodeId);
            currentNode = nextNode;
        }

        Console.WriteLine("{0} steps are required to reach ZZZ", steps);
        return steps;
    }

}
