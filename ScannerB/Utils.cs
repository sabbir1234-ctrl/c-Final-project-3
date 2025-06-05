using System;
using System.Diagnostics;

public static class Utils
{
    public static string GetDirectoryFromUser()
    {
        Console.Write("Enter directory path: ");
        return Console.ReadLine();
    }

    public static void SetProcessorAffinity(int core)
    {
        Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)(1 << core);
    }
}
