using NUnit.Framework;
using RimuTec.Faker.Extensions;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests.Extensions {
   [TestFixture]
   public class StringExtensionsTests {
      [Test]
      public void ToWordList_HappyDays() {
         // arrange
         var sentence = "Must be equal to or greater than zero.";

         // act
         var wordList = string.Join("||", sentence.ToWordList());

         // assert
         Assert.AreEqual("must||be||equal||to||or||greater||than||zero", wordList);
      }

      [Test]
      public void ToWordList_For_Question() {
         // arrange
         var sentence = "What is the answer to all questions?";

         // act
         var wordList = string.Join("||", sentence.ToWordList());

         // assert
         Assert.AreEqual("what||is||the||answer||to||all||questions", wordList);
      }

      [Test]
      public void Regexify_Digits() {
         // arrange
         var template = @"[0-8]\d{2}-\d{2}-\d{4}";

         // act
         var result = template.Regexify();

         // assert
         Assert.AreEqual(9, Regex.Matches(result, @"\d").Count);
         Assert.AreEqual(2, Regex.Matches(result, @"-").Count);
         Assert.IsFalse(result.Contains("#"));
      }
   }
}
