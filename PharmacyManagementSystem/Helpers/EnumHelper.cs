using System.Reflection;
using System.ComponentModel;

namespace PharmacyManagementSystem.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttribute<DescriptionAttribute>();

            return attr?.Description ?? value.ToString();
        }
    }
}
