using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;

public static class PipeSender
{
    // Sends the provided word count data to a named pipe server (e.g., the "Master" process).
    // Parameters:
    // - pipeName: the name of the named pipe to connect to.
    // - data: a nested dictionary where:
    //     - The key is the filename (string),
    //     - The value is another dictionary containing word counts (word -> count).
    public static void SendToMaster(string pipeName, Dictionary<string, Dictionary<string, int>> data)
    {
        // Create a named pipe client stream to connect to the specified pipe on the local machine.
        using var pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out);

        // Connect to the pipe server. This will block until the server is ready.
        pipeClient.Connect();

        // Create a StreamWriter to send text data through the pipe.
        // AutoFlush = true ensures each line is written immediately to the pipe.
        using var writer = new StreamWriter(pipeClient) { AutoFlush = true };

        // Iterate through each file's word count dictionary.
        foreach (var fileEntry in data)
        {
            string fileName = fileEntry.Key;
            var wordCounts = fileEntry.Value;

            // Iterate through each word and its count in the current file.
            foreach (var wordEntry in wordCounts)
            {
                string word = wordEntry.Key;
                int count = wordEntry.Value;

                // Write the data to the pipe in the format: filename:word:count
                writer.WriteLine($"{fileName}:{word}:{count}");
            }
        }
    }
}
