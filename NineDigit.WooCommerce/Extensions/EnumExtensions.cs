using System.ComponentModel;
using System.Reflection;

namespace NineDigit.WooCommerce;

internal static class EnumExtensions
{
    public static string GetDescription(this Enum enumValue)
    {
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString())
            ?? throw new InvalidOperationException("Enum member field cannot be resolved.");

        var attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>()
            ?? throw new InvalidOperationException($"{nameof(DescriptionAttribute)} not found on member '{enumValue}' of enum '{enumValue.GetType()}'.");
        
        return attribute.Description;
    }
}
