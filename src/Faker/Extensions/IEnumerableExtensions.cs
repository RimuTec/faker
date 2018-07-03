using System.Collections.Generic;
using System.Linq;

namespace RimuTec.Faker.Extensions {
   internal static class IEnumerableExtensions {
      public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
         // Ref: Comment by user 'grenade' on answer https://stackoverflow.com/a/4262134/411428
         return source.OrderBy(x => RandomNumber.Next());
      }
   }
}
