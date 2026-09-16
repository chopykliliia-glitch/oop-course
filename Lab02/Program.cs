System.Threading.Thread.CurrentThread.CurrentCulture =
    System.Globalization.CultureInfo.InvariantCulture;

Console.WriteLine("=== Lab 02 ===");
Console.WriteLine("Choose a task:");
Console.WriteLine("1 - Patient weights");
Console.WriteLine("2 - Appointment sorting");
Console.WriteLine("3 - Weekly clinic schedule");
Console.WriteLine("4 - Appointment matrix");
Console.WriteLine("5 - Square matrix diagonals");
Console.WriteLine("6 - Jagged array");
Console.WriteLine("7 - BMI ranking");
Console.WriteLine("8 - 3D array");
Console.WriteLine();

Console.WriteLine("Enter task number: ");
int task = int.Parse(Console.ReadLine()!);

Console.WriteLine();

switch (task)
{
    case 1:
        Lab02.Task1.Run();
        break;

    case 2:
        Lab02.Task2.Run();
        break;

    case 3:
        Lab02.Task3.Run();
        break;

    case 4:
        Lab02.Task4.Run();
        break;

    case 5:
        Lab02.Task5.Run();
        break;

    case 6:
        Lab02.Task6.Run();
        break;

    case 7:
        Lab02.Task7.Run();
        break;

    case 8:
        Lab02.Task8.Run();
        break;

    default:
        Console.WriteLine("Invalid task number.");
        break;
}