namespace Lab02;

public static class Task4
{
    public static void Run()
    {
        Console.WriteLine("Enter the number of doctors: ");
        int n = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter the number of working days: ");
        int m = int.Parse(Console.ReadLine()!);

        int[,] matrix = new int[n, m];

        Console.WriteLine("Enter the appointment counts:");

        for (int i = 0; i < n; i++)
        {
            string[] values = Console.ReadLine()!.Split(' ');

            for (int j = 0; j < m; j++)
            {
                matrix[i, j] = int.Parse(values[j]);
            }
        }

        int max = matrix[0, 0];
        int maxRow = 0;
        int maxCol = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (matrix[i, j] > max)
                {
                    max = matrix[i, j];
                    maxRow = i;
                    maxCol = j;
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            int rowSum = 0;

            for (int j = 0; j < m; j++)
            {
                rowSum += matrix[i, j];
            }

            Console.WriteLine($"Doctor {i + 1}: {rowSum} appointments");
        }

        Console.Write("By days: ");

        for (int j = 0; j < m; j++)
        {
            int columnSum = 0;

            for (int i = 0; i < n; i++)
            {
                columnSum += matrix[i, j];
            }

            Console.Write(columnSum);

            if (j < m - 1)
            {
                Console.Write(", ");
            }
        }

        Console.WriteLine();
        Console.WriteLine(
            $"Maximum: {max} (Doctor {maxRow + 1}, Day {maxCol + 1})");
    }
}