using NUnit.Framework;
using System;
using System.Collections;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(BusinessTestsFixtureData), nameof(BusinessTestsFixtureData.FixtureParams))]
   public class BusinessTests : FixtureBase
   {
      public BusinessTests(string locale)
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
      public void CreditCardExpiryDate_HappyDays()
      {
         var expiry = Business.CreditCardExpiryDate();
         Assert.GreaterOrEqual(expiry, DateTime.Today.Date.AddDays(365));
         Assert.LessOrEqual(expiry, DateTime.Today.Date.AddDays(365 * 3));
      }

      [Test]
      public void CreditCardNumber_HappyDays()
      {
         var creditCardNumber = Business.CreditCardNumber();
         Assert.AreEqual(3, RegexMatchesCount(creditCardNumber, "-"));
         Assert.AreEqual(4, RegexMatchesCount(creditCardNumber, "[0-9]{4}"));
      }

      [Test]
      public void CreditCardType_HappyDays()
      {
         var type = Business.CreditCardType();
         Assert.IsTrue("['visa', 'mastercard', 'american_express', 'discover', 'diners_club', 'jcb', 'switch', 'solo', 'dankort', 'maestro', 'forbrugsforeningen', 'laser']"
            .Contains($"'{type}'"));
      }
   }

   public static class BusinessTestsFixtureData
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
