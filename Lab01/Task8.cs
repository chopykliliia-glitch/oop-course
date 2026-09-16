namespace Lab01;

public static class Task8
{
    static double CalculateBMI(double weight, double height)
    {
        return weight / (height * height);
    }

    static string GetBMICategory(double bmi)
    {
        if (bmi < 18.5)
        {
            return "underweight";
        }
        else if (bmi < 25)
        {
            return "normal";
        }
        else if (bmi < 30)
        {
            return "overweight";
        }
        else
        {
            return "obesity";
        }
    }

    static double CalculateCost(double price, int visits, int discount)
    {
        return price * visits * (1 - discount / 100.0);
    }

    static string GetAgeCategory(int age)
    {
        if (age <= 17)
        {
            return "child";
        }
        else if (age <= 59)
        {
            return "adult";
        }
        else
        {
            return "pensioner";
        }
    }
    static string GetPressureStatus(int systolic, int diastolic)
    {
        if (systolic < 120 && diastolic < 80)
        {
            return "normal";
        }
        else if (systolic < 130 && diastolic < 80)
        {
            return "elevated";
        }
        else if (systolic < 140 || diastolic < 90)
        {
            return "hypertension stage 1";
        }
        else
        {
            return "hypertension stage 2";
        }
    }

    static int CalculateAge(int birthYear)
    {
        return 2026 - birthYear;
    }

    public static void Run()
    {
        Console.WriteLine("Enter weight (kg): ");
        double weight = double.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter height (m): ");
        double height = double.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter price: ");
        double price = double.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter number of visits: ");
        int visits = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter discount: ");
        int discount = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter birth year: ");
        int birthYear = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter systolic pressure: ");
        int systolic = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter diastolic pressure: ");
        int diastolic = int.Parse(Console.ReadLine()!);

        double bmi = CalculateBMI(weight, height);
        double total = CalculateCost(price, visits, discount);
        int age = CalculateAge(birthYear);

        Console.WriteLine($"BMI: {bmi:F2} -> {GetBMICategory(bmi)}");
        Console.WriteLine($"Total: {total:F2} UAH");
        Console.WriteLine($"Age: {age} year, category: {GetAgeCategory(age)}");
        Console.WriteLine($"Blood pressure: {systolic}/{diastolic} - {GetPressureStatus(systolic, diastolic)}");
    }
}
