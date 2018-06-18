using NUnit.Framework;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class NameTests {
      [Test]
      public void FirstName_HappyDays() {
         // arrange

         // act
         var firstName = Name.FirstName();

         // assert
         Assert.IsTrue(!string.IsNullOrWhiteSpace(firstName));
      }
   }
}
