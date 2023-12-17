using System.Text.RegularExpressions;

namespace AoC2023
{
    public partial class Day02
    {
        public struct GameSet
        {
            public int Red;
            public int Green;
            public int Blue;
        }

        /**
         * <summary>Returns the game sets in string representation as a list of GameSets.</summary>
         * <returns>List of GameSets retrieved from the given string.</returns>
         */
        private static IEnumerable<GameSet> GetGameSets(string str)
        {
            var gameMatch = GameRegex().Match(str);
            if (!gameMatch.Success)
            {
                return new List<GameSet>();
            }

            var sets = gameMatch.Groups["game"].Value.Split("; ");
            var gameSets = new List<GameSet>();
            sets.ToList().ForEach(setString =>
            {
                var set = ParseSet(setString);
                gameSets.Add(set);
            });

            return gameSets;
        }
        
        /**
         * <summary>Parse one set to a GameSet object.</summary>
         * <returns>GameSet object from set string.</returns>
         */
        private static GameSet ParseSet(string str)
        {
            var currentSet = new GameSet();

            var ballStrings = str.Split(", ");
            ballStrings.ToList().ForEach(balls =>
            {
                var values = balls.Trim().Split(" ");
                switch (values[1])
                {
                    case "red":
                        currentSet.Red = int.Parse(values[0]);
                        break;
                    case "green":
                        currentSet.Green = int.Parse(values[0]);
                        break;
                    case "blue":
                        currentSet.Blue = int.Parse(values[0]);
                        break;
                }
            });

            return currentSet;
        }

        /**
         * <summary>Calculates the smallest possible amount of balls in the game.</summary>
         *
         * <returns>GameSet object with smallest possible ball count.</returns>
         */
        private static GameSet GetSmallestGameSetForGame(string game)
        {
            var sets = GetGameSets(game);
            var smallestSet = new GameSet() { Red = 0, Green = 0, Blue = 0 };

            sets.ToList().ForEach(set =>
            {
                if (set.Red > smallestSet.Red) smallestSet.Red = set.Red;
                if (set.Green > smallestSet.Green) smallestSet.Green = set.Green;
                if (set.Blue > smallestSet.Blue) smallestSet.Blue = set.Blue;
            });
            
            return smallestSet;
        }
        
        /**
         * <summary>Checks for games that are possible with the given cube count constraint.</summary>
         *
         * <returns>List of game IDs that are possible.</returns>
         */
        private static IEnumerable<int> GetPossibleGames(IEnumerable<string> games, GameSet cubeCount)
        {
            return (from line in games
                let gamePossible = GetGameSets(line)
                    .ToList()
                    .FindAll(set => set.Red > cubeCount.Red || set.Green > cubeCount.Green || set.Blue > cubeCount.Blue)
                    .Count == 0
                where gamePossible
                select int.Parse(GameRegex().Match(line).Groups["gameNumber"].Value)).ToList();
        }

        /**
         * <summary>Calculates the sum of all possible game IDs.</summary>
         *
         * <returns>Sum of all possible game IDs.</returns>
         */
        public static int Problem01(IEnumerable<string> puzzleInput, GameSet cubeCount)
        {
            return GetPossibleGames(puzzleInput, cubeCount).Sum();
        }
        
        /**
         * <summary>Calculates the sum of the power of the smallest possible ball counts for each game</summary>
         *
         * <returns>Sum of the powers of the games.</returns>
         */
        public static int Problem02(IEnumerable<string> puzzleInput)
        {
            return puzzleInput
                .Select(GetSmallestGameSetForGame)
                .Select(smallestSet => smallestSet.Red * smallestSet.Green * smallestSet.Blue)
                .Sum();
        }

        [GeneratedRegex("^Game (?<gameNumber>\\d+): (?<game>.+)", RegexOptions.ExplicitCapture)]
        private static partial Regex GameRegex();
    }    
}
