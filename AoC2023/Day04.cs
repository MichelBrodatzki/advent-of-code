namespace AoC2023
{
    public class Day04
    {
        /**
         * <summary>Extracts the elf's numbers from a given scratchcard.</summary>
         *
         * <returns>List containing the elf's numbers.</returns>
         */
        private static IEnumerable<int> GetOwnNumbers(string card)
        {
            if (!card.StartsWith("Card"))
            {
                // Not a valid card string
                return new List<int>();
            }

            // The elf's numbers are after the pipe symbol
            var numberParts = card.Split("|");

            if (numberParts.Length != 2)
            {
                // Not a valid card string
                return new List<int>();
            }

            // The numbers are seperated by one or more spaces
            return numberParts[1]
                .Trim()
                .Split(" ")
                .Select(number => number.Trim())
                .Where(number => number.Length > 0)
                .Select(int.Parse);
        }
        
        /**
         * <summary>Extracts the winning numbers from a given scratchcard.</summary>
         *
         * <returns>List containing the winning numbers.</returns>
         */
        private static IEnumerable<int> GetWinningNumbers(string card)
        {
            if (!card.StartsWith("Card"))
            {
                // Not a valid card string
                return new List<int>();
            }

            // The winning numbers are in front of the pipe symbol
            var numberParts = card.Split("|");

            if (numberParts.Length != 2)
            {
                // Not a valid card string
                return new List<int>();
            }

            // The winning numbers stand after the 'Card [number]:' prefix
            numberParts = numberParts[0].Split(":");
            if (numberParts.Length != 2)
            {
                // Not a valid card string
                return new List<int>();
            }

            // The numbers are seperated by one or more spaces
            return numberParts[1]
                .Trim()
                .Split(" ")
                .Select(number => number.Trim())
                .Where(number => number.Length > 0)
                .Select(int.Parse);
        }
        
        /**
         * <summary>
         * Calculates the total point score for all scratchcards after evaluating the winning numbers.
         * </summary>
         *
         * <returns>Sum of all points.</returns>
         */
        public static int Problem01(List<string> puzzleInput)
        {
            var points = 0;
            
            puzzleInput.ForEach(card =>
            {
                // Get the amount of correct numbers by intersecting the winning numbers with the elf's numbers
                var correctNumbers = GetWinningNumbers(card).Intersect(GetOwnNumbers(card)).Count();
                
                // The first correct number gives 1 point
                // All further correct number double the points.
                // This means that the total score is $2^{n-1}$ for all scratch cards with one or more correct numbers
                if (correctNumbers > 0) points += (int)Math.Pow(2, correctNumbers - 1);
            });
            
            return points;
        }   
        
        
        /**
         * <summary>Returns the total amount of scratch card after evaluating the winning numbers.</summary>
         *
         * <returns>Total amount of scratchcards.</returns>
         */
        public static int Problem02(List<string> puzzleInput)
        {
            // Dictionary which maps the card number to the amount of cards the elf has
            var cardCopies = new Dictionary<int, int>();

            for (var cardNumber = 0; cardNumber < puzzleInput.Count; ++cardNumber)
            {
                // Add the current card to the dictionary, or if it already exists, add one copy
                if (!cardCopies.TryAdd(cardNumber, 1))
                {
                    cardCopies[cardNumber]++;
                }
                
                var correctNumbers = GetWinningNumbers(puzzleInput[cardNumber])
                    .Intersect(GetOwnNumbers(puzzleInput[cardNumber])).Count();
                var multiplicator = cardCopies[cardNumber];

                // Add the correct amount of copies to the next N cards
                for (var modifyCardOffset = 1; modifyCardOffset <= correctNumbers; ++modifyCardOffset)
                {
                    if (cardNumber + modifyCardOffset >= puzzleInput.Count) break;
                    
                    // If a subsequent card has no representation in the cardsCopies dictionary,
                    // add it with the correct amount of copies, else just add the copy count from this card
                    if (!cardCopies.TryAdd(cardNumber + modifyCardOffset, multiplicator))
                    {
                        cardCopies[cardNumber + modifyCardOffset] += multiplicator;
                    }
                }
            }

            // Sum all copy amounts
            return cardCopies.Select(copy => copy.Value).Sum();
        }   
    }    
}