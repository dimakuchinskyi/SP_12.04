using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        List<object> items = new List<object> { 1, "hello", 3.14, DateTime.Now };

        Thread thread = new Thread(() => PrintCollection(items));
        thread.Start();
        thread.Join();
    }

    static void PrintCollection(IEnumerable<object> collection)
    {
        foreach (var item in collection)
        {
            Console.WriteLine(item.ToString());
        }
    }
}