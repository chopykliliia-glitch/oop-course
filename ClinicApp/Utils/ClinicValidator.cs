namespace ClinicApp.Utils;

public static class ClinicValidator
{
    public static void ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Поле не може бути порожнім.", fieldName);
        if (value.Length > 50)
            throw new ArgumentException("Довжина не може перевищувати 50 символів.", fieldName);
    }

    public static void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Телефон не може бути порожнім.", "Phone");
        if (phone.Length != 10)
            throw new ArgumentException("Телефон має містити рівно 10 цифр.", "Phone");
        foreach (char c in phone)
        {
            if (c < '0' || c > '9')
                throw new ArgumentException("Телефон має містити лише цифри.", "Phone");
        }
    }

    public static void ValidateDate(DateTime value, string fieldName)
    {
        if (value > DateTime.Today)
            throw new ArgumentOutOfRangeException(fieldName, "Дата не може бути в майбутньому.");
        if (value.Year < 1900)
            throw new ArgumentOutOfRangeException(fieldName, "Дата не може бути раніше 1900 року.");
    }

    public static void ValidatePositive(int value, string fieldName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(fieldName, "Значення має бути більшим за нуль.");
    }
}