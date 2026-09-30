namespace ClinicApp;

public static class ClinicFormatter
{
    public static string FormatBloodType(BloodType bloodType)
    {
        if (bloodType == BloodType.APositive)
            return "A+";

        if (bloodType == BloodType.ANegative)
            return "A-";

        if (bloodType == BloodType.BPositive)
            return "B+";

        if (bloodType == BloodType.BNegative)
            return "B-";

        if (bloodType == BloodType.ABPositive)
            return "AB+";

        if (bloodType == BloodType.ABNegative)
            return "AB-";

        if (bloodType == BloodType.OPositive)
            return "O+";

        if (bloodType == BloodType.ONegative)
            return "O-";

        return "Unknown";
    }

    public static string FormatSpeciality(Speciality speciality)
    {
        if (speciality == Speciality.Cardiologist)
            return "Кардіологія";

        if (speciality == Speciality.Dentist)
            return "Стоматологія";

        if (speciality == Speciality.Dermatologist)
            return "Дерматологія";

        if (speciality == Speciality.Neurologist)
            return "Неврологія";

        if (speciality == Speciality.Pediatrician)
            return "Педіатрія";

        if (speciality == Speciality.Surgeon)
            return "Хірургія";

        if (speciality == Speciality.Therapist)
            return "Терапія";

        return "Невідома спеціальність";
    }
    
    public static string FormatAge(int age)
    {
        int lastTwoDigits = age % 100;

        if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
        {
            return $"{age} років";
        }

        int lastDigit = age % 10;

        if (lastDigit == 1)
        {
            return $"{age} рік";
        }

        if (lastDigit >= 2 && lastDigit <= 4)
        {
            return $"{age} роки";
        }

        return $"{age} років";
    }
    
    public static string FormatAge(Patient patient)
    {
        return FormatAge(patient.Age);
    }

    public static string FormatPhone(string phone)
    {
        if (phone.Length != 10)
        {
            return phone;
        }

        for (int i = 0; i < phone.Length; i++)
        {
            if (!char.IsDigit(phone[i]))
            {
                return phone;
            }
        }

        return $"({phone.Substring(0, 3)}) " +
               $"{phone.Substring(3, 3)}-" +
               $"{phone.Substring(6, 4)}";
    }
    
    public static bool TryParsePhone(
        string phone,
        out string formattedPhone)
    {
        formattedPhone = FormatPhone(phone);

        if (phone.Length != 10)
        {
            formattedPhone = "";
            return false;
        }

        for (int i = 0; i < phone.Length; i++)
        {
            if (!char.IsDigit(phone[i]))
            {
                formattedPhone = "";
                return false;
            }
        }

        return true;
    }
    
    public static bool TryGetAgeInfo(
        Patient patient,
        out int age,
        out string category)
    {
        age = patient.Age;
        category = patient.GetAgeCategory();

        return true;
    }
}