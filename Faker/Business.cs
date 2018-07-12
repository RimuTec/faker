using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using YamlDotNet.Serialization;

namespace RimuTec.Faker {
   /// <summary>
   /// Generators for business related data
   /// </summary>
   public static class Business {
      // Resources used by this class from https://github.com/stympy/faker/blob/master/lib/locales/en/business.yml

      static Business() {
         const string yamlFileName = "RimuTec.Faker.locales.en.business.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _business = locale.en.faker.business;
      }

      /// <summary>
      /// Generates a credit card expiry date.
      /// </summary>
      /// <returns></returns>
      public static DateTime CreditCardExpiryDate() {
         return DateTime.Today.Date.AddDays(365 * RandomNumber.Next(1, 4));
      }

      /// <summary>
      /// Generates a credit card number.
      /// </summary>
      /// <returns></returns>
      public static string CreditCardNumber() {
         return _business.CreditCardNumbers.Sample();
      }

      /// <summary>
      /// Returns a credit card type, e.g. 'visa'.
      /// </summary>
      /// <returns></returns>
      public static string CreditCardType() {
         return _business.CreditCardTypes.Sample();
      }

      private static readonly business _business;

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
         public business business { get; set; }
      }

      internal class business {
         [YamlMember(Alias = "credit_card_numbers", ApplyNamingConventions = false)]
         public string[] CreditCardNumbers { get; set; }

         [YamlMember(Alias = "credit_card_types", ApplyNamingConventions = false)]
         public string[] CreditCardTypes { get; set; }
      }
#pragma warning restore IDE1006 // Naming Styles
   }
}
