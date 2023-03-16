using Human;
using Laptop;

public static class Program
{
    static readonly int countLaptop = 5;
    static readonly int maxCountLaptop = 5;

    static readonly Queue<HumanClass> humans = new();
    static readonly Stack<LaptopClass> laptops = new();
    static readonly Semaphore semaphore = new(countLaptop, maxCountLaptop);

    private static void Main(string[] args)
    {
        CreatingQueueHumans();

        CreatingStackLaptop();

        StartingThreads(CreatingThreads(humans.Count, new ParameterizedThreadStart(CallWithSemaphore)));

        ShowQueueHumans();
    }

    static void CreatingQueueHumans()
    {
        humans.Enqueue(new HumanClass { Name = "Вася", Gender = true });
        humans.Enqueue(new HumanClass { Name = "Петя", Gender = true });
        humans.Enqueue(new HumanClass { Name = "Оля", Gender = false });
        humans.Enqueue(new HumanClass { Name = "Алиса", Gender = false });
        humans.Enqueue(new HumanClass { Name = "Федя", Gender = true });
        humans.Enqueue(new HumanClass { Name = "Паша", Gender = true });
        humans.Enqueue(new HumanClass { Name = "Маша", Gender = false });
        humans.Enqueue(new HumanClass { Name = "Василиса", Gender = false });
        humans.Enqueue(new HumanClass { Name = "Ваня", Gender = true });
        humans.Enqueue(new HumanClass { Name = "Таня", Gender = false });
    }

    static void CreatingStackLaptop()
    {
        laptops.Push(new LaptopClass { Name = "Samsung" });
        laptops.Push(new LaptopClass { Name = "Xiaomi" });
        laptops.Push(new LaptopClass { Name = "Lenovo" });
        laptops.Push(new LaptopClass { Name = "ASUS" });
        laptops.Push(new LaptopClass { Name = "HUAWEI" });
    }

    static Thread[] CreatingThreads(int count, ParameterizedThreadStart parameterized)
    {
        Thread[] threads = new Thread[count];

        for (int i = 0; i < count; i++)
        {
            threads[i] = new Thread(parameterized)
            {
                Name = $"[поток{i}]"
            };
        }
        return threads;
    }

    static void StartingThreads(Thread[] threads)
    {
        foreach (var t in threads)
        {
            t.Start(humans);
        }
    }

    static void CallWithSemaphore(object? obj)
    {
        if (obj is Queue<HumanClass> humans)
        {
            if (humans.Count > 0 && laptops.Count > 0)
            {
                semaphore.WaitOne();

                Console.WriteLine($"Работает поток = {Thread.CurrentThread.Name}");
                try
                {
                    var human = humans.Dequeue();
                    var laptop = laptops.Pop();
                    human.GetLaptop(laptop);

                    Thread.Sleep(200);

                    human.SetLaptop(laptop);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
            }
        }
    }

    static void GetLaptop(this HumanClass human, LaptopClass laptop)
    {
        string word;
        if (human.Gender)
        {
            word = "арендавал";
        }
        else
        {
            word = "арендавала";
        }
        Console.WriteLine($"{human.Name} {word} {laptop.Name}");
    }

    static void SetLaptop(this HumanClass human, LaptopClass laptop)
    {
        string word;
        if (human.Gender)
        {
            word = "вернул";
        }
        else
        {
            word = "вернула";
        }
        Console.WriteLine($"{human.Name} {word} {laptop.Name}");
        laptops.Push(laptop);

        semaphore.Release();
    }

    static void ShowQueueHumans()
    {
        if (humans.Count == 0)
        {
            return;
        }

        Console.WriteLine($"{Environment.NewLine}Очередь за ноутбуками:");
        foreach (var human in humans)
        {
            Console.WriteLine(human.Name);
        }
        Console.WriteLine(Environment.NewLine);
    }
}