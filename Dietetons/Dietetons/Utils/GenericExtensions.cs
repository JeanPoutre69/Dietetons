using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietetons.Utils
{
    public static class GenericExtensions
    {
        public static T[] AsArray<T>(this T obj)
        {
            return new[] { obj };
        }

        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }

        public static string Stringify(this IEnumerable<string> strings, string middleDelimiter, string eventualEndsDelimiter = null)
        {
            if (strings.Count() == 0)
                return string.Empty;

            var endsDelimiter = eventualEndsDelimiter ?? string.Empty;
            if (strings.Count() == 1)
                return endsDelimiter + strings.First() + endsDelimiter;

            var strToReturn = endsDelimiter;
            foreach (var str in strings.Take(strings.Count() - 1))
            {
                strToReturn += str + middleDelimiter;
            }
            strToReturn += strings.Last() + endsDelimiter;
            return strToReturn;
        }
    }
}
