namespace Advent2024.Day6;

public class Day6
{
    private static readonly string _exInput =
        "....#.....\r\n.........#\r\n..........\r\n..#.......\r\n.......#..\r\n..........\r\n.#..^.....\r\n........#.\r\n#.........\r\n......#...";
    private static readonly string _exInput2 =
        "....#.....\r\n...#..#..#\r\n.....#....\r\n..#.......\r\n.......#..\r\n..........\r\n.#..^.....\r\n........#.\r\n#.........\r\n......#...";

    public static void Solution(out int part1, out int part2)
    {
        var input = Input.Read("Day6");
        
        var tempData = input.Split(Environment.NewLine).Select(row => row.ToCharArray()).ToArray();

        int rows = tempData.Length;
        int cols = tempData[0].Length;

        char[,] map = new char[rows, cols];
        char[,] secondMap = new char[rows, cols];
        
        (int, int) guardPosition = (-1, -1);
        (int, int) guardPosition2 = (-1, -1);
        int sum = 0;
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                secondMap[x, y] = tempData[x][y];
                map[x, y] = tempData[x][y];
                if (guardPosition == (-1, -1) && tempData[x][y] == '^')
                {
                    guardPosition = (x, y);
                    guardPosition2 = (x, y);
                }
            }
        }

        HashSet<(int,int)> obstaclePositions = new HashSet<(int,int)>();
        
        bool movement = true;
        Direction currentDirection = Direction.Up;
        Direction oldDirection = Direction.Down;
        do
        {
            movement = OneDirectionMovement(map, ref guardPosition, currentDirection);
            oldDirection = currentDirection;
            currentDirection = NextDirection(currentDirection);
        } while(movement);
        
        switch (oldDirection)
        {
            case Direction.Up:
                map[guardPosition.Item1,guardPosition.Item2] = '^';
                break;
            case Direction.Down:
                map[guardPosition.Item1,guardPosition.Item2] = 'v';
                break;
            case Direction.Left:
                map[guardPosition.Item1,guardPosition.Item2] = '<';
                break;
            case Direction.Right:
                map[guardPosition.Item1,guardPosition.Item2] = '>';
                break;
        }
        
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 'X' || map[i, j] == '<' || map[i, j] == '>' || map[i, j] == '^' || map[i, j] == 'v')
                {
                    sum += 1;
                    obstaclePositions.Add((i, j));
                }
                Console.Write(map[i,j]);
            }
            Console.Write('\n');
        }
        Console.Write('\n');
        
        Target movement2 = Target.Start;
        Direction currentDirection2 = Direction.Up;
        Direction oldDirection2 = Direction.Down;
        int sum2 = 0;
        bool isAfterObstacle = false;
        foreach (var obstaclePosition in obstaclePositions)
        {
            // Console.WriteLine("Obstacle position : " + (obstaclePosition.Item1 + "," + obstaclePosition.Item2));
            secondMap[obstaclePosition.Item1, obstaclePosition.Item2] = 'O';
            Dictionary<(int,int), Direction> visited = new Dictionary<(int,int), Direction>();
            do
            {
                movement2 = OneDirectionMovement2(secondMap, ref guardPosition2,ref sum2, ref isAfterObstacle, ref visited, currentDirection2);
                oldDirection2 = currentDirection2;
                currentDirection2 = NextDirection(currentDirection2);
                
            } while(movement2 is Target.Obstacle or Target.OObstacle);

            if (movement2 != Target.Loop)
            {
                switch (oldDirection2)
                {
                    case Direction.Up:
                        secondMap[guardPosition2.Item1,guardPosition2.Item2] = '^';
                        break;
                    case Direction.Down:
                        secondMap[guardPosition2.Item1,guardPosition2.Item2] = 'v';
                        break;
                    case Direction.Left:
                        secondMap[guardPosition2.Item1,guardPosition2.Item2] = '<';
                        break;
                    case Direction.Right:
                        secondMap[guardPosition2.Item1,guardPosition2.Item2] = '>';
                        break;
                }
            }
            movement2 = Target.Start;
            currentDirection2 = Direction.Up;
            oldDirection2 = Direction.Down;
            isAfterObstacle = false;
            guardPosition2 = (-1, -1);
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    secondMap[x, y] = tempData[x][y];
                    if (guardPosition2 == (-1, -1) && tempData[x][y] == '^')
                    {
                        guardPosition2 = (x, y);
                    }
                }
            }
        }
        
        part1 = sum;
        part2 = sum2;
    }

    private static bool OneDirectionMovement(char[,] map, ref (int x,int y) guardPosition, Direction direction = Direction.Up)
    {
        (int x, int y) nextPosition = (-1, -1);
        
        map[guardPosition.x,guardPosition.y] = 'X';
        nextPosition = direction switch
        {
            Direction.Up => (guardPosition.x - 1, guardPosition.y),
            Direction.Down => (guardPosition.x + 1, guardPosition.y),
            Direction.Left => (guardPosition.x, guardPosition.y - 1),
            Direction.Right => (guardPosition.x, guardPosition.y + 1),
        };
        while (map[nextPosition.x,nextPosition.y] != '#')
        {
            map[guardPosition.x,guardPosition.y] = 'X';
            switch (direction)
            {
                case Direction.Up:
                    guardPosition = (guardPosition.x - 1, guardPosition.y);
                    map[guardPosition.x,guardPosition.y] = '^';
                    nextPosition = (guardPosition.x - 1, guardPosition.y);
                    break;
                case Direction.Down:
                    guardPosition = (guardPosition.x + 1, guardPosition.y);
                    map[guardPosition.x,guardPosition.y] = 'v';
                    nextPosition = (guardPosition.x + 1, guardPosition.y);
                    break;
                case Direction.Left:
                    guardPosition = (guardPosition.x, guardPosition.y - 1);
                    map[guardPosition.x,guardPosition.y] = '<';
                    nextPosition = (guardPosition.x, guardPosition.y - 1);
                    break;
                case Direction.Right:
                    guardPosition = (guardPosition.x, guardPosition.y + 1);
                    map[guardPosition.x,guardPosition.y] = '>';
                    nextPosition = (guardPosition.x, guardPosition.y + 1);
                    break;
            }
            
            if (nextPosition.x > map.GetLength(0) - 1 || nextPosition.x < 0 || nextPosition.y > map.GetLength(1) - 1 ||
                nextPosition.y < 0)
                return false;

        }

        return true;
    } 
    private static Target OneDirectionMovement2(char[,] map, ref (int x,int y) guardPosition, ref int count, ref bool isAfterObstacle, ref Dictionary<(int,int), Direction> cachedHashtags, Direction direction = Direction.Up)
    {
        (int x, int y) nextPosition = (-1, -1);
        nextPosition = direction switch
        {
            Direction.Up => (guardPosition.x - 1, guardPosition.y),
            Direction.Down => (guardPosition.x + 1, guardPosition.y),
            Direction.Left => (guardPosition.x, guardPosition.y - 1),
            Direction.Right => (guardPosition.x, guardPosition.y + 1),
        };
        
        bool isFirstMovement = true;
        bool mightBeLoop = false;

       
        
        while (map[nextPosition.x,nextPosition.y] != '#')
        {
            if (cachedHashtags.ContainsKey(guardPosition) &&
                direction == cachedHashtags[guardPosition])
            {
                count++;
                return Target.Loop;
            }
            cachedHashtags.TryAdd((guardPosition.x, guardPosition.y), direction);
            
            if (map[nextPosition.x, nextPosition.y] == 'O' && !isFirstMovement)
            {
                isAfterObstacle = true;
                map[guardPosition.x, guardPosition.y] = '+';
                return Target.OObstacle;
            }

            isFirstMovement = false;
            if (!isFirstMovement)
            {
                if (direction is Direction.Up or Direction.Down)
                {
                    if (map[guardPosition.x, guardPosition.y] == '.')
                        map[guardPosition.x, guardPosition.y] = '|';
                    else if (map[guardPosition.x, guardPosition.y] == '-')
                        map[guardPosition.x, guardPosition.y] = '+';
                    else if (map[guardPosition.x, guardPosition.y] == '+')
                    {
                        map[guardPosition.x, guardPosition.y] = '+';
                    }
                    else if(map[guardPosition.x, guardPosition.y] == '|')
                    {
                        map[guardPosition.x, guardPosition.y] = '|';
                    }
                }
                else
                {
                    if (map[guardPosition.x, guardPosition.y] == '.') 
                        map[guardPosition.x, guardPosition.y] = '-';
                    else if (map[guardPosition.x, guardPosition.y] == '|')
                        map[guardPosition.x, guardPosition.y] = '+';
                    else if (map[guardPosition.x, guardPosition.y] == '+')
                    {
                        map[guardPosition.x, guardPosition.y] = '+';
                    }
                    else if (map[guardPosition.x, guardPosition.y] == '-')
                    {
                        map[guardPosition.x, guardPosition.y] = '-';
                    }
                }

            }
           
            switch (direction)
            {
                case Direction.Up:
                    guardPosition = (guardPosition.x - 1, guardPosition.y);
                    nextPosition = (guardPosition.x - 1, guardPosition.y);
                    break;
                case Direction.Down:
                    guardPosition = (guardPosition.x + 1, guardPosition.y);
                    nextPosition = (guardPosition.x + 1, guardPosition.y);
                    break;
                case Direction.Left:
                    guardPosition = (guardPosition.x, guardPosition.y - 1);
                    nextPosition = (guardPosition.x, guardPosition.y - 1);
                    break;
                case Direction.Right:
                    guardPosition = (guardPosition.x, guardPosition.y + 1);
                    nextPosition = (guardPosition.x, guardPosition.y + 1);
                    break;
            }
            
           
            if (nextPosition.x > map.GetLength(0) - 1 || nextPosition.x < 0 || nextPosition.y > map.GetLength(1) - 1 ||
                nextPosition.y < 0)
                return Target.End;

        }
        
        if (mightBeLoop)
        {
            count++;
            return Target.Loop;
        }
        map[guardPosition.x,guardPosition.y] = '+';
        return Target.Obstacle;
    }

    private static Direction NextDirection(Direction currentDirection)
    {
        return (Direction)((int)++currentDirection % 4);
    }
    
    public enum Direction { Up, Right, Down,Left}

    public enum Target
    {
        Obstacle,
        OObstacle,
        End,
        Loop,
        Start
    };
}