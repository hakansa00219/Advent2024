namespace Advent2024;

public record struct Node(int X, int Y, char Character)
{
    public int X { get; init; } = X;
    public int Y { get; init; } = Y;
    public char Character { get; init; } = Character;
    public bool IsVisited { get; set; } = false;
    public Dictionary<char,bool> borderChecks = new Dictionary<char, bool>();
    
    public bool Equals(Node obj)
    {
        return obj.X == X && obj.Y == Y;
    }

    public override int GetHashCode()
    {
        return (X, Y).GetHashCode();
    }
    public void SearchSameCharacterNodes_Day12(ref Node[,] nodeMap, ref int regionSize, ref int perimeters, ref List<(Node,Node)> pairs)
    {
        List<(int X, int Y)> neighbours = new List<(int X, int Y)>()
        {
            (-1, 0),
            (1, 0),
            (0, -1),
            (0, 1)
        };

        if (!IsVisited)
        {
            IsVisited = true;
            regionSize++;
            perimeters += 4;
        }


        for (var i = 0; i < neighbours.Count; i++)
        {
            int neighbourX = X + neighbours[i].X;
            int neighbourY = Y + neighbours[i].Y;

            if (!nodeMap.IsInside(neighbourX, neighbourY)) continue;
            
            var neighbourNode = nodeMap[neighbourX, neighbourY];
            
            if (neighbourNode.Character != Character) continue;
            
            if(!pairs.Contains((this,neighbourNode)) && !pairs.Contains((neighbourNode, this)))
            {
                // Console.WriteLine((this, neighbourNode + " pair added"));
                pairs.Add((this, neighbourNode));
            }
            
            if (neighbourNode.IsVisited) continue;
            
            nodeMap[neighbourX, neighbourY].IsVisited = true;
            regionSize++;
            perimeters += 4;

            nodeMap[neighbourX, neighbourY].SearchSameCharacterNodes_Day12(ref nodeMap, ref regionSize, ref perimeters, ref pairs);
        }

        
    }
    
    public int GetRegionEdgeSize_Day12(ref Node[,] plantMap)
    {
        (int X, int Y, Direction D) startNode = (X, Y, Direction.Right);

        (int X, int Y, Direction D) currentNode = startNode;
        (int X, int Y, Direction D) nextNode = (X, Y + 1, Direction.Right);
        Console.WriteLine(startNode);
        int edgeCount = 0;
        do
        {
            if (!plantMap.IsInside(nextNode.X, nextNode.Y))
            {
                //Change only direction do not move position
                currentNode = TurnDirection(currentNode.X, currentNode.Y, currentNode.D);
                nextNode = ContinueDirection(currentNode.X, currentNode.Y, currentNode.D);
                Console.WriteLine("Is not inside: " + currentNode);
                edgeCount++;
                continue;
            }

            if (plantMap[nextNode.X, nextNode.Y].IsVisited)
            {
                currentNode = TurnDirection(currentNode.X, currentNode.Y, currentNode.D);
                Console.WriteLine("IsVisited " + currentNode);
                nextNode = currentNode;
                edgeCount++;
                continue;
            }
            
            if (plantMap[nextNode.X, nextNode.Y].Character != Character)
            {
                currentNode = TurnDirection(currentNode.X, currentNode.Y, currentNode.D);
                nextNode = ContinueDirection(currentNode.X, currentNode.Y, currentNode.D);
                Console.WriteLine("Character Dif " + currentNode);
                edgeCount++;
                continue;
            }

            currentNode = nextNode;
            
            Console.WriteLine("Continue Dir " + currentNode);
            nextNode = ContinueDirection(nextNode.X, nextNode.Y, nextNode.D);
            
        }while(currentNode.X != startNode.X || currentNode.Y != startNode.Y || currentNode.D != startNode.D);

        Console.WriteLine($"{Character} character edgeCount: {edgeCount}");
        return edgeCount;
    }
    
    public (int X, int Y, Direction D) TurnDirection(int posX, int posY, Direction direction)
    {
        return direction switch
        {
            Direction.Up => (posX, posY, Direction.Right),
            Direction.Down => (posX, posY, Direction.Left),
            Direction.Left => (posX, posY, Direction.Up),
            Direction.Right => (posX, posY, Direction.Down),
        };
    }


    public (int X, int Y, Direction D) ContinueDirection(int posX, int posY, Direction direction)
    {
        return direction switch
        {
            Direction.Up => (posX - 1, posY, Direction.Up),
            Direction.Down => (posX + 1, posY, Direction.Down),
            Direction.Right => (posX, posY + 1, Direction.Right),
            Direction.Left => (posX, posY - 1, Direction.Left),
        };
    }

    public struct Direct
    {
        public Direction First;
        public Direction Second;
        public Direction Third;
        public Direction Fourth;
    }

    public enum Direction
    { 
        Up, Down, Left, Right
    }
}