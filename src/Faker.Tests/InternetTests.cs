using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
         var suffix = Internet.DomainSuffix();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(suffix));
         Assert.IsFalse(suffix.Contains("#"));
         Assert.IsFalse(suffix.Contains("?"));
         Assert.AreEqual(0, Regex.Matches(suffix, @"[A-Z]").Count);
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
   }
}
