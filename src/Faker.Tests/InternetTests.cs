using NUnit.Framework;
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
         Assert.AreEqual(1, Regex.Matches(domainName, @"\.").Count);
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
         Assert.AreEqual(1, Regex.Matches(emailAddress, @"@").Count);
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
         Assert.AreEqual("nancy", userName);
      }

      private static void AllAssertions(string candidate) {
         Assert.IsFalse(string.IsNullOrWhiteSpace(candidate));
         Assert.IsFalse(candidate.Contains("#"));
         Assert.IsFalse(candidate.Contains("?"));
         Assert.AreEqual(0, Regex.Matches(candidate, @"[\s]").Count);
         Assert.AreEqual(0, Regex.Matches(candidate, @"[A-Z]").Count);
      }
   }
}
