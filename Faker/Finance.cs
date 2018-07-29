using RimuTec.Faker.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker
{
   /// <summary>
   /// Credit card types for which numbers can be generated.
   /// </summary>
   public enum CreditCardType
   {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
      Visa, Mastercard, Discover, AmericanExpress, DinersClub, Jcb, Switch, Solo, Dankort, Maestro, Forbrugsforeningen, Laser
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
   }

   /// <summary>
   /// Generators for finance related data, e.g. fake credit card numbers.
   /// </summary>
   public class Finance : GeneratorBase<Finance>
   {
      private Finance() { }

      /// <summary>
      /// Generate a credit card number, optionally from a selection of types. Example: "3018-348979-1853". For available types
      /// <see cref="CreditCardType"/>.
      /// </summary>
      /// <param name="types">Zero or more credit card types to choose from. Default is all.</param>
      /// <returns></returns>
      public static string CreditCard(params CreditCardType[] types)
      {
         if (types.Length == 0)
         {
            types = new[] { CreditCardType.Visa, CreditCardType.Mastercard, CreditCardType.Discover,
               CreditCardType.AmericanExpress, CreditCardType.DinersClub, CreditCardType.Jcb, CreditCardType.Switch,
               CreditCardType.Solo, CreditCardType.Dankort, CreditCardType.Maestro, CreditCardType.Forbrugsforeningen,
               CreditCardType.Laser
            };
         }

         var type = types.Sample();
         string typeAsString = type.ToString();
         var parts = Regex.Matches(typeAsString, @"[A-Z]{1}[a-z]{1,}").Cast<Match>().Select(m => m.Value).ToArray();
         var partsList = new List<string>(parts).ConvertAll(d => d.ToLower());
         var yamlIdentifier = string.Join("_", partsList);
         var template = Fetch($"finance.credit_card.{yamlIdentifier}").Trim('/').Numerify();
         template = Regex.Replace(template, @"\[(\d)-(\d)\]", range => {
            return RandomNumber.Next(int.Parse(range.Groups[1].Value), int.Parse(range.Groups[2].Value)).ToString();
         }, RegexOptions.Compiled);

         // Luhn check digit if required:
         var checkDigit = Regex.Replace(template, @"[^0-9]", string.Empty, RegexOptions.Compiled).ToDigitList().LuhnCheckDigit();
         template = template.Replace("L", checkDigit.ToString());
         return template;
      }
   }
}
