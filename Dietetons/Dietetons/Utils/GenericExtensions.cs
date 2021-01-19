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
    }
}
