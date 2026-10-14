using System.Globalization;
using ClinicApp;
using ClinicApp.Enums;
using ClinicApp.Models;
using ClinicApp.Managers;
using ClinicApp.Utils;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
Console.OutputEncoding = System.Text.Encoding.UTF8;

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
    Speciality.Cardiology,
    "LIC-001",
    "0441234567");

Doctor doctor2 = new Doctor(
    "Наталія",
    "Мороз",
    Speciality.Neurology,
    "LIC-002",
    "0442345678");

Doctor doctor3 = new Doctor(
    "Андрій",
    "Власенко",
    Speciality.Pediatrics,
    "LIC-003",
    "0443456789");

doctor1.Schedule = new WorkSchedule(8, 16);
doctor2.Schedule = new WorkSchedule(9, 18);

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

clinic.MedicalRecords.Add(new Diagnosis(
    1, 1, DateTime.Today.AddDays(-30), "I10", "Гіпертонічна хвороба", true));

clinic.MedicalRecords.Add(new Diagnosis(
    1, 1, DateTime.Today.AddDays(-5), "J06.9", "Гострий ринофарингіт"));

clinic.MedicalRecords.Add(new LabResult(
    1, 1, DateTime.Today.AddDays(-14), "Гемоглобін", 145, "г/л", "120–160", true));

clinic.MedicalRecords.Add(new LabResult(
    1, 1, DateTime.Today.AddDays(-10), "Холестерин", 6.2, "ммоль/л", "< 5.2", false));

clinic.MedicalRecords.Add(new Prescription(
    1, 1, DateTime.Today.AddDays(-5), "Лізиноприл", "10 мг", 30, "1 раз на добу вранці"));

clinic.MedicalRecords.Add(new Prescription(
    2, 2, DateTime.Today.AddDays(-40), "Ібупрофен", "200 мг", 10, "після їжі"));

clinic.MedicalRecords.Add(new Diagnosis(
    2, 2, DateTime.Today.AddDays(-20), "G43.9", "Мігрень"));

clinic.MedicalRecords.Add(new LabResult(
    3, 3, DateTime.Today.AddDays(-3), "Глюкоза", 4.8, "ммоль/л", "3.9–5.5", true));

while (true)
{
    Console.WriteLine();
    Console.WriteLine("========== КЛІНІКА ==========");
    Console.WriteLine("1 — Пацієнти");
    Console.WriteLine("2 — Лікарі");
    Console.WriteLine("3 — Записи");
    Console.WriteLine("4 — Медична картка");
    Console.WriteLine("5 — Звіт");
    Console.WriteLine("6 — Розклад");
    Console.WriteLine("7 — Тест зростаючого масиву");
    Console.WriteLine("8 — Тест Task 03");
    Console.WriteLine("9 — Тест Task 04");
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
        MedicalRecordsMenu(clinic);
    }
    else if (choice == "5")
    {
        clinic.GenerateReport();
    }
    else if (choice == "6")
    {
        Console.Write("Дата (рррр-мм-дд): ");

        DateTime date =
            DateTime.Parse(Console.ReadLine()!);

        clinic.DisplaySchedule(date);
    }
    else if (choice == "7")
    {
        GrowablePatientTest();
    }
    else if (choice == "8")
    {
        Task03Test(clinic);
    }
    else if (choice == "9")
    {
        Task04Test(clinic);
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
            try
            {
                Console.Write("Ім'я: ");
                string firstName = Console.ReadLine()!;

                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine()!;

                Console.Write("Дата народження (рррр-мм-дд): ");
                DateTime dateOfBirth = DateTime.Parse(Console.ReadLine()!);

                BloodType bloodType = ReadBloodType();

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
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Помилка: невірний формат дати.");
            }
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

            int id =
                int.Parse(Console.ReadLine()!);

            bool removed =
                clinic.Patients.Remove(id);

            if (removed)
            {
                Console.WriteLine("Пацієнта видалено.");
            }
            else
            {
                Console.WriteLine(
                    "Пацієнта з таким ID не знайдено.");
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
            try
            {
                Console.Write("Ім'я: ");
                string firstName = Console.ReadLine()!;

                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine()!;

                Speciality speciality = ReadSpeciality();

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
                while (!int.TryParse(Console.ReadLine(), out workStartHour))
                {
                    Console.Write("Введіть число: ");
                }

                Console.Write("Кінець роботи (година): ");
                int workEndHour;
                while (!int.TryParse(Console.ReadLine(), out workEndHour))
                {
                    Console.Write("Введіть число: ");
                }

                doctor.Schedule = new WorkSchedule(workStartHour, workEndHour);

                clinic.Doctors.Add(doctor);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
        }
        else if (choice == "3")
        {
            Speciality speciality = ReadSpeciality();

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

            int id =
                int.Parse(Console.ReadLine()!);

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

            int id =
                int.Parse(Console.ReadLine()!);

            bool removed =
                clinic.Doctors.Remove(id);

            if (removed)
            {
                Console.WriteLine("Лікаря видалено.");
            }
            else
            {
                Console.WriteLine(
                    "Лікаря з таким ID не знайдено.");
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
                Console.WriteLine(
                    "Список лікарів порожній.");
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
            try
            {
                Console.WriteLine();
                Console.WriteLine("=== Пацієнти ===");

                clinic.Patients.DisplayAll();

                Console.Write("ID пацієнта: ");
                int patientId = int.Parse(Console.ReadLine()!);

                Console.WriteLine();
                Console.WriteLine("=== Лікарі ===");

                clinic.Doctors.DisplayAll();

                Console.Write("ID лікаря: ");
                int doctorId = int.Parse(Console.ReadLine()!);

                Console.Write("Дата та час (рррр-мм-дд гг:хх): ");
                DateTime scheduledAt = DateTime.Parse(Console.ReadLine()!);

                Console.Write("Тривалість у хвилинах: ");
                int durationMinutes = int.Parse(Console.ReadLine()!);

                clinic.Appointments.Book(
                    patientId,
                    doctorId,
                    scheduledAt,
                    durationMinutes);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Помилка: невірний формат даних.");
            }
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

static void Task03Test(Clinic clinic)
{
    Console.WriteLine();
    Console.WriteLine("========== ТЕСТ TASK 03 ==========");

    Console.WriteLine();
    Console.WriteLine("--- Індексатор PatientManager ---");

    Patient? patient = clinic.Patients[0];

    if (patient == null)
    {
        Console.WriteLine("Пацієнта не знайдено.");
    }
    else
    {
        Console.WriteLine(patient);
    }

    Console.WriteLine();
    Console.WriteLine("--- Індексатор DoctorManager ---");

    Doctor? doctor = clinic.Doctors[0];

    if (doctor == null)
    {
        Console.WriteLine("Лікаря не знайдено.");
    }
    else
    {
        Console.WriteLine(doctor);
    }

    Console.WriteLine();
    Console.WriteLine("--- Індексатор AppointmentManager ---");

    Appointment? appointment = clinic.Appointments[0];

    if (appointment == null)
    {
        Console.WriteLine("Запис не знайдено.");
    }
    else
    {
        clinic.Appointments.DisplayAppointment(
            appointment);
    }

    Console.WriteLine();
    Console.WriteLine("--- ClinicFormatter ---");

    Console.WriteLine(
        "Група крові: " +
        ClinicFormatter.FormatBloodType(
            BloodType.APositive));

    Console.WriteLine(
        "Спеціальність: " +
        ClinicFormatter.FormatSpeciality(
            Speciality.Cardiology));

    Console.WriteLine(
        "Вік: " +
        ClinicFormatter.FormatAge(21));

    Console.WriteLine(
        "Телефон: " +
        ClinicFormatter.FormatPhone(
            "0501234567"));

    Console.WriteLine();
    Console.WriteLine("==================================");
}

static void Task04Test(Clinic clinic)
{
    Console.WriteLine();
    Console.WriteLine("========== ТЕСТ TASK 04 ==========");

    Console.WriteLine();
    Console.WriteLine("--- TryFindById ---");

    int id = 1;

    if (clinic.Patients.TryFindById(id, out Patient p))
    {
        Console.WriteLine(p.FullName);
    }
    else
    {
        Console.WriteLine("не знайдено");
    }

    Console.WriteLine();
    Console.WriteLine("--- ?. оператор ---");

    Console.WriteLine(
        clinic.Patients.FindById(99)?.FullName);

    Console.WriteLine();
    Console.WriteLine("--- ?? оператор ---");

    Console.WriteLine(
        clinic.Patients.FindById(99)?.FullName ??
        "не знайдено");

    Console.WriteLine();
    Console.WriteLine("--- FindBySpeciality(Speciality) ---");

    Doctor[] doctorsByEnum =
        clinic.Doctors.FindBySpeciality(
            Speciality.Cardiology);

    for (int i = 0; i < doctorsByEnum.Length; i++)
    {
        Console.WriteLine(doctorsByEnum[i]);
    }

    Console.WriteLine();
    Console.WriteLine("--- FindBySpeciality(string) ---");

    Doctor[] doctorsByString =
        clinic.Doctors.FindBySpeciality("кардіо");

    for (int i = 0; i < doctorsByString.Length; i++)
    {
        Console.WriteLine(doctorsByString[i]);
    }

    Console.WriteLine();
    Console.WriteLine("--- GetByDate(year, month, day) ---");

    Appointment[] appointments =
        clinic.Appointments.GetByDate(
            2026,
            9,
            25);

    clinic.Appointments.DisplayList(
        appointments);

    Console.WriteLine();
    Console.WriteLine("--- FindByBloodType ---");

    Patient[] bloodTypePatients =
        clinic.Patients.FindByBloodType(
            BloodType.APositive);

    for (int i = 0; i < bloodTypePatients.Length; i++)
    {
        Console.WriteLine(bloodTypePatients[i]);
    }

    Console.WriteLine();
    Console.WriteLine("--- Doctor TryFindById ---");

    if (clinic.Doctors.TryFindById(
            1,
            out Doctor doctor))
    {
        Console.WriteLine(doctor.FullName);
    }
    else
    {
        Console.WriteLine("не знайдено");
    }

    Console.WriteLine();
    Console.WriteLine("--- Value Type ---");

    WorkSchedule morning =
        new WorkSchedule(8, 16);

    WorkSchedule copy = morning;

    Console.WriteLine($"morning: {morning}");
    Console.WriteLine($"copy:    {copy}");

    copy = new WorkSchedule(10, 18);

    Console.WriteLine();
    Console.WriteLine("Після зміни copy:");
    Console.WriteLine($"morning: {morning}");
    Console.WriteLine($"copy:    {copy}");

    Console.WriteLine();
    Console.WriteLine("==================================");
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
        Console.WriteLine(
            "Пацієнта з ID 10 не знайдено.");
    else
        Console.WriteLine($"Знайдено: {found}");

    found = manager.FindById(99);

    if (found == null)
        Console.WriteLine(
            "Пацієнта з ID 99 не знайдено.");
    else
        Console.WriteLine($"Знайдено: {found}");

    Console.WriteLine();
    Console.WriteLine($"Кількість: {manager.Count}");
    Console.WriteLine($"Ємність: {manager.Capacity}");
}

static BloodType ReadBloodType()
{
    Console.WriteLine("Група крові:");

    for (int i = 1; i <= 8; i++)
    {
        Console.WriteLine(
            $"{i} — {ClinicFormatter.FormatBloodType((BloodType)i)}");
    }

    Console.Write("Ваш вибір: ");

    if (int.TryParse(Console.ReadLine(), out int number) &&
        number >= 1 && number <= 8)
    {
        return (BloodType)number;
    }

    return BloodType.Unknown;
}

static Speciality ReadSpeciality()
{
    Console.WriteLine("Спеціальність:");

    for (int i = 1; i <= 8; i++)
    {
        Console.WriteLine(
            $"{i} — {ClinicFormatter.FormatSpeciality((Speciality)(i - 1))}");
    }

    Console.Write("Ваш вибір: ");

    if (int.TryParse(Console.ReadLine(), out int number) &&
        number >= 1 && number <= 8)
    {
        return (Speciality)(number - 1);
    }

    return Speciality.General;
}
static bool ReadInt(string prompt, out int value)
{
    Console.Write(prompt);

    if (int.TryParse(Console.ReadLine(), out value))
        return true;

    Console.WriteLine("Помилка: введіть ціле число.");
    return false;
}

static bool ReadDouble(string prompt, out double value)
{
    Console.Write(prompt);

    if (double.TryParse(Console.ReadLine(), NumberStyles.Float,
            CultureInfo.InvariantCulture, out value))
        return true;

    Console.WriteLine("Помилка: введіть число з крапкою, напр. 6.2.");
    return false;
}

static bool ReadYesNo(string prompt, out bool value)
{
    value = false;
    Console.Write(prompt);

    string? input = Console.ReadLine();

    if (input == "1")
    {
        value = true;
        return true;
    }

    if (input == "0")
        return true;

    Console.WriteLine("Помилка: введіть 1 або 0.");
    return false;
}

static bool ReadExistingPatientId(Clinic clinic, out int id)
{
    if (!ReadInt("ID пацієнта: ", out id))
        return false;

    if (clinic.Patients.FindById(id) == null)
    {
        Console.WriteLine("Пацієнта з таким ID не знайдено.");
        return false;
    }

    return true;
}

static bool ReadExistingDoctorId(Clinic clinic, out int id)
{
    if (!ReadInt("ID лікаря: ", out id))
        return false;

    if (clinic.Doctors.FindById(id) == null)
    {
        Console.WriteLine("Лікаря з таким ID не знайдено.");
        return false;
    }

    return true;
}
static void MedicalRecordsMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("========== МЕДИЧНА КАРТКА ==========");
        Console.WriteLine("1 — Картка пацієнта");
        Console.WriteLine("2 — Усі записи пацієнта");
        Console.WriteLine("3 — Додати діагноз");
        Console.WriteLine("4 — Додати аналіз");
        Console.WriteLine("5 — Додати рецепт");
        Console.WriteLine("6 — Записи лікаря");
        Console.WriteLine("0 — Назад");
        Console.WriteLine("====================================");
        Console.Write("Оберіть: ");

        string choice = Console.ReadLine()!;

        if (choice == "1")
        {
            clinic.Patients.DisplayAll();

            if (!ReadExistingPatientId(clinic, out int patientId))
                continue;

            clinic.MedicalRecords.DisplayPatientSummary(patientId);
        }
        else if (choice == "2")
        {
            clinic.Patients.DisplayAll();

            if (!ReadExistingPatientId(clinic, out int patientId))
                continue;

            MedicalRecord[] records =
                clinic.MedicalRecords.GetByPatient(patientId);

            if (records.Length == 0)
            {
                Console.WriteLine("У пацієнта немає записів.");
            }
            else
            {
                for (int i = 0; i < records.Length; i++)
                {
                    Console.WriteLine(
                        $"{records[i].GetRecordType()}: {records[i].GetSummary()}");
                }
            }
        }
        else if (choice == "3")
        {
            try
            {
                clinic.Patients.DisplayAll();
                clinic.Doctors.DisplayAll();

                if (!ReadExistingPatientId(clinic, out int patientId))
                    continue;

                if (!ReadExistingDoctorId(clinic, out int doctorId))
                    continue;

                Console.Write("Код діагнозу (напр. J06.9): ");
                string code = Console.ReadLine()!;

                Console.Write("Опис: ");
                string description = Console.ReadLine()!;

                if (!ReadYesNo("Хронічне? (1=так, 0=ні): ", out bool isChronic))
                    continue;

                Diagnosis diagnosis = new Diagnosis(
                    patientId, doctorId, DateTime.Today,
                    code, description, isChronic);

                clinic.MedicalRecords.Add(diagnosis);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
        }
        else if (choice == "4")
        {
            try
            {
                clinic.Patients.DisplayAll();
                clinic.Doctors.DisplayAll();

                if (!ReadExistingPatientId(clinic, out int patientId))
                    continue;

                if (!ReadExistingDoctorId(clinic, out int doctorId))
                    continue;

                Console.Write("Назва аналізу: ");
                string testName = Console.ReadLine()!;

                if (!ReadDouble("Значення (число): ", out double value))
                    continue;

                Console.Write("Одиниці виміру: ");
                string unit = Console.ReadLine()!;

                Console.Write("Норма (напр. 4.0–9.0): ");
                string range = Console.ReadLine()!;

                if (!ReadYesNo("В нормі? (1=так, 0=ні): ", out bool isNormal))
                    continue;

                LabResult lab = new LabResult(
                    patientId, doctorId, DateTime.Today,
                    testName, value, unit, range, isNormal);

                clinic.MedicalRecords.Add(lab);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
        }
        else if (choice == "5")
        {
            try
            {
                clinic.Patients.DisplayAll();
                clinic.Doctors.DisplayAll();

                if (!ReadExistingPatientId(clinic, out int patientId))
                    continue;

                if (!ReadExistingDoctorId(clinic, out int doctorId))
                    continue;

                Console.Write("Препарат: ");
                string medication = Console.ReadLine()!;

                Console.Write("Дозування (напр. 10 мг): ");
                string dosage = Console.ReadLine()!;

                if (!ReadInt("Кількість днів: ", out int days))
                    continue;

                Console.Write("Інструкція (Enter — пропустити): ");
                string instructions = Console.ReadLine() ?? "";

                Prescription prescription = new Prescription(
                    patientId, doctorId, DateTime.Today,
                    medication, dosage, days, instructions);

                clinic.MedicalRecords.Add(prescription);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
        }
        else if (choice == "6")
        {
            clinic.Doctors.DisplayAll();

            if (!ReadExistingDoctorId(clinic, out int doctorId))
                continue;

            clinic.MedicalRecords.DisplayList(
                clinic.MedicalRecords.GetByDoctor(doctorId));
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
