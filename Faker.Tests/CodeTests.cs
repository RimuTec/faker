using NUnit.Framework;
using System;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class CodeTests : FixtureBase {
      [Test]
      public void Npi_HappyDays() {
         var npi = Code.Npi();
         Assert.AreEqual(10, npi.Length);
         Assert.AreEqual(10, RegexMatchesCount(npi, @"[0-9]"));
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
   }
}
