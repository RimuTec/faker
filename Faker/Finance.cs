using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace RimuTec.Faker
{
   /// <summary>
   /// Credit card types for which numbers can be generated.
   /// </summary>
   public enum CreditCardType
   {
      Visa, Mastercard, Discover, AmericanExpress, DinersClub, Jcb, Switch, Solo, Dankort, Maestro, Forbrugsforeningen, Laser
   }

   /// <summary>
   /// Generators for finance related data, e.g. fake credit card numbers.
   /// </summary>
   public static class Finance
   {
      // Resources used by this class from https://github.com/stympy/faker/blob/master/lib/locales/en/finance.yml

      static Finance()
      {
         const string yamlFileName = "RimuTec.Faker.locales.en.finance.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName, new MyCustomConverter());
         _finance = locale.en.faker.finance;
      }

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
         var template = _finance.CreditCard[type.ToString()].Sample();
         template = template.Numerify();

         // Luhn check digit if required:
         var checkDigit = Regex.Replace(template, @"[^0-9]", "").ToDigitList().CheckDigit();
         template = template.Replace("L", checkDigit.ToString());
         return template;
      }

      private static readonly finance _finance;

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
         public finance finance { get; set; }
      }

      internal class finance
      {
         [YamlMember(Alias = "credit_card", ApplyNamingConventions = false)]
         public Dictionary<string, List<string>> CreditCard { get; set; }
      }
#pragma warning restore IDE1006 // Naming Styles

      internal class MyCustomConverter : IYamlTypeConverter
      {

         // This class helps reading the finance.yaml file. There are cases where there
         // is only one template for a given credit card. The way the yaml file is written,
         // YamlDotNet interprets that value as scalar instead of a list.

         // Implementation inspired by 
         // https://www.cyotek.com/blog/using-custom-type-converters-with-csharp-and-yamldotnet-part-1
         // https://www.cyotek.com/blog/using-custom-type-converters-with-csharp-and-yamldotnet-part-2

         public bool Accepts(Type type)
         {
            return type == typeof(Dictionary<string, List<string>>);
         }

         public object ReadYaml(IParser parser, Type type)
         {
            if (parser.Current.GetType() != typeof(MappingStart))
            {
               throw new InvalidDataException("Invalid YAML content.");
            }
            var result = new Dictionary<string, List<string>>();
            parser.MoveNext();
            while (parser.Current.GetType() != typeof(MappingEnd))
            {
               if (parser.Current is Scalar creditCardType)
               {
                  var newKey = creditCardType.Value.ToPascalCasing();
                  var list = new List<String>();
                  parser.MoveNext();
                  if (parser.Current is SequenceStart sequenceStart)
                  {
                     parser.MoveNext();
                     while (parser.Current.GetType() != typeof(SequenceEnd))
                     {
                        if (parser.Current is Scalar creditCardTemplate)
                        {
                           list.Add(creditCardTemplate.Value.Trim('/'));
                        }
                        parser.MoveNext();
                     }
                  }
                  else if (parser.Current is Scalar creditCardTemplate)
                  {
                     list.Add(creditCardTemplate.Value.Trim('/'));
                     parser.MoveNext();
                  }
                  result.Add(newKey, list);
               }
               else
               {
                  parser.MoveNext();
               }
            }

            parser.MoveNext(); // skip mapping end

            return result;
         }

         public void WriteYaml(IEmitter emitter, object value, Type type)
         {
            // No need to write yaml, so it's not implemented.
            throw new NotImplementedException();
         }
      }
   }
}
