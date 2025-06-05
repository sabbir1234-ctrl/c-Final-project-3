using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class FileIndexer
{
    public static Dictionary<string, Dictionary<string, int>> IndexDirectory(string dir)
    {
        var result = new Dictionary<string, Dictionary<string, int>>();

        foreach (var file in Directory.GetFiles(dir, "*.txt"))
        {
            string content = File.ReadAllText(file);
            string[] words = content
                .Split(new[] { ' ', '\r', '\n', '.', ',', ';', ':', '-', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);

            var wordCounts = words
                .GroupBy(w => w.ToLower())
                .ToDictionary(g => g.Key, g => g.Count());

            result[Path.GetFileName(file)] = wordCounts;
        }

        return result;
    }
}
