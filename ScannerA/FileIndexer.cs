using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class FileIndexer
{
    // This method takes a directory path and returns a dictionary that maps
    // each .txt filename to a dictionary of word counts found in that file.
    public static Dictionary<string, Dictionary<string, int>> IndexDirectory(string dir)
    {
        // Outer dictionary to hold results:
        // Key: filename, Value: dictionary of word counts for that file.
        var result = new Dictionary<string, Dictionary<string, int>>();

        // Loop through all .txt files in the specified directory
        foreach (var file in Directory.GetFiles(dir, "*.txt"))
        {
            // Read the entire content of the file as a single string
            string content = File.ReadAllText(file);

            // Split the content into words using common delimiters
            string[] words = content
                .Split(new[] { ' ', '\r', '\n', '.', ',', ';', ':', '-', '?', '!' },
                       StringSplitOptions.RemoveEmptyEntries);

            // Group words by lowercase version (to treat "Word" and "word" the same),
            // then count occurrences of each word
            var wordCounts = words
                .GroupBy(w => w.ToLower())
                .ToDictionary(g => g.Key, g => g.Count());

            // Store the word count dictionary in the result using the filename as the key
            result[Path.GetFileName(file)] = wordCounts;
        }

        // Return the final result mapping filenames to their word count dictionaries
        return result;
    }
}
