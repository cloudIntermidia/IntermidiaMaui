using System.Collections;

namespace Intermidia.Utilities
{
    public static class EnumerableExtension
    {
        public static bool IsEmpty(this IEnumerable enumarable)
        {
            return !enumarable.Cast<object>().Any();
        }

        public static bool IsNotEmpty(this IEnumerable enumarable)
        {
            return enumarable.Cast<object>().Any();
        }
    }
}