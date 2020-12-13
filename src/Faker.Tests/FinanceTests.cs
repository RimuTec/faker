using System.Collections;
using NUnit.Framework;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(FinanceTestsFixtureData), nameof(FinanceTestsFixtureData.FixtureParams))]
   public class FinanceTests : FixtureBase
   {
      public FinanceTests(string locale)
      {
         Locale = locale;
      }

      [SetUp]
      public void SetUp()
      {
         Config.Locale = Locale;
      }

      private string Locale { get; }

      [Test]
      public void CreditCard_With_Default_Values()
      {
         var cc = Finance.CreditCard();
         Assert.AreEqual(0, RegexMatchesCount(cc, "[/#]"));
      }

      [Test]
      public void CreditCard_With_Luhn_Checksum()
      {
         var tries = 10;
         while(tries-- > 0)
         {
            var cc = Finance.CreditCard(CreditCardType.Mastercard);
            Assert.IsTrue(cc.StartsWith("5") || cc.StartsWith("6"));
            Assert.AreEqual(0, RegexMatchesCount(cc, "[/#]"));
            Assert.AreEqual(0, RegexMatchesCount(cc, "L"));
            Assert.AreEqual(0, RegexMatchesCount(cc, @"[\[\]]"));
         }
      }

      [Test]
      public void CreditCard_With_Multiple_Types()
      {
         var cc = Finance.CreditCard(CreditCardType.Forbrugsforeningen, CreditCardType.AmericanExpress, CreditCardType.Dankort);
         Assert.AreEqual(0, RegexMatchesCount(cc, "[/#]"), $"{nameof(cc)} is '{cc}'");
         Assert.AreEqual(0, RegexMatchesCount(cc, "L"));
      }
   }

   public static class FinanceTestsFixtureData
   {
      public static IEnumerable FixtureParams
      {
         get
         {
            yield return new TestFixtureData("bg");
            yield return new TestFixtureData("ca");
            yield return new TestFixtureData("ca-CAT");
            yield return new TestFixtureData("da-DK");
            yield return new TestFixtureData("de");
            yield return new TestFixtureData("de-AT");
            yield return new TestFixtureData("de-CH");
            yield return new TestFixtureData("ee");
            yield return new TestFixtureData("en");
            yield return new TestFixtureData("en-AU");
            yield return new TestFixtureData("en-au-ocker");
            yield return new TestFixtureData("en-BORK");
            yield return new TestFixtureData("en-CA");
            yield return new TestFixtureData("en-GB");
            yield return new TestFixtureData("en-IND");
            yield return new TestFixtureData("en-MS");
            yield return new TestFixtureData("en-NEP");
            yield return new TestFixtureData("en-NG");
            yield return new TestFixtureData("en-NZ");
            yield return new TestFixtureData("en-PAK");
            yield return new TestFixtureData("en-SG");
            yield return new TestFixtureData("en-UG");
            yield return new TestFixtureData("en-US");
            yield return new TestFixtureData("en-ZA");
            yield return new TestFixtureData("es");
            yield return new TestFixtureData("es-MX");
            yield return new TestFixtureData("fa");
            yield return new TestFixtureData("fi-FI");
            yield return new TestFixtureData("fr");
            yield return new TestFixtureData("fr-CA");
            yield return new TestFixtureData("fr-CH");
            yield return new TestFixtureData("he");
            yield return new TestFixtureData("id");
            yield return new TestFixtureData("it");
            yield return new TestFixtureData("ja");
            yield return new TestFixtureData("ko");
            yield return new TestFixtureData("lv");
            yield return new TestFixtureData("nb-NO");
            yield return new TestFixtureData("nl");

            // Not testing locale 'no' since the file no.yml has an incorrect format at the time of writing.
            // Note that we won't fix the content as we take the file 'as-is' from https://github.com/faker-ruby/faker
            //yield return new TestFixtureData("no", null);

            yield return new TestFixtureData("pl");
            yield return new TestFixtureData("pt");
            yield return new TestFixtureData("pt-BR");
            yield return new TestFixtureData("ru");
            yield return new TestFixtureData("sk");
            yield return new TestFixtureData("sv");
            yield return new TestFixtureData("tr");
            yield return new TestFixtureData("uk");
            yield return new TestFixtureData("vi");
            yield return new TestFixtureData("zh-CN");
            yield return new TestFixtureData("zh-TW");
         }
      }
   }
}
