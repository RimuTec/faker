using RimuTec.Faker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimuTec.Faker
{
   /// <summary>
   /// Generators for company related data.
   /// </summary>
   public class Company : GeneratorBase<Company>
   {
      private Company() { }
      
      /// <summary>
      /// Get a random company name. Example: "Hirthe-Ritchie"
      /// </summary>
      /// <returns></returns>
      public static string Name()
      {
         var nameTemplate = Fetch("company.name");
         var result = Parse(nameTemplate);
         return result;
      }

      /// <summary>
      /// Get a random company name suffix. Example: "Group"
      /// </summary>
      /// <returns></returns>
      public static string Suffix()
      {
         return Fetch("company.suffix");
      }

      /// <summary>
      /// Get a random industry. Example: "Information Services"
      /// </summary>
      /// <returns></returns>
      public static string Industry()
      {
         return Fetch("company.industry");
      }

      /// <summary>
      /// Generate a buzzword-laden catch phrase. Example: "Business-focused coherent parallelism"
      /// </summary>
      /// <returns></returns>
      public static string CatchPhrase()
      {
         var buzzwords = Translate("company.buzzwords");
         var words = new List<string>();
         buzzwords.ForEach(x => words.Add(x.Value.Sample()));
         return string.Join(" ", words);
      }

      /// <summary>
      /// Get a random buzzword. Example: "Business-focused"
      /// </summary>
      /// <returns></returns>
      public static string Buzzword()
      {
         var buzzwords = Translate("company.buzzwords");
         var foo = buzzwords.Aggregate(new List<string>(), (list, a) =>
         {
            list.AddRange(a.Value.ToList());
            return list;
         });
         return foo.Sample().ToLower();
      }

      /// <summary>
      /// When a straight answer won't do, BS to the rescue! Example: "empower one-to-one web-readiness"
      /// </summary>
      /// <returns></returns>
      /// <example>"empower one-to-one web-readiness"</example>
      public static string Bs()
      {
         var bs = Translate("company.bs");
         var sb = new StringBuilder();
         bs.ForEach(x => sb.Append(x.Value.Sample() + " "));
         return sb.ToString().Trim();
      }

      /// <summary>
      /// Returns a URL to a random fake company logo in PNG format.
      /// </summary>
      /// <returns></returns>
      public static string Logo()
      {
         // Selection of fake but convincing logos for real-looking test data:
         // https://github.com/pigment/fake-logos
         // Available under a Creative Commons (CC) "Attribution-ShareAlike 4.0 International (CC BY-SA 4.0) 
         // license. License https://creativecommons.org/licenses/by-sa/4.0/
         return $"https://pigment.github.io/fake-logos/logos/medium/color/#{RandomNumber.Next(13)}.png";
      }

      /// <summary>
      /// Get a random company type. Example: "Privately Held"
      /// </summary>
      /// <returns></returns>
      public static string Type()
      {
         return Fetch("company.type");
      }

      /// <summary>
      /// Generate US employee identification number. Example: "34-8488813"
      /// </summary>
      /// <returns></returns>
      public static string Ein()
      {
         return "##-#######".Numerify();
      }

      /// <summary>
      /// Generate "Data Universal Numbering System". Example: "08-341-3736"
      /// </summary>
      /// <returns></returns>
      public static string DunsNumber()
      {
         return "##-###-####".Numerify();
      }

      /// <summary>
      /// Get a random profession. E.g. "Firefighter"
      /// </summary>
      /// <returns></returns>
      public static string Profession()
      {
         return Fetch("company.profession");
      }

      /// <summary>
      /// Generates a random Swedish organization number. See more here https://sv.wikipedia.org/wiki/Organisationsnummer
      /// </summary>
      /// <returns></returns>
      public static string SwedishOrganizationNumber()
      {
         // Valid leading digit: 1, 2, 3, 5, 6, 7, 8, 9
         // Valid third digit: >=2
         // Last digit is a control digit
         var validLeadingDigits = new List<int> { 1, 2, 3, 5, 6, 7, 8, 9 };
         var digits = new List<int> {
            validLeadingDigits.Sample(),
            RandomNumber.Next(10),
            RandomNumber.Next(2, 10)
         };
         digits.AddRange(6.Times(x => RandomNumber.Next(10)));
         digits.Add(digits.LuhnCheckDigit());
         return ConvertToString(digits);
      }

      /// <summary>
      /// Get a random Czech organization number. Example: "77778171"
      /// </summary>
      /// <returns></returns>
      public static string CzechOrganizationNumber()
      {
         var sum = 0;
         var digits = new List<int>();
         var weights = new int[] { 8, 7, 6, 5, 4, 3, 2 };
         foreach (var weight in weights)
         {
            digits.Add(RandomNumber.Next(10));
            sum += (weight * digits.Last());
         }
         digits.Add((11 - (sum % 11)) % 10);
         return ConvertToString(digits);
      }

      /// <summary>
      /// Get a random French SIREN number. Example: "819489626"
      /// </summary>
      /// <returns></returns>
      public static string FrenchSirenNumber()
      {
         // See more here https://fr.wikipedia.org/wiki/Syst%C3%A8me_d%27identification_du_r%C3%A9pertoire_des_entreprises
         List<int> digits = FrenchSirenNumberDigits();
         return ConvertToString(digits);
      }

      private static List<int> FrenchSirenNumberDigits()
      {
         var digits = new List<int>();
         digits.AddRange(8.Times(x => RandomNumber.Next(10)));
         digits.AppendLuhnCheckDigit();
         return digits;
      }

      /// <summary>
      /// Get a random French SIRET number. Example: "81948962600013"
      /// </summary>
      /// <returns></returns>
      public static string FrenchSiretNumber()
      {
         var location = RandomNumber.Next(100).ToString().PadLeft(4, '0');
         var digits = new List<int>(FrenchSirenNumberDigits());
         digits.AddRange(location.Select(x => int.Parse(x.ToString())));
         digits.AppendLuhnCheckDigit();
         return ConvertToString(digits);
      }

      /// <summary>
      /// Get a random Norwegian organization number. Example: "839071558"
      /// </summary>
      /// <returns></returns>
      public static string NorwegianOrganizationNumber()
      {
         // Valid leading digits: 8, 9
         int? mod11Check = null;
         List<int> digits = new List<int>();
         while (mod11Check == null)
         {
            digits = new List<int> {
               RandomNumber.Next(8, 10)
            };
            digits.AddRange(7.Times(x => RandomNumber.Next(10)));
            mod11Check = digits.Mod11();
         }
         digits.Add(mod11Check.Value);
         return ConvertToString(digits);
      }

      /// <summary>
      /// Gets a random Australian business number. Example: "81137773602"
      /// </summary>
      /// <returns></returns>
      public static string AustralianBusinessNumber()
      {
         var @base = new List<int>();
         @base.AddRange(9.Times(x => RandomNumber.Next(10)));
         var toCheck = new List<int>();
         toCheck.AddRange(2.Times(x => 0));
         toCheck.AddRange(@base);
         var digits = new List<int>();
         digits.Add(99 - (AbnChecksum(toCheck) % 89));
         digits.AddRange(@base);
         return ConvertToString(digits);
      }

      /// <summary>
      /// Gets a random Spanish organization number. Example: "P2344979"
      /// </summary>
      /// <returns></returns>
      public static string SpanishOrganizationNumber()
      {
         // Valid leading characters: A, B, C, D, E, F, G, H, J, N, P, Q, R, S, U, V, W
         // 7 digit numbers
         var letters = "A, B, C, D, E, F, G, H, J, N, P, Q, R, S, U, V, W".Replace(",", string.Empty).Split(' ');
         var digits = new List<int>();
         digits.AddRange(7.Times(x => RandomNumber.Next(10)));
         return $"{letters.Sample()}{ConvertToString(digits)}";
      }

      /// <summary>
      /// Get a random Polish taxpayer identification number. Example: "1060000062"
      /// </summary>
      /// <returns></returns>
      public static string PolishTaxpayerIdentificationNumber()
      {
         // More info https://pl.wikipedia.org/wiki/NIP
         List<int> digits;
         var weights = new List<int> { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
         do
         {
            digits = new List<int>();
            digits.AddRange(3.Times(x => RandomNumber.Next(1, 9)));
            digits.AddRange(7.Times(x => RandomNumber.Next(10)));
         } while (WeightSum(digits, weights) % 11 != digits[9]);
         return ConvertToString(digits);
      }

      /// <summary>
      /// Get a random Polish register of national economy number. 
      /// </summary>
      /// <param name="length">Length of number. Valid values are 9 and 14.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">Thrown when 'length' is a value other than 9 or 14.</exception>
      public static string PolishRegisterOfNationalEconomy(int length = 9)
      {
         // More info https://pl.wikipedia.org/wiki/REGON
         if (length != 9 && length != 14)
         {
            throw new ArgumentOutOfRangeException(nameof(length), "Must be either 9 or 14.");
         }
         List<int> digits;
         do
         {
            digits = new List<int>();
            digits.AddRange(length.Times(x => RandomNumber.Next(10)));
         } while (CollectRegonSum(digits) != digits.Last());
         return ConvertToString(digits);
      }

      private static int WeightSum(List<int> array, List<int> weights)
      {
         var sum = 0;
         for (int i = 0; i < weights.Count(); i++)
         {
            sum += array[i] * weights[i];
         }
         return sum;
      }

      private static int CollectRegonSum(List<int> digits)
      {
         var weights = digits.Count() == 9 ?
            new List<int> { 8, 9, 2, 3, 4, 5, 6, 7 }
            : new List<int> { 2, 4, 8, 5, 0, 9, 7, 3, 6, 1, 2, 4, 8 };
         var sum = WeightSum(digits, weights) % 11;
         return sum == 10 ? 0 : sum;
      }

      private static string ConvertToString(List<int> digits)
      {
         return digits.Aggregate(new StringBuilder(), (sb, d) => sb.Append($"{d}")).ToString();
      }

      private static int AbnChecksum(List<int> digits)
      {
         var abnWeights = new int[] { 10, 1, 3, 5, 7, 9, 11, 13, 15, 17, 19 };
         var sum = 0;
         for (int i = 0; i < abnWeights.Length; i++)
         {
            sum += abnWeights[i] * digits[i];
         }
         return sum;
      }
   }
}
