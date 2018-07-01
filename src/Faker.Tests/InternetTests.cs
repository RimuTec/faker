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
         Assert.IsFalse(string.IsNullOrWhiteSpace(domainName));
         Assert.IsFalse(domainName.Contains("#"));
         Assert.IsFalse(domainName.Contains("?"));
         Assert.AreEqual(1, Regex.Matches(domainName, @"\.").Count);
         Assert.AreEqual(0, Regex.Matches(domainName, @"[A-Z]").Count);
      }

      [Test]
      public void DomainSuffix_HappyDays() {
         // arrange

         // act
         var domainSuffix = Internet.DomainSuffix();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(domainSuffix));
         Assert.IsFalse(domainSuffix.Contains("#"));
         Assert.IsFalse(domainSuffix.Contains("?"));
         Assert.AreEqual(0, Regex.Matches(domainSuffix, @"[A-Z]").Count);
      }

      [Test]
      public void DomainWord_HappyDays() {
         // arrange

         // act
         var domainWord = Internet.DomainWord();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(domainWord));
         Assert.IsFalse(domainWord.Contains("#"));
         Assert.IsFalse(domainWord.Contains("?"));
         Assert.AreEqual(0, Regex.Matches(domainWord, @"[A-Z]").Count);
      }

      [Test]
      public void UserName_HappyDays() {
         // arrange

         // act
         var userName = Internet.UserName();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(userName));
         Assert.IsFalse(userName.Contains("#"));
         Assert.IsFalse(userName.Contains("?"));
         Assert.AreEqual(0, Regex.Matches(userName, @"[A-Z]").Count);
      }
   }
}
