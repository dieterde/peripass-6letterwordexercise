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

// filter target words from input
var targets = words.Where(w => w.Length == targetLength).ToArray();

// find and print compositions
var finder = new WordCompositionFinder(dict);
foreach (var target in targets.OrderBy(t => t, StringComparer.Ordinal))
{
    foreach (var parts in finder.SplitIntoKnownWords(target, minParts, maxParts))
    {
        Console.WriteLine($"{string.Join("+", parts)}={target}");
    }
}

Console.WriteLine("=== end execution ===");