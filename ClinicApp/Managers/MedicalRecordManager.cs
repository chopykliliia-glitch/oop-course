using ClinicApp.Models;

namespace ClinicApp.Managers;

public class MedicalRecordManager
{
    private const int MaxRecords = 1000;
    private readonly MedicalRecord[] _records = new MedicalRecord[MaxRecords];
    private int _count;

    public int Count => _count;

    public MedicalRecord? this[int index] =>
        index >= 0 && index < _count ? _records[index] : null;

    public void Add(MedicalRecord record)
    {
        if (_count >= MaxRecords)
        {
            Console.WriteLine("Досягнуто ліміт записів.");
            return;
        }
        _records[_count++] = record;
        Console.WriteLine($"Запис [{record.Id}] {record.GetRecordType()} додано.");
    }

    public MedicalRecord? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
            if (_records[i].Id == id)
                return _records[i];
        return null;
    }

    public MedicalRecord[] GetByPatient(int patientId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId) n++;

        MedicalRecord[] result = new MedicalRecord[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId)
                result[k++] = _records[i];
        return result;
    }

    public MedicalRecord[] GetByDoctor(int doctorId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].DoctorId == doctorId) n++;

        MedicalRecord[] result = new MedicalRecord[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].DoctorId == doctorId)
                result[k++] = _records[i];
        return result;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Записів немає.");
            return;
        }
        for (int i = 0; i < _count; i++)
            Console.WriteLine(_records[i]);
    }

    public void DisplayList(MedicalRecord[] records)
    {
        if (records.Length == 0)
        {
            Console.WriteLine("Записів не знайдено.");
            return;
        }
        for (int i = 0; i < records.Length; i++)
            Console.WriteLine(records[i]);
    }

    public Diagnosis[] GetDiagnoses(int patientId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis)
                n++;

        Diagnosis[] result = new Diagnosis[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis d)
                result[k++] = d;
        return result;
    }

    public LabResult[] GetLabResults(int patientId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is LabResult)
                n++;

        LabResult[] result = new LabResult[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is LabResult l)
                result[k++] = l;
        return result;
    }

    public Prescription[] GetPrescriptions(int patientId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Prescription)
                n++;

        Prescription[] result = new Prescription[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Prescription p)
                result[k++] = p;
        return result;
    }

    public Diagnosis[] GetChronicDiagnoses(int patientId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis d && d.IsChronic)
                n++;

        Diagnosis[] result = new Diagnosis[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis d && d.IsChronic)
                result[k++] = d;
        return result;
    }

    public Prescription[] GetActivePrescriptions(int patientId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Prescription p && p.IsActive())
                n++;

        Prescription[] result = new Prescription[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Prescription p && p.IsActive())
                result[k++] = p;
        return result;
    }

    public void DisplayPatientSummary(int patientId)
    {
        MedicalRecord[] records = GetByPatient(patientId);
        if (records.Length == 0)
        {
            Console.WriteLine($"Для пацієнта #{patientId} записів немає.");
            return;
        }

        int diagnoses = 0, labs = 0, prescriptions = 0;
        for (int i = 0; i < records.Length; i++)
        {
            if (records[i] is Diagnosis) diagnoses++;
            else if (records[i] is LabResult) labs++;
            else if (records[i] is Prescription) prescriptions++;
        }

        Console.WriteLine($"=== Медична картка пацієнта #{patientId} ===");
        Console.WriteLine($"Всього записів: {records.Length} (діагнозів: {diagnoses}, аналізів: {labs}, рецептів: {prescriptions})");

        Diagnosis[] chronic = GetChronicDiagnoses(patientId);
        if (chronic.Length > 0)
        {
            Console.WriteLine($"Хронічні діагнози ({chronic.Length}):");
            for (int i = 0; i < chronic.Length; i++)
                Console.WriteLine("  " + chronic[i]);
        }

        Prescription[] active = GetActivePrescriptions(patientId);
        if (active.Length > 0)
        {
            Console.WriteLine($"Активні рецепти ({active.Length}):");
            for (int i = 0; i < active.Length; i++)
                Console.WriteLine($"  {active[i]} | до {active[i].ExpiresAt:dd.MM.yyyy}");
        }
    }
}