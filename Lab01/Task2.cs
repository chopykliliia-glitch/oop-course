namespace Lab01;

public static class Task2
{
    public static void Run()
    {
        Console.WriteLine("Enter price: ");
        double price = double.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter number of visits: ");
        int visit = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter discount: ");
        int discount = int.Parse(Console.ReadLine()!);

        double total = price * visit * (1 - discount / 100.0);

        Console.WriteLine($"Total: {total:F2}")
            ;
    }
}