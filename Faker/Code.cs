using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace RimuTec.Faker {
   /// <summary>
   /// Generators for different types of codes, e.g. ISBN, EAN, IMEI, ASIN, etc.
   /// </summary>
   public static class Code {
      // Resources used by this class from https://github.com/stympy/faker/blob/master/lib/locales/en/code.yml

      static Code() {
         const string yamlFileName = "RimuTec.Faker.locales.en.code.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _code = locale.en.faker.code;
      }

      /// <summary>
      /// By default generates 10 sign isbn code in format 123456789-X. You can pass 13 to generate new 13 sign code.
      /// </summary>
      /// <param name="base"></param>
      /// <returns></returns>
      public static string Isbn(int @base = 10) {
         if(@base != 10 && @base != 13) {
            throw new ArgumentOutOfRangeException(nameof(@base), "Must be either 10 or 13.");
         }
         return @base == 13 ? GenerateBase13Isbn() : GenerateBase10Isbn();
      }

      /// <summary>
      /// Generates a 10 digit NPI (National Provider Identifier issued to health care
      /// providers in the United States.
      /// </summary>
      /// <returns></returns>
      public static string Npi() {
         return RandomNumber.Next(Math.Pow(10, 10)).ToString().PadLeft(10, '0');
      }

      private static string GenerateBase10Isbn() {
         var digits = @"\d{9}".Regexify().ToDigitList();
         string checkDigit = Isbn10CheckDigit(digits);
         return $"{string.Join("", digits)}-{checkDigit}";
      }

      private static string GenerateBase13Isbn() {
         var digits = @"\d{12}".Regexify().ToDigitList();
         int checkDigit = Isbn13CheckDigit(digits);
         return $"{string.Join("", digits)}-{checkDigit}";
      }

      private static int Isbn13CheckDigit(IList<int> digits) {
         // https://en.wikipedia.org/wiki/Check_digit#ISBN_13
         // https://isbn-information.com/check-digit-for-the-13-digit-isbn.html
         var remainder = 0;
         for (var index = 0; index < digits.Count(); index++) {
            int current = digits[index];
            if (index.Even()) {
               remainder += current;
            }
            else {
               remainder += current * 3;
            }
         }
         remainder = remainder % 10;
         int checkDigit = 0;
         if (remainder != 0) {
            checkDigit = 10 - remainder;
         }

         return checkDigit;
      }

      private static string Isbn10CheckDigit(IList<int> digits) {
         // https://en.wikipedia.org/wiki/Check_digit#ISBN_10
         // https://isbn-information.com/the-10-digit-isbn.html
         var remainder = 0;
         for (var index = 0; index < digits.Count(); index++) {
            int current = digits[index];
            int multiplier = (10 - index);
            remainder += multiplier * current;
         }
         remainder %= 11;
         var checkDigit = 11 - remainder;
         return remainder == 10 ? "X" : remainder.ToString();
      }

      private static readonly code _code;

#pragma warning disable IDE1006 // Naming Styles
      // Helper classes for reading the yaml file. Note that the class names are
      // intentionally lower case.

      internal class locale {
         public en en { get; set; }
      }

      internal class en {
         public faker faker { get; set; }
      }

      internal class faker {
         public code code { get; set; }
      }

      internal class code {
         [YamlMember(Alias = "asin", ApplyNamingConventions = false)]
         public string[] Asin { get; set; }
      }
#pragma warning restore IDE1006 // Naming Styles
   }
}
