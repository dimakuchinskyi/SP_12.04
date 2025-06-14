using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("Reaction Time Game: Press any key as soon as you see 'GO!'");
        Random rnd = new Random();  
        
        int waitTime = rnd.Next(2000, 5000);
        Thread.Sleep(waitTime);

        Console.WriteLine("GO!");

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Console.ReadKey(true);

        stopwatch.Stop();
        Console.WriteLine($"Your reaction time: {stopwatch.ElapsedMilliseconds} ms");
    }
}