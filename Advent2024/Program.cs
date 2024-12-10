using Advent2024.Day1;
using Advent2024.Day2;
using Advent2024.Day3;
using Advent2024.Day4;
using Advent2024.Day5;
using Advent2024.Day6;
using Advent2024.Day7;
using Advent2024.Day8;

class Program
{
    static void Main(string[] args)
    {
        
        // Day1.Solution(out int sum, out int sum2);
        // Day2.Solution(out int part1, out int part2);
        // Day3.Solution(out int part1, out int part2); 
        // Day4.Solution(out int part1, out int part2);
        // Day5.Solution(out int part1, out int part2);
        // Day6.Solution(out int part1, out int part2);
        // Day7.Solution(out long part1, out long part2);
        Day8.Solution(out int part1, out int part2);
        
        Console.WriteLine("Solution 1:" + part1);
        Console.WriteLine("Solution 2:" + part2);
        Console.ReadKey();
    }
}