namespace AoC2023
{
    public class Day06
    {
        private struct Race
        {
            public long Time;
            public long Distance;
        }

        private static IEnumerable<Race> ParseRaces(IList<string> races, bool ignoreSpaces = false)
        {
            if (races.Count != 2) throw new Exception("Couldn't parse races. Input too long.");
            if (!races[0].StartsWith("Time:") || !races[1].StartsWith("Distance:")) throw new Exception("Couldn't parse races. Input structure invalid.");

            if (ignoreSpaces)
            {
                races[0] = races[0].Replace(" ", "");
                races[1] = races[1].Replace(" ", "");
            }
            
            var time = races[0]
                .Remove(0, 5)
                .Trim()
                .Split(" ")
                .Where(str => str.Length > 0)
                .Select(long.Parse)
                .ToList();
            
            var distance = races[1]
                .Remove(0, 9)
                .Trim()
                .Split(" ")
                .Where(str => str.Length > 0)
                .Select(long.Parse)
                .ToList();

            if (time.Count != distance.Count) throw new Exception("Couldn't parse races. Asymetrical structure.");

            return time.Select((t, index) => new Race() { Time = t, Distance = distance[index] }).ToList();
        }

        private static long GetWinningTimeCount(Race race)
        {
            var minTime = (race.Time - Math.Sqrt(Math.Pow(race.Time, 2) - 4.0 * race.Distance)) / 2.0;
            var maxTime = (race.Time + Math.Sqrt(Math.Pow(race.Time, 2) - 4.0 * race.Distance)) / 2.0;

            return (long)Math.Ceiling(maxTime) - (long)Math.Floor(minTime) - 1; 
        }
        
        public static long Problem01(List<string> puzzleInput)
        {
            var product = 1L;
            
            ParseRaces(puzzleInput)
                .Select(GetWinningTimeCount)
                .ToList()
                .ForEach(amount => product *= amount);
            
            return product;
        }

        public static long Problem02(List<string> puzzleInput)
        {
            return ParseRaces(puzzleInput, true)
                .Select(GetWinningTimeCount)
                .ToList()
                .First();
        }
    }
}