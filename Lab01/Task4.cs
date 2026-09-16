namespace Lab01;

public static class Task4
{
    public static void Run()
    {
        Console.WriteLine("Enter systolic blood pressure:");
        int systolic = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter diastolic blood pressure:");
        int diastolic = int.Parse(Console.ReadLine()!);

        string status;

        if (systolic < 120 && diastolic < 80)
        {
            status = "normal";
        }
        else if (systolic < 130 && diastolic < 80)
        {
            status = "elevated";
        }
        else if (systolic < 140 || diastolic < 90)
        {
            status = "hypertension stage 1";
        }
        else
        {
            status = "hypertension stage 2";
        }

        Console.WriteLine($"Blood pressure: {systolic}/{diastolic} — {status}");
    }
}