using Human;
using Laptop;

public static class Program
{
    private static void Main(string[] args)
    {
        HumanClass human = new()
        {
            Name = "Оля",
            Gender = false
        };

        LaptopClass laptop = new()
        {
            Name = "Samsung"
        };

        human.GetLaptop(laptop);
    }

    public static void GetLaptop(this HumanClass human, LaptopClass laptop)
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
}