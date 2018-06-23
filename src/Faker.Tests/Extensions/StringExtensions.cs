using System.Collections.Generic;

namespace RimuTec.Faker.Tests.Extensions {
   static class StringExtensions {
      public static string RemoveFullStops(this string source) {
         return source.Replace(".", string.Empty);
      }

      public static IEnumerable<string> ToWordList(this string source) {
         return source.RemoveFullStops().ToLower().Split(' ');
      }
   }
}
