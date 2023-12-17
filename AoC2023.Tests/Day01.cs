namespace AoC2023.Tests;

[Category("Day 01")]
public class TestsDay01
{
    [Test]
    public void Day01_Problem01()
    {
        var puzzleInput = new List<string>
        {
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet"
        };
        
        Assert.That(Day01.Problem01(puzzleInput), Is.EqualTo(142));
    }

    [Test]
    public void Day01_Problem02()
    {
        var puzzleInput = new List<string>
        {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen"
        };
        
        Assert.That(Day01.Problem02(puzzleInput), Is.EqualTo(281));
    }
}