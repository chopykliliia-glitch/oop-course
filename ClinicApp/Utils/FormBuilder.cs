using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class FormBuilder
{
    public static T Build<T>() where T : new()
    {
        T obj = new T();

        foreach (PropertyInfo prop in typeof(T).GetProperties())
        {
            if (prop.GetSetMethod() == null)
            {
                continue;
            }

            string hint = BuildHint(prop);

            while (true)
            {
                Console.Write($"  {prop.Name}{hint}: ");
                string input = Console.ReadLine() ?? "";

                try
                {
                    object converted = ConvertInput(input, prop.PropertyType);
                    prop.SetValue(obj, converted);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("  Некоректне значення, спробуйте ще раз.");
                }
            }
        }

        return obj;
    }

    private static string BuildHint(PropertyInfo prop)
    {
        List<string> parts = new List<string>();

        if (prop.GetCustomAttribute<RequiredAttribute>() != null)
        {
            parts.Add("обов'язкове");
        }

        MaxLengthAttribute? maxLength =
            prop.GetCustomAttribute<MaxLengthAttribute>();

        if (maxLength != null)
        {
            parts.Add($"макс. {maxLength.Length}");
        }

        MinValueAttribute? minValue =
            prop.GetCustomAttribute<MinValueAttribute>();

        if (minValue != null)
        {
            parts.Add($"мін. {minValue.Min}");
        }

        if (parts.Count == 0)
        {
            return "";
        }

        return " (" + string.Join(", ", parts) + ")";
    }

    private static object ConvertInput(string input, Type propertyType)
    {
        Type target = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

        if (target.IsEnum)
        {
            return Enum.Parse(target, input, true);
        }

        return Convert.ChangeType(input, target);
    }
}