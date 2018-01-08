using System;

namespace Asg5_vxc152130
{
    public static class StringExtensions
    {
       static  StringComparison comparator = StringComparison.OrdinalIgnoreCase;
        public static bool ContainsIgnoreCase(this string source, string toCheck)
        {
            return source.IndexOf(toCheck, comparator) >= 0;
        }
    }
}
