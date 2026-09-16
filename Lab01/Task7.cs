namespace Lab01;

public static class Task7
{
    public static void Run()
    {
        Console.WriteLine("Enter number of appointments: ");
        int n = int.Parse(Console.ReadLine()!);

        decimal[] costs = new decimal[n];

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Enter cost #{i + 1}: ");
            costs[i] = decimal.Parse(Console.ReadLine()!);
        }

        decimal sum = 0;
        decimal min = costs[0];
        decimal max = costs[0];

        foreach (decimal cost in costs)
        {
            sum += cost;

            if (cost < min)
                min = cost;

            if (cost > max)
                max = cost;
        }

        decimal average = sum / n;

        int aboveAverage = 0;

        for (int i = 0; i < n; i++)
        {
            if (costs[i] > average)
                aboveAverage++;
        }

        int index = 0;

        while (index < n && costs[index] <= 1000)
        {
            index++;
        }

        Console.WriteLine();
        Console.WriteLine("=== Appointments Report ===");
        Console.WriteLine($"Count: {n}");
        Console.WriteLine($"Total: {sum:F2} UAH");
        Console.WriteLine($"Average: {average:F2} UAH");
        Console.WriteLine($"Min / Max: {min:F2} / {max:F2} UAH");
        Console.WriteLine($"Above average: {aboveAverage} of {n}");

        if (index < n)
        {
            Console.WriteLine($"First > 1000:  #{index + 1} — {costs[index]:F2} UAH");
        }
        else
        {
            Console.WriteLine("First > 1000:  none");
        }
    }
}