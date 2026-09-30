using ClinicApp;

Clinic clinic = new Clinic("Медична Клініка");

clinic.Patients.Add(new Patient(
    "Іван",
    "Петренко",
    new DateTime(1985, 5, 10),
    BloodType.APositive,
    "0501234567"));

clinic.Patients.Add(new Patient(
    "Олена",
    "Коваль",
    new DateTime(1992, 8, 15),
    BloodType.BNegative,
    "0672345678"));

clinic.Patients.Add(new Patient(
    "Максим",
    "Бойко",
    new DateTime(2010, 3, 20),
    BloodType.OPositive,
    "0933456789"));

clinic.Patients.Add(
    new Patient("Марія", "Ткач"));

Doctor doctor1 = new Doctor(
    "Олег",
    "Сидоренко",
    Speciality.Cardiologist,
    "LIC-001",
    "0441234567");

Doctor doctor2 = new Doctor(
    "Наталія",
    "Мороз",
    Speciality.Neurologist,
    "LIC-002",
    "0442345678");

Doctor doctor3 = new Doctor(
    "Андрій",
    "Власенко",
    Speciality.Pediatrician,
    "LIC-003",
    "0443456789");

doctor1.WorkEndHour = 16;

doctor2.WorkStartHour = 9;
doctor2.WorkEndHour = 18;

clinic.Doctors.Add(doctor1);
clinic.Doctors.Add(doctor2);
clinic.Doctors.Add(doctor3);

clinic.Appointments.Book(
    1,
    1,
    new DateTime(2026, 9, 25, 10, 0, 0),
    30);

clinic.Appointments.Book(
    2,
    2,
    new DateTime(2026, 9, 25, 11, 0, 0),
    45);

clinic.Appointments.Book(
    3,
    3,
    new DateTime(2026, 9, 25, 9, 0, 0),
    20);

while (true)
{
    Console.WriteLine();
    Console.WriteLine("========== КЛІНІКА ==========");
    Console.WriteLine("1 — Пацієнти");
    Console.WriteLine("2 — Лікарі");
    Console.WriteLine("3 — Записи");
    Console.WriteLine("4 — Розклад");
    Console.WriteLine("5 — Звіт");
    Console.WriteLine("6 — Тест зростаючого масиву");
    Console.WriteLine("0 — Вихід");
    Console.WriteLine("==============================");
    Console.Write("Ваш вибір: ");

    string choice = Console.ReadLine()!;

    if (choice == "1")
    {
        PatientMenu(clinic);
    }
    else if (choice == "2")
    {
        DoctorMenu(clinic);
    }
    else if (choice == "3")
    {
        AppointmentMenu(clinic);
    }
    else if (choice == "4")
    {
        Console.Write("Дата (рррр-мм-дд): ");

        DateTime date = DateTime.Parse(Console.ReadLine()!);

        clinic.DisplaySchedule(date);
    }
    else if (choice == "5")
    {
        clinic.GenerateReport();
    }
    else if (choice == "6")
    {
        GrowablePatientTest();
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

static void PatientMenu(Clinic clinic)
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
            clinic.Patients.DisplayAll();
        }
        else if (choice == "2")
        {
            Console.Write("Ім'я: ");
            string firstName = Console.ReadLine()!;

            Console.Write("Прізвище: ");
            string lastName = Console.ReadLine()!;

            Console.Write("Дата народження (рррр-мм-дд): ");
            DateTime dateOfBirth =
                DateTime.Parse(Console.ReadLine()!);

            Console.WriteLine("Група крові:");
            Console.WriteLine("1 — A+");
            Console.WriteLine("2 — A-");
            Console.WriteLine("3 — B+");
            Console.WriteLine("4 — B-");
            Console.WriteLine("5 — O+");
            Console.WriteLine("6 — O-");
            Console.WriteLine("7 — AB+");
            Console.WriteLine("8 — AB-");
            Console.Write("Ваш вибір: ");

            string bloodChoice = Console.ReadLine()!;

            BloodType bloodType;

            if (bloodChoice == "1")
                bloodType = BloodType.APositive;
            else if (bloodChoice == "2")
                bloodType = BloodType.ANegative;
            else if (bloodChoice == "3")
                bloodType = BloodType.BPositive;
            else if (bloodChoice == "4")
                bloodType = BloodType.BNegative;
            else if (bloodChoice == "5")
                bloodType = BloodType.OPositive;
            else if (bloodChoice == "6")
                bloodType = BloodType.ONegative;
            else if (bloodChoice == "7")
                bloodType = BloodType.ABPositive;
            else if (bloodChoice == "8")
                bloodType = BloodType.ABNegative;
            else
                bloodType = BloodType.Unknown;

            Console.Write("Телефон: ");
            string phone = Console.ReadLine()!;

            Patient patient = new Patient(
                firstName,
                lastName,
                dateOfBirth,
                bloodType,
                phone);

            clinic.Patients.Add(patient);
        }
        else if (choice == "3")
        {
            Console.Write("Введіть ім'я або прізвище: ");
            string name = Console.ReadLine()!;

            Patient[] found =
                clinic.Patients.FindByName(name);

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

            bool removed = clinic.Patients.Remove(id);

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
            clinic.Patients.DisplayStats();
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

static void DoctorMenu(Clinic clinic)
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
            clinic.Doctors.DisplayAll();
        }
        else if (choice == "2")
        {
            Console.Write("Ім'я: ");
            string firstName = Console.ReadLine()!;

            Console.Write("Прізвище: ");
            string lastName = Console.ReadLine()!;

            Console.WriteLine("Спеціальність:");
            Console.WriteLine("1 — Cardiologist");
            Console.WriteLine("2 — Dentist");
            Console.WriteLine("3 — Dermatologist");
            Console.WriteLine("4 — Neurologist");
            Console.WriteLine("5 — Pediatrician");
            Console.WriteLine("6 — Surgeon");
            Console.WriteLine("7 — Therapist");
            Console.Write("Ваш вибір: ");

            string specialityChoice = Console.ReadLine()!;

            Speciality speciality;

            if (specialityChoice == "1")
                speciality = Speciality.Cardiologist;
            else if (specialityChoice == "2")
                speciality = Speciality.Dentist;
            else if (specialityChoice == "3")
                speciality = Speciality.Dermatologist;
            else if (specialityChoice == "4")
                speciality = Speciality.Neurologist;
            else if (specialityChoice == "5")
                speciality = Speciality.Pediatrician;
            else if (specialityChoice == "6")
                speciality = Speciality.Surgeon;
            else if (specialityChoice == "7")
                speciality = Speciality.Therapist;
            else
                speciality = Speciality.Unknown;

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

            while (!int.TryParse(
                       Console.ReadLine(),
                       out workStartHour) ||
                   workStartHour < 0 ||
                   workStartHour > 23)
            {
                Console.Write(
                    "Введіть годину від 0 до 23: ");
            }

            Console.Write("Кінець роботи (година): ");

            int workEndHour;

            while (!int.TryParse(
                       Console.ReadLine(),
                       out workEndHour) ||
                   workEndHour < 0 ||
                   workEndHour > 23 ||
                   workEndHour <= workStartHour)
            {
                Console.Write(
                    "Введіть правильну годину від 0 до 23: ");
            }

            doctor.WorkStartHour = workStartHour;
            doctor.WorkEndHour = workEndHour;

            clinic.Doctors.Add(doctor);
        }
        else if (choice == "3")
        {
            Console.WriteLine("Спеціальність:");
            Console.WriteLine("1 — Cardiologist");
            Console.WriteLine("2 — Dentist");
            Console.WriteLine("3 — Dermatologist");
            Console.WriteLine("4 — Neurologist");
            Console.WriteLine("5 — Pediatrician");
            Console.WriteLine("6 — Surgeon");
            Console.WriteLine("7 — Therapist");
            Console.Write("Ваш вибір: ");

            string specialityChoice = Console.ReadLine()!;

            Speciality speciality;

            if (specialityChoice == "1")
                speciality = Speciality.Cardiologist;
            else if (specialityChoice == "2")
                speciality = Speciality.Dentist;
            else if (specialityChoice == "3")
                speciality = Speciality.Dermatologist;
            else if (specialityChoice == "4")
                speciality = Speciality.Neurologist;
            else if (specialityChoice == "5")
                speciality = Speciality.Pediatrician;
            else if (specialityChoice == "6")
                speciality = Speciality.Surgeon;
            else if (specialityChoice == "7")
                speciality = Speciality.Therapist;
            else
                speciality = Speciality.Unknown;

            Doctor[] found =
                clinic.Doctors.FindBySpeciality(speciality);

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

            Doctor? doctor =
                clinic.Doctors.FindById(id);

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

            bool removed =
                clinic.Doctors.Remove(id);

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
            clinic.Doctors.DisplayStats();
        }
        else if (choice == "7")
        {
            Doctor[] doctors =
                clinic.Doctors.GetAll();

            if (doctors.Length == 0)
            {
                Console.WriteLine("Список лікарів порожній.");
            }
            else
            {
                for (int i = 0;
                     i < doctors.Length;
                     i++)
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

static void AppointmentMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("========== ЗАПИСИ ==========");
        Console.WriteLine("1 — Створити запис");
        Console.WriteLine("2 — Показати всі майбутні");
        Console.WriteLine("3 — Показати записи пацієнта");
        Console.WriteLine("4 — Показати записи лікаря");
        Console.WriteLine("5 — Показати записи за датою");
        Console.WriteLine("6 — Скасувати запис");
        Console.WriteLine("7 — Завершити запис");
        Console.WriteLine("0 — Назад");
        Console.WriteLine("============================");
        Console.Write("Ваш вибір: ");

        string choice = Console.ReadLine()!;

        if (choice == "1")
        {
            Console.WriteLine();
            Console.WriteLine("=== Пацієнти ===");

            clinic.Patients.DisplayAll();

            Console.Write("ID пацієнта: ");

            int patientId =
                int.Parse(Console.ReadLine()!);

            Console.WriteLine();
            Console.WriteLine("=== Лікарі ===");

            clinic.Doctors.DisplayAll();

            Console.Write("ID лікаря: ");

            int doctorId =
                int.Parse(Console.ReadLine()!);

            Console.Write(
                "Дата та час (рррр-мм-дд гг:хх): ");

            DateTime scheduledAt =
                DateTime.Parse(Console.ReadLine()!);

            Console.Write(
                "Тривалість у хвилинах: ");

            int durationMinutes =
                int.Parse(Console.ReadLine()!);

            clinic.Appointments.Book(
                patientId,
                doctorId,
                scheduledAt,
                durationMinutes);
        }
        else if (choice == "2")
        {
            Appointment[] appointments =
                clinic.Appointments.GetUpcoming();

            clinic.Appointments.DisplayList(
                appointments);
        }
        else if (choice == "3")
        {
            clinic.Patients.DisplayAll();

            Console.Write("ID пацієнта: ");

            int patientId =
                int.Parse(Console.ReadLine()!);

            Appointment[] appointments =
                clinic.Appointments.GetByPatient(
                    patientId);

            clinic.Appointments.DisplayList(
                appointments);
        }
        else if (choice == "4")
        {
            clinic.Doctors.DisplayAll();

            Console.Write("ID лікаря: ");

            int doctorId =
                int.Parse(Console.ReadLine()!);

            Appointment[] appointments =
                clinic.Appointments.GetByDoctor(
                    doctorId);

            clinic.Appointments.DisplayList(
                appointments);
        }
        else if (choice == "5")
        {
            Console.Write("Дата (рррр-мм-дд): ");

            DateTime date =
                DateTime.Parse(Console.ReadLine()!);

            Appointment[] appointments =
                clinic.Appointments.GetByDate(date);

            clinic.Appointments.DisplayList(
                appointments);
        }
        else if (choice == "6")
        {
            Console.Write("ID запису: ");

            int id =
                int.Parse(Console.ReadLine()!);

            Console.Write("Причина скасування: ");

            string reason =
                Console.ReadLine()!;

            clinic.Appointments.Cancel(
                id,
                reason);
        }
        else if (choice == "7")
        {
            Console.Write("ID запису: ");

            int id =
                int.Parse(Console.ReadLine()!);

            clinic.Appointments.Complete(id);
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

static void GrowablePatientTest()
{
    GrowablePatientManager manager =
        new GrowablePatientManager();

    Console.WriteLine();
    Console.WriteLine("=== Тест зростаючого масиву ===");

    for (int i = 1; i <= 20; i++)
    {
        Patient patient = new Patient(
            "Тест",
            "Пацієнт" + i,
            new DateTime(2000, 1, 1),
            BloodType.OPositive,
            "0000000000");

        manager.Add(patient);
    }

    Console.WriteLine();
    Console.WriteLine("Тест пошуку:");

    Patient? found = manager.FindById(10);

    if (found == null)
        Console.WriteLine("Пацієнта з ID 10 не знайдено.");
    else
        Console.WriteLine($"Знайдено: {found}");

    found = manager.FindById(99);

    if (found == null)
        Console.WriteLine("Пацієнта з ID 99 не знайдено.");
    else
        Console.WriteLine($"Знайдено: {found}");

    Console.WriteLine();
    Console.WriteLine($"Кількість: {manager.Count}");
    Console.WriteLine($"Ємність: {manager.Capacity}");
}