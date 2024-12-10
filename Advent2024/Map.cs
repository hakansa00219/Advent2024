namespace Advent2024;

public static class Map
{
    public static void Visualize(char[,] map)
    {
        Console.Write('\n');
        Console.Out.Flush();
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i,j]);
                Console.Out.Flush();
            }
            Console.Write('\n');
            Console.Out.Flush();
        }
        Console.Write('\n');
        Console.Out.Flush();
    }
    public static char[,] ToMap(this char[][] s)
    {
        int rows = s.Length;
        int cols = s[0].Length;

        char[,] map = new char[rows, cols];
        
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                map[x, y] = s[x][y];
            }
        }

        return map;
    }
    public static char[,] ToMap(this string[] s)
    {
        int rows = s.Length;
        int cols = s[0].Length;

        char[,] map = new char[rows, cols];
        
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                map[x, y] = s[x][y];
            }
        }

        return map;
    }

    public static bool IsInside(this char[,] map, int x, int y)
    {
        return x <= map.GetLength(0) - 1 && x >= 0 && y <= map.GetLength(1) - 1 && y >= 0;
    }
    public static bool IsInside(this char[,] map, (int x,int y) pos)
    {
        return pos.x <= map.GetLength(0) - 1 && pos.x >= 0 && pos.y <= map.GetLength(1) - 1 && pos.y >= 0;
    }
}