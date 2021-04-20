using System;
using System.ComponentModel;

namespace CarbonBlazor
{
    public static class EnumExtension
    {
        public static string GetDescription<T>(this T @enum)
        {
            var field = @enum.GetType().GetField(@enum.ToString());

            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return @enum.ToString();
        }
    }
}