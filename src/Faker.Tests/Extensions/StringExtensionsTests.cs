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
   }
}
