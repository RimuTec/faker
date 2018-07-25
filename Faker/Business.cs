using System;

namespace RimuTec.Faker
{
   /// <summary>
   /// Generators for business related data
   /// </summary>
   public class Business : GeneratorBase
   {
      private Business() { }

      /// <summary>
      /// Generates a credit card expiry date.
      /// </summary>
      /// <returns></returns>
      public static DateTime CreditCardExpiryDate()
      {
         return DateTime.Today.Date.AddDays(365 * RandomNumber.Next(1, 4));
      }

      /// <summary>
      /// Generates a credit card number.
      /// </summary>
      /// <returns></returns>
      public static string CreditCardNumber()
      {
         return Fetch("business.credit_card_numbers");
      }

      /// <summary>
      /// Returns a credit card type, e.g. 'visa'.
      /// </summary>
      /// <returns></returns>
      public static string CreditCardType()
      {
         return Fetch("business.credit_card_types");
      }
   }
}
