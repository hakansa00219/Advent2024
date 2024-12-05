using System.Text.RegularExpressions;

namespace Advent2024.Day4;

public class Day4
{
    private static string _exInput =
        "MMMSXXMASM\nMSAMXMSMSA\nAMXSXMAAMM\nMSAMASMSMX\nXMASAMXAMM\nXXAMMXXAMA\nSMSMSASXSS\nSAXAMASAAA\nMAMMMXMMMM\nMXMXAXMASX";
    
    public static void Solution(out int part1, out int part2)
    {
        var input = Input.Read("Day4");
        
        var lines = input.Split("\n");

        char[][] visualInput = lines.ToList().Select(x => x.Select(x => x != '\n' ? x = '.' : x = '\n').ToArray()).ToArray();
        char[][] visualInput2 = lines.ToList().Select(x => x.Select(x => x != '\n' ? x = '.' : x = '\n').ToArray()).ToArray();

        int xmasCount = 0;
        int masCount = 0;
        
        for (var col = 0; col < lines.Length; col++)
        {
            for (var row = 0; row < lines[col].Length; row++)
            {
                
                switch (lines[col][row])
                {
                    case 'X':
                        CheckNeighbours(lines, ref visualInput, col, row, ref xmasCount);
                        break;
                    case 'A':
                        if (row != 0 && row != lines.Length - 1 && col != 0 && col != lines[col].Length -1)
                            CheckNeighboursPart2(lines, ref visualInput2, col, row, ref masCount);
                        break;
                    default:
                        break;
                }
            }
        }
        
        foreach (var chars in visualInput)
        {
            Console.WriteLine(new string(chars));
        }
        Console.WriteLine("");
        foreach (var chars in visualInput2)
        {
            Console.WriteLine(new string(chars));
        }
        part1 = xmasCount;
        part2 = masCount;
    }


    private static void CheckNeighbours(string[] lines, ref char[][] visualInput2, int col, int row, ref int xmasCount)
    {
        (int colOffSet, int rowOffset)[] directions = new (int, int)[]
        {
            (-1, -1),
            (0, -1),
            (1, -1),
            (1, 0),
            (1, 1),
            (0, 1),
            (-1, 1),
            (-1, 0)
        };
        int numRows = lines.Length;
        int numCols = lines[0].Length;

        foreach (var (colOffset, rowOffset) in directions)
        {
            if (IsWithinBounds(col + colOffset, row + rowOffset, numRows, numCols) &&
                IsWithinBounds(col + 2 * colOffset, row + 2 * rowOffset, numRows, numCols) &&
                IsWithinBounds(col + 3 * colOffset, row + 3 * rowOffset, numRows, numCols))
            {
                if (lines[col + colOffset][row + rowOffset] == 'M' &&
                    lines[col + 2 * colOffset][row + 2 * rowOffset] == 'A' &&
                    lines[col + 3 * colOffset][row + 3 * rowOffset] == 'S')
                {
                    visualInput2[col][row] = 'X';
                    visualInput2[col + colOffset][row + rowOffset] = 'M';
                    visualInput2[col + 2 * colOffset][row + 2 * rowOffset] = 'A';
                    visualInput2[col + 3 * colOffset][row + 3 * rowOffset] = 'S';
                    
                    xmasCount++;
                }
            }
        }
    }
    private static void CheckNeighboursPart2(string[] lines, ref char[][] visualInput, int col, int row, ref int masCount)
    {
        (int colOffset, int rowOffset)[] directions = new (int, int)[]
        {
            (-1, -1),
            (1, -1),
            (1, 1),
            (-1, 1),
        };
        
        foreach (var (colOffset, rowOffset) in directions)
        {
            if ((lines[col + colOffset][row + rowOffset] == 'M' &&
                 lines[col - colOffset][row - rowOffset] == 'S' &&
                 lines[col + colOffset][row - rowOffset] == 'M' &&
                 lines[col - colOffset][row + rowOffset] == 'S'))
            {
                visualInput[col + colOffset][row + rowOffset] = 'M';
                visualInput[col - colOffset][row - rowOffset] = 'S';
                visualInput[col][row] = 'A';
                visualInput[col + colOffset][row - rowOffset] = 'M';
                visualInput[col - colOffset][row + rowOffset] = 'S';
                
                masCount++;
                break;
            }
            if ((lines[col + colOffset][row + rowOffset] == 'M' &&
                 lines[col + colOffset][row - rowOffset] == 'S' &&
                 lines[col - colOffset][row + rowOffset] == 'M' && 
                 lines[col - colOffset][row - rowOffset] == 'S'))
            {
                visualInput[col + colOffset][row + rowOffset] = 'M';
                visualInput[col + colOffset][row - rowOffset] = 'S';
                visualInput[col][row] = 'A';
                visualInput[col - colOffset][row + rowOffset] = 'M';
                visualInput[col - colOffset][row - rowOffset] = 'S';
                
                masCount++;
                break;
            }
        }
    }
    
    private static bool IsWithinBounds(int row, int col, int numRows, int numCols)
    {
        return row >= 0 && row < numRows && col >= 0 && col < numCols;
    }
    
}