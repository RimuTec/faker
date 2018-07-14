using NUnit.Framework;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class PhoneNumberTests
   {
      [Test]
      public void CellPhone_HappyDays()
      {
         var number = PhoneNumber.CellPhone();
         Assert.IsFalse(number.Contains("#"));
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
      }

      [Test]
      public void LandLine_HappyDays()
      {
         var number = PhoneNumber.LandLine();
         Assert.IsFalse(number.Contains("#"));
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
      }
   }
}
