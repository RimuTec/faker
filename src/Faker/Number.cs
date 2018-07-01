using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RimuTec.Faker {
   /// <summary>
   /// Generators for numbers as strings.
   /// </summary>
   internal static class Number {
      /// <summary>
      /// Creates a number as string with a specified number of digits. The method is the quivalent to Faker::Number.number() in Ruby.
      /// </summary>
      /// <param name="digits">Number of digits. Must be greater than zero. Default value is 10.</param>
      /// <returns></returns>
      public static string Create(int digits) {
         var num = "";
         if (digits > 1) {
            num = NonZeroDigit();
            digits--;
         }
         return num + LeadingZeroNumber(digits);
      }

      private static string LeadingZeroNumber(int digits) {
         var result = digits.Times(x => new Range("0", "9").ToArray().Random());
         return string.Join("", result);
      }

      private static string NonZeroDigit() {
         return RandomNumber.Next(1, 10).ToString();
      }
   }
}
