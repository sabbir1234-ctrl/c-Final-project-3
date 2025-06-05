using System;
using System.Collections.Concurrent;

public class WordAggregator
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, int>> _data = new();

    public void ProcessLine(string line)
    {
        // Format: file.txt:word:count
        string[] parts = line.Split(':');
        if (parts.Length != 3) return;

        string file = parts[0];
        string word = parts[1];
        if (!int.TryParse(parts[2], out int count)) return;

        var fileDict = _data.GetOrAdd(file, _ => new ConcurrentDictionary<string, int>());
        fileDict.AddOrUpdate(word, count, (_, old) => old + count);
    }

    public void PrintResults()
    {
        foreach (var fileEntry in _data)
        {
            foreach (var wordEntry in fileEntry.Value)
            {
                Console.WriteLine($"{fileEntry.Key}:{wordEntry.Key}:{wordEntry.Value}");
            }
        }
    }
}