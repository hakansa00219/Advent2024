using Advent2024.Day1;
using Advent2024.Day2;
using Advent2024.Day3;

class Program
{
    static void Main(string[] args)
    {
        
        // Day1.Solution(out int sum, out int sum2);
        // Day2.Solution(out int part1, out int part2);
        Day3.Solution(out int part1, out int part2); 
        
        Console.WriteLine("Solution 1:" + part1);
        Console.WriteLine("Solution 2:" + part2);
        Console.ReadKey();
    }
}