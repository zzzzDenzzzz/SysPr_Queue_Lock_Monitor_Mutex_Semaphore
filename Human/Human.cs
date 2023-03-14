namespace Human
{
    public class HumanClass
    {
        public string Name { get; set; } = "NoName";
        public bool Gender { get; set; } = false;

        public void HasEntered()
        {
            string word;
            if (Gender)
            {
                word = "вошел";
            }
            else
            {
                word = "вошла";
            }
            Console.WriteLine($"{Name} {word} в кабинку.");
        }

        public void DialingNumber()
        {
            Random random = new(DateTime.Now.Millisecond);
            int number = 0;
            int minValue = 0;
            int maxValue = 10;
            for (int i = minValue; i < maxValue; i++)
            {
                number += random.Next(minValue, maxValue);
            }
        }

        public void Called()
        {
            string word;
            if (Gender)
            {
                word = "позвонил";
            }
            else
            {
                word = "позвонила";
            }
            Console.WriteLine($"{Name} {word}.");
        }

        public void CameOut()
        {
            string word;
            if (Gender)
            {
                word = "вышел";
            }
            else
            {
                word = "вышла";
            }
            Console.WriteLine($"{Name} {word}.");
        }
    }
}