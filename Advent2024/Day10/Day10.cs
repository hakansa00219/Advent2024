namespace Advent2024.Day10;

public class Day10
{
    public static readonly string _exInput =
        "89010123\r\n78121874\r\n87430965\r\n96549874\r\n45678903\r\n32019012\r\n01329801\r\n10456732";

    public static readonly string _exInput2 = "0123\r\n1234\r\n8765\r\n9876";

    public static readonly string _exInput3 =
        "...0...\r\n...1...\r\n...2...\r\n6543456\r\n7.....7\r\n8.....8\r\n9.....9";

    public static readonly string _exInput4 =
        "..90..9\r\n...1.98\r\n...2..7\r\n6543456\r\n765.987\r\n876....\r\n987....";

    public static readonly string _exInput5 =
        "10..9..\r\n2...8..\r\n3...7..\r\n4567654\r\n...8..3\r\n...9..2\r\n.....01";

    public static readonly string _exInput6 = ".....0.\r\n..4321.\r\n..5..2.\r\n..6543.\r\n..7..4.\r\n..8765.\r\n..9....";

    public static void Solution(out int part1, out int part2)
    {
        var input = Input.Read("Day10");

        var currentInput = input;

        var map = currentInput.Split(Environment.NewLine).ToMap();

        var trailheadCount = 0;
        var score = 0;
        var rating = 0;

        for (var x = 0; x < map.GetLength(0); x++)
        for (var y = 0; y < map.GetLength(1); y++)
        {
            if (map[x, y] != '0') continue;
            
            if (CheckTrailhead(map, (x, y), out int scoreAdding, out int ratingAdding))
            {
                trailheadCount++;
                
                Console.WriteLine($"{trailheadCount}. Trailhead score: " + scoreAdding);
                Console.WriteLine($"{trailheadCount}. Trailhead rating: " + ratingAdding);
            }
            
            score += scoreAdding;
            rating += ratingAdding;
        }

        Console.WriteLine("Trailhead Count: " + trailheadCount);
        part1 = score;
        part2 = rating;
    }

    private static bool CheckTrailhead(char[,] map, (int x, int y) currentPosition, out int score, out int rating)
    {
        Stack<Node> paths = new Stack<Node>();
        paths.Push(new Node(currentPosition.x, currentPosition.y) { Value = int.Parse(map[currentPosition.x, currentPosition.y].ToString()) });

        HashSet<Node> visitedPeak = new HashSet<Node>();
        bool trailHead = false;

        var scores = 0;
        var ratings = 0;

        while (paths.Count > 0)
        {
            var node = paths.Pop();

            if (node.Value == 9)
            {
                trailHead = true;
                if(!visitedPeak.Contains(node))
                    scores++;
                visitedPeak.Add(node);

                ratings++;

            }

            Node upNode = new Node(node.X - 1, node.Y);
            Node downNode = new Node(node.X + 1, node.Y);
            Node leftNode = new Node(node.X, node.Y - 1);
            Node rightNode = new Node(node.X, node.Y + 1);

            if (CheckIsNodeValid(map, node, upNode))
            {
                upNode.Value = int.Parse(map[upNode.X, upNode.Y].ToString());
                paths.Push(upNode);
            }
            if (CheckIsNodeValid(map, node, downNode))
            {
                downNode.Value = int.Parse(map[downNode.X, downNode.Y].ToString());
                paths.Push(downNode);
            }
            if (CheckIsNodeValid(map, node, leftNode))
            {
                leftNode.Value = int.Parse(map[leftNode.X, leftNode.Y].ToString());
                paths.Push(leftNode);
            }
            if (CheckIsNodeValid(map, node, rightNode))
            {
                rightNode.Value = int.Parse(map[rightNode.X, rightNode.Y].ToString());
                paths.Push(rightNode);
            }
                
        }

        score = scores;
        rating = ratings;

        
        return trailHead;
    }

    private static bool CheckIsNodeValid(char[,] map, Node checkedNode, Node otherNode)
    {
        return map.IsInside(otherNode.X, otherNode.Y) && int.TryParse(map[otherNode.X, otherNode.Y].ToString(), out int value) && checkedNode.isValueGradual(value);
    }

    private struct Node(int x, int y)
    {
        public int X = x;
        public int Y = y;
        public int Value;

        public bool isValueGradual(int value)
        {
            return value == Value + 1;
        }
    }
}