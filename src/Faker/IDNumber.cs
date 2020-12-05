using RimuTec.Faker.Extensions;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker
{
   /// <summary>
   /// Generates fake US Social Security number (SSN), both valid and invalid or Spanish citizen
   /// identifier (DNI or NIE).
   /// </summary>
   public class IdNumber : GeneratorBase<IdNumber>
   {
      private IdNumber() { }

      /// <summary>
      /// Generate an invalid US Social Security Number. Example: "311-72-0000"
      /// </summary>
      /// <returns></returns>
      public static string Invalid()
      {
         return Parse(Fetch("id_number.invalid")).Numerify();
      }

      /// <summary>
      /// Generate a Spanish citizen identifier (DNI) 
      /// </summary>
      /// <returns></returns>
      public static string SpanishCitizenNumber()
      {
         var num = Number.Create(8);
         var mod = int.Parse(num) % 23;
         var check = _checks[mod];
         return $"{num}-{check}";
      }

      /// <summary>
      /// Generate a Spanish foreign born citizen identifier (NIE)
      /// </summary>
      /// <returns></returns>
      public static string SpanishForeignCitizenNumber()
      {
         var code = "XYZ";
         var digits = Number.Create(7);
         var prefix = code.Sample();
         var prefixVal = code.IndexOf(prefix).ToString();
         var mod = int.Parse($"{prefixVal}{digits}") % 23;
         var check = _checks[mod];
         return $"{prefix}-{digits}-{check}";
      }

      /// <summary>
      /// Generate a valid US Social Security number. Example: "552-56-3593"
      /// </summary>
      /// <returns></returns>
      public static string Valid()
      {
         return Parse(Fetch("id_number.valid"));
      }

      /// <summary>
      /// Generate a valid US Social Security Number, checking if any of the
      /// segements is all zeros.
      /// </summary>
      /// <returns></returns>
      public static string SsnValid()
      {
         var ssn = @"[0-8]\d{2}-\d{2}-\d{4}".Regexify();
         //We could still have all 0s in one segment or another
         if (_invalid_SSN.Any(r => Regex.Matches(ssn, r).Count > 0))
         {
            _SSN_Valid_recursive = true;
            ssn = SsnValid();
         }
         return ssn;
      }

      internal static string[] _invalid_SSN = new string[] {
         @"0{3}-\d{2}-\d{4}",
         @"\d{3}-0{2}-\d{4}",
         @"\d{3}-\d{2}-0{4}",
         @"666-\d{2}-\d{4}",
         @"9\d{2}-\d{2}-\d{4}"
      };

      internal static bool _SSN_Valid_recursive = false;
      private static readonly string _checks = "TRWAGMYFPDXBNJZSQVHLCKE";
   }
}
