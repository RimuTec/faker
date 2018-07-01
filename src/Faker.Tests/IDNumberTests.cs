using NUnit.Framework;
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
         var tries = 1000;
         while(tries-- > 0) {
            var validSSN = IDNumber.Valid();
         }
      }
   }
}
