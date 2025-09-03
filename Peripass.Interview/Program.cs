using System.Collections.Immutable;
using Peripass.Interview.Adapters;
using Peripass.Interview.Domain;

Console.WriteLine("----------------------------------------------------");
Console.WriteLine("Peripass interview - Technical Developer test - v0.1");
Console.WriteLine("----------------------------------------------------");

Console.WriteLine("=== start execution ===");

// config
var inputPath = args.Length > 0 ? args[0] : "input.txt";
int targetLength = 6;
int minParts = 2;
int? maxParts = null;

// check if file exists
if (!File.Exists(inputPath))
{
    Console.WriteLine($"Error: file '{inputPath}' not found.");
    return;
}

// load input file and words
var words = await FileInputReader.ReadWordsAsync(inputPath);
var dict = words.ToImmutableHashSet(StringComparer.Ordinal);

// define targets words
var targets = dict.Where(w => w.Length == targetLength)
                  .OrderBy(w => w, StringComparer.Ordinal);

// find and print compositions
var finder = new WordCompositionFinder(dict);

foreach (var target in targets)
{
    var lines = finder.Find(target, minParts, maxParts)
                      .Select(parts => string.Join("+", parts))
                      .Distinct()
                      .OrderBy(s => s, StringComparer.Ordinal);

    foreach (var line in lines)
        Console.WriteLine($"{line}={target}");
}

Console.WriteLine("=== end execution ===");