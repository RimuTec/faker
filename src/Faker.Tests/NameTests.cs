using NUnit.Framework;
using System;
using System.Linq;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class NameTests : FixtureBase
   {
      [SetUp]
      public void SetUp()
      {
         RandomNumber.ResetSeed(42);
      }

      [TearDown]
      public void TearDown()
      {
         Config.Locale = "en";
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
      public void FullName_Localized()
      {
         Config.Locale = "de";
         var fullName = Name.FullName();
         var parts = fullName.Split(' ');
         var firstNames = Fetch("name.first_name");
         Assert.IsTrue(firstNames.Contains("Wiebke"));
         foreach(var part in parts)
         {
            if(firstNames.Contains(part))
            {
               return;
            }
         }
         Assert.Fail("Not using localized first name");
      }

      [Test]
      public void FullName_With_Nobility_Title()
      {
         RandomNumber.ResetSeed(42);
         Config.Locale = "de";
         var fullName = Name.FullName();
         var nobilityTitles = Fetch("name.nobility_title");
         var tries = 1;
         while(!fullName.Split(' ').Any(x => nobilityTitles.Contains(x))) {
            tries++;
            if(tries > 20)
            {
               Assert.Fail("Not using locale for full name");
            }
            fullName = Name.FullName();
         }
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
      public void FirstName_With_LocaleRu()
      {
         Config.Locale = "ru";
         var firstName = Name.FirstName();
         Assert.IsTrue(!firstName.Contains("_first_name"), $"firstName was '{firstName}'");
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
      public void LastName_With_LocaleEn()
      {
         Config.Locale = "en";
         var lastName = Name.LastName();
         Assert.IsTrue(!string.IsNullOrWhiteSpace(lastName));
      }

      [Test]
      public void LastName_With_LocalePl()
      {
         Config.Locale = "pl";
         var lastName = Name.LastName();
         Assert.IsTrue(!string.IsNullOrWhiteSpace(lastName));
      }

      [Test]
      public void LastName_With_LocaleRu()
      {
         Config.Locale = "ru";
         var lastName = Name.LastName();
         Assert.IsTrue(!lastName.Contains("_last_name"), $"lastName was '{lastName}'");
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
      public void Prefix_Localized()
      {
         Config.Locale = "de";
         var prefix = Name.Prefix();
         var prefixes = Fetch("name.prefix");
         Assert.IsTrue(prefixes.Contains("Dipl.-Ing."));
         Assert.IsTrue(prefixes.Contains(prefix));
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

      [Test]
      public void Suffix_Localized()
      {
         RandomNumber.ResetSeed(42);
         Config.Locale = "pt";
         var suffix = Name.Suffix();
         var tries = 0;
         var suffixes = new[] { "Neto", "Filho" };
         while (!suffixes.Contains(suffix))
         {
            tries++;
            if(tries > 10)
            {
               Assert.Fail("Not using locale");
            }
            suffix = Name.Suffix();
         }
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
         Assert.AreEqual("Must be greater than 0. (Parameter 'characterCount')", ex.Message);
      }
   }
}
