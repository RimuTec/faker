using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System.Linq;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;

namespace RimuTec.Faker
{
   /// <summary>
   /// Generates fake US Social Security number (SSN), both valid and invalid or Spanish citizen
   /// identifier (DNI or NIE).
   /// </summary>
   public static class IDNumber
   {
      // Resources used by this class from https://github.com/stympy/faker/blob/master/lib/locales/en/id_number.yml

      static IDNumber()
      {
         const string yamlFileName = "RimuTec.Faker.locales.en.id_number.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _idNumber = locale.en.faker.id_number;
      }

      /// <summary>
      /// Generate an invalid US Social Security Number. Example: "311-72-0000"
      /// </summary>
      /// <returns></returns>
      public static string Invalid()
      {
         return _idNumber.Invalid.Sample().Numerify();
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
         // _idNumber.Valid is "#{IDNumber.ssn_valid}" which requires Translate() which
         // we don't have yet.
         return SSN_Valid();
      }

      /// <summary>
      /// Generate a valid US Social Security Number, checking if any of the
      /// segements is all zeros.
      /// </summary>
      /// <returns></returns>
      public static string SSN_Valid()
      {
         var ssn = @"[0-8]\d{2}-\d{2}-\d{4}".Regexify();
         //We could still have all 0s in one segment or another
         if (_invalid_SSN.Any(r => Regex.Matches(ssn, r).Count > 0))
         {
            _SSN_Valid_recursive = true;
            ssn = SSN_Valid();
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

      private static id_number _idNumber;

#pragma warning disable IDE1006 // Naming Styles
      // Helper classes for reading the yaml file. Note that the class names are
      // intentionally lower case.

      internal class locale
      {
         public en en { get; set; }
      }

      internal class en
      {
         public faker faker { get; set; }
      }

      internal class faker
      {
         [YamlMember(Alias = "id_number", ApplyNamingConventions = false)]
         public id_number id_number { get; set; }
      }

      internal class id_number
      {
         [YamlMember(Alias = "valid", ApplyNamingConventions = false)]
         public string Valid { get; set; }

         [YamlMember(Alias = "invalid", ApplyNamingConventions = false)]
         public string[] Invalid { get; set; }
      }
#pragma warning restore IDE1006 // Naming Styles
   }
}
