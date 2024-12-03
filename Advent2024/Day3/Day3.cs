using System.Text.RegularExpressions;

namespace Advent2024.Day3;

public class Day3
{
    public static string _exInput = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
    public static string _exInput2 = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

    public static void Solution(out int part1, out int part2)
    {
        var input = Input.Read("Day3");

        part1 = Regex.Matches(input, @"mul\(([0-9]{1,3}),([0-9]{1,3})\)").Select(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value)).Sum();
        var matches = Regex.Matches(input, @"mul\(([0-9]{1,3}),([0-9]{1,3})\)|do\(\)|don't\(\)");

        bool continueMul = true;
        var sum = 0;
        foreach (Match match in matches)
        {
            if (match.Value.Equals("don't()"))
            {
                continueMul = false;
            }
            else if (match.Value.Equals("do()"))
            {
                continueMul = true;
            }
            else
            {
                if (continueMul)
                {
                    sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                }
            }
            
        }
        part2 = sum;
    }
}

//mul\(([0-9]{1,3}),([0-9]{1,3})\)