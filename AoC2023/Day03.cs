using System.Numerics;
using System.Text.RegularExpressions;

namespace AoC2023
{
    public partial class Day03
    {
        public struct PartNumber
        {
            public Vector2 Position;
            public int Number;

            public bool Equals(PartNumber other)
            {
                return Position.Equals(other.Position) && Number == other.Number;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Position, Number);
            }
        }

        /**
         * <summary>Retrieves whole number at given point in string.</summary>
         *
         * <returns>PartNumber object with number and position of whole part number in string.</returns>
         */
        private static PartNumber GetPartNumberAtPoint(string line, Vector2 position)
        {
            var index = (int)position.X;
            if (!char.IsDigit(line[index]))
            {
                // Return zero as part number, as the passed parameters don't start at a digit
                return new PartNumber() { Position = Vector2.Zero, Number = 0 };
            }

            var startIndex = index;
            while (char.IsDigit(line[startIndex]))
            {
                startIndex--;
                if (startIndex == -1) break;
            }

            startIndex++;

            var length = 0;
            while (char.IsDigit(line[startIndex + length]))
            {
                length++;
                if (line.Length <= startIndex + length) break;
            }

            return new PartNumber()
            {
                Position = position with { X = startIndex },
                Number = int.Parse(line.Substring(startIndex, length))
            };
        }
        
        /**
         * <summary>Searches for unique part numbers around a point in a list of strings interpreted as a map.</summary>
         *
         * <returns>List of unique part numbers around given point.</returns>
         */
        private static IEnumerable<PartNumber> SearchAroundPoint(IReadOnlyList<string> input, Vector2 position)
        {
            var partNumbers = new HashSet<PartNumber>();

            for (var yDelta = -1; yDelta <= 1; ++yDelta)
            {
                for (var xDelta = -1; xDelta <= 1; ++xDelta)
                {
                    if (char.IsDigit(input[(int)(position.Y) + yDelta][(int)(position.X) + xDelta]))
                    {
                        partNumbers.Add(
                            GetPartNumberAtPoint(
                                    input[(int)(position.Y) + yDelta], 
                                    new Vector2((int)(position.X) + xDelta, (int)(position.Y) + yDelta)
                                )
                            );
                    }
                }    
            }

            return partNumbers;
        }
        
        /**
         * <summary>Calculates the sum of unique part numbers around symbols.</summary>
         *
         * <returns>Sum of unique part numbers around symbols.</returns>
         */
        public static int Problem01(List<string> puzzleInput)
        {
            var partNumbers = new HashSet<PartNumber>();
            
            for (var lineNumber = 0; lineNumber < puzzleInput.Count; ++lineNumber)
            {
                var symbolMatch = SymbolRegex().Match(puzzleInput[lineNumber]);
                while (symbolMatch.Success)
                {
                    SearchAroundPoint(puzzleInput, new Vector2(symbolMatch.Index, lineNumber))
                        .ToList()
                        .ForEach(partNumber => partNumbers.Add(partNumber));

                    symbolMatch = symbolMatch.NextMatch();
                }
            }
            
            return partNumbers.Select(partNumber => partNumber.Number).Sum();
        }

        /**
         * <summary>
         * Calculates the sum of all gear ratios (product of two part numbers around a star) in the given configuration.
         * </summary>
         *
         * <returns>Sum of all gear ratios.</returns>
         */
        public static int Problem02(List<string> puzzleInput)
        {
            var gearRatios = new List<int>();
            
            for (var lineNumber = 0; lineNumber < puzzleInput.Count; ++lineNumber)
            {
                var symbolMatch = StarRegex().Match(puzzleInput[lineNumber]);
                while (symbolMatch.Success)
                {
                    var results = 
                        SearchAroundPoint(puzzleInput, new Vector2(symbolMatch.Index, lineNumber))
                            .ToList();
                    
                    if (results.Count == 2) gearRatios.Add(results.First().Number * results.Last().Number);
                    
                    symbolMatch = symbolMatch.NextMatch();
                }
            }

            return gearRatios.Sum();
        }

        [GeneratedRegex(@"[*]", RegexOptions.IgnoreCase, "de-DE")]
        private static partial Regex StarRegex();
        
        [GeneratedRegex(@"[^a-zA-Z0-9.\r\n]", RegexOptions.IgnoreCase, "de-DE")]
        private static partial Regex SymbolRegex();
    }
}