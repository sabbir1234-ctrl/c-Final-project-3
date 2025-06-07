using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class FileIndexer
{
    // IndexDirectory takes a directory path and returns a dictionary:
    // - Key: the filename
    // - Value: a dictionary of word counts for that file
    public static Dictionary<string, Dictionary<string, int>> IndexDirectory(string dir)
    {
        // Create a dictionary to store word counts for each file
        var result = new Dictionary<string, Dictionary<string, int>>();

        // Iterate over all .txt files in the specified directory
        foreach (var file in Directory.GetFiles(dir, "*.txt"))
        {
            // Read the full content of the file as a string
            string content = File.ReadAllText(file);

            // Split content into words using common delimiters (space, punctuation, etc.)
            string[] words = content
                .Split(new[] { ' ', '\r', '\n', '.', ',', ';', ':', '-', '?', '!' },
                       StringSplitOptions.RemoveEmptyEntries);

            // Count the occurrences of each word (case-insensitive)
            var wordCounts = words
                .GroupBy(w => w.ToLower()) // Convert to lowercase for consistent counting
                .ToDictionary(g => g.Key, g => g.Count()); // Create dictionary of word -> count

            // Add the word count dictionary to the result using the file name as the key
            result[Path.GetFileName(file)] = wordCounts;
        }

        // Return the complete index: a mapping of filenames to their word frequencies
        return result;
    }
}
