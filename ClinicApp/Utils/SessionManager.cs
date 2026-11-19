using System.Globalization;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class SessionManager
{
    private const string PatientsSection = "[PATIENTS]";
    private const string EndMarker = "[END]";
    private const string DateFormat = "dd.MM.yyyy";
    private const int ExpectedFields = 5;

    private readonly string _sessionPath;

    public SessionManager(string sessionPath = "session.dat")
    {
        _sessionPath = sessionPath;
    }

    public bool Exists()
    {
        return File.Exists(_sessionPath);
    }

    public void Save(Clinic clinic)
    {
        Patient[] patients = clinic.Patients.GetAll();
        
        using StreamWriter writer = new StreamWriter(_sessionPath, false, Encoding.UTF8);

        writer.WriteLine(PatientsSection);

        for (int i = 0; i < patients.Length; i++)
        {
            Patient p = patients[i];

            writer.WriteLine(
                $"{p.FirstName},{p.LastName},{p.DateOfBirth:dd.MM.yyyy},{p.BloodType},{p.Phone}");
        }

        writer.WriteLine(EndMarker);
    }

    public int Load(Clinic clinic)
    {
        if (!Exists())
        {
            return 0;
        }

        string[] lines = File.ReadAllLines(_sessionPath, Encoding.UTF8);

        string currentSection = "";
        int added = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (line == "")
            {
                continue;
            }
            
            if (line.StartsWith("["))
            {
                currentSection = line;
                continue;
            }
            
            if (currentSection == "")
            {
                continue;
            }

            if (currentSection == PatientsSection)
            {
                if (TryLoadPatient(clinic, line))
                {
                    added++;
                }
            }
        }

        return added;
    }

    private static bool TryLoadPatient(Clinic clinic, string line)
    {
        try
        {
            string[] parts = line.Split(',');

            if (parts.Length != ExpectedFields)
            {
                return false;
            }

            string firstName = parts[0].Trim();
            string lastName = parts[1].Trim();

            DateTime dateOfBirth = DateTime.ParseExact(
                parts[2].Trim(),
                DateFormat,
                CultureInfo.InvariantCulture);

            BloodType bloodType =
                (BloodType)Enum.Parse(typeof(BloodType), parts[3].Trim());

            if (!Enum.IsDefined(typeof(BloodType), bloodType))
            {
                return false;
            }

            string phone = parts[4].Trim();

            if (PatientExists(clinic, firstName, lastName, dateOfBirth))
            {
                return false;
            }

            Patient patient = new Patient(
                firstName,
                lastName,
                dateOfBirth,
                bloodType,
                phone);

            clinic.Patients.Add(patient);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }
        catch (OverflowException)
        {
            return false;
        }
    }
    
    private static bool PatientExists(
        Clinic clinic,
        string firstName,
        string lastName,
        DateTime dateOfBirth)
    {
        Patient[] existing = clinic.Patients.GetAll();

        for (int i = 0; i < existing.Length; i++)
        {
            if (existing[i].FirstName == firstName &&
                existing[i].LastName == lastName &&
                existing[i].DateOfBirth.Date == dateOfBirth.Date)
            {
                return true;
            }
        }

        return false;
    }
}