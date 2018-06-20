using NUnit.Framework;
using System;
using System.Linq;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class NameTests {
      [SetUp]
      public void SetUp() {
         RandomNumber.ResetSeed(42);
      }

      [Test]
      public void FullName_HappyDays() {
         // arrange

         // act
         var fullName = Name.FullName();

         // assert
         Assert.GreaterOrEqual(fullName.Count(c => c == ' '), 1);
         Assert.GreaterOrEqual(fullName.Length, 3);
      }

      [Test]
      public void FullName_Twice_NotEqual() {
         // arrange

         // act
         var fullName1 = Name.FullName();
         var fullName2 = Name.FullName();

         // assert
         Assert.AreNotEqual(fullName1, fullName2);
      }

      [Test]
      public void NameWithMiddle_HappyDays() {
         // arrange

         // act
         var nameWithMiddle = Name.NameWithMiddle();

         // assert
         Assert.GreaterOrEqual(nameWithMiddle.Count(c => c == ' '), 2);
         Assert.GreaterOrEqual(nameWithMiddle.Length, 5);
      }

      [Test]
      public void NameWithMiddle_Twice_NotEqual() {
         // arrange

         // act
         var nameWithMiddle1 = Name.NameWithMiddle();
         var nameWithMiddle2 = Name.NameWithMiddle();

         // assert
         Assert.AreNotEqual(nameWithMiddle1, nameWithMiddle2);
      }

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
      public void MiddleName_HappyDays() {
         // arrange

         // act
         var middleName = Name.MiddleName();

         // assert
         Assert.IsTrue(!string.IsNullOrWhiteSpace(middleName));
      }

      [Test]
      public void MiddleName_Twice_NotEqual() {
         // arrange

         // act
         var middleName1 = Name.MiddleName();
         var middleName2 = Name.MiddleName();

         // assert
         Assert.AreNotEqual(middleName1, middleName2);
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

      [Test]
      public void Prefix_HappyDays() {
         // arrange

         // act
         var prefix = Name.Prefix();

         // assert
         Assert.IsTrue(!string.IsNullOrWhiteSpace(prefix));
      }

      [Test]
      public void Prefix_Twice_NotEqual() {
         // arrange

         // act
         var prefix1 = Name.Prefix();
         var prefix2 = Name.Prefix();

         // assert
         Assert.AreNotEqual(prefix1, prefix2);
      }

      [Test]
      public void Suffix_HappyDays() {
         // arrange

         // act
         var suffix = Name.Suffix();

         // assert
         Assert.IsTrue(!string.IsNullOrWhiteSpace(suffix));
      }

      [Test]
      public void Suffix_Twice_NotEqual() {
         // arrange

         // act
         var suffix1 = Name.Suffix();
         var suffix2 = Name.Suffix();

         // assert
         Assert.AreNotEqual(suffix1, suffix2);
      }

      /// <summary>
      /// Tests Initials() with default value, currently 3.
      /// </summary>
      [Test]
      public void Initials_HappyDays() {
         // arrange
         const int characterCountDefault = 3;

         // act
         var initials = Name.Initials();

         // assert
         Assert.AreEqual(characterCountDefault, initials.Length);
         Assert.AreEqual(0, initials.Count(c => c == ' '));
         Assert.AreEqual(0, initials.Count(c => c == '.'));
      }

      [Test]
      public void Initials_With_MinimumLength() {
         // arrange
         const int expectedCharacterCount = 1;

         // act
         var initials = Name.Initials(expectedCharacterCount);

         // assert
         Assert.AreEqual(expectedCharacterCount, initials.Length);
      }

      [Test]
      public void Initials_With_Invalid_CharacterCount() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Name.Initials(0));

         // assert
         Assert.AreEqual("characterCount", ex.ParamName);
         Assert.AreEqual("Must be greater than 0.\r\nParameter name: characterCount", ex.Message);
      }
   }
}
