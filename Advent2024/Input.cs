namespace Advent2024;

public static class Input
{
    public static string Read(string filename)
    {
        string relativePath = $"Inputs/{filename}.txt";
        string inputPath = Path.Combine($"{Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent}", relativePath);
        return File.ReadAllText(inputPath);
    }
}