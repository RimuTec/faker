using NUnit.Framework;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class FinanceTests : FixtureBase {
      [Test]
      public void CreditCard_With_Default_Values() {
         // arrange

         // act
         var cc = Finance.CreditCard();

         // assert
         Assert.AreEqual(0, RegexMatchesCount(cc, @"[/#]"));
      }

      [Test]
      public void CreditCard_With_Luhn_Checksum() {
         // arrange

         // act
         var cc = Finance.CreditCard(CreditCardType.Mastercard);

         // assert
         Assert.IsTrue(cc.StartsWith("5") || cc.StartsWith("6"));
         Assert.AreEqual(0, RegexMatchesCount(cc, @"[/#]"));
         Assert.AreEqual(0, RegexMatchesCount(cc, @"L"));
      }

      [Test]
      public void CreditCard_With_Multiple_Types() {
         // arrange

         // act
         var cc = Finance.CreditCard(CreditCardType.Forbrugsforeningen, CreditCardType.AmericanExpress, CreditCardType.Dankort);

         // assert
         Assert.AreEqual(0, RegexMatchesCount(cc, @"[/#]"), $"{nameof(cc)} is '{cc}'");
         Assert.AreEqual(0, RegexMatchesCount(cc, @"L"));
      }
   }
}
