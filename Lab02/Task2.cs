namespace Lab02;

public static class Task2
{
    public static void Run()
    {
        Console.WriteLine("Enter the number of appointments: ");
        int n = int.Parse(Console.ReadLine()!);

        int[] queue = new int[n];

        Console.WriteLine("Enter the appointment costs (UAH): ");

        for (int i = 0; i < n; i++)
        {
            queue[i] = int.Parse(Console.ReadLine()!);
        }

        string before = string.Join(" ", queue);

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (queue[j] > queue[j + 1])
                {
                    int temp = queue[j];
                    queue[j] = queue[j + 1];
                    queue[j + 1] = temp;
                }
            }
        }

        string after = string.Join(" ", queue);

        int min = queue[0];
        int max = queue[n - 1];

        Console.WriteLine($"Queue (before): {before}");
        Console.WriteLine($"Queue (after):  {after}");
        Console.WriteLine($"Cheapest:       {min} UAH");
        Console.WriteLine($"Most expensive: {max} UAH");
    }
}