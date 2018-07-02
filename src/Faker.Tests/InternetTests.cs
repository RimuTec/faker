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
      }

      [Test]
      public void UserName_HappyDays() {
         // arrange

         // act
         var userName = Internet.UserName();

         // assert
         AllAssertions(userName);
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
