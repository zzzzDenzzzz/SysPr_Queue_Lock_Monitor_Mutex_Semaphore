using Human;

public class Program
{
    static readonly Queue<HumanClass> humans = new();
    static readonly object locker = new();

    private static void Main(string[] args)
    {
        CreatingQueue();

        StartingThreads(CreatingThreads(humans.Count, new ParameterizedThreadStart(CallNotMonitor)));

        CreatingStub();

        Message();

        CreatingQueue();

        StartingThreads(CreatingThreads(humans.Count, new ParameterizedThreadStart(CallWithMonitor)));

        Console.ReadLine();
    }

    static void CreatingStub()
    {
        Thread thread = new(() => { });
        thread.Start();
        thread.Join();
    }

    static void CreatingQueue()
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

    static void Message()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Отработал метод без lock.{Environment.NewLine}" +
            $"Теперь работает метод с lock.{Environment.NewLine}");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
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

    static void CallWithMonitor(object? obj)
    {
        if (obj is Queue<HumanClass> humans)
        {
            if (humans.Count > 0)
            {
                bool acquiredLock = false;
                try
                {
                    Monitor.Enter(locker, ref acquiredLock);
                    Console.WriteLine($"Работает поток = {Thread.CurrentThread.Name}");
                    var human = humans.Dequeue();
                    human.HasEntered();
                    human.DialingNumber();
                    human.Called();
                    human.CameOut();
                    Console.WriteLine(new string('~', humans.Count));
                    Thread.Sleep(200);
                }
                finally
                {
                    if (acquiredLock)
                    {
                        Monitor.Exit(locker);
                    }
                }
            }
        }
    }

    static void CallNotMonitor(object? obj)
    {
        if (obj is Queue<HumanClass> humans)
        {
            if (humans.Count > 0)
            {
                Console.WriteLine($"Работает поток = {Thread.CurrentThread.Name}");
                var human = humans.Dequeue();
                human.HasEntered();
                human.DialingNumber();
                human.Called();
                human.CameOut();
                Console.WriteLine(new string('~', humans.Count));
                Thread.Sleep(100);
            }
        }
    }
}