using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Appium
{
    public static class IEnumerableExtensions
    {
        public static bool HasOnlyOne<T>(this IEnumerable<T> collection)
        {
            var enumerator = collection.GetEnumerator();
            return enumerator.MoveNext() && !enumerator.MoveNext();
        }
    }
}
