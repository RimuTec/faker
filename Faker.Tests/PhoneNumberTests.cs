using NUnit.Framework;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class PhoneNumberTests {
      [Test]
      public void CellPhone_HappyDays() {
         // arrange

         // act
         var number = PhoneNumber.CellPhone();

         // assert
         Assert.IsFalse(number.Contains("#"));
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
      }

      [Test]
      public void LandLine_HappyDays() {
         // arrange

         // act
         var number = PhoneNumber.LandLine();

         // assert
         Assert.IsFalse(number.Contains("#"));
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
      }
   }
}
