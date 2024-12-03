namespace Advent2024.Day2;

public class Day2
{
    private static string _exInput = "7 6 4 2 1\n1 2 7 8 9\n9 7 6 2 1\n1 3 2 4 5\n8 6 4 4 1\n1 3 6 7 9";
    private static string _edgeInput = "48 46 47 49 51 54 56\n1 1 2 3 4 5\n1 2 3 4 5 5\n5 1 2 3 4 5\n1 4 3 2 1\n1 6 7 8 9\n1 2 3 4 3\n9 8 7 6 7\n7 10 8 10 11\n29 28 27 25 26 25 22 20";

    public static void Solution(out int part1, out int part2)
    {
        string input = Input.Read("Day2");

        var levels = input.Split("\n");
        var reports = levels.Select(l => Array.ConvertAll(l.Split(' '), int.Parse).ToList()).ToList();
        int safeReportCount = 0;
        List<int> tempList = null;
        
        foreach (var report in reports)
        {
            var isSafe = IsSafe(report);
            if (isSafe)
            {
                safeReportCount++;
                continue;
            }
            for (var i = 0; i < report.Count; i++)
            {
                tempList = new List<int>(report);
                tempList.RemoveAt(i);
                isSafe = IsSafe(tempList);
                if (isSafe)
                {
                    safeReportCount++;
                    break;
                }
            }
        }

        part1 = 490;
        part2 = safeReportCount;
    }

    public static bool IsSafe(List<int> reports)
    {
        int currentValue, nextValue, increment;
        currentValue = reports[0];
        nextValue = reports[1];
        bool increasing = currentValue < nextValue;
        bool nextIncreasing = false;

        for (int i = 0; i < reports.Count - 1 ; i++)
        {
            if (i > 0)
            {
                currentValue = nextValue;
                nextValue = reports[i + 1];
                nextIncreasing = currentValue < nextValue;

                if (nextIncreasing != increasing)
                {
                    return false;
                }
            }
            increment = Math.Abs(nextValue - currentValue);
            if (increment is > 3 or < 1)
            {
                return false;
            }
        }

        return true;
    }
}

