using NUnit.Framework;
using System;
using System.Collections;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(CodeTestsFixtureData), nameof(CodeTestsFixtureData.FixtureParams))]
   public class CodeTests : FixtureBase
   {
      public CodeTests(string locale)
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
      public void Asin()
      {
         var asin = Code.Asin();
         Assert.AreEqual(1, RegexMatchesCount(asin, @"^B000([A-Z]|\d){6}$"));
      }

      [Test]
      public void Ean_With_Default_Value()
      {
         const int digitCount = 13;
         var ean = Code.Ean();
         Assert.AreEqual(digitCount, ean.Length);
         Assert.AreEqual(digitCount, RegexMatchesCount(ean, "[0-9]"));
      }

      [Test]
      public void Ean_Base8()
      {
         const int @base = 8;
         var ean = Code.Ean(@base);
         Assert.AreEqual(@base, ean.Length);
         Assert.AreEqual(@base, RegexMatchesCount(ean, "[0-9]"));
      }

      [Test]
      public void Ean_Base13()
      {
         const int @base = 13;
         var ean = Code.Ean(@base);
         Assert.AreEqual(@base, ean.Length);
         Assert.AreEqual(@base, RegexMatchesCount(ean, "[0-9]"));
      }

      [Test]
      public void Ean_With_Invalid_Value()
      {
         const int invalid = 42;
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Code.Ean(invalid));
         Assert.AreEqual("Must be either 8 or 13. (Parameter 'base')", ex.Message);
      }

      [Test]
      public void Imei_HappyDays()
      {
         var imei = Code.Imei();
         Assert.AreEqual(15, imei.Length);
      }

      [Test]
      public void Isbn_With_Default_Value()
      {
         var isbn = Code.Isbn();
         Assert.AreEqual(11, isbn.Length);
         Assert.AreEqual('-', isbn[9]);
      }

      [Test]
      public void Isbn_Base13()
      {
         var isbn = Code.Isbn(13);
         Assert.AreEqual(14, isbn.Length);
      }

      [Test]
      public void Isbn_With_Invalid_Value()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Code.Isbn(42));
         Assert.AreEqual("Must be either 10 or 13. (Parameter 'base')", ex.Message);
      }

      [Test]
      public void Npi_HappyDays()
      {
         var npi = Code.Npi();
         Assert.AreEqual(10, npi.Length);
         Assert.AreEqual(10, RegexMatchesCount(npi, "[0-9]"));
      }

      [Test]
      public void Nric_With_Default_Values()
      {
         var nric = Code.Nric();
         Assert.AreEqual(1, RegexMatchesCount(nric, @"^[ST]\d{7}[A-JZ]$"));
      }

      [Test]
      public void Nric_Within_Age_Range()
      {
         var birthyear1 = DateTime.Today.AddYears(-15).Year.ToString().Substring(2, 2);
         var birthyear2 = DateTime.Today.AddYears(-16).Year.ToString().Substring(2, 2);
         var nric = Code.Nric(15, 16);
         Assert.IsTrue(nric.StartsWith($"T{birthyear1}") || nric.StartsWith($"T{birthyear2}"));
      }

      [Test]
      public void Nric_Invalid_MinAge()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Code.Nric(minAge: 0));
         Assert.AreEqual("Must be greater than zero. (Parameter 'minAge')", ex.Message);
      }

      [Test]
      public void Nric_Invalid_MaxAge()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Code.Nric(maxAge: 0));
         Assert.AreEqual("Must be greater than minAge. (Parameter 'maxAge')", ex.Message);
      }

      [Test]
      public void Nric_MaxAge_Greater_Than_MinAge()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Code.Nric(minAge: 42, maxAge: 41));
         Assert.AreEqual("Must be greater than minAge. (Parameter 'maxAge')", ex.Message);
      }

      [Test]
      public void Rut()
      {
         var rut = Code.Rut();
         Assert.AreEqual(10, rut.Length);
         Assert.AreEqual(9, RegexMatchesCount(rut, "[0-9K]"));
         Assert.AreEqual('-', rut[8]);
      }

      [Test]
      public void Sin()
      {
         var sin = Code.Sin();
         Assert.AreEqual(1, RegexMatchesCount(sin, @"\d{9}"));
         Assert.AreEqual(9, sin.Length);
         Assert.IsTrue(LuhnChecksumValid(sin));
      }

      public static bool LuhnChecksumValid(string digits)
      {
         // Implementation of this method taken from https://en.wikipedia.org/wiki/Luhn_algorithm#C#
         int sum = 0;
         int len = digits.Length;
         for (int i = 0; i < len; i++)
         {
            int add = (digits[i] - '0') * (2 - ((i + len) % 2));
            add -= add > 9 ? 9 : 0;
            sum += add;
         }
         return sum % 10 == 0;
      }
   }

   public static class CodeTestsFixtureData
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
