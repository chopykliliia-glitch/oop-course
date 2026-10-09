using System.Text.RegularExpressions;

namespace ClinicApp.Utils;

public static class ClinicValidator
{
    private static readonly Regex PhoneRegex = new(@"^[0-9]{10}\z");
    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+\z");

    public static void ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Поле не може бути порожнім.", fieldName);
        if (value.Length > 50)
            throw new ArgumentException("Довжина не може перевищувати 50 символів.", fieldName);
    }

    public static void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) || !PhoneRegex.IsMatch(phone))
            throw new ArgumentException("Телефон має містити рівно 10 цифр.", "Phone");
    }

    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !EmailRegex.IsMatch(email))
            throw new ArgumentException("Некоректний email.", "Email");
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