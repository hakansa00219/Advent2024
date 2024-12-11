using System.Text;

namespace Advent2024.Day9;

public class Day9
{
    private static readonly string _exInput = "2333133121414131402";
    private static readonly string _exInput2 = "12345";

    public static void Solution(out long part1, out long part2)
    {
        var input = Input.Read("Day9");

        List<File> files = new List<File>();

        var currentInput = input;

        int fileCount = 0;
        
        for (var i = 0; i < currentInput.Length; i++)
        {
            int count = int.Parse(currentInput[i].ToString());
            fileCount += count;
            for (var j = 0; j < count; j++)
            {
                if (i % 2 == 0)
                {
                    files.Add(new File() {FileName = (i * 0.5f).ToString()});
                }
                else
                {
                    files.Add(new File() {FileName = "."});
                }
            }
            
            
            
        }
        
        bool stopDotSearcher = false;
        bool stopNumSearcher = false;
        
        var part1Files = new List<File>(files);
        for (int i = 0, j = part1Files.Count - 1;;)
        {
            if (part1Files[j].FileName != ".")
            {
                stopNumSearcher = true;
            }
            else j--;

            if (part1Files[i].FileName == ".")
            {
                stopDotSearcher = true;
            }
            else i++;

            if (stopDotSearcher && stopNumSearcher)
            {
                part1Files[i] = new File { FileName = part1Files[j].FileName };
                part1Files[j] = new File { FileName = "." };
                j--;
                i++;

                stopDotSearcher = false;
                stopNumSearcher = false;
            }
            
            if (i >= j)
                break;
        }


        var result = part1Files.Where(x => x.FileName != ".").ToArray();

        long sum = 0;
        for (var i = 0; i < result.Count(); i++)
        {
            sum += i * long.Parse(result[i].FileName);
        }

        part1 = sum;
        
        SortedDictionary<int, int> emptySizes = new SortedDictionary<int, int>(Comparer<int>.Create((x, y) => x.CompareTo(y)));
        
        var part2Files = new List<File>(files);
        int dotIndex = -1;
        for (var i = 0; i < part2Files.Count; i++)
        {
            if (part2Files[i].FileName == ".")
            {
                if (emptySizes.ContainsKey(dotIndex))
                {
                    emptySizes[dotIndex]++;
                }
                else
                {
                    dotIndex = i;
                    emptySizes.Add(dotIndex, 1);
                }
            }
            else
            {
                dotIndex = -1;
            }
        }

        int sameFileCount = 0;
        string sameFileName = "";
        for (var j = part2Files.Count - 1; j >= 0; j--)
        {
            if (part2Files[j].FileName != ".")
            {
                if (sameFileName == part2Files[j].FileName)
                {
                    sameFileCount++;
                    continue;
                }
                else if (sameFileName == "")
                {
                    sameFileCount = 1;
                    sameFileName = part2Files[j].FileName;
                }
                else
                {
                    TryToMoveFile(emptySizes, sameFileCount, part2Files, sameFileName, j);
                    sameFileCount = 1;
                    sameFileName = part2Files[j].FileName;
                }
            }
            else
            {
                TryToMoveFile(emptySizes, sameFileCount, part2Files, sameFileName, j);
                sameFileName = "";
                sameFileCount = 0;
            }
        }
        
        long sum2 = 0;
        for (var i = 0; i < part2Files.Count(); i++)
        {
            if(part2Files[i].FileName != ".")
                sum2 += i * long.Parse(part2Files[i].FileName);
        }
        
        part2 = sum2;
    }

    private static void TryToMoveFile(SortedDictionary<int, int> emptySizes, int sameFileCount, List<File> part2Files, string sameFileName,
        int j)
    {
        try
        {
            var emptySize = emptySizes.First(x => x.Value >= sameFileCount);

            if (emptySize.Key > j) return;
            
            for (var k = 0; k < sameFileCount; k++)
            {
                part2Files[emptySize.Key + k] = new File() { FileName = sameFileName };
                part2Files[j + 1 + k] = new File() { FileName = "." };
            }
                        

            if (emptySize.Value - sameFileCount == 0) emptySizes.Remove(emptySize.Key);
            else
            {
                emptySizes.Remove(emptySize.Key);
                emptySizes.Add(emptySize.Key + sameFileCount, emptySize.Value - sameFileCount);
            }

        }
        catch
        {
                    
        }
    }

    public struct File
    {
        public string FileName;
    }
}