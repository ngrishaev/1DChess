using System;
using System.Collections.Generic;

namespace Common
{
    public static class IEnumerableExtensions
    {
        public static T FindFirstOrDefault<T>(this IEnumerable<T> collection, Func<T, bool> finder, T @default)
        {
            foreach (var element in collection)
            {
                if (finder.Invoke(element))
                    return element;
            }

            return @default;
        }

        public static U FindFirstOrDefault<T, U>(this IEnumerable<T> collection, Func<T, bool> finder, Func<T, U> transform, U @default)
        {
            foreach (var element in collection)
            {
                if (finder.Invoke(element))
                    return transform.Invoke(element);
            }
            return @default;
        }
    }
}