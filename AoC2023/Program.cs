using AoC;

namespace AoC2023
{
    public class Program
    {
        private static void RunDay01(List<string> puzzleInput)
        {
            Console.WriteLine("--- DAY 01 ---");
            Console.WriteLine($"Problem 01: {Day01.Problem01(puzzleInput)}");
            Console.WriteLine($"Problem 02: {Day01.Problem02(puzzleInput)}");
            Console.WriteLine("");
        }

        private static void RunDay02(List<string> puzzleInput)
        {
            Day02.GameSet problem01Cubes = new Day02.GameSet() { Red = 12, Green = 13, Blue = 14 };
            
            Console.WriteLine("--- DAY 02 ---");
            Console.WriteLine($"Problem 01: {Day02.Problem01(puzzleInput, problem01Cubes)}");
            Console.WriteLine($"Problem 02: {Day02.Problem02(puzzleInput)}");
            Console.WriteLine("");
        }
        
        public static void Main(string[] args)
        {
            RunDay01(Tools.GetPuzzleInput("../../../Inputs/Day01.txt"));
            RunDay02(Tools.GetPuzzleInput("../../../Inputs/Day02.txt"));
        }
    }
}