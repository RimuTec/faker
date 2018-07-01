using NUnit.Framework;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class IDNumberTests {
      [Test]
      public void Valid_SSN() {
         // arrange

         // act
         var validSSN = IDNumber.Valid();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(validSSN));
         Assert.IsFalse(validSSN.Contains("#"));
         Assert.IsFalse(validSSN.Contains("?"));
         Assert.AreEqual(2, Regex.Matches(validSSN, "-").Count);
      }

      [Test]
      public void Valid_SSN_Avoid_All_Zeros_In_One_Segment() {
         // This test has no assertion. It calls Valid() often enough so that
         // an invalid candidate number will be generated which will be discarded
         // and a new one is generated recursively until a valid SSN has been
         // generated.

         // arrange
         var tries = 500;
         
         // act
         while(tries-- > 0) {
            var validSSN = IDNumber.Valid();
         }
      }

      [Test]
      public void Invalid_HappyDays() {
         // arrange

         // act
         var invalid = IDNumber.Invalid();

         // assert
         Assert.IsTrue(IDNumber._invalid_SSN.Any(r => Regex.Matches(invalid, r).Count > 0));
      }
   }
}
