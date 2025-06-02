using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

public class PipeListener
{
    private readonly string _pipeName;
    private readonly Action<string> _onMessage;

    public PipeListener(string pipeName, Action<string> onMessage)
    {
        _pipeName = pipeName;
        _onMessage = onMessage;
    }

    public void Start()
    {
        Thread thread = new(() =>
        {
            using var pipeServer = new NamedPipeServerStream(_pipeName, PipeDirection.In);
            Console.WriteLine($"Waiting for connection on pipe: {_pipeName}");
            pipeServer.WaitForConnection();
            Console.WriteLine($"Connected to {_pipeName}");

            using var reader = new StreamReader(pipeServer);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                _onMessage(line);
            }

            Console.WriteLine($"Pipe {_pipeName} finished receiving.");
        });

        thread.Start();
    }
}
