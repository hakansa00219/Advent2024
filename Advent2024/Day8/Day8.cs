namespace Advent2024.Day8;

public class Day8
{
    private static readonly string _exInput =
        "............\r\n........0...\r\n.....0......\r\n.......0....\r\n....0.......\r\n......A.....\r\n............\r\n............\r\n........A...\r\n.........A..\r\n............\r\n............";

    public static void Solution(out int part1, out int part2)
    {
        var input = Input.Read("Day8");
        
        var tempData = input.Split(Environment.NewLine).ToArray();

        var map = tempData.ToMap();

        int antinodeCount1 = 0;
        int antinodeCount2 = 0;

        Dictionary<char, List<(int,int,bool)>> foundAntennas = new();

        for (var x = 0; x < map.GetLength(0); x++)
        for (var y = 0; y < map.GetLength(1); y++)
        {
            var c = map[x, y];
            
            if (c != '.')
            {
                //Found antenna
                if(!foundAntennas.ContainsKey(c))
                    foundAntennas.Add(c, [(x, y, false)]);
                else 
                    foundAntennas[c].Add((x,y, c == '#'));
                //Try to get all other same frequency antennas
                if (c == '#') continue;
                
                // Console.WriteLine("_____________________");
                // Console.Out.Flush();
                // Console.WriteLine("Char: " + c);
                // Console.Out.Flush();
                
                // Map.Visualize(map);
                
                if (!foundAntennas.TryGetValue(c, out List<(int x, int y, bool hasAntinode)>? antennas) && antennas != null) ;
                
                
                foreach (var (xx, yy, hasAntinode) in antennas)
                {
                    if(xx == x && yy == y) continue;
                    
                    (int x, int y) diff = (x - xx, y - yy);

                    //Part1
                    // int aX = x + diff.x;
                    // int aY = y + diff.y;
                    
                    
                    // if (map.IsInside(aX, aY) && map[aX, aY] != '#')
                    // {
                    //     if(map[aX, aY] == '.')
                    //         map[aX, aY] = '#';
                    //     
                    //     antinodeCount1++;
                    // }
                    //End Part1

                    int aX = x + diff.x;
                    int aY = y + diff.y;
                    
                    while(map.IsInside(aX, aY) && map[aX, aY] != '#')
                    {
                        if (map[aX, aY] == '.')
                        {
                            map[aX, aY] = '#';
                        }
                        
                        antinodeCount2++;
                        
                        // Console.WriteLine("Done " + (aX,aY) + $" | {antinodeCount}");
                        // Console.Out.Flush();
                        
                        aX += diff.x;
                        aY += diff.y;
                        
                    }

                    //Part1
                    // int bX = xx - diff.x;
                    // int bY = yy - diff.y;

                    
                    // if (map.IsInside(bX, bY) && map[bX, bY] != '#')
                    // {
                    //     if(map[bX, bY] == '.')
                    //         map[bX, bY] = '#';
                    //     
                    //     antinodeCount1++;
                    // }
                    //End Part1

                    int bX = xx - diff.x;
                    int bY = yy - diff.y;
                    
                    while(map.IsInside(bX, bY) && map[bX, bY] != '#')
                    {

                        if (map[bX, bY] == '.')
                        {
                            map[bX, bY] = '#';
                        }
                        antinodeCount2++;
                        // Console.WriteLine("Done " + (bX,bY) + $" | {antinodeCount}");
                        // Console.Out.Flush();
                        
                        bX -= diff.x;
                        bY -= diff.y;
                    }
                        
                }
                
            }
        }

        Map.Visualize(map);

        part1 = antinodeCount1;
        part2 = antinodeCount2;
    }
}