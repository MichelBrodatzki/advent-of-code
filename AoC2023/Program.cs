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
        }

        private static void RunDay02(List<string> puzzleInput)
        {
            Day02.GameSet problem01Cubes = new Day02.GameSet() { Red = 12, Green = 13, Blue = 14 };
            
            Console.WriteLine("--- DAY 02 ---");
            Console.WriteLine($"Problem 01: {Day02.Problem01(puzzleInput, problem01Cubes)}");
            Console.WriteLine($"Problem 02: {Day02.Problem02(puzzleInput)}");
        }
        
        private static void RunDay03(List<string> puzzleInput)
        {
            Console.WriteLine("--- DAY 03 ---");
            Console.WriteLine($"Problem 01: {Day03.Problem01(puzzleInput)}");
            Console.WriteLine($"Problem 02: {Day03.Problem02(puzzleInput)}");
        }
        
        private static void RunDay04(List<string> puzzleInput)
        {
            Console.WriteLine("--- DAY 04 ---");
            Console.WriteLine($"Problem 01: {Day04.Problem01(puzzleInput)}");
            Console.WriteLine($"Problem 02: {Day04.Problem02(puzzleInput)}");
        }
        
        private static void RunDay05(List<string> puzzleInput)
        {
            Console.WriteLine("--- DAY 05 ---");
            Console.WriteLine($"Problem 01: {Day05.Problem01(puzzleInput)}");
            Console.WriteLine($"Problem 02: {Day05.Problem02(puzzleInput)}");
        }
        
        private static void RunDay06(List<string> puzzleInput)
        {
            Console.WriteLine("--- DAY 06 ---");
            Console.WriteLine($"Problem 01: {Day06.Problem01(puzzleInput)}");
            Console.WriteLine($"Problem 02: {Day06.Problem02(puzzleInput)}");
        }
        
        public static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            RunDay01(Tools.GetPuzzleInput("../../../Inputs/Day01.txt"));
            watch.Stop();
            Console.WriteLine($"Day 01 finished in {watch.ElapsedMilliseconds}ms");
            Console.WriteLine("");
            
            watch.Restart();
            RunDay02(Tools.GetPuzzleInput("../../../Inputs/Day02.txt"));
            watch.Stop();
            Console.WriteLine($"Day 02 finished in {watch.ElapsedMilliseconds}ms");
            Console.WriteLine("");
            
            watch.Restart();
            RunDay03(Tools.GetPuzzleInput("../../../Inputs/Day03.txt"));
            watch.Stop();
            Console.WriteLine($"Day 03 finished in {watch.ElapsedMilliseconds}ms");
            Console.WriteLine("");
            
            watch.Restart();
            RunDay04(Tools.GetPuzzleInput("../../../Inputs/Day04.txt"));
            watch.Stop();
            Console.WriteLine($"Day 04 finished in {watch.ElapsedMilliseconds}ms");
            Console.WriteLine("");
            
            watch.Restart();
            RunDay05(Tools.GetPuzzleInput("../../../Inputs/Day05.txt"));
            watch.Stop();
            Console.WriteLine($"Day 05 finished in {watch.ElapsedMilliseconds}ms");
            Console.WriteLine("");
            
            watch.Restart();
            RunDay06(Tools.GetPuzzleInput("../../../Inputs/Day06.txt"));
            watch.Stop();
            Console.WriteLine($"Day 06 finished in {watch.ElapsedMilliseconds}ms");
            Console.WriteLine("");
        }
    }
}