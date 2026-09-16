namespace Lab01;

public static class Task1
{
    public static void Run()
    {
        Console.WriteLine("Enter your weight (kg): ");
        double weight = double.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter your height (m): ");
        double height = double.Parse(Console.ReadLine()!);

        double bmi = weight / (height * height);

        Console.WriteLine($"IMT: {bmi:F2}");
    }
}