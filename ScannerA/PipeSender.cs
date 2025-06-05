using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;

public static class PipeSender
{
    public static void SendToMaster(string pipeName, Dictionary<string, Dictionary<string, int>> data)
    {
        using var pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out);
        pipeClient.Connect();

        using var writer = new StreamWriter(pipeClient) { AutoFlush = true };

        foreach (var fileEntry in data)
        {
            foreach (var wordEntry in fileEntry.Value)
            {
                writer.WriteLine($"{fileEntry.Key}:{wordEntry.Key}:{wordEntry.Value}");
            }
        }
    }
}
