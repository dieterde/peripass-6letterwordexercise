namespace Peripass.Interview.Domain;

public sealed class WordCompositionFinder
{
    private readonly ISet<string> _dict;

    public WordCompositionFinder(ISet<string> dictionary)
    {
        _dict = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
    }

    public IEnumerable<IReadOnlyList<string>> Find(string target, int minParts = 2, int? maxParts = null)
    {
        if (string.IsNullOrWhiteSpace(target)) yield break;
        if (!_dict.Contains(target)) yield break;

        var current = new List<string>(capacity: 6);

        foreach (var parts in Enumerate(target, 0, 0, current, maxParts))
        {
            if (parts.Count >= minParts)
                yield return parts;
        }
    }

    private IEnumerable<IReadOnlyList<string>> Enumerate(string target, int start, int usedParts, List<string> current,
                                                         int? maxParts)
    {
        if (start == target.Length)
        {
            yield return current.ToArray();
            yield break;
        }

        if (maxParts.HasValue && usedParts >= maxParts.Value)
            yield break;

        for (int end = start + 1; end <= target.Length; end++)
        {
            var piece = target.Substring(start, end - start);
            if (_dict.Contains(piece))
            {
                current.Add(piece);

                foreach (var result in Enumerate(target, end, usedParts + 1, current, maxParts))
                    yield return result;

                current.RemoveAt(current.Count - 1);
            }
        }
    }
}
