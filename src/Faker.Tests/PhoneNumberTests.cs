using NUnit.Framework;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(DefaultFixtureData), nameof(DefaultFixtureData.FixtureParams))]
   public class PhoneNumberTests
   {
      public PhoneNumberTests(string locale)
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
