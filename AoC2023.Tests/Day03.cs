namespace AoC2023.Tests
{
    [Category("Day 03")]
    public class TestsDay03
    {
        [Test]
        public void Day03_Problem01()
        {
            var puzzleInput = new List<string>
            {
                "467..114..",
                "...*......",
                "..35..633.",
                "......#...",
                "617*......",
                ".....+.58.",
                "..592.....",
                "......755.",
                "...$.*....",
                ".664.598.."
            };

            Assert.That(Day03.Problem01(puzzleInput), Is.EqualTo(4361));
        }

        [Test]
        public void Day03_Problem02()
        {
            var puzzleInput = new List<string>
            {
                "467..114..",
                "...*......",
                "..35..633.",
                "......#...",
                "617*......",
                ".....+.58.",
                "..592.....",
                "......755.",
                "...$.*....",
                ".664.598.."
            };

            Assert.That(Day03.Problem02(puzzleInput), Is.EqualTo(467835));
        }
    }    
}
