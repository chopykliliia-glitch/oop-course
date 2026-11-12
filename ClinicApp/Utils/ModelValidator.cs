using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class ModelValidator
{
    public static ValidationResult Validate(object obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        ValidationResult result = new ValidationResult();

        foreach (PropertyInfo prop in obj.GetType().GetProperties())
        {

            if (!prop.CanRead || prop.GetIndexParameters().Length > 0)
            {
                continue;
            }

            object? value = prop.GetValue(obj);

            RequiredAttribute? required =
                prop.GetCustomAttribute<RequiredAttribute>();

            if (required != null)
            {
                bool isEmpty = value == null ||
                    (value is string text && string.IsNullOrWhiteSpace(text));

                if (isEmpty)
                {
                    result.AddError($"{prop.Name}: {required.ErrorMessage}");
                }
            }

            MaxLengthAttribute? maxLength =
                prop.GetCustomAttribute<MaxLengthAttribute>();

            if (maxLength != null && value is string str)
            {
                if (str.Length > maxLength.Length)
                {
                    string message = maxLength.ErrorMessage;

                    if (message == "")
                    {
                        message = $"Maximum length is {maxLength.Length}.";
                    }

                    result.AddError($"{prop.Name}: {message}");
                }
            }

            MinValueAttribute? minValue =
                prop.GetCustomAttribute<MinValueAttribute>();

            if (minValue != null && value != null)
            {
                try
                {
                    double number = Convert.ToDouble(value);

                    if (number < minValue.Min)
                    {
                        string message = minValue.ErrorMessage;

                        if (message == "")
                        {
                            message = $"Value must be at least {minValue.Min}.";
                        }

                        result.AddError($"{prop.Name}: {message}");
                    }
                }
                catch (FormatException)
                {
                }
                catch (InvalidCastException)
                {
                }
            }
        }

        return result;
    }

    public static void PrintInfo(Type type)
    {
        Console.WriteLine(type.Name);

        foreach (PropertyInfo prop in type.GetProperties())
        {
            string line = $"  {prop.Name,-14}";

            foreach (Attribute attr in prop.GetCustomAttributes())
            {
                string name = attr.GetType().Name;

                if (name.EndsWith("Attribute"))
                {
                    name = name.Substring(0, name.Length - "Attribute".Length);
                }

                line += $" [{name}]";
            }

            Console.WriteLine(line);
        }
    }
}