namespace Lab02;

public static class Task8
{
    public static void Run()
    {
        Console.WriteLine("Enter the number of departments: ");
        int d = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter the number of weeks: ");
        int w = int.Parse(Console.ReadLine()!);

        int[,,] patients = new int[d, w, 2];
        int[] totals = new int[d];

        Console.WriteLine("Enter the number of patients:");

        for (int i = 0; i < d; i++)
        {
            for (int j = 0; j < w; j++)
            {
                Console.WriteLine($"Department {i + 1}, Week {j + 1}, Morning:");
                patients[i, j, 0] = int.Parse(Console.ReadLine()!);

                Console.WriteLine($"Department {i + 1}, Week {j + 1}, Evening:");
                patients[i, j, 1] = int.Parse(Console.ReadLine()!);

                totals[i] += patients[i, j, 0];
                totals[i] += patients[i, j, 1];
            }
        }

        int busiestDepartment = 0;

        for (int i = 0; i < d; i++)
        {
            Console.WriteLine();
            Console.WriteLine($"Department {i + 1}:");

            for (int j = 0; j < w; j++)
            {
                int weekTotal =
                    patients[i, j, 0] +
                    patients[i, j, 1];

                Console.WriteLine(
                    $"  Week {j + 1}: morning {patients[i, j, 0]}, " +
                    $"evening {patients[i, j, 1]} -> total {weekTotal}");
            }

            Console.WriteLine($"  Total: {totals[i]} patients");

            if (totals[i] > totals[busiestDepartment])
            {
                busiestDepartment = i;
            }
        }

        Console.WriteLine();
        Console.WriteLine(
            $"Busiest department: Department {busiestDepartment + 1} " +
            $"({totals[busiestDepartment]} patients)");
    }
}