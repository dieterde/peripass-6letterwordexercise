namespace Peripass.Interview.Adapters;

public static class FileInputReader
{
    public static async Task<List<string>> ReadWordsAsync(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Input file not found: {path}");
        }

        var lines = await File.ReadAllLinesAsync(path);
        return lines.Select(l => l.Trim())
                    .Where(l => !string.IsNullOrEmpty(l))
                    .Distinct(StringComparer.Ordinal)
                    .ToList();
    }
}