using NUnit.Framework;
using System;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class BusinessTests : FixtureBase {
      [Test]
      public void CreditCardExpiryDate_HappyDays() {
         var expiry = Business.CreditCardExpiryDate();
         Assert.GreaterOrEqual(expiry, DateTime.Today.Date.AddDays(365));
         Assert.LessOrEqual(expiry, DateTime.Today.Date.AddDays(365 * 3));
      }

      [Test]
      public void CreditCardNumber_HappyDays() {
         var creditCardNumber = Business.CreditCardNumber();
         Assert.AreEqual(3, RegexMatchesCount(creditCardNumber, @"-"));
         Assert.AreEqual(4, RegexMatchesCount(creditCardNumber, @"[0-9]{4}"));
      }

      [Test]
      public void CreditCardType_HappyDays() {
         var type = Business.CreditCardType();
         Assert.IsTrue("['visa', 'mastercard', 'american_express', 'discover', 'diners_club', 'jcb', 'switch', 'solo', 'dankort', 'maestro', 'forbrugsforeningen', 'laser']"
            .Contains($"'{type}'"));
      }
   }
}
