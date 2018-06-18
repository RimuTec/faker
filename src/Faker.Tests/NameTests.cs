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

      [Test]
      public void FirstName_Twice_NotEqual() {
         // arrange

         // act
         var firstName1 = Name.FirstName();
         var firstName2 = Name.FirstName();

         // assert
         Assert.AreNotEqual(firstName1, firstName2);
      }

      [Test]
      public void LastName_HappyDays() {
         // arrange

         // act
         var lastName = Name.LastName();

         // assert
         Assert.IsTrue(!string.IsNullOrWhiteSpace(lastName));
      }

      [Test]
      public void LastName_Twice_NotEqual() {
         // arrange

         // act
         var lastName1 = Name.LastName();
         var lastName2 = Name.LastName();

         // assert
         Assert.AreNotEqual(lastName1, lastName2);
      }
   }
}
