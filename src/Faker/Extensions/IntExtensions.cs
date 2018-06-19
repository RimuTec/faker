using System;
using System.Collections.Generic;
using System.Text;

namespace RimuTec.Faker.Extensions {
   internal static class IntExtensions {
      public static IEnumerable<T> Times<T>(this int count, Func<int, T> func) {
         for (var i = 0; i < count; i++)
            yield return func.Invoke(i);
      }
   }
}
