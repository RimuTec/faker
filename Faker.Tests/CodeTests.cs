using NUnit.Framework;
using System;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class CodeTests : FixtureBase {
      [Test]
      public void Ean_With_Default_Value() {
         const int digitCount = 13;
         var ean = Code.Ean();
         Assert.AreEqual(digitCount, ean.Length);
         Assert.AreEqual(digitCount, RegexMatchesCount(ean, @"[0-9]"));
      }

      [Test]
      public void Ean_Base8() {
         const int @base = 8;
         var ean = Code.Ean(@base);
         Assert.AreEqual(@base, ean.Length);
         Assert.AreEqual(@base, RegexMatchesCount(ean, @"[0-9]"));
      }

      [Test]
      public void Ean_Base13() {
         const int @base = 13;
         var ean = Code.Ean(@base);
         Assert.AreEqual(@base, ean.Length);
         Assert.AreEqual(@base, RegexMatchesCount(ean, @"[0-9]"));
      }

      [Test]
      public void Ean_With_Invalid_Value() {
         const int invalid = 42;
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Code.Ean(invalid));
         Assert.AreEqual("Must be either 8 or 13.\r\nParameter name: base", ex.Message);
      }

      [Test]
      public void Isbn_With_Default_Value() {
         var isbn = Code.Isbn();
         Assert.AreEqual(11, isbn.Length);
         Assert.AreEqual('-', isbn[9]);
      }

      [Test]
      public void Isbn_Base13() {
         var isbn = Code.Isbn(13);
         Assert.AreEqual(14, isbn.Length);
      }

      [Test]
      public void Isbn_With_Invalid_Value() {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Code.Isbn(42));
         Assert.AreEqual("Must be either 10 or 13.\r\nParameter name: base", ex.Message);
      }

      [Test]
      public void Npi_HappyDays() {
         var npi = Code.Npi();
         Assert.AreEqual(10, npi.Length);
         Assert.AreEqual(10, RegexMatchesCount(npi, @"[0-9]"));
      }

      [Test]
      public void Rut() {
         var rut = Code.Rut();
         Assert.AreEqual(10, rut.Length);
         Assert.AreEqual(9, RegexMatchesCount(rut, @"[0-9K]"));
         Assert.AreEqual('-', rut[8]);
      }
   }
}
