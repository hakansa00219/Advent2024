namespace Advent2024.Day7;

public class Day7
{
    private static readonly string _exInput =
        "190: 10 19\r\n3267: 81 40 27\r\n83: 17 5\r\n156: 15 6\r\n7290: 6 8 6 15\r\n161011: 16 10 13\r\n192: 17 8 14\r\n21037: 9 7 18 13\r\n292: 11 6 16 20";
    public static void Solution(out long part1, out long part2)
    {
        var input = Input.Read("Day7");

        string[] lines = input.Split(Environment.NewLine);

        long total1 = 0;
        long total2 = 0;
        
        foreach (var line in lines)
        {
            var parts = line.Split(": ");
            var result = long.Parse(parts[0]);
            var nums = parts[1];
            
            var numArray = nums.Split(' ').Select(long.Parse).ToArray();

            long? sum = Calculate(numArray, result, false);
            long? sum2 = Calculate(numArray, result, true);

            if (sum != null)
            {
                total1 += sum.Value;
            }

            if (sum2 != null)
            {
                total2 += sum2.Value;
            }
            
        }

        part1 = total1;
        part2 = total2;
    }

    private static long? Calculate(long[] nums, long result, bool part2)
    {
        Stack<Calculation> calculations = new Stack<Calculation>();
        
        calculations.Push(new Calculation() {currentResult = 0, number = nums[0], calculationType = '+', orderIndex = 0});

        while (calculations.Count > 0)
        {
            var current = calculations.Pop();

            switch (current.calculationType)
            {
                case '+':
                    current.currentResult += current.number;
                    break;
                case '*':
                    current.currentResult *= current.number;
                    break;
                case '|' when part2:
                    current.currentResult = (long)Math.Pow(10,current.number.ToString().Length) * current.currentResult + current.number;
                    break;
            }
            
            current.orderIndex++;
            
            if (current.orderIndex < nums.Length)
            {
                calculations.Push(current with { number = nums[current.orderIndex], calculationType = '+' });
                calculations.Push(current with { number = nums[current.orderIndex], calculationType = '*' });
                if(part2)
                    calculations.Push(current with { number = nums[current.orderIndex], calculationType = '|' });
            }
            else if (current.orderIndex == nums.Length && result.Equals(current.currentResult))
            {
                return current.currentResult;
            }
            else
            {
                continue;
            }
        }

        return null;
    }

    public struct Calculation
    {
        public long currentResult;
        public long number;
        public int orderIndex;
        public char calculationType;
    }
    
}