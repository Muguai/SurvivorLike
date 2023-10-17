
using System;
namespace Utils;

public static class EnumHelper
{
    public static string EnumToString<T>(T enumValue) where T : Enum
    {
        return enumValue.ToString();
    }

    public static T StringToEnum<T>(string enumString) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), enumString);
    }

    public static bool IsEnum<T>()
    {
        return typeof(T).IsEnum;
    }
}
