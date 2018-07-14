using System.Collections.Generic;
using System.Linq;

namespace RimuTec.Faker.Extensions
{
   /// <summary>
   /// Extensions for Mod11 check digits calculations
   /// </summary>
   internal static class Mod11Extensions
   {
      /// <summary>
      /// Calculates the check digits potentially returning null which can also be interpreted as 'X'.
      /// </summary>
      /// <param name="digits">List with digits.</param>
      /// <returns>Check digit. Null if modulo is 11.</returns>
      public static int? Mod11(this List<int> digits)
      {
         // uses mod11 (ISO-7064)
         var weight = new int[] { 2, 3, 4, 5, 6, 7,
                                  2, 3, 4, 5, 6, 7,
                                  2, 3, 4, 5, 6, 7 };
         var sum = 0;
         for (var i = 0; i < digits.Count(); i++)
         {
            sum += digits[i] * weight[i];
         }
         var modulo = sum % 11;
         switch (modulo)
         {
            case 0:
               return modulo;
            case 1:
               return null;
            default:
               return 11 - modulo;
         }
      }
   }
}
