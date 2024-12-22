using System.Numerics;
using System.Text.RegularExpressions;

namespace Advent2024.Day13;

public class Day13
{
    private static readonly string _exInput =
        "Button A: X+94, Y+34\r\nButton B: X+22, Y+67\r\nPrize: X=8400, Y=5400\r\n\r\nButton A: X+26, Y+66\r\nButton B: X+67, Y+21\r\nPrize: X=12748, Y=12176\r\n\r\nButton A: X+17, Y+86\r\nButton B: X+84, Y+37\r\nPrize: X=7870, Y=6450\r\n\r\nButton A: X+69, Y+23\r\nButton B: X+27, Y+71\r\nPrize: X=18641, Y=10279";

    public static void Solution(out long part1, out long part2)
    {
        var input = Input.Read("Day13");

        var currentInput = input;
        const int aTokenCost = 3;
        const int bTokenCost = 1;

        long totalCost = 0;
        
        var machines = currentInput.Split("\r\n\r\n");
        foreach (var machine in machines)
        {
            var lines = machine.Split(Environment.NewLine);

            var btnA = lines[0].Substring(10);
            var btnB = lines[1].Substring(10);
            var prizes = lines[2].Substring(7);

            var btnASplitted = btnA.Split(',');
            var btnBSplitted = btnB.Split(',');
            var prizeSplitted = prizes.Split(", ");

            (float x, float y) aMove = (int.Parse(btnASplitted[0].Substring(2)),
                int.Parse(btnASplitted[1].Substring(2)));

            (float x, float y) bMove = (int.Parse(btnBSplitted[0].Substring(2)),
                int.Parse(btnBSplitted[1].Substring(2)));

            (float x, float y) prize = (int.Parse(prizeSplitted[0].Substring(2)),
                int.Parse(prizeSplitted[1].Substring(2)));

            float solA, solB;

            solB = ((aMove.x / aMove.y) * prize.y - prize.x) /
                   ((aMove.x / aMove.y) * bMove.y - bMove.x);
            solA = (prize.x - bMove.x * solB) / aMove.x;

            if (solA > 100 || solB > 100 || solA < 0 || solB < 0) continue;

            if (solA % 1 < 0.0001f || solA % 1 > 0.9998f && solB % 1 < 0.0001f || solB % 1 > 0.9998f)
            {
                totalCost += (int)Math.Round(solA) * aTokenCost + (int)Math.Round(solB) * bTokenCost;
                Console.WriteLine("A : " + solA + ", B : " + solB);
                Console.WriteLine("Cost = " + (Math.Round(solA) * aTokenCost + Math.Round(solB) * bTokenCost));
            }

        }

        part1 = totalCost;

        totalCost = 0;

        foreach (var machine in machines)
        {
            var lines = machine.Split(Environment.NewLine);

            var btnA = lines[0].Substring(10);
            var btnB = lines[1].Substring(10);
            var prizes = lines[2].Substring(7);

            var btnASplitted = btnA.Split(',');
            var btnBSplitted = btnB.Split(',');
            var prizeSplitted = prizes.Split(", ");

            (double x, double y) aMove = (int.Parse(btnASplitted[0].Substring(2)),
                int.Parse(btnASplitted[1].Substring(2)));

            (double x, double y) bMove = (int.Parse(btnBSplitted[0].Substring(2)),
                int.Parse(btnBSplitted[1].Substring(2)));

            (double x, double y) prize = (int.Parse(prizeSplitted[0].Substring(2)),
                int.Parse(prizeSplitted[1].Substring(2)));

            prize.x += 10000000000000;
            prize.y += 10000000000000;

            Console.WriteLine($"Prize : {prize.x}, {prize.y}, A: {aMove.x}, {aMove.y}, B: {bMove.x}, {bMove.y} ");

            double solA, solB;

            solB = ((aMove.x / aMove.y) * prize.y - prize.x) /
                   ((aMove.x / aMove.y) * bMove.y - bMove.x);
            solA = (prize.x - bMove.x * solB) / aMove.x;

            if ( /*solA > 100 || solB > 100 ||*/ solA < 0 || solB < 0) continue;

            if ( /*solA % 1 < 0.0001f || solA % 1 > 0.9998f && solB % 1 < 0.0001f || solB % 1 > 0.9998f*/
                solA % 1 != 0 && solB % 1 != 0)
            {
                totalCost += (long)Math.Round(solA) * aTokenCost + (long)Math.Round(solB) * bTokenCost;
                Console.WriteLine("A : " + solA + ", B : " + solB);
                Console.WriteLine("Cost = " + (Math.Round(solA) * aTokenCost + Math.Round(solB) * bTokenCost));
            }

        }

        part2 = totalCost;
    }


}