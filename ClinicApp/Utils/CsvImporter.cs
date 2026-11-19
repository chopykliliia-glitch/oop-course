using System.Globalization;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class CsvImporter
{
    private const int ExpectedFields = 5;

    public ImportResult ImportPatients(Clinic clinic, string filePath)
    {
        ImportResult result = new ImportResult();

        if (!File.Exists(filePath))
        {
            result.AddError(0, $"Файл не знайдено: {filePath}");
            return result;
        }

        string[] lines;

        try
        {
            lines = File.ReadAllLines(filePath, Encoding.UTF8);
        }
        catch (IOException e)
        {
            result.AddError(0, "Не вдалося прочитати файл: " + e.Message);
            return result;
        }
        catch (UnauthorizedAccessException e)
        {
            result.AddError(0, "Немає доступу до файлу: " + e.Message);
            return result;
        }
        
        for (int i = 1; i < lines.Length; i++)
        {
            int lineNumber = i + 1;

            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                continue;
            }
            
            try
            {
                Patient patient = ParsePatient(lines[i]);

                clinic.Patients.Add(patient);
                result.AddSuccess();
            }
            catch (FormatException e)
            {
                result.AddError(lineNumber, e.Message);
            }
            catch (ArgumentException e)
            {

                result.AddError(lineNumber, e.Message);
            }
        }

        return result;
    }

    private static Patient ParsePatient(string line)
    {
        string[] parts = line.Split(',');

        if (parts.Length != ExpectedFields)
        {
            throw new FormatException(
                $"Рядок має {parts.Length} {FieldWord(parts.Length)} замість {ExpectedFields}.");
        }

        string firstName = parts[0].Trim();
        string lastName = parts[1].Trim();
        string dateText = parts[2].Trim();
        string bloodText = parts[3].Trim();
        string phone = parts[4].Trim();

        DateTime dateOfBirth;

        try
        {
            dateOfBirth = DateTime.ParseExact(
                dateText,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture);
        }
        catch (FormatException)
        {
            throw new FormatException(
                $"Невірна дата '{dateText}' (очікується dd.MM.yyyy).");
        }

        BloodType bloodType;

        try
        {
            bloodType = (BloodType)Enum.Parse(typeof(BloodType), bloodText);
        }
        catch (ArgumentException)
        {
            throw new FormatException($"Невідома група крові '{bloodText}'.");
        }
        catch (OverflowException)
        {
            throw new FormatException($"Невідома група крові '{bloodText}'.");
        }
        
        if (!Enum.IsDefined(typeof(BloodType), bloodType))
        {
            throw new FormatException($"Невідома група крові '{bloodText}'.");
        }
        
        return new Patient(firstName, lastName, dateOfBirth, bloodType, phone);
    }

    private static string FieldWord(int count)
    {
        int lastTwo = count % 100;
        int last = count % 10;

        if (lastTwo >= 11 && lastTwo <= 19)
        {
            return "полів";
        }

        if (last == 1)
        {
            return "поле";
        }

        if (last >= 2 && last <= 4)
        {
            return "поля";
        }

        return "полів";
    }
}