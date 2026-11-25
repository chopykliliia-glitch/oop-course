using System.Text;
using ClinicApp.Events;

namespace ClinicApp.Utils;

public class ClinicLogger
{
    private readonly string _logPath;

    public ClinicLogger(string logPath = "clinic.log")
    {
        _logPath = logPath;
    }

    public void LogInfo(string message)
    {
        Write("INFO ", message);
    }

    public void LogWarning(string message)
    {
        Write("WARN ", message);
    }

    public void LogError(string message)
    {
        Write("ERROR", message);
    }

    // ===== Обробники подій =====

    public void OnPatientAdded(object? sender, PatientEventArgs e) =>
        LogInfo($"Новий пацієнт #{e.PatientId}: {e.FullName}");

    public void OnAppointmentBooked(object? sender, AppointmentEventArgs e) =>
        LogInfo(
            $"Запис #{e.AppointmentId} створено: " +
            $"пацієнт #{e.PatientId} → лікар #{e.DoctorId}");

    public void OnUrgentBooked(object? sender, AppointmentEventArgs e)
    {
        string text = $"ТЕРМІНОВИЙ запис #{e.AppointmentId}: {e.Notes}";

        LogWarning(text);
        WriteAlert(text);
    }

    public void OnAppointmentCancelled(object? sender, AppointmentEventArgs e) =>
        LogInfo($"Запис #{e.AppointmentId} скасовано");

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e) =>
        LogInfo($"Запис #{e.AppointmentId} завершено");

    public void OnPaymentReceived(object? sender, PaymentEventArgs e) =>
        LogInfo($"Оплата запису #{e.AppointmentId}: {e.Amount.ToString("F2")} грн");

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e) =>
        LogInfo(
            $"План #{e.PlanId} завершено: " +
            $"пацієнт #{e.PatientId}, {e.Diagnosis}");

    // ===== Решта класу =====

    public bool Exists()
    {
        return File.Exists(_logPath);
    }

    public string[] GetLastLines(int n)
    {
        if (n <= 0 || !Exists())
        {
            return new string[0];
        }

        string[] allLines = File.ReadAllLines(_logPath, Encoding.UTF8);

        int start = allLines.Length - n;
        if (start < 0)
        {
            start = 0;
        }

        int count = allLines.Length - start;
        string[] result = new string[count];
        Array.Copy(allLines, start, result, 0, count);

        return result;
    }

    public void Clear()
    {
        File.Delete(_logPath);
    }

    private void WriteAlert(string message)
    {
        try
        {
            Directory.CreateDirectory("alerts");

            string path = Path.Combine(
                "alerts",
                $"urgent_{DateTime.Now:yyyy-MM-dd}.txt");

            string line =
                $"[{DateTime.Now:HH:mm:ss}] {message}" +
                Environment.NewLine;

            File.AppendAllText(path, line, Encoding.UTF8);
        }
        catch (IOException)
        {
            // збій логування не повинен валити програму
        }
        catch (UnauthorizedAccessException)
        {
        }
    }

    private void Write(string level, string message)
    {
        string line =
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}" +
            Environment.NewLine;

        try
        {
            File.AppendAllText(_logPath, line, Encoding.UTF8);
        }
        catch (IOException)
        {
            // збій логування не повинен валити програму
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}