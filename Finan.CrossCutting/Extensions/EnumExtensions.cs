using System;
using System.ComponentModel;
using System.Reflection;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        DescriptionAttribute attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }

    public static List<EnumItem> GetEnumList<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(e => new EnumItem
            {
                Value = Convert.ToInt32(e), // Converte para int
                Name = e.ToString(),
                Description = e.GetDescription()
            })
            .ToList();
    }

}
public class EnumItem
{
    public int Value { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}