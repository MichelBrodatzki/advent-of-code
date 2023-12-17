namespace AoC2023.Tests
{
    [Category("Day 06")]
    public class TestsDay06
    {
        [Test]
        public void Day06_Problem01()
        {
            var puzzleInput = new List<string>
            {
                "Time:      7  15   30",
                "Distance:  9  40  200"
            };

            Assert.That(Day06.Problem01(puzzleInput), Is.EqualTo(288));
        }

        [Test]
        public void Day06_Problem02()
        {
            var puzzleInput = new List<string>
            {
                "Time:      7  15   30",
                "Distance:  9  40  200"
            };

            Assert.That(Day06.Problem02(puzzleInput), Is.EqualTo(71503));
        }
    }    
}