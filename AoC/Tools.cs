namespace AoC
{
    public class Tools
    {
        public static List<string> GetPuzzleInput(string filePath)
        {
            return new List<string>(File.ReadAllLines(filePath));
        }    
    }
}