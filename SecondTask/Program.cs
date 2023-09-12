using System;
using System.IO;
using System.Threading;

class Bank
{
    private int money;
    private string name;
    private int percent;
    private object lockObject = new object();
    private Thread writerThread;

    public int Money
    {
        get => money;
        set
        {
            lock (lockObject)
            {
                money = value;
                StartWriterThread();
            }
        }
    }

    public string Name
    {
        get => name;
        set
        {
            lock (lockObject)
            {
                name = value;
                StartWriterThread();
            }
        }
    }

    public int Percent
    {
        get => percent;
        set
        {
            lock (lockObject)
            {
                percent = value;
                StartWriterThread();
            }
        }
    }

    public Bank()
    {
        writerThread = new Thread(WritePropertiesToFile);
        writerThread.Start();
    }

    private void WritePropertiesToFile()
    {
        while (true)
        {
            lock (lockObject)
            {
                using (StreamWriter writer = new StreamWriter("bank_properties.txt"))
                {
                    writer.WriteLine($"Money: {money}");
                    writer.WriteLine($"Name: {name}");
                    writer.WriteLine($"Percent: {percent}");
                }
            }
            Thread.Sleep(1000);
        }
    }

    private void StartWriterThread()
    {
        if (!writerThread.IsAlive)
        {
            writerThread = new Thread(WritePropertiesToFile);
            writerThread.Start();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Bank bank = new Bank();

        bank.Money = 10000;
        bank.Name = "My Bank";
        bank.Percent = 5;


        bank.Money = 15000;

        Console.WriteLine("Done");
    }
}