using System.Text.RegularExpressions;

namespace AoC2023
{
    public class Day01
    {
        /**
         * <summary>Extracts all digits from <c>str</c> and returns a sorted dictionary with all digits
         * and their indices.</summary>
         *
         * <returns>Dictionary which maps the characters index in the string to its digit value.</returns>
         */
        private static SortedDictionary<int, int> ExtractDigits(string str)
        {
            var digits = new SortedDictionary<int, int>();
            var index = 0;
            str.ToList().ForEach(c =>
            {
                if (char.IsDigit(c))
                {
                    digits.Add(index, c - '0');
                }

                index++;
            });
            return digits;
        }

        /**
         * <summary>Extracts all spelt-out digits (e.g. 'one', 'two', ...) from <c>str</c> and returns a sorted
         * dictionary with all digits and their indices.</summary>
         *
         * <returns>Dictionary which maps the characters index in the string to its digit value.</returns>
         */
        private static SortedDictionary<int, int> ExtractSpeltOutDigits(string str)
        {
            // Map of all spelt out digits to their numeric counterparts
            var words = new Dictionary<string, int>
            {
                ["zero"] = 0, ["one"] = 1, ["two"] = 2, ["three"] = 3, ["four"] = 4, ["five"] = 5, ["six"] = 6,
                ["seven"] = 7, ["eight"] = 8, ["nine"] = 9
            };
            
            var digits = new SortedDictionary<int, int>();

            // Try to match all spelt out digits with regex, but don't replace them!
            // There may be overlaps that get destroyed by replacing.
            // Only save the match's indices and the respective digit to the digits dict.
            foreach (var word in words.Keys)
            {
                var currentRegex = new Regex(word, RegexOptions.IgnoreCase);

                var m = currentRegex.Match(str);
                while (m.Success)
                {
                    digits.Add(m.Index, words[word]);
                    m = m.NextMatch();
                }
            }

            return digits;
        }

        /**
         * <summary>
         * Combines the returned dictionaries from the methods <c>ExtractDigits</c> and <c>ExtractSpeltOutDigits</c>
         * </summary>
         *
         * <returns>The combined dictionary of all digits in the given string</returns>
         */
        private static SortedDictionary<int, int> ExtractAllDigits(string str)
        {
            // Merge both dictionaries
            var digitsInLine = ExtractDigits(str);
            ExtractSpeltOutDigits(str).ToList().ForEach(digit => digitsInLine.Add(digit.Key, digit.Value));
            return digitsInLine;
        }
        
        /**
         * <summary>Appends the last digit in a line to the first digit of the same line and returns the sum of these
         * numbers for all lines.</summary>
         *
         * <returns>Sum of all newly built numbers from the lines.</returns>
         */
        public static int Problem01(IEnumerable<string> puzzleInput)
        {
            return puzzleInput
                .Select(ExtractDigits)
                .Select(digitsInLine => digitsInLine.First().Value * 10 + digitsInLine.Last().Value)
                .Sum();
        }

        /**
         * <summary>Appends the last digit in a line to the first digit of the same line and returns the sum of these
         * numbers for all lines. This method also looks for spelt out digits.</summary>
         *
         * <returns>Sum of all newly built numbers from the lines.</returns>
         */
        public static int Problem02(IEnumerable<string> puzzleInput)
        {
            return puzzleInput
                .Select(ExtractAllDigits)
                .Select(digitsInLine => digitsInLine.First().Value * 10 + digitsInLine.Last().Value)
                .Sum(); 
        }
    }
}