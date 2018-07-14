﻿using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;

namespace RimuTec.Faker
{
   /// <summary>
   /// Generators for different types of codes, e.g. ISBN, EAN, IMEI, ASIN, etc.
   /// </summary>
   public static class Code
   {
      // Resources used by this class from https://github.com/stympy/faker/blob/master/lib/locales/en/code.yml

      static Code()
      {
         const string yamlFileName = "RimuTec.Faker.locales.en.code.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _code = locale.en.faker.code;
      }

      /// <summary>
      /// Be default generates a 13 digits EAN code in format 1234567890123.
      /// </summary>
      /// <param name="base">Base for EAN code. Must be 8 or 13. Default is 13.</param>
      /// <returns></returns>
      public static string Ean(int @base = 13)
      {
         if (@base != 8 && @base != 13)
         {
            throw new ArgumentOutOfRangeException(nameof(@base), "Must be either 8 or 13.");
         }
         return @base == 8 ? GenerateBase8Ean() : GenerateBase13Ean();
      }

      /// <summary>
      /// By default generates 10 sign isbn code in format 123456789-X. You can pass 13 to generate new 13 sign code.
      /// </summary>
      /// <param name="base"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">If <paramref name="base"/> was value other than 10 or 13.</exception>
      public static string Isbn(int @base = 10)
      {
         if (@base != 10 && @base != 13)
         {
            throw new ArgumentOutOfRangeException(nameof(@base), "Must be either 10 or 13.");
         }
         return @base == 13 ? GenerateBase13Isbn() : GenerateBase10Isbn();
      }

      /// <summary>
      /// Generates a 10 digit NPI (National Provider Identifier issued to health care
      /// providers in the United States.
      /// </summary>
      /// <returns></returns>
      public static string Npi()
      {
         return RandomNumber.Next(Math.Pow(10, 10)).ToString().PadLeft(10, '0');
      }

      /// <summary>
      /// By default generates a Singaporean NRIC ID for someone aged 18 to 65.
      /// </summary>
      /// <param name="minAge">Minimum age. Default is 18.</param>
      /// <param name="maxAge">Maximum age. Default is 65. Must be equal to or greater than <paramref name="minAge"/></param>
      /// <returns></returns>
      public static string Nric(int minAge = 18, int maxAge = 65)
      {
         if(minAge <= 0)
         {
            throw new ArgumentOutOfRangeException(nameof(minAge), "Must be greater than zero.");
         }
         if(maxAge < minAge)
         {
            throw new ArgumentOutOfRangeException(nameof(maxAge), $"Must be greater than {nameof(minAge)}.");
         }
         // https://en.wikipedia.org/wiki/National_Registration_Identity_Card#Structure_of_the_NRIC_number.2FFIN
         var birthyear = Date.Birthday(minAge, maxAge).Year;
         var prefix = birthyear < 2000 ? "S" : "T";
         var values = birthyear.ToString().Substring(2,2); // get last two digits
         values += @"\d{5}".Regexify();
         var checkAlpha = GenerateNricCheckAlphabet(values.ToDigitList(), prefix);
         return $"{prefix}{values}{checkAlpha}";
      }

      private static string GenerateNricCheckAlphabet(IList<int> digits, string prefix)
      {
         var weight = "2765432".ToDigitList();
         var total = 0;
         for(var i = 0; i < digits.Count(); i++ )
         {
            total += weight[i] * digits[i];
         }
         if( prefix == "T" )
         {
            total += 4;
         }
         return "ABCDEFGHIZJ"[10 - (total % 11)].ToString();
      }

      /// <summary>
      /// Generates a Chilean RUT code (tax identification number, Rol Único Tributario)
      /// </summary>
      /// <returns></returns>
      public static string Rut()
      {
         var value = Number.Create(8);
         var checkDigit = RutVerificatorDigit(value);
         return $"{value}-{checkDigit}";
      }

      private static string RutVerificatorDigit(string value)
      {
         // https://www.vesic.org/english/blog-eng/net/verifying-chilean-rut-code-tax-number/
         var digits = value.ToDigitList();
         var total = 0;
         for (var i = 0; i < digits.Count(); i++)
         {
            total += digits[i] * _rutCheckDigit[i];
         }
         var rest = 11 - (total % 11);
         switch (rest)
         {
            case 11:
               return "0";
            case 10:
               return "K";
            default:
               return $"{rest}";
         }
      }

      private static string GenerateBase10Isbn()
      {
         var digits = @"\d{9}".Regexify().ToDigitList();
         string checkDigit = Isbn10CheckDigit(digits);
         return $"{string.Join("", digits)}-{checkDigit}";
      }

      private static string GenerateBase13Isbn()
      {
         var digits = @"\d{12}".Regexify().ToDigitList();
         int checkDigit = Isbn13CheckDigit(digits);
         return $"{string.Join("", digits)}-{checkDigit}";
      }

      private static string GenerateBase8Ean()
      {
         var values = @"\d{7}".Regexify();
         var digits = values.ToDigitList();
         var remainder = 0;
         for (var i = 0; i < digits.Count(); i++)
         {
            remainder += _eanCheckDigit8[i] * digits[i];
         }
         remainder = 10 - remainder % 10;
         var remainderAsString = remainder == 10 ? "0" : $"{remainder}";
         return $"{values}{remainderAsString}";
      }

      private static string GenerateBase13Ean()
      {
         var values = @"\d{12}".Regexify();
         var digits = values.ToDigitList();
         var remainder = 0;
         for (var i = 0; i < digits.Count(); i++)
         {
            remainder += _eanCheckDigit13[i] * digits[i];
         }
         remainder = 10 - remainder % 10;
         var remainderAsString = remainder == 10 ? "0" : $"{remainder}";
         return $"{values}{remainderAsString}";
      }

      private static int Isbn13CheckDigit(IList<int> digits)
      {
         // https://en.wikipedia.org/wiki/Check_digit#ISBN_13
         // https://isbn-information.com/check-digit-for-the-13-digit-isbn.html
         var remainder = 0;
         for (var index = 0; index < digits.Count(); index++)
         {
            int current = digits[index];
            if (index.Even())
            {
               remainder += current;
            }
            else
            {
               remainder += current * 3;
            }
         }
         remainder = remainder % 10;
         int checkDigit = 0;
         if (remainder != 0)
         {
            checkDigit = 10 - remainder;
         }

         return checkDigit;
      }

      private static string Isbn10CheckDigit(IList<int> digits)
      {
         // https://en.wikipedia.org/wiki/Check_digit#ISBN_10
         // https://isbn-information.com/the-10-digit-isbn.html
         var remainder = 0;
         for (var index = 0; index < digits.Count(); index++)
         {
            int current = digits[index];
            int multiplier = (10 - index);
            remainder += multiplier * current;
         }
         remainder %= 11;
         var checkDigit = 11 - remainder;
         return remainder == 10 ? "X" : remainder.ToString();
      }

      private static readonly int[] _eanCheckDigit8 = new int[] { 3, 1, 3, 1, 3, 1, 3 };
      private static readonly int[] _eanCheckDigit13 = new int[] { 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3 };

      private static readonly int[] _rutCheckDigit = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };

      private static readonly code _code;

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
         public code code { get; set; }
      }

      internal class code
      {
         [YamlMember(Alias = "asin", ApplyNamingConventions = false)]
         public string[] Asin { get; set; }
      }
#pragma warning restore IDE1006 // Naming Styles
   }
}
