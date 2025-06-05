class Program
{
    static void Main()
    {
        // Set affinity to CPU core 3
        Utils.SetProcessorAffinity(3);

        var aggregator = new WordAggregator();

        var listener1 = new PipeListener("agent1", aggregator.ProcessLine);
        var listener2 = new PipeListener("agent2", aggregator.ProcessLine);

        listener1.Start();
        listener2.Start();

        Console.WriteLine("Master is listening to both agents. Press any key to exit...");
        Console.ReadKey();

        Console.WriteLine("\nFinal Aggregated Word Index:");
        aggregator.PrintResults();
    }
}