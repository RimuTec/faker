using NUnit.Framework;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(DefaultFixtureData), nameof(DefaultFixtureData.FixtureParams))]
   public class IDNumberTests
   {
      public IDNumberTests(string locale)
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
      public void Valid_SSN()
      {
         var validSSN = IdNumber.SsnValid();
         Assert.IsFalse(string.IsNullOrWhiteSpace(validSSN));
         Assert.IsFalse(validSSN.Contains("#"));
         Assert.IsFalse(validSSN.Contains("?"));
         Assert.AreEqual(2, Regex.Matches(validSSN, "-").Count);
      }

      [Test]
      public void Valid_SSN_Avoid_All_Zeros_In_One_Segment()
      {
         // This test has no assertion. It calls SsnValid() often enough so that
         // an invalid candidate number will be generated which will be discarded
         // and a new one is generated recursively until a valid SSN has been
         // generated.
         var tries = 500;
         IdNumber._SSN_Valid_recursive = false;
         var recursive = false;
         while (tries-- > 0 && !recursive)
         {
            _ = IdNumber.SsnValid();
            recursive = IdNumber._SSN_Valid_recursive;
         }
         Assert.IsTrue(recursive);
      }

      [Test]
      public void Invalid_HappyDays()
      {
         var invalid = IdNumber.Invalid();
         Assert.IsTrue(IdNumber._invalid_SSN.Any(r => Regex.Matches(invalid, r).Count > 0));
      }

      [Test]
      public void SpanishCitizenNumber_HappyDays()
      {
         var dni = IdNumber.SpanishCitizenNumber();
         Assert.IsFalse(string.IsNullOrWhiteSpace(dni));
         Assert.IsFalse(dni.Contains("#"));
         Assert.IsFalse(dni.Contains("?"));
         Assert.AreEqual(1, Regex.Matches(dni, @"-\w").Count);
         Assert.AreEqual(10, dni.Length);
      }

      [Test]
      public void SpanishForeignCitizenNumber_HappyDays()
      {
         var nie = IdNumber.SpanishForeignCitizenNumber();
         Assert.IsFalse(string.IsNullOrWhiteSpace(nie));
         Assert.IsFalse(nie.Contains("#"));
         Assert.IsFalse(nie.Contains("?"));
         Assert.AreEqual(2, Regex.Matches(nie, @"-\w").Count);
         Assert.AreEqual(11, nie.Length);
      }
   }
}
