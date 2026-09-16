namespace Lab01;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Choose task:");
        Console.WriteLine("1 - Task 1");
        Console.WriteLine("2 - Task 2");
        Console.WriteLine("3 - Task 3");
        Console.WriteLine("4 - Task 4");
        Console.WriteLine("5 - Task 5");
        Console.WriteLine("6 - Task 6");
        Console.WriteLine("7 - Task 7");
        Console.WriteLine("8 - Task 8");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                Task1.Run();
                break;

            case "2":
                Task2.Run();
                break;

            case "3":
                Task3.Run();
                break;

            case "4":
                Task4.Run();
                break;

            case "5":
                Task5.Run();
                break;

            case "6":
                Task6.Run();
                break;

            case "7":
                Task7.Run();
                break;

            case "8":
                Task8.Run();
                break;

            default:
                Console.WriteLine("Wrong choice.");
                break;
        }
    }
}