using System.Numerics;

namespace Advent2024.Day14;

public class Day14
{
    private static readonly string _exInput =
        "p=0,4 v=3,-3\r\np=6,3 v=-1,-3\r\np=10,3 v=-1,2\r\np=2,0 v=2,-1\r\np=0,0 v=1,3\r\np=3,0 v=-2,-2\r\np=7,6 v=-1,-3\r\np=3,0 v=-1,-2\r\np=9,3 v=2,3\r\np=7,3 v=-1,2\r\np=2,4 v=2,-3\r\np=9,5 v=-3,-3";

    public static void Solution(out long part1, out long part2)
    {
        var input = Input.Read("Day14");

        const int mainRow = 101, exRow = 11;
        const int mainColumn = 103, exColumn = 7;
        const int simulationTimeInSeconds = 100;

        var currentInput = input;
        
        var lines = currentInput.Split(Environment.NewLine);
        List<Point> points = new List<Point>();
        
        foreach (var line in lines)
        {
            var sides = line.Split(' ');
            var position = Array.ConvertAll(sides[0][2..].Split(','), int.Parse);
            var velocity = Array.ConvertAll(sides[1][2..].Split(','), int.Parse);
            points.Add(new Point(position[1], position[0], new Vector2(velocity[0], velocity[1])));
        }

        var map = Map.NewMap(mainRow, mainColumn, ".");
        
        foreach (var point in points)
        {
            IncreaseElement(ref map, point.X, point.Y);
            // Map.Visualize(map);
        }
        
        // Map.Visualize(map);

        for (int i = 0; i < simulationTimeInSeconds; i++)
        {
            foreach (var point in points)
            {
                var nextX = point.X + (int)point.Velocity.Y;
                var nextY = point.Y + (int)point.Velocity.X;
                
                if(nextX < 0) nextX += map.GetLength(0);
                else if(nextX >= map.GetLength(0)) nextX %= map.GetLength(0);
                if(nextY < 0) nextY += map.GetLength(1);
                else if (nextY >= map.GetLength(1)) nextY %= map.GetLength(1);

                map[point.X, point.Y] = map[point.X, point.Y] is "." or "1" ? "." : (int.Parse(map[point.X, point.Y]) - 1).ToString();
                IncreaseElement(ref map, nextX, nextY);
                point.X = nextX;
                point.Y = nextY;
                
            }
            
            // Map.Visualize(map);
        }
        
        // Map.Visualize(map);

        int halfX = (int)Math.Floor((double)mainRow / 2);
        int halfY = (int)Math.Floor((double)mainColumn / 2);

        int qTL = 0, qTR = 0, qBL = 0, qBR = 0;
        
        
        for (var y = 0; y < map.GetLength(1); y++)
        for (var i = 0; i < map.GetLength(0); i++)
        {
            if (map[i,y] == ".") continue;
            if (i < halfX && y < halfY) qTL += int.Parse(map[i, y]);
            if (i > halfX && y < halfY) qBL += int.Parse(map[i, y]);
            if (i < halfX && y > halfY) qTR += int.Parse(map[i, y]);
            if (i > halfX && y > halfY) qBR += int.Parse(map[i, y]);
        }
        
        part1 = qTL * qBL * qTR * qBR;
        part2 = 0;
    }

    private static void IncreaseElement(ref string[,] map, int X, int Y)
    {
        map[X, Y] = map[X,Y] == "." ? "1" : (int.Parse(map[X, Y]) + 1).ToString();
    }

    private record Point(int X, int Y, Vector2 Velocity)
    {
        public int X { get; set; } = X;
        public int Y { get; set; } = Y;
        public Vector2 Velocity { get; set; } = Velocity;
    }
}