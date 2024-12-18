using System.Collections.Concurrent;

namespace Advent2024.Day11;

public class Day11
{
    public static readonly string _exInput = "0 1 10 99 999";
    public static readonly string _exInput2 = "125 17";
    
    public static void Solution(out long part1, out long part2)
    {
        var input = Input.Read("Day11");

        var currentInput = input;
        int blinkCount1 = 25;
        int blinkCount2 = 75;

        var inputList = currentInput.Split(' ').ToList();
        ConcurrentDictionary<string, long> caches = new ConcurrentDictionary<string, long>();
        
        long result = 0;
        List<long> sols1 = inputList.AsParallel().Select(x =>
        {
            result = Blink(x, blinkCount1, caches);
            return result;
        }).ToList();
        result = 0;
        List<long> sols2 = inputList.AsParallel().Select(x =>
        {
            result = Blink(x, blinkCount2, caches);
            return result;
        }).ToList();

        // var part1List = sols.Slice(0, 25);

        part1 = sols1.Sum();
        part2 = sols2.Sum();
    }

    private static long Blink(string num, long depth, ConcurrentDictionary<string, long> caches)
    {
        long result;
        if (depth <= 0L) result = 1L;
        else if (caches.TryGetValue($"{depth}:{num}", out result))
            return result;
        else if (num == "0")
        {
            result = Blink("1", depth - 1L, caches);
        }
        else if (num.Length % 2 == 0)
        {
            result = Blink(num[..(num.Length / 2)], depth - 1L, caches);
            result += Blink(num[(num.Length / 2)..].TrimStart('0').PadLeft(1, '0'), depth - 1L, caches);
        }
        else
        {
            long number = long.Parse(num);
            result = Blink($"{number * 2024}", depth - 1L, caches);
        }
        
        caches[$"{depth}:{num}"] = result;
        
        return result;
    }
    
}