namespace Advent2024.Day12;

public class Day12
{
    private static readonly string _exInput = "AAAA\r\nBBCD\r\nBBCC\r\nEEEC";
    private static readonly string _exInput2 = "OOOOO\r\nOXOXO\r\nOOOOO\r\nOXOXO\r\nOOOOO";
    private static readonly string _exInput3 = "RRRRIICCFF\r\nRRRRIICCCF\r\nVVRRRCCFFF\r\nVVRCCCJFFF\r\nVVVVCJJCFE\r\nVVIVCCJJEE\r\nVVIIICJJEE\r\nMIIIIIJJEE\r\nMIIISIJEEE\r\nMMMISSJEEE";
    private static readonly string _exInput4 = "EEEEE\r\nEXXXX\r\nEEEEE\r\nEXXXX\r\nEEEEE";
    private static readonly string _exInput5 = "AAAAAA\r\nAAABBA\r\nAAABBA\r\nABBAAA\r\nABBAAA\r\nAAAAAA";
    
    public static void Solution(out int part1, out int part2)
    {
        var input = Input.Read("Day12");

        var currentInput = _exInput;

        Node[,] plantMap = currentInput.Split(Environment.NewLine).ToNodeMap();
        Map.Visualize(plantMap);

        int totalPrice = 0;
        int totalEdgeSize = 0;

        for (var x = 0; x < plantMap.GetLength(0); x++)
        for (var y = 0; y < plantMap.GetLength(1); y++)
        {
            if (plantMap[x, y].IsVisited) continue;

            int regionSize = 0, perimeters = 0, edgeSize = 0;
            List<(Node, Node)> pairs = new List<(Node, Node)>();

            edgeSize = plantMap[x, y].GetRegionEdgeSize_Day12(ref plantMap);

            plantMap[x, y].SearchSameCharacterNodes_Day12(ref plantMap, ref regionSize, ref perimeters, ref pairs);
            
            perimeters -= pairs.Count * 2;
            int price = regionSize * perimeters;
            
            // Console.WriteLine(
            //     $"Character {plantMap[x, y].Character}'s price: {price} , region size: {regionSize} , perimeters: {perimeters} ");

            totalPrice += price;
            totalEdgeSize += edgeSize;
        }
        
        part1 = totalPrice;
        part2 = totalEdgeSize;
    }

    
}