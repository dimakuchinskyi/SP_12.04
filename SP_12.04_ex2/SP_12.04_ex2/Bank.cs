using System;
using System.IO;
using System.Threading;

public class Bank
{
    private int _money;
    private string _name;
    private int _percent;
    private readonly object _lock = new object();

    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            StartLoggingThread();
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            StartLoggingThread();
        }
    }

    public int Percent
    {
        get => _percent;
        set
        {
            _percent = value;
            StartLoggingThread();
        }
    }

    private void StartLoggingThread()
    {
        Thread thread = new Thread(LogToFile);
        thread.Start();
    }

    private void LogToFile()
    {
        lock (_lock)
        {
            string log = $"Name: {_name}, Money: {_money}, Percent: {_percent}, Time: {DateTime.Now}\n";
            File.AppendAllText("bank_log.txt", log);
        }
    }
}