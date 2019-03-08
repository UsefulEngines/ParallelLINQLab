using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOL
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> WithSampling<T>(this IEnumerable<T> source, int every)
        {
            int idx = 0;
            foreach (var item in source)
            {
                if (++idx % every == 0) yield return item;
                else continue;
            }
        }

    }
}
