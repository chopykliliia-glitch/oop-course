using ClinicApp;

PatientManager patientManager = new PatientManager();

patientManager.Add(new Patient(
    "Іван",
    "Петренко",
    new DateTime(1985, 5, 10),
    "A+",
    "0501234567"));

patientManager.Add(new Patient(
    "Олена",
    "Коваль",
    new DateTime(1992, 8, 15),
    "B-",
    "0672345678"));

patientManager.Add(new Patient(
    "Максим",
    "Бойко",
    new DateTime(2010, 3, 20),
    "O+",
    "0933456789"));

patientManager.Add(new Patient("Марія", "Ткач"));

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
        PatientMenu(patientManager);
    }
    else if (choice == "2")
    {
        Console.WriteLine("Меню лікарів буде в Завданні 4.");
    }
    else if (choice == "0")
    {
        Console.WriteLine("Програму завершено.");
        break;
    }
    else
    {
        Console.WriteLine("Невірний вибір.");
    }
}

static void PatientMenu(PatientManager manager)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("========== ПАЦІЄНТИ ==========");
        Console.WriteLine("1 — Показати всіх");
        Console.WriteLine("2 — Додати пацієнта");
        Console.WriteLine("3 — Знайти за ім'ям");
        Console.WriteLine("4 — Видалити");
        Console.WriteLine("5 — Статистика");
        Console.WriteLine("0 — Назад");
        Console.WriteLine("===============================");
        Console.Write("Ваш вибір: ");

        string choice = Console.ReadLine()!;

        if (choice == "1")
        {
            manager.DisplayAll();
        }
        else if (choice == "2")
        {
            Console.Write("Ім'я: ");
            string firstName = Console.ReadLine()!;

            Console.Write("Прізвище: ");
            string lastName = Console.ReadLine()!;

            Console.Write("Дата народження (рррр-мм-дд): ");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine()!);

            Console.Write("Група крові: ");
            string bloodType = Console.ReadLine()!;

            Console.Write("Телефон: ");
            string phone = Console.ReadLine()!;

            Patient patient = new Patient(
                firstName,
                lastName,
                dateOfBirth,
                bloodType,
                phone);

            manager.Add(patient);
        }
        else if (choice == "3")
        {
            Console.Write("Введіть ім'я або прізвище: ");
            string name = Console.ReadLine()!;

            Patient[] found = manager.FindByName(name);

            if (found.Length == 0)
            {
                Console.WriteLine("Пацієнтів не знайдено.");
            }
            else
            {
                Console.WriteLine("Знайдені пацієнти:");

                for (int i = 0; i < found.Length; i++)
                {
                    Console.WriteLine(found[i]);
                }
            }
        }
        else if (choice == "4")
        {
            Console.Write("Введіть ID пацієнта: ");
            int id = int.Parse(Console.ReadLine()!);

            bool removed = manager.Remove(id);

            if (removed)
            {
                Console.WriteLine("Пацієнта видалено.");
            }
            else
            {
                Console.WriteLine("Пацієнта з таким ID не знайдено.");
            }
        }
        else if (choice == "5")
        {
            manager.DisplayStats();
        }
        else if (choice == "0")
        {
            break;
        }
        else
        {
            Console.WriteLine("Невірний вибір.");
        }
    }
}