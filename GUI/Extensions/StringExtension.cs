using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class StringExtension
    {
        public static bool Contains(this String strings, string value, StringComparison stringComparison)
        {
            int index = strings.IndexOf(value, 0, stringComparison);

            return index != -1 ? true : false;
        }
    }
}
