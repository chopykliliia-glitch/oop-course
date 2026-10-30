using System.Globalization;
using ClinicApp;
using ClinicApp.Enums;
using ClinicApp.Interfaces;
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

clinic.Appointments.BookUrgent(
    2,
    2,
    new DateTime(2026, 9, 25, 12, 0, 0),
    "біль у грудях");

clinic.Appointments.BookSpecialist(
    1,
    1,
    new DateTime(2026, 9, 25, 14, 0, 0),
    "кардіологія",
    60);

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


UrgentAppointment urgentObj = new UrgentAppointment(
    1, 1, DateTime.Today.AddDays(1).AddHours(14), "тест");

Appointment viaBase = urgentObj;
UrgentAppointment viaUrgent = urgentObj;

Console.WriteLine("== new vs override ==");
Console.WriteLine(
    $"Через Appointment:       {viaBase.GetDescription()} | пріоритет {viaBase.GetPriority()}");
Console.WriteLine(
    $"Через UrgentAppointment: {viaUrgent.GetDescription()} | пріоритет {viaUrgent.GetPriority()}");

// 1) Чому GetDescription() через змінну типу Appointment дає опис термінового прийому?
// Відповідь: Метод virtual, а в UrgentAppointment він override. Тому C# дивиться
// на тип об'єкта (UrgentAppointment), а не змінної, і виконує версію підкласу.
//
// 2) Чому GetPriority() через ту саму змінну повертає 3, хоча об'єкт — UrgentAppointment?
// Відповідь: Метод не virtual, а в підкласі прихований через new. Такий метод
// обирається за типом змінної (Appointment), тому повертається 3.
//
// 3) Що треба змінити в Appointment і UrgentAppointment, щоб GetPriority() теж працював поліморфно?
// Відповідь: В Appointment додати virtual, а в UrgentAppointment замінити new на override.
while (true)
{
    Console.WriteLine();
    Console.WriteLine("╔══════════════════════════════════════════════╗");
    Console.WriteLine("║           МЕДИЧНА КЛІНІКА                    ║");
    Console.WriteLine("╠══════════════════════════════════════════════╣");
    Console.WriteLine("║  1. Пацієнти       — реєстрація, пошук       ║");
    Console.WriteLine("║  2. Лікарі         — персонал, розклад       ║");
    Console.WriteLine("║  3. Записи         — прийоми, скасування     ║");
    Console.WriteLine("║  4. Медична картка — діагнози, рецепти       ║");
    Console.WriteLine("║  5. Рахунки        — оплата, борги           ║");
    Console.WriteLine("║  6. Звіт           — загальна статистика     ║");
    Console.WriteLine("║  0. Вийти                                    ║");
    Console.WriteLine("╚══════════════════════════════════════════════╝");
    Console.Write("Оберіть розділ: ");

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
        BillingMenu(clinic);
    }
    else if (choice == "6")
    {
        clinic.GenerateReport();
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
            if (!ReadInt("Введіть ID пацієнта: ", out int id))
                continue;

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
        Console.WriteLine("5 — Вільні години лікаря");
        Console.WriteLine("6 — Видалити");
        Console.WriteLine("7 — Статистика");
        Console.WriteLine("8 — Показати всіх через GetAll()");
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
            if (!ReadInt("Введіть ID лікаря: ", out int id))
                continue;

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
            if (!ReadInt("ID лікаря: ", out int doctorId))
                continue;

            Doctor? selected = clinic.Doctors.FindById(doctorId);

            if (selected == null)
            {
                Console.WriteLine("Лікаря не знайдено");
                continue;
            }

            ISchedulable schedulable = selected;

            Console.Write("Дата (dd.MM.yyyy): ");

            if (!DateTime.TryParseExact(
                    Console.ReadLine(),
                    "dd.MM.yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime date))
            {
                Console.WriteLine("Помилка: невірний формат дати.");
                continue;
            }

            if (!ReadInt("Скільки слотів: ", out int slotCount))
                continue;

            try
            {
                DateTime[] slots =
                    schedulable.GetAvailableSlots(date, slotCount);

                for (int i = 0; i < slots.Length; i++)
                {
                    Console.WriteLine(slots[i].ToString("HH:mm"));
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
        }
        else if (choice == "6")
        {
            if (!ReadInt("Введіть ID лікаря: ", out int id))
                continue;

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
        else if (choice == "7")
        {
            clinic.Doctors.DisplayStats();
        }
        else if (choice == "8")
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
        Console.WriteLine("8 — Скасувати всі записи пацієнта");
        Console.WriteLine("9 — За типом прийому");
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

            if (!ReadInt("ID пацієнта: ", out int patientId))
                continue;

            Appointment[] appointments =
                clinic.Appointments.GetByPatient(
                    patientId);

            clinic.Appointments.DisplayList(
                appointments);
        }
        else if (choice == "4")
        {
            clinic.Doctors.DisplayAll();

            if (!ReadInt("ID лікаря: ", out int doctorId))
                continue;

            Appointment[] appointments =
                clinic.Appointments.GetByDoctor(
                    doctorId);

            clinic.Appointments.DisplayList(
                appointments);
        }
        else if (choice == "5")
        {
            Console.Write("Дата (рррр-мм-дд): ");

            if (!DateTime.TryParse(
                    Console.ReadLine(),
                    out DateTime date))
            {
                Console.WriteLine("Помилка: невірний формат дати.");
                continue;
            }

            Appointment[] appointments =
                clinic.Appointments.GetByDate(date);

            clinic.Appointments.DisplayList(
                appointments);
        }
        else if (choice == "6")
        {
            if (!ReadInt("ID запису: ", out int id))
                continue;

            Console.Write("Причина скасування: ");

            string reason =
                Console.ReadLine() ?? "";

            clinic.Appointments.Cancel(
                id,
                reason);
        }
        else if (choice == "7")
        {
            if (!ReadInt("ID запису: ", out int id))
                continue;

            clinic.Appointments.Complete(id);
        }
        else if (choice == "8")
        {
            if (!ReadInt("ID пацієнта: ", out int patientId))
                continue;

            Console.Write("Причина (Enter — без причини): ");
            string reason = Console.ReadLine() ?? "";

            Appointment[] mine =
                clinic.Appointments.GetByPatient(patientId);

            int cancelled = AppointmentManager.CancelAll(mine, reason);

            Console.WriteLine($"Скасовано записів: {cancelled}");
        }
        else if (choice == "9")
        {
            Console.WriteLine("  1. Термінові");
            Console.WriteLine("  2. Консультації спеціаліста");
            Console.WriteLine("  3. Звичайні");
            Console.Write("Оберіть: ");

            string typeChoice = Console.ReadLine()!;

            if (typeChoice == "1")
            {
                clinic.Appointments.DisplayList(
                    clinic.Appointments.GetUrgent());
            }
            else if (typeChoice == "2")
            {
                clinic.Appointments.DisplayList(
                    clinic.Appointments.GetSpecialist());
            }
            else if (typeChoice == "3")
            {
                clinic.Appointments.DisplayList(
                    clinic.Appointments.GetRegular());
            }
            else
            {
                Console.WriteLine("Невірний вибір.");
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

static void BillingMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("── Рахунки ───────────────────");
        Console.WriteLine("  1. Борги пацієнта");
        Console.WriteLine("  2. Всі неоплачені записи");
        Console.WriteLine("  3. Оплатити запис");
        Console.WriteLine("  4. Загальний борг клініки");
        Console.WriteLine("  0. Назад");
        Console.Write("Оберіть: ");

        string choice = Console.ReadLine()!;

        if (choice == "1")
        {
            if (!ReadInt("ID пацієнта: ", out int patientId))
                continue;

            IPayable[] unpaid =
                clinic.Billing.GetUnpaidByPatient(patientId);

            Console.WriteLine($"Неоплачені записи пацієнта #{patientId}:");
            clinic.Billing.DisplayUnpaid(unpaid);

            decimal debt = clinic.Billing.GetPatientDebt(patientId);
            Console.WriteLine($"Борг: {debt.ToString("F2")} грн");
        }
        else if (choice == "2")
        {
            IPayable[] unpaid = clinic.Billing.GetAllUnpaid();

            clinic.Billing.DisplayUnpaid(unpaid);
        }
        else if (choice == "3")
        {
            if (!ReadInt("ID запису для оплати: ", out int appointmentId))
                continue;

            if (clinic.Billing.PayAppointment(appointmentId))
            {
                Console.WriteLine($"Запис [{appointmentId}] оплачено.");
            }
            else
            {
                Console.WriteLine(
                    "Не вдалося оплатити: запис не знайдено, вже оплачено або скасовано.");
            }
        }
        else if (choice == "4")
        {
            decimal total = clinic.Billing.GetTotalDebt();

            Console.WriteLine(
                $"Загальний борг по клініці: {total.ToString("F2")} грн");
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