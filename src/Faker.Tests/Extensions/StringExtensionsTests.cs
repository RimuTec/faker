using NUnit.Framework;

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
   }
}
