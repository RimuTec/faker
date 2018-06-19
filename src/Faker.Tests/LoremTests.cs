using NUnit.Framework;
using System.Linq;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class LoremTests {
      [Test]
      public void Words_HappyDays() {
         // arrange
         const int wordCount = 42;

         // act
         var words = Lorem.Words(wordCount);

         // assert
         Assert.AreEqual(wordCount, words.Count());
         Assert.AreEqual(0, words.Count(x => string.IsNullOrWhiteSpace(x)));
      }

      [Test]
      public void GetFirstWord() {
         // arrange

         // act
         var firstWord = Lorem.GetFirstWord();

         // assert
         Assert.AreEqual("alias", firstWord);
      }

      [Test]
      public void Sentence_HappyDays() {
         // arrange

         // act
         var sentence = Lorem.Sentence(/* 4 is default */);

         // assert
         Assert.IsTrue(sentence.EndsWith("."));
         Assert.GreaterOrEqual(sentence.Count(c => c == ' '), 3);
      }

      [Test]
      public void Sentences_HappyDays() {
         // arrange

         // act
         var sentences = Lorem.Sentences(7);

         // assert
         Assert.AreEqual(7, sentences.Count());
         Assert.AreEqual(7, sentences.Count(x => x.EndsWith(".")));
         Assert.AreEqual(7, sentences.Count(x => x.Contains(" ")));
      }

      [Test]
      public void Paragraph_HappyDays() {
         // arrange

         // act
         var paragraph = Lorem.Paragraph(/* 3 is default */);

         // assert
         Assert.GreaterOrEqual(paragraph.Count(c => c == '.'), 3);
         Assert.Greater(paragraph.Count(c => c == ' '), 9);
      }

      [Test]
      public void Paragraphs_HappyDays() {
         // arrange

         // act
         var paragraphs = Lorem.Paragraphs(7);

         // assert
         Assert.AreEqual(7, paragraphs.Count());
         Assert.AreEqual(7, paragraphs.Count(x => x.EndsWith(".")));
         Assert.AreEqual(7, paragraphs.Count(x => x.Contains(" ")));
      }
   }
}
