using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        Process.Start("ChartDisplayApp.exe");
        Thread.Sleep(4000); // Wait 4 seconds before starting DataInputApp
        Process.Start("DataInputApp.exe");
        Console.WriteLine("Both apps started. Press Enter to exit host.");
        Console.ReadLine();
    }
}