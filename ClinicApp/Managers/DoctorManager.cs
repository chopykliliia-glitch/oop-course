using ClinicApp.Models;
using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Managers;


public class DoctorManager
{
    private const int MaxDoctors = 50;

    private Doctor[] _doctors = new Doctor[MaxDoctors];

    private int _count = 0;

    public int Count
    {
        get
        {
            return _count;
        }
    }

    public Doctor? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                return null;
            }

            return _doctors[index];
        }
    }

    public void Add(Doctor doctor)
    {
        if (_count >= MaxDoctors)
        {
            Console.WriteLine("Досягнуто ліміту лікарів.");
            return;
        }

        _doctors[_count] = doctor;
        _count++;

        Console.WriteLine($"Лікаря [{doctor.Id}] {doctor.FullName} додано.");
    }

    public Doctor? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                return _doctors[i];
            }
        }

        return null;
    }

    public bool TryFindById(int id, out Doctor doctor)
    {
        Doctor? found = FindById(id);

        if (found == null)
        {
            doctor = null!;
            return false;
        }

        doctor = found;
        return true;
    }
    
    public Doctor[] FindBySpeciality(string speciality)
    {
        int foundCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (ClinicFormatter
                .FormatSpeciality(_doctors[i].Speciality)
                .ToLower()
                .Contains(speciality.ToLower()))
            {
                foundCount++;
            }
        }

        Doctor[] result = new Doctor[foundCount];

        int resultIndex = 0;

        for (int i = 0; i < _count; i++)
        {
            if (ClinicFormatter
                .FormatSpeciality(_doctors[i].Speciality)
                .ToLower()
                .Contains(speciality.ToLower()))
            {
                result[resultIndex] = _doctors[i];
                resultIndex++;
            }
        }

        return result;
    }
    public Doctor[] FindBySpeciality(Speciality speciality)
    {
        int foundCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality == speciality)
            {
                foundCount++;
            }
        }

        Doctor[] result = new Doctor[foundCount];

        int resultIndex = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality == speciality)
            {
                result[resultIndex] = _doctors[i];
                resultIndex++;
            }
        }

        return result;
    }

    public Doctor[] GetAll()
    {
        Doctor[] result = new Doctor[_count];

        for (int i = 0; i < _count; i++)
        {
            result[i] = _doctors[i];
        }

        return result;
    }

    public bool Remove(int id)
    {
        int foundIndex = -1;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                foundIndex = i;
                break;
            }
        }

        if (foundIndex == -1)
        {
            return false;
        }

        for (int i = foundIndex; i < _count - 1; i++)
        {
            _doctors[i] = _doctors[i + 1];
        }

        _doctors[_count - 1] = null!;
        _count--;

        return true;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Список лікарів порожній.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"=== Лікарі ({_count} / {MaxDoctors}) ===");

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_doctors[i]);
        }

        Console.WriteLine("────────────────────────────────────────────────────────────");
    }

    public void DisplayStats()
    {
        if (_count == 0)
        {
            Console.WriteLine("Немає лікарів для статистики.");
            return;
        }

        int availableCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].IsAvailableNow)
            {
                availableCount++;
            }
        }

        Console.WriteLine();
        Console.WriteLine("=== Статистика лікарів ===");
        Console.WriteLine($"Всього:         {_count}");
        Console.WriteLine($"Доступні зараз: {availableCount}");
        Console.WriteLine("По спеціальностях:");

        for (int i = 0; i < _count; i++)
        {
            bool alreadyShown = false;

            for (int j = 0; j < i; j++)
            {
                if (_doctors[j].Speciality == _doctors[i].Speciality)
                {
                    alreadyShown = true;
                    break;
                }
            }

            if (alreadyShown)
            {
                continue;
            }

            int specialityCount = 0;

            for (int j = 0; j < _count; j++)
            {
                if (_doctors[j].Speciality == _doctors[i].Speciality)
                {
                    specialityCount++;
                }
            }

            Console.WriteLine(
                $"  {ClinicFormatter.FormatSpeciality(_doctors[i].Speciality)}: {specialityCount}");
        }

        Console.WriteLine("==========================");
    }
}