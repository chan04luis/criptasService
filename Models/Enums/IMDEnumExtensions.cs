using System.ComponentModel;

namespace Models.Enums
{
    public static class IMDEnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            if (Attribute.GetCustomAttribute(enumValue.GetType().GetField(enumValue.ToString()), typeof(DescriptionAttribute)) is DescriptionAttribute descriptionAttribute)
            {
                return descriptionAttribute.Description;
            }

            return string.Empty;
        }

        public static T? GetValue<T>(this string description) where T : Enum
        {
            foreach (Enum value in Enum.GetValues(typeof(T)))
            {
                if (value.GetDescription() == description)
                {
                    return (T)value;
                }
            }

            return default(T);
        }

        public static string GetDefaultValue(Enum enumValue)
        {
            if (Attribute.GetCustomAttribute(enumValue.GetType().GetField(enumValue.ToString()), typeof(DefaultValueAttribute)) is DefaultValueAttribute defaultValueAttribute)
            {
                return defaultValueAttribute.Value.ToString().ToLower();
            }

            return string.Empty;
        }
    }
}
