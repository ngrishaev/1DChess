using System;
using System.Collections.Generic;

namespace Common
{
    public static class IEnumerableExtensions
    {
        public static Maybe<T> MaybeFind<T>(this IEnumerable<T> collection, Func<T, bool> finder)
        {
            foreach (var element in collection)
            {
                if (finder.Invoke(element))
                    return Maybe<T>.Yes(element);
            }
            return Maybe<T>.No();
        }
    }
}