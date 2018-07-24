using System;
using System.Collections.Generic;

namespace RimuTec.Faker.Extensions
{
   internal static class IntExtensions
   {
      public static IEnumerable<T> Times<T>(this int count, Func<int, T> func)
      {
         for (var i = 0; i < count; i++)
         {
            yield return func.Invoke(i);
         }

         // The following implementation would increase run time of the test suite 
         // by approximately 20%:
         //var list = new List<T>();
         //for (var i = 0; i < count; i++)
         //{
         //   list.Add(func.Invoke(i));
         //}
         //return list;
      }

      public static void TimesDo(this int count, Action<int> action)
         // Do not rename this to Times() as it would cause the wrong overload to be
         // picked.
      {
         for (var i = 0; i < count; i++)
         {
            action.Invoke(i);
         }
      }

      public static bool IsEven(this int i)
      {
         return i % 2 == 0;
      }
   }
}
