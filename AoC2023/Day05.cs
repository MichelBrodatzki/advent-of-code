using System.Text.RegularExpressions;
using AoC;

namespace AoC2023
{
    public partial class Day05
    {
        public partial class Almanac
        {
            public class PathNotFoundException : Exception
            {
                public PathNotFoundException(string message) 
                    : base(message)
                {
                }
            }

            public class MapNotFoundException : Exception
            {
                public MapNotFoundException(string message)
                    : base(message)
                {
                }
            }
            
            public struct CategoryMap
            {
                public string Source;
                public string Destination;
            }

            public struct Range
            {
                public long Start;
                public long End;
            }
            
            private readonly List<Range> _seeds = new();

            private readonly Dictionary<CategoryMap, Dictionary<Range, Range>> _maps = new();

            /// <summary>
            /// Parses a line containing the seeds and adds it to this class' seed array.
            /// </summary>
            /// <param name="seedLine">The line containing the seed numbers to parse</param>
            /// <param name="useSeedRange">
            /// Set to true, if every second number is a range length and not another seed
            /// </param>
            /// <exception cref="InputFileInvalidException">
            /// Thrown when <c>seedLine</c> isn't a line containing seed numbers.
            /// </exception>
            private void ParseSeeds(string seedLine, bool useSeedRange = false)
            {
                if (!seedLine.StartsWith("seeds:")) return;
                
                var splitLine = seedLine.Split(":");
                if (splitLine.Length != 2) return;

                // Problem 02 needs a different parsing algorithm
                if (!useSeedRange)
                {
                    splitLine[1]
                        .Split(" ")
                        .Select(str => str.Trim())
                        .Where(str => str.Length > 0)
                        .Select(long.Parse)
                        .ToList()
                        .ForEach(seed => { _seeds.Add(new Range() { Start = seed, End = seed }); });
                }
                else
                {
                    var numbers = splitLine[1]
                        .Split(" ")
                        .Select(str => str.Trim())
                        .Where(str => str.Length > 0)
                        .Select(long.Parse)
                        .ToList();

                    if (numbers.Count % 2 != 0) 
                        throw new InputFileInvalidException("Input file isn't correctly structured at seeds");

                    for (var seedIndex = 0; seedIndex < numbers.Count; seedIndex += 2)
                    {
                        _seeds.Add(new Range()
                        {
                            Start = numbers[seedIndex],
                            End = numbers[seedIndex] + numbers[seedIndex + 1] - 1
                        });
                    }
                }
            }
            
            /// <summary>
            /// Constructor for the almanac.
            /// </summary>
            /// <param name="almanac">Lines of the almanac</param>
            /// <param name="useSeedRange">
            /// Set to true, if every second number is a range length and not another seed
            /// </param>
            public Almanac(List<string> almanac, bool useSeedRange = false)
            {
                var currentMap = new CategoryMap() { Source = "", Destination = "" };

                almanac.ForEach(line =>
                {
                    // Check if it's a line containing seeds
                    if (line.StartsWith("seeds:"))
                    {
                        ParseSeeds(line, useSeedRange);
                        return;
                    }

                    // Don't just take the example maps, as there could be more in the real input
                    var categoryMatch = CategoryRegex().Match(line);
                    if (categoryMatch.Success)
                    {
                        currentMap = new CategoryMap()
                        {
                            Source = categoryMatch.Groups["sourceCategory"].Value,
                            Destination = categoryMatch.Groups["destinationCategory"].Value
                        };

                        _maps.TryAdd(currentMap, new Dictionary<Range, Range>());
                        return;
                    }

                    // Stop here if the currentMap isn't set properly
                    if (currentMap.Source == "" || currentMap.Destination == "") return;
                    
                    // Parse mapping line
                    var mapMatch = MapRegex().Match(line);
                    if (!mapMatch.Success) return;

                    _maps[currentMap].Add(
                        new Range()
                        {
                            Start = long.Parse(mapMatch.Groups["sourceStart"].Value), 
                            End = long.Parse(mapMatch.Groups["sourceStart"].Value)
                                + long.Parse(mapMatch.Groups["rangeLength"].Value) - 1
                        },
                        new Range()
                        {
                            Start = long.Parse(mapMatch.Groups["destinationStart"].Value), 
                            End = long.Parse(mapMatch.Groups["destinationStart"].Value)
                                + long.Parse(mapMatch.Groups["rangeLength"].Value) - 1
                        }
                    );
                });
            }

            /// <summary>
            /// Finds a mapped path between <c>sourceCategory</c> and <c>destinationCategory</c>.
            /// It's mandatory that there's only one way from source to destination. 
            /// </summary>
            /// <param name="sourceCategory">Name of the source category</param>
            /// <param name="destinationCategory">Name of the destination category</param>
            /// <returns>
            /// Returns the names of the maps between <c>sourceCategory</c> and <c>destinationCategory</c>
            /// including both.
            /// </returns>
            /// <exception cref="InputFileInvalidException">
            /// Thrown when the input file isn't structured properly.
            /// </exception>
            /// <exception cref="PathNotFoundException">
            /// Thrown if no path between <c>sourceCategory</c> and <c>destinationCategory</c> has been found.
            /// </exception>
            private List<string> FindPath(string sourceCategory, string destinationCategory)
            {
                var path = new List<string>() { sourceCategory };

                // Catch infinite loops if a path element isn't found
                var foundAnythingInCurrentPass = true;
                while (path.Last() != destinationCategory && foundAnythingInCurrentPass)
                {
                    var nextStep = _maps
                        .Where(map => map.Key.Source == path.Last())
                        .ToList();
                    
                    // In this case mapping is a one-to-one relationship
                    if (nextStep.Count > 1) throw new InputFileInvalidException("Mapping is not one-to-one");
                    
                    foundAnythingInCurrentPass = nextStep.Any();
                    
                    if (nextStep.Any()) path.Add(nextStep.First().Key.Destination);
                }

                // If the path isn't complete, error out
                if (path.Last() != destinationCategory) throw new PathNotFoundException("Path not found!");
                
                return path;
            }

            /// <summary>
            /// Finds and returns the map between <c>sourceCategory</c> and <c>destinationCategory</c>
            /// </summary>
            /// <param name="sourceCategory"></param>
            /// <param name="destinationCategory"></param>
            /// <returns>Map between <c>sourceCategory</c> and <c>destinationCategory</c></returns>
            /// <exception cref="MapNotFoundException">
            /// Thrown when no map between <c>sourceCategory</c> and <c>destinationCategory</c> was found
            /// </exception>
            /// <exception cref="InputFileInvalidException">
            /// Thrown when the input file isn't structured properly.
            /// </exception>
            private Dictionary<Range, Range> GetMap(string sourceCategory, string destinationCategory)
            {
                var relevantMap = _maps
                    .Where(map =>
                        map.Key.Source == sourceCategory && map.Key.Destination == destinationCategory)
                    .ToList();

                if (relevantMap.Count == 0)
                    throw new MapNotFoundException(
                        $"Map from {sourceCategory} to {destinationCategory} not found"
                        );
                if (relevantMap.Count != 1) throw new InputFileInvalidException("Maps are not unique");

                return relevantMap.First().Value;
            }
            
            /// <summary>
            /// Returns the mapped ranges from <c>sourceCategory</c> to <c>destinationCategory</c>
            /// </summary>
            /// <param name="sourceCategory">The source category</param>
            /// <param name="destinationCategory">The destination category</param>
            /// <param name="sourceValues">The values to be mapped</param>
            /// <returns>Mapped values from <c>sourceCategory</c> to <c>destinationCategory</c></returns>
            /// <exception cref="InputFileInvalidException">
            /// Thrown when the input file isn't structured properly.
            /// </exception>
            private IEnumerable<Range> Get(string sourceCategory, string destinationCategory, List<Range> sourceValues)
            {
                var path = FindPath(sourceCategory, destinationCategory);
                for (var currentCategoryIndex = 0; currentCategoryIndex < path.Count - 1; ++currentCategoryIndex)
                {
                    var mapDict = 
                        GetMap(path[currentCategoryIndex], path[currentCategoryIndex + 1]);
                    var newValues = new List<Range>();
                    
                    foreach (var val in sourceValues)
                    {
                        var finished = false;
                        var remainingSourceRange = val;

                        while (!finished)
                        {
                            // Check if the current remaining source range's start fits inside an available map
                            var mapCandidates = mapDict
                                .Where(map =>
                                    map.Key.Start <= remainingSourceRange.Start &&
                                    map.Key.End >= remainingSourceRange.Start)
                                .ToList();
                            
                            if (mapCandidates.Count > 1) throw new InputFileInvalidException("Map has more than one mapping");
                            
                            if (!mapCandidates.Any())
                            {
                                // Check if there's an available map that starts inside the
                                // current remaining source range
                                mapCandidates = mapDict
                                    .Where(map =>
                                        map.Key.Start >= remainingSourceRange.Start &&
                                        map.Key.Start <= remainingSourceRange.End)
                                    .ToList();

                                if (!mapCandidates.Any())
                                {
                                    // If no available map overlaps the current remaining range, it's not mappable,
                                    // so just save it as it is and be done with the current source value
                                    newValues.Add(remainingSourceRange);
                                    finished = true;
                                    continue;
                                }
                                
                                // Save the part of the current remaining range that doesn't intersect the found map,
                                // as it can't be mapped.
                                // Adjust the remaining source range's start to the newly found map's start.
                                newValues.Add(remainingSourceRange with { End = mapCandidates.First().Key.Start - 1 });
                                remainingSourceRange.Start = mapCandidates.First().Key.Start;
                            }

                            var map = mapCandidates.First();

                            if (map.Key.End < remainingSourceRange.End)
                            {
                                // The end of the current range is after the map's source range's end
                                // Map what we can and adapt the start of the remaining source range
                                newValues.Add(map.Value with {
                                    Start = map.Value.Start + (remainingSourceRange.Start - map.Key.Start) 
                                });
                                remainingSourceRange.Start = map.Key.End + 1;
                            }
                            else
                            {
                                // The current range fits completely in the map's source range
                                // Map it and finish the current source value
                                newValues.Add(new Range() { 
                                    Start = map.Value.Start + (remainingSourceRange.Start - map.Key.Start),
                                    End = map.Value.Start + (remainingSourceRange.End - map.Key.Start) 
                                });
                                finished = true;
                            }
                        }
                    }

                    sourceValues = newValues;
                }

                return sourceValues;
            }
            
            /// <summary>
            /// Returns the mapped to <c>category</c> ranges from the input seeds of the Almanac.
            /// </summary>
            /// <param name="category">The destination category</param>
            /// <returns>Mapped ranges</returns>
            public IEnumerable<Range> GetFromInputSeeds(string category)
            {
                return Get("seed", category, _seeds);
            }
            
            [GeneratedRegex(@"^(?<sourceCategory>\w+)-to-(?<destinationCategory>\w+) map:$", 
                RegexOptions.IgnoreCase)]
            private static partial Regex CategoryRegex();

            [GeneratedRegex(@"^(?<destinationStart>\d+) (?<sourceStart>\d+) (?<rangeLength>\d+)$",
                RegexOptions.IgnoreCase)]
            private static partial Regex MapRegex();
        }
        
        /// <summary>
        /// Treats the seed line as seperate seeds and returns the lowest location number.
        /// </summary>
        /// <param name="puzzleInput">The almanac in text form</param>
        /// <returns>Lowest location number after mapping</returns>
        public static long Problem01(List<string> puzzleInput)
        {
            var islandIslandsAlmanac = new Almanac(puzzleInput);
            
            return islandIslandsAlmanac
                .GetFromInputSeeds("location")
                .Select(range => range.Start)
                .Min();
        }

        /// <summary>
        /// Treats the seed line as seeds with their range lengths and returns the lowest location number.
        /// </summary>
        /// <param name="puzzleInput">The almanac in text form</param>
        /// <returns>Lowest location number after mapping</returns>
        public static long Problem02(List<string> puzzleInput)
        {
            var islandIslandsAlmanac = new Almanac(puzzleInput, true);
            
            return islandIslandsAlmanac
                .GetFromInputSeeds("location")
                .Select(range => range.Start)
                .Min();
        }
    }
}