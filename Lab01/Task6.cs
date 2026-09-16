namespace Lab01;

public static class Task6
{
    public static void Run()
    {
        Console.WriteLine("Enter card number: ");
        int card = int.Parse(Console.ReadLine()!);

        int lastDigit = card % 10;

        string department = lastDigit switch
        {
            0 or 1 => "general therapy",
            2 or 3 => "surgery",
            4 or 5 => "cardiology",
            6 or 7 => "neurology",
            8 or 9 => "ophthalmology"
        };

        string discount = "no";
        if (card % 2 == 0)
        {
            discount = "yes";
        }

        string checkup = "no";
        if (card % 3 == 0)
        {
            checkup = "yes";
        }

        Console.WriteLine($"Department: {department}");
        Console.WriteLine($"Discount:   {discount}");
        Console.WriteLine($"Checkup:    {checkup}");
    }
}