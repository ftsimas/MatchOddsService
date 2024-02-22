using MatchOddsService.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MatchOddsService.Helpers
{
    public static class SpecifierHelper
    {
        public static string ToDTOString(this Specifier specifier)
        {
            string displayName = null;

            var type = typeof(Specifier);
            var enumName = Enum.GetName(type, specifier);

            if (enumName != null)
            {
                displayName = type.GetField(enumName)
                    ?.GetCustomAttribute<DisplayAttribute>()
                    ?.Name;
            }

            return displayName ?? specifier.ToString();
        }

        public static Specifier FromDTOString(string specifier)
        {
            var enumField = typeof(Specifier).GetFields()
                .Single(f => f.GetCustomAttribute<DisplayAttribute>()?.Name == specifier);

            return Enum.Parse<Specifier>(enumField.Name);
        }
    }
}
