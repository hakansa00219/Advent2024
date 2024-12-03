namespace Advent2024.Day1;

class Day1
{
    private static string _exampleInput = "3   4\n4   3\n2   5\n1   3\n3   9\n3   3";
        
    public static void Solution(out int part1, out int part2)
    {
        string input = Input.Read("Day1");
        var nums = input.Split("\n");

        var leftNums = nums.Select(x => int.Parse(x.Split("   ")[0])).OrderBy(x => x).ToArray();
        var rightNums = nums.Select(x => int.Parse(x.Split("   ")[1])).OrderBy(x => x).ToArray();
            
        Func<int, int> arrayFirstSol = index => Math.Abs(leftNums[index] - rightNums[index]);
        Func<int, int> arraySol = index => Math.Abs(leftNums[index] * rightNums.Count(x => x == leftNums[index]));
            
        part1 = Enumerable.Range(0, nums.Length).Select((_, i) => arrayFirstSol(i)).Sum();
        part2 = Enumerable.Range(0, nums.Length).Select((_, i) => arraySol(i)).Sum();
        
        Console.WriteLine("Day 1");
    }
}