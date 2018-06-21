using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class LoremTests {
      [SetUp]
      public void SetUp() {
         RandomNumber.ResetSeed(42);
      }

      [Test]
      public void Word_HappyDays() {
         // arrange

         // act
         var word = Lorem.Word();

         // assert
         Assert.AreEqual("libero", word);
      }

      [Test]
      public void Word_Twice_NotEqual() {
         // arrange

         // act
         var word1 = Lorem.Word();
         var word2 = Lorem.Word();

         // assert
         Assert.AreNotEqual(word1, word2);
      }

      [Test]
      public void Words_HappyDays() {
         // arrange

         // act
         var words = Lorem.Words();

         // assert
         Assert.AreEqual(3, words.Count());
         Assert.AreEqual("cupiditate dolorem voluptatem", string.Join(" ", words));
         Assert.AreEqual(0, words.Count(x => string.IsNullOrWhiteSpace(x)));
      }

      [Test]
      public void Words_With_WordCount() {
         // arrange
         const int wordCount = 42;

         // act
         var words = Lorem.Words(wordCount);

         // assert
         Assert.AreEqual(wordCount, words.Count());
         Assert.AreEqual(0, words.Count(x => string.IsNullOrWhiteSpace(x)));
      }

      [Test]
      public void Words_With_Invalid_WordCount() {
         // arrange
         var invalidWordCount = 0;

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Words(invalidWordCount));

         // assert
         Assert.AreEqual("Must be greater than zero.\r\nParameter name: wordCount", ex.Message);
      }

      [Test]
      public void Words_With_Supplemental() {
         // arrange
         const int defaultWordCount = 3;

         // act
         var words = Lorem.Words(supplemental: true);

         // assert
         Assert.AreEqual(defaultWordCount, words.Count());
         Assert.AreEqual("deficio candidus vel", string.Join(" ", words));
      }

      [Test]
      public void Words_Twice_Are_Different() {
         // arrange

         // act
         var words1 = Lorem.Words();
         var words2 = Lorem.Words();

         // assert
         Assert.AreNotEqual(words1, words2);
      }

      [Test]
      public void Words_WithSupplementalWords_Twice_Are_Different() {
         // arrange

         // act
         var words1 = Lorem.Words(supplemental: true);
         var words2 = Lorem.Words(supplemental: true);

         // assert
         Assert.AreNotEqual(words1, words2);
      }

      [Test]
      public void Multibyte_HappyDays() {
         // arrange

         // act
         var multibyte = Lorem.Multibyte();

         // assert
         var asString = new String(new char[] { multibyte });
         Assert.AreEqual(1, asString.Length);
         var withoutNonAscii = Regex.Replace(asString, @"[^\u0000-\u007F]", string.Empty);
         Assert.AreEqual(0, withoutNonAscii.Length);
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
