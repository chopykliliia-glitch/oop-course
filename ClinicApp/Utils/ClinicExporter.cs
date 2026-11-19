using System.Text;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class ClinicExporter
{
    private const string Separator = "----------------------------------------";

    private readonly Clinic _clinic;
    private readonly string _baseDir;

    public ClinicExporter(Clinic clinic, string baseDir = "reports")
    {
        _clinic = clinic;
        _baseDir = baseDir;
    }

    public string ExportPatients()
    {
        string path = GetReportPath("patients.txt");
        Patient[] patients = _clinic.Patients.GetAll();

        using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);

        WriteHeader(writer, "Пацієнти");

        for (int i = 0; i < patients.Length; i++)
        {
            Patient p = patients[i];

            writer.WriteLine(
                $"{i + 1,3}. [{p.Id}] {p.FullName} | " +
                $"{ClinicFormatter.FormatAge(p.Age)} | " +
                $"{ClinicFormatter.FormatBloodType(p.BloodType)} | " +
                $"{ClinicFormatter.FormatPhone(p.Phone)}");
        }

        WriteFooter(writer, $"Всього: {patients.Length}");

        return path;
    }

    public string ExportAppointments()
    {
        string path = GetReportPath("appointments.txt");
        Appointment[] appointments = _clinic.Appointments.GetAll();

        using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);

        WriteHeader(writer, "Записи на прийом");

        for (int i = 0; i < appointments.Length; i++)
        {
            writer.WriteLine($"{i + 1,3}. {appointments[i]}");
        }

        WriteFooter(writer, $"Всього: {appointments.Length}");

        return path;
    }

    public string ExportBilling()
    {
        string path = GetReportPath("billing.txt");
        IPayable[] unpaid = _clinic.Billing.GetAllUnpaid();
        decimal totalDebt = _clinic.Billing.GetTotalDebt();

        using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);

        WriteHeader(writer, "Неоплачені рахунки");

        for (int i = 0; i < unpaid.Length; i++)
        {
            writer.WriteLine($"{i + 1,3}. {unpaid[i]}");
        }

        WriteFooter(
            writer,
            $"Неоплачених: {unpaid.Length}",
            $"Загальний борг: {totalDebt.ToString("F2")} грн");

        return path;
    }

    public string ExportTreatmentPlans()
    {
        string path = GetReportPath("treatment_plans.txt");
        TreatmentPlan[] plans = _clinic.TreatmentPlans.GetAll();

        using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);

        WriteHeader(writer, "Плани лікування");

        for (int i = 0; i < plans.Length; i++)
        {
            writer.WriteLine($"{i + 1,3}. {plans[i]}");
        }

        WriteFooter(writer, $"Всього: {plans.Length}");

        return path;
    }

    public string[] ExportAll()
    {
        return new string[]
        {
            ExportPatients(),
            ExportAppointments(),
            ExportBilling(),
            ExportTreatmentPlans()
        };
    }

    // Єдине місце, де створюється тека з датою і формується шлях до файлу
    private string GetReportPath(string fileName)
    {
        string dir = Path.Combine(_baseDir, DateTime.Now.ToString("yyyy-MM-dd"));
        Directory.CreateDirectory(dir);

        return Path.Combine(dir, fileName);
    }

    private static void WriteHeader(StreamWriter writer, string title)
    {
        writer.WriteLine($"=== {title} — звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine(Separator);
    }

    private static void WriteFooter(StreamWriter writer, params string[] summaryLines)
    {
        writer.WriteLine(Separator);

        for (int i = 0; i < summaryLines.Length; i++)
        {
            writer.WriteLine(summaryLines[i]);
        }
    }
}