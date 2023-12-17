namespace AoC2023.Tests
{
    [Category("Day 02")]
    public class TestsDay02
    {
        [Test]
        public void Day02_Problem01()
        {
            var puzzleInput = new List<string>
            {
                "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
                "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
            };

            Day02.GameSet cubeCount = new Day02.GameSet() { Red = 12, Green = 13, Blue = 14 };
                
            Assert.That(Day02.Problem01(puzzleInput, cubeCount), Is.EqualTo(8));
        }

        [Test]
        public void Day02_Problem02()
        {
            var puzzleInput = new List<string>
            {
                "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
                "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
            };
            
            Assert.That(Day02.Problem02(puzzleInput), Is.EqualTo(2286));
        }
    }    
}
