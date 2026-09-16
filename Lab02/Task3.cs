namespace Lab02;

public static class Task3
{
    public static void Run()
    {
        string[] days =
        {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"
        };

        int[] counts = new int[7];

        Console.WriteLine("Enter the number of patients for each day:");

        for (int i = 0; i < 7; i++)
        {
            Console.Write($"{days[i]}: ");
            counts[i] = int.Parse(Console.ReadLine()!);
        }

        int total = 0;
        int maxIndex = 0;
        int minIndex = 0;

        for (int i = 0; i < 7; i++)
        {
            total += counts[i];

            if (counts[i] > counts[maxIndex])
            {
                maxIndex = i;
            }

            if (counts[i] < counts[minIndex])
            {
                minIndex = i;
            }
        }

        Console.WriteLine();

        for (int i = 0; i < 7; i++)
        {
            Console.WriteLine($"{days[i],-12}: {counts[i]} patients");
        }

        Console.WriteLine($"Total:        {total}");
        Console.WriteLine($"Most busy:    {days[maxIndex]} ({counts[maxIndex]})");
        Console.WriteLine($"Least busy:   {days[minIndex]} ({counts[minIndex]})");
    }
}