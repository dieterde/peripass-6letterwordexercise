namespace Peripass.Interview.Domain;

public sealed class WordCompositionFinder
{
    private readonly ISet<string> _dict;

    public WordCompositionFinder(ISet<string> dictionary)
    {
        _dict = dictionary;
    }

    public IEnumerable<IReadOnlyList<string>> SplitIntoKnownWords(string target, int minParts, int? maxParts)
    {
        if (!_dict.Contains(target))
            yield break;

        var memo = new Dictionary<(int start, int? remain), List<List<string>>>();

        foreach (var parts in FindCompositions(target, 0, maxParts, memo))
        {
            if (parts.Count >= minParts)
                yield return parts;
        }
    }

    private IEnumerable<List<string>> FindCompositions(string target, int start, int? remaining, Dictionary<(int start, int? remain), 
                                                       List<List<string>>> memo)
    {
        var key = (start, remaining);
        if (memo.TryGetValue(key, out var cached)) return cached;

        var results = new List<List<string>>();

        if (start == target.Length)
        {
            results.Add(new List<string>());
        }
        else if (remaining is null || remaining.Value > 0)
        {
            for (int end = start + 1; end <= target.Length; end++)
            {
                var piece = target.AsSpan(start, end - start).ToString();
                if (_dict.Contains(piece))
                {
                    var nextRemain = remaining is null ? null : remaining - 1;
                    foreach (var tail in FindCompositions(target, end, nextRemain, memo))
                    {
                        var combined = new List<string>(1 + tail.Count) { piece };
                        combined.AddRange(tail);
                        results.Add(combined);
                    }
                }
            }
        }

        memo[key] = results;
        return results;
    }
}
