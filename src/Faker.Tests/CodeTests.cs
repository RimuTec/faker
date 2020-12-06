using NUnit.Framework;
using System;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class CodeTests : FixtureBase
   {
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
}
