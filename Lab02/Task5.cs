namespace Lab02;

public static class Task5
{
    public static void Run()
    {
        Console.WriteLine("Enter the matrix size: ");
        int n = int.Parse(Console.ReadLine()!);

        int[,] matrix = new int[n, n];

        Console.WriteLine("Enter the matrix:");

        for (int i = 0; i < n; i++)
        {
            string[] values = Console.ReadLine()!.Split(' ');

            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = int.Parse(values[j]);
            }
        }

        int[] mainDiagonal = new int[n];
        int[] secondaryDiagonal = new int[n];

        int mainSum = 0;
        int secondarySum = 0;

        for (int i = 0; i < n; i++)
        {
            mainDiagonal[i] = matrix[i, i];
            secondaryDiagonal[i] = matrix[i, n - 1 - i];

            mainSum += mainDiagonal[i];
            secondarySum += secondaryDiagonal[i];
        }

        Console.WriteLine(
            $"Main diagonal: {string.Join(", ", mainDiagonal)} " +
            $"(sum = {mainSum})");

        Console.WriteLine(
            $"Secondary diagonal: {string.Join(", ", secondaryDiagonal)} " +
            $"(sum = {secondarySum})");
    }
}