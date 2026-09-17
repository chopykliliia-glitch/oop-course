namespace Lab02;

public static class Task6
{
    public static void Run()
    {
        Console.WriteLine("Enter the number of doctors: ");
        int n = int.Parse(Console.ReadLine()!);

        int[][] costs = new int[n][];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter the number of appointments for Doctor {i + 1}: ");
            int k = int.Parse(Console.ReadLine()!);

            costs[i] = new int[k];

            Console.WriteLine("Enter the appointment costs:");

            for (int j = 0; j < k; j++)
            {
                costs[i][j] = int.Parse(Console.ReadLine()!);
            }
        }

        int biggestIncome = 0;
        int biggestIncomeDoctor = 0;

        for (int i = 0; i < n; i++)
        {
            int total = 0;

            foreach (int cost in costs[i])
            {
                total += cost;
            }

            double average = (double)total / costs[i].Length;

            Console.WriteLine(
                $"Doctor {i + 1}: {costs[i].Length} appointments, " +
                $"total = {total} UAH, average = {average:F2} UAH");

            if (total > biggestIncome)
            {
                biggestIncome = total;
                biggestIncomeDoctor = i;
            }
        }

        Console.WriteLine(
            $"Highest income: Doctor {biggestIncomeDoctor + 1} " +
            $"({biggestIncome} UAH)");
    }
}