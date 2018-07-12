using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class BusinessTests : FixtureBase {
      [Test]
      public void CreditCardNumber_HappyDays() {
         // arrange

         // act
         var creditCardNumber = Business.CreditCardNumber();

         // asserts
         Assert.AreEqual(3, RegexMatchesCount(creditCardNumber, @"-"));
         Assert.AreEqual(4, RegexMatchesCount(creditCardNumber, @"[0-9]{4}"));
      }
   }
}
