using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class InternetTests {
      [Test]
      public void DomainName_HappyDays() {
         // arrange

         // act
         var domainName = Internet.DomainName();

         // assert
         AllAssertions(domainName);
         Assert.AreEqual(1, RegexMatchesCount(domainName, @"\."));
      }

      [Test]
      public void DomainSuffix_HappyDays() {
         // arrange

         // act
         var domainSuffix = Internet.DomainSuffix();

         // assert
         AllAssertions(domainSuffix);
      }

      [Test]
      public void DomainWord_HappyDays() {
         // arrange

         // act
         var domainWord = Internet.DomainWord();

         // assert
         AllAssertions(domainWord);
      }

      [Test]
      public void Email_Default_Values() {
         // arrange

         // act
         var emailAddress = Internet.Email();

         // assert
         AllAssertions(emailAddress);
         Assert.AreEqual(1, RegexMatchesCount(emailAddress, @"@"));
      }

      [Test]
      public void Email_With_Specific_Name() {
         // arrange
         var firstName = Name.FirstName(); // can also be any other name

         // act
         var emailAddress = Internet.Email(firstName);

         // assert
         AllAssertions(emailAddress);
         Assert.IsTrue(emailAddress.StartsWith($"{firstName.ToLower()}@"), $"Email address is: '{emailAddress}'");
      }

      [Test]
      public void UserName_With_Default_Values() {
         // arrange

         // act
         var userName = Internet.UserName();

         // assert
         AllAssertions(userName);
      }

      [Test]
      public void UserName_With_Given_Name() {
         // arrange

         // act
         var userName = Internet.UserName("Nancy");

         // assert
         AllAssertions(userName);
         Assert.AreEqual("nancy", userName);
      }

      [Test]
      public void UserName_With_Given_Separators() {
         // arrange
         char[] separators = new[] { '-', '_', '+' };

         // act
         var userName = Internet.UserName(separators: separators);

         // assert
         AllAssertions(userName);
         Assert.GreaterOrEqual(1, RegexMatchesCount(userName, @"[-_+]"));
      }

      [Test]
      public void UserName_With_No_Separator_Guaranteed() {
         // arrange

         // act
         var userName = Internet.UserName(null, new char[0]);

         // assert
         AllAssertions(userName);
         Assert.AreEqual(0, RegexMatchesCount(userName, @"[-_+]"));
      }

      [Test]
      public void UserName_With_String_Argument() {
         // arrange
         var firstName = "bo";
         var lastName = "peep";
         var tries = 10;

         // act
         while (tries-- > 0) {
            var userName = Internet.UserName($"{firstName} {lastName}");

            // assert
            AllAssertions(userName);
            Assert.IsTrue(Regex.Match(userName, @"(bo(_|\.)peep|peep(_|\.)bo)").Success, $"userName was '{userName}'");
         }
      }

      [Test]
      public void UserName_With_Min_Length() {
         // arrange
         const int minLength = 7;
         var tries = 10;

         // act
         while (tries-- > 0) {
            var userName = Internet.UserName(minLength);

            // assert
            Assert.GreaterOrEqual(userName.Length, minLength);
         }
      }

      [Test]
      public void UserName_With_Ridiculous_Value() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>Internet.UserName((int)Math.Pow(10, 6) + 1));

         // assert
         Assert.AreEqual("Must be equal to or less than 10^6.\r\nParameter name: minLength", ex.Message);
      }

      private static void AllAssertions(string candidate) {
         Assert.IsFalse(string.IsNullOrWhiteSpace(candidate));
         Assert.IsFalse(candidate.Contains("#"));
         Assert.IsFalse(candidate.Contains("?"));
         Assert.AreEqual(0, Regex.Matches(candidate, @"[\s]").Count, $"Candidate was {candidate}");
         Assert.AreEqual(0, Regex.Matches(candidate, @"[A-Z]").Count, $"Candidate was {candidate}");
      }

      protected static int RegexMatchesCount(string input, string pattern) {
         return Regex.Matches(input, pattern, RegexOptions.Compiled).Count;
      }
   }
}
