using ClinicApp;

PatientManager patientManager = new PatientManager();
DoctorManager DoctorManager = new DoctorManager();

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
        DoctorMenu(DoctorManager);
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
static void DoctorMenu(DoctorManager manager)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("========== ЛІКАРІ ==========");
        Console.WriteLine("1 — Показати всіх");
        Console.WriteLine("2 — Додати лікаря");
        Console.WriteLine("3 — Знайти за спеціальністю");
        Console.WriteLine("4 — Знайти за ID");
        Console.WriteLine("5 — Видалити");
        Console.WriteLine("6 — Статистика");
        Console.WriteLine("7 — Показати всіх через GetAll()");
        Console.WriteLine("0 — Назад");
        Console.WriteLine("============================");
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

            Console.Write("Спеціальність: ");
            string speciality = Console.ReadLine()!;

            Console.Write("Номер ліцензії: ");
            string licenseNumber = Console.ReadLine()!;

            Console.Write("Телефон: ");
            string phone = Console.ReadLine()!;

            Doctor doctor = new Doctor(
                firstName,
                lastName,
                speciality,
                licenseNumber,
                phone);

            Console.Write("Початок роботи (година): ");
            int workStartHour;

            while (!int.TryParse(Console.ReadLine(), out workStartHour) ||
                   workStartHour < 0 ||
                   workStartHour > 23)
            {
                Console.Write("Введіть годину від 0 до 23: ");
            }

            Console.Write("Кінець роботи (година): ");
            int workEndHour;

            while (!int.TryParse(Console.ReadLine(), out workEndHour) ||
                   workEndHour < 0 ||
                   workEndHour > 23 ||
                   workEndHour <= workStartHour)
            {
                Console.Write("Введіть правильну годину від 0 до 23: ");
            }

            doctor.WorkStartHour = workStartHour;
            doctor.WorkEndHour = workEndHour;

            manager.Add(doctor);
        }
        else if (choice == "3")
        {
            Console.Write("Введіть спеціальність: ");
            string speciality = Console.ReadLine()!;

            Doctor[] found = manager.FindBySpeciality(speciality);

            if (found.Length == 0)
            {
                Console.WriteLine("Лікарів не знайдено.");
            }
            else
            {
                Console.WriteLine("Знайдені лікарі:");

                for (int i = 0; i < found.Length; i++)
                {
                    Console.WriteLine(found[i]);
                }
            }
        }
        else if (choice == "4")
        {
            Console.Write("Введіть ID лікаря: ");
            int id = int.Parse(Console.ReadLine()!);

            Doctor? doctor = manager.FindById(id);

            if (doctor == null)
            {
                Console.WriteLine("Лікаря не знайдено.");
            }
            else
            {
                Console.WriteLine(doctor);
            }
        }
        else if (choice == "5")
        {
            Console.Write("Введіть ID лікаря: ");
            int id = int.Parse(Console.ReadLine()!);

            bool removed = manager.Remove(id);

            if (removed)
            {
                Console.WriteLine("Лікаря видалено.");
            }
            else
            {
                Console.WriteLine("Лікаря з таким ID не знайдено.");
            }
        }
        else if (choice == "6")
        {
            manager.DisplayStats();
        }
        else if (choice == "7")
        {
            Doctor[] doctors = manager.GetAll();

            if (doctors.Length == 0)
            {
                Console.WriteLine("Список лікарів порожній.");
            }
            else
            {
                for (int i = 0; i < doctors.Length; i++)
                {
                    Console.WriteLine(doctors[i]);
                }
            }
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