using System.Text;
using ClinicApp.Enums;
using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class PatientPassportWriter
{
    private readonly Clinic _clinic;
    private readonly string _folder;

    public PatientPassportWriter(Clinic clinic, string folder = "patients")
    {
        _clinic = clinic;
        _folder = folder;

        Directory.CreateDirectory(_folder);
    }

    public void OnPatientAdded(object? sender, PatientEventArgs e) =>
        WritePassport(e.PatientId);

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e) =>
        WritePassport(e.PatientId);

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e) =>
        WritePassport(e.PatientId);

    private void WritePassport(int patientId)
    {
        Patient? patient = _clinic.Patients.FindById(patientId);

        if (patient == null)
        {
            return;
        }

        MedicalRecord[] records = _clinic.MedicalRecords.GetByPatient(patientId);
        Appointment[] appointments = _clinic.Appointments.GetByPatient(patientId);
        TreatmentPlan[] plans = _clinic.TreatmentPlans.GetByPatient(patientId);
        decimal debt = _clinic.Billing.GetPatientDebt(patientId);

        string path = Path.Combine(_folder, $"passport_{patientId}.txt");

        try
        {
            using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);

            writer.WriteLine($"=== Паспорт пацієнта #{patientId} ===");
            writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
            writer.WriteLine();

            writer.WriteLine($"Ім'я: {patient.FullName}");
            writer.WriteLine(
                $"Дата народження: {patient.DateOfBirth:dd.MM.yyyy} ({patient.Age} р.)"); // ПЕРЕВІР: DateOfBirth
            writer.WriteLine(
                $"Група крові: {ClinicFormatter.FormatBloodType(patient.BloodType)}   " +
                $"Телефон: {patient.Phone}"); // ПЕРЕВІР: Phone
            writer.WriteLine();

            writer.WriteLine("-- Діагнози --");
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] is Diagnosis d)
                {
                    writer.WriteLine(d.GetSummary());
                }
            }

            writer.WriteLine("-- Аналізи --");
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] is LabResult l)
                {
                    writer.WriteLine(l.GetSummary());
                }
            }

            writer.WriteLine("-- Рецепти --");
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] is Prescription p)
                {
                    writer.WriteLine(p.GetSummary());
                }
            }

            writer.WriteLine();
            writer.WriteLine("-- Записи --");
            for (int i = 0; i < appointments.Length; i++)
            {
                Appointment a = appointments[i];
                writer.WriteLine(
                    $"[{a.Id}] {a.GetDescription()} | {a.ScheduledAt:dd.MM.yyyy HH:mm} | " +
                    $"{a.Status} | {a.GetCost().ToString("F2")} грн");
            }

            writer.WriteLine();
            writer.WriteLine("-- Плани лікування --");
            for (int i = 0; i < plans.Length; i++)
            {
                writer.WriteLine(plans[i]);
            }

            writer.WriteLine();
            writer.WriteLine($"Заборгованість: {debt.ToString("F2")} грн");
        }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }
    }
}