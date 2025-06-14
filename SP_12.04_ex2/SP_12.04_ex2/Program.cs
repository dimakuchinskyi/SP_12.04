using System;

class Program
{
    static void Main()
    {
        Bank bank = new Bank();
        bank.Name = "MyBank";
        bank.Money = 1000;
        bank.Percent = 5;

        Console.WriteLine("Bank properties updated. Check bank_log.txt for logs.");
    }
}