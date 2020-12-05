using NUnit.Framework;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class FinanceTests : FixtureBase
   {
      [Test]
      public void CreditCard_With_Default_Values()
      {
         var cc = Finance.CreditCard();
         Assert.AreEqual(0, RegexMatchesCount(cc, @"[/#]"));
      }

      [Test]
      public void CreditCard_With_Luhn_Checksum()
      {
         var tries = 10;
         while(tries-- > 0)
         {
            var cc = Finance.CreditCard(CreditCardType.Mastercard);
            Assert.IsTrue(cc.StartsWith("5") || cc.StartsWith("6"));
            Assert.AreEqual(0, RegexMatchesCount(cc, @"[/#]"));
            Assert.AreEqual(0, RegexMatchesCount(cc, @"L"));
            Assert.AreEqual(0, RegexMatchesCount(cc, @"[\[\]]"));
         }
      }

      [Test]
      public void CreditCard_With_Multiple_Types()
      {
         var cc = Finance.CreditCard(CreditCardType.Forbrugsforeningen, CreditCardType.AmericanExpress, CreditCardType.Dankort);
         Assert.AreEqual(0, RegexMatchesCount(cc, @"[/#]"), $"{nameof(cc)} is '{cc}'");
         Assert.AreEqual(0, RegexMatchesCount(cc, @"L"));
      }
   }
}
