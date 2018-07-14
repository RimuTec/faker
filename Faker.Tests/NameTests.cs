using NUnit.Framework;
using System;
using System.Linq;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class NameTests
   {
      [SetUp]
      public void SetUp()
      {
         RandomNumber.ResetSeed(42);
      }

      [Test]
      public void FullName_HappyDays()
      {
         var fullName = Name.FullName();
         Assert.GreaterOrEqual(fullName.Count(c => c == ' '), 1);
         Assert.GreaterOrEqual(fullName.Length, 3);
      }

      [Test]
      public void FullName_Twice_NotEqual()
      {
         var fullName1 = Name.FullName();
         var fullName2 = Name.FullName();
         Assert.AreNotEqual(fullName1, fullName2);
      }

      [Test]
      public void NameWithMiddle_HappyDays()
      {
         var nameWithMiddle = Name.NameWithMiddle();
         Assert.GreaterOrEqual(nameWithMiddle.Count(c => c == ' '), 2);
         Assert.GreaterOrEqual(nameWithMiddle.Length, 5);
      }

      [Test]
      public void NameWithMiddle_Twice_NotEqual()
      {
         var nameWithMiddle1 = Name.NameWithMiddle();
         var nameWithMiddle2 = Name.NameWithMiddle();
         Assert.AreNotEqual(nameWithMiddle1, nameWithMiddle2);
      }

      [Test]
      public void FirstName_HappyDays()
      {
         var firstName = Name.FirstName();
         Assert.IsTrue(!string.IsNullOrWhiteSpace(firstName));
      }

      [Test]
      public void FirstName_Twice_NotEqual()
      {
         var firstName1 = Name.FirstName();
         var firstName2 = Name.FirstName();
         Assert.AreNotEqual(firstName1, firstName2);
      }

      [Test]
      public void MiddleName_HappyDays()
      {
         var middleName = Name.MiddleName();
         Assert.IsTrue(!string.IsNullOrWhiteSpace(middleName));
      }

      [Test]
      public void MiddleName_Twice_NotEqual()
      {
         var middleName1 = Name.MiddleName();
         var middleName2 = Name.MiddleName();
         Assert.AreNotEqual(middleName1, middleName2);
      }

      [Test]
      public void LastName_HappyDays()
      {
         var lastName = Name.LastName();
         Assert.IsTrue(!string.IsNullOrWhiteSpace(lastName));
      }

      [Test]
      public void LastName_Twice_NotEqual()
      {
         var lastName1 = Name.LastName();
         var lastName2 = Name.LastName();
         Assert.AreNotEqual(lastName1, lastName2);
      }

      [Test]
      public void Prefix_HappyDays()
      {
         var prefix = Name.Prefix();
         Assert.IsTrue(!string.IsNullOrWhiteSpace(prefix));
      }

      [Test]
      public void Prefix_Twice_NotEqual()
      {
         var prefix1 = Name.Prefix();
         var prefix2 = Name.Prefix();
         Assert.AreNotEqual(prefix1, prefix2);
      }

      [Test]
      public void Suffix_HappyDays()
      {
         var suffix = Name.Suffix();
         Assert.IsTrue(!string.IsNullOrWhiteSpace(suffix));
      }

      [Test]
      public void Suffix_Twice_NotEqual()
      {
         var suffix1 = Name.Suffix();
         var suffix2 = Name.Suffix();
         Assert.AreNotEqual(suffix1, suffix2);
      }

      /// <summary>
      /// Tests Initials() with default value, currently 3.
      /// </summary>
      [Test]
      public void Initials_HappyDays()
      {
         const int characterCountDefault = 3;
         var initials = Name.Initials();
         Assert.AreEqual(characterCountDefault, initials.Length);
         Assert.AreEqual(0, initials.Count(c => c == ' '));
         Assert.AreEqual(0, initials.Count(c => c == '.'));
      }

      [Test]
      public void Initials_With_MinimumLength()
      {
         const int expectedCharacterCount = 1;
         var initials = Name.Initials(expectedCharacterCount);
         Assert.AreEqual(expectedCharacterCount, initials.Length);
      }

      [Test]
      public void Initials_With_Invalid_CharacterCount()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Name.Initials(0));
         Assert.AreEqual("characterCount", ex.ParamName);
         Assert.AreEqual("Must be greater than 0.\r\nParameter name: characterCount", ex.Message);
      }
   }
}
