namespace Lab01;

public static class Task3
{
    public static void Run()
    {
        Console.WriteLine("Enter birth year: ");
        int birthYear = int.Parse(Console.ReadLine()!);

        int age = 2026 - birthYear;

        Console.WriteLine($"Age: {age} year");

        if(age <= 17)
        {
            Console.WriteLine("Category: child");
        }
        else if(age <= 59)
        {
            Console.WriteLine("Category: adult");
        }
        else
        {
            Console.WriteLine("Category: pensioner");
        }
    }
}