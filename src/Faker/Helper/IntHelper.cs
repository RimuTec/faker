using System;
using System.Collections.Generic;

namespace RimuTec.Faker.Helper {
   internal class IntHelper {
      public static IEnumerable<int> Repeat(int from, int to, Func<int, int> initializer) {
         var list = new List<int>();
         for (var i = from; i <= to; i++) {
            list.Add(initializer.Invoke(i));
         }
         return list;
      }
   }
}
