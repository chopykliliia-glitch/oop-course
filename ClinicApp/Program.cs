using ClinicApp;

while (true)
{
    Console.WriteLine();
    Console.WriteLine("========== КЛІНІКА ==========");
    Console.WriteLine("1 — Пацієнти");
    Console.WriteLine("2 — Лікарі");
    Console.WriteLine("0 — Вихід");
    Console.WriteLine("==============================");
    Console.Write("Ваш вибір: ");

    string choice = Console.ReadLine()!;

    if (choice == "1")
    {
        Console.WriteLine();
        Console.WriteLine("----- ПАЦІЄНТИ -----");

        Patient patient1 = new Patient(
            "Іван",
            "Петренко",
            new DateTime(1985, 5, 10),
            "A+",
            "0501234567");

        Patient patient2 = new Patient(
            "Олена",
            "Коваль",
            new DateTime(1992, 8, 15),
            "B-",
            "0672345678");

        Patient patient3 = new Patient(
            "Максим",
            "Бойко",
            new DateTime(2010, 3, 20),
            "O+",
            "0933456789");

        Patient patient4 = new Patient();

        Patient patient5 = new Patient("Марія", "Ткач");

        Console.WriteLine(patient1);
        Console.WriteLine(patient2);
        Console.WriteLine(patient3);
        Console.WriteLine(patient4);
        Console.WriteLine(patient5);
    }
    else if (choice == "2")
    {
        Console.WriteLine();
        Console.WriteLine("----- ЛІКАРІ -----");

        Doctor doctor1 = new Doctor(
            "Олег",
            "Сидоренко",
            "Кардіологія",
            "LIC-001",
            "0441234567");

        Doctor doctor2 = new Doctor(
            "Наталія",
            "Мороз",
            "Неврологія",
            "LIC-002",
            "0442345678");

        Doctor doctor3 = new Doctor(
            "Андрій",
            "Власенко",
            "Педіатрія",
            "LIC-003",
            "0443456789");

        Doctor doctor4 = new Doctor();

        doctor1.WorkEndHour = 16;

        doctor2.WorkStartHour = 9;
        doctor2.WorkEndHour = 18;

        Console.WriteLine(doctor1);
        Console.WriteLine(doctor2);
        Console.WriteLine(doctor3);
        Console.WriteLine(doctor4);
    }
    else if (choice == "0")
    {
        Console.WriteLine("Програму завершено.");
        break;
    }
    else
    {
        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
    }

    Console.WriteLine();
    Console.WriteLine("Натисніть Enter, щоб повернутися до меню...");
    Console.ReadLine();
}