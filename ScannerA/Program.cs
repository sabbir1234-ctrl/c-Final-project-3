using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        string directory = args.Length > 0 ? args[0] : Utils.GetDirectoryFromUser();
        string pipeName = "agent1";  // Scanner B will use "agent2"

        // Set CPU affinity to core 1
        Utils.SetProcessorAffinity(1);

        // Thread to read and index files
        Thread readThread = new(() =>
        {
            var index = FileIndexer.IndexDirectory(directory);

            // Thread to send data to master
            Thread sendThread = new(() =>
            {
                PipeSender.SendToMaster(pipeName, index);
            });

            sendThread.Start();
            sendThread.Join();
        });

        readThread.Start();
        readThread.Join();

        Console.WriteLine("Scanner A finished.");
    }
}
