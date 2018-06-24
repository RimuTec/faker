using NUnit.Framework;
using RimuTec.Faker.Tests.Extensions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class LoremTests {
      public LoremTests() {
         var wordList = Lorem._WordList;
         _wordList = wordList.Distinct().ToArray();

         var supplementaryList = Lorem._SupplementalWordList;
         _supplementalWordList = supplementaryList.Distinct().ToArray();

         var intersection = _wordList.Intersect(_supplementalWordList);
         _jointWords = intersection.Distinct().ToArray();

         Assert.Greater(_wordList.Count(), 20);
         Assert.Greater(_supplementalWordList.Count(), 20);
      }


      [OneTimeSetUp]
      public void FixtureSetUp() {
      }

      [SetUp]
      public void SetUp() {
         RandomNumber.ResetSeed(42);
      }

      [Test]
      public void Character_HappyDays() {
         // arrange

         // act
         var character = Lorem.Character();

         // assert
         Assert.AreEqual(1, character.Length);
      }

      [Test]
      public void Characters_With_Default_Value() {
         // arrange

         // act
         var characters = Lorem.Characters();

         // assert
         Assert.AreEqual(255, characters.Length);
         Assert.AreEqual(0, characters.Count(c => c == ' '));
      }

      [Test]
      public void Characters_With_Random_CharCount() {
         // arrange
         var charCount = RandomNumber.Next(42, 84);

         // act
         var characters = Lorem.Characters(charCount);

         // assert
         Assert.AreEqual(charCount, characters.Length);
         Assert.AreEqual(0, characters.Count(c => c == ' '));
      }

      [Test]
      public void Characters_With_Invalid_CharCount() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Characters(-1));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: charCount", ex.Message);
      }

      [Test]
      public void Characters_Returns_Empty_String_When_CharCount_Zero() {
         // arrange

         // act
         var characters = Lorem.Characters(0);

         // assert
         Assert.AreEqual(string.Empty, characters);
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
         foreach(var word in words) {
            if(_jointWords.Contains(word)) {
               continue;
            }
            Assert.True(_wordList.Contains(word));
            Assert.False(_supplementalWordList.Contains(word));
         }
      }

      [Test]
      public void Words_With_Invalid_WordCount() {
         // arrange
         var invalidWordCount = -1;

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Words(invalidWordCount));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: wordCount", ex.Message);
      }

      [Test]
      public void Words_With_WordCount_Zero_Returns_Empty_Enumerable() {
         // arrange

         // act
         var words = Lorem.Words(0);

         // assert
         Assert.AreEqual(0, words.Count());
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
      public void Words_Uses_Words_From_Supplementary_List() {
         // arrange

         // act
         var words = Lorem.Words(42, supplemental: true);

         // assert
         foreach(var word in words) {
            if(_supplementalWordList.Contains(word)
               || !_jointWords.Contains(word)) {
               return;
            }
         }
         Assert.Fail("Words() does not consider supplementary words.");
      }

      [Test]
      public void Words_Uses_Words_From_Basic_List_Only() {
         // arrange

         // act
         var words = Lorem.Words(_wordList.Count(), supplemental: false);

         // assert
         foreach(var word in words) {
            Assert.IsTrue(_wordList.Contains(word));
         }
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
         Assert.AreEqual(1, multibyte.Length);
         var withoutNonAscii = Regex.Replace(multibyte, @"[^\u0000-\u007F]", string.Empty);
         Assert.AreEqual(0, withoutNonAscii.Length);
      }

      [Test]
      public void Sentence_HappyDays() {
         // arrange

         // act
         var sentence = Lorem.Sentence(/* 4 is default */);

         // assert
         Assert.IsTrue(sentence.EndsWith("."));
         Assert.AreEqual(0, Regex.Match(sentence, "^[A-Z]").Index); // first letter to be upper case
         var matches = Regex.Matches(sentence, "[a-z]+");
         Assert.GreaterOrEqual(matches.Count, 4);
         Assert.GreaterOrEqual(sentence.Count(c => c == ' '), 3);
      }

      [Test]
      public void Sentence_Non_Default_Value() {
         // arrange
         const int minimumWordCount = 42;

         // act
         var sentence = Lorem.Sentence(minimumWordCount);

         // assert
         var matches = Regex.Matches(sentence, "[a-zA-Z]+");
         Assert.GreaterOrEqual(matches.Count, minimumWordCount);
      }

      [Test]
      public void Sentence_With_Exact_WordCount() {
         // arrange
         var randomWordCount = RandomNumber.Next(42);

         // act
         var sentence = Lorem.Sentence(randomWordCount, randomWordsToAdd: 0);

         // assert
         var matches = Regex.Matches(sentence, "[a-zA-Z]+");
         Assert.AreEqual(randomWordCount, matches.Count);
      }

      [Test]
      public void Sentence_With_Words_From_Supplementary_List() {
         // arrange
         RandomNumber.ResetSeed(42);
         var wordCount = 42;

         // act
         var sentence = Lorem.Sentence(wordCount, supplemental: true).ToLower();

         // assert
         var checkCount = 0;
         foreach(var word in sentence.ToWordList()) {
            if(!_jointWords.Contains(word)
               && _supplementalWordList.Contains(word)) {
               return;
            }
            checkCount++;
         }
         Assert.Greater(checkCount, 0);
         Assert.Fail("Sentence() does not consider supplementary words.");
      }

      [Test]
      public void Sentence_With_Invalid_WordCount() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Sentence(-1));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: wordCount", ex.Message);
      }

      [Test]
      public void Sentence_With_Invalid_RandomWordsToAdd() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Sentence(7, randomWordsToAdd: -1));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: randomWordsToAdd", ex.Message);
      }

      [Test]
      public void Sentence_With_Exact_WordCount_Of_Zero_Is_Empty_String() {
         // arrange

         // act
         var sentence = Lorem.Sentence(0, randomWordsToAdd: 0);

         // assert
         Assert.AreEqual(string.Empty, sentence);
      }

      [Test]
      public void Sentences_HappyDays() {
         // arrange
         const int sentenceCount = 7;

         // act
         var sentences = Lorem.Sentences(sentenceCount);

         // assert
         Assert.AreEqual(sentenceCount, sentences.Count());
         Assert.AreEqual(sentenceCount, sentences.Count(x => x.EndsWith(".")));
         Assert.AreEqual(sentenceCount, sentences.Count(x => x.Contains(" ")));
      }

      [Test]
      public void Sentences_With_Default_Value() {
         // arrange
         const int defaultValue = 3;

         // act
         var sentences = Lorem.Sentences();

         // assert
         Assert.AreEqual(defaultValue, sentences.Count());
      }

      [Test]
      public void Sentences_With_Words_From_Supplementary_List() {
         // arrange

         // act
         var sentences = Lorem.Sentences(42, true);

         // assert
         var checkCount = 0;
         foreach(var sentence in sentences) {
            foreach(var word in sentence.ToWordList()) {
               if(_supplementalWordList.Contains(word)
                  && !_jointWords.Contains(word) ) {
                  return;
               }
               checkCount++;
            }
         }
         Assert.Greater(checkCount, 0);
         Assert.Fail("Sentences() does not make use of supplementary list.");
      }

      [Test]
      public void Sentences_With_Invalid_SentenceCount() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Sentences(-1));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: sentenceCount", ex.Message);
      }

      [Test]
      public void Sentences_Zero_Is_Valid_For_SentenceCount() {
         // arrange

         // act
         var sentences = Lorem.Sentences(0);

         // assert
         Assert.AreEqual(0, sentences.Count());
      }

      [Test]
      public void Paragraph_HappyDays() {
         // arrange

         // act
         var paragraph = Lorem.Paragraph(/* 3 is default */);

         // assert
         Assert.AreEqual(3, paragraph.Count(c => c == '.'));
         Assert.AreEqual(8, paragraph.Count(c => c == ' '));
      }

      [Test]
      public void Paragraph_NonDefault_Value() {
         // arrange
         const int sentenceCount = 42;

         // act
         var paragraph = Lorem.Paragraph(sentenceCount);

         // assert
         Assert.GreaterOrEqual(paragraph.Count(c => c == '.'), sentenceCount);
         foreach(var word in paragraph.ToWordList()) {
            if(_supplementalWordList.Contains(word)) {
               continue;
            }
            Assert.IsTrue(_wordList.Contains(word));
            Assert.IsFalse(_supplementalWordList.Contains(word));
         }
      }

      [Test]
      public void Paragraph_SentenceCount_Zero_Returns_Empty_Paragraph() {
         // arrange

         // act
         var paragraph = Lorem.Paragraph(0);

         // assert
         Assert.AreEqual(string.Empty, paragraph);
      }

      [Test]
      public void Paragraph_Invalid_SentenceCount() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Paragraph(-1));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: sentenceCount", ex.Message);
      }

      [Test]
      public void Paragraph_Use_Supplementary_List() {
         // arrange

         // act
         var paragraph = Lorem.Paragraph(supplemental: true);

         // assert
         var checkCount = 0;
         foreach(var word in paragraph.ToWordList()) {
            checkCount++;
            if (!_jointWords.Contains(word)
               && _supplementalWordList.Contains(word)) {
               return;
            }
         }
         Assert.AreEqual(checkCount, paragraph.ToWordList().Count());
         Assert.Fail("Paragraph() does not consider supplementary list.");
      }

      [Test]
      public void Paragraph_With_Invalid_RandomSentencesToAdd() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Paragraph(randomSentencesToAdd: -1));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: randomSentencesToAdd", ex.Message);
      }

      [Test]
      public void Paragraphs_HappyDays() {
         // arrange
         const int paragraphCount = 7;

         // act
         var paragraphs = Lorem.Paragraphs(paragraphCount);

         // assert
         Assert.AreEqual(paragraphCount, paragraphs.Count());
         Assert.AreEqual(paragraphCount, paragraphs.Count(x => x.EndsWith(".")));
         Assert.AreEqual(paragraphCount, paragraphs.Count(x => x.Contains(" ")));
      }

      [Test]
      public void Paragraphs_With_Zero() {
         // arrange

         // act
         var paragraph = Lorem.Paragraphs(0);

         // assert
         Assert.AreEqual(0, paragraph.Count());
      }

      [Test]
      public void Paragraphs_With_DefaultValues() {
         // arrange

         // act
         var paragraphs = Lorem.Paragraphs();

         // assert
         Assert.AreEqual(3, paragraphs.Count());
      }

      [Test]
      public void Paragraphs_With_Invalid_ParagraphCount() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Paragraphs(-1));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: paragraphCount", ex.Message);
      }

      [Test]
      public void Paragraphs_With_Supplemental() {
         // arrange

         // act
         var paragraphs = Lorem.Paragraphs(supplemental: true);

         // assert
         var checkCount = 0;
         foreach(var paragraph in paragraphs) {
            foreach(var word in paragraph.ToWordList()) {
               checkCount++;
               if(!_jointWords.Contains(word)
                  && _supplementalWordList.Contains(word)) {
                  return;
               }
            }
         }
         Assert.Greater(checkCount, paragraphs.Count());
         Assert.Fail("Paragraphs() doesn't consider supplementary list.");
      }

      [Test]
      public void Paragraphs_Without_Supplemental() {
         // arrange

         // act
         var paragraphs = Lorem.Paragraphs(supplemental: false);

         // assert
         var checkCount = 0;
         foreach (var paragraph in paragraphs) {
            foreach (var word in paragraph.ToWordList()) {
               checkCount++;
               if (!_jointWords.Contains(word)
                  && _supplementalWordList.Contains(word)) {
                  Assert.Fail("Paragraphs() shouldn't consider supplementary list.");
               }
            }
         }
         Assert.Greater(checkCount, paragraphs.Count());
      }

      [Test]
      public void ParagraphByChars_Default_Values() {
         // arrange
         const int defaultChars = 256 + 1; // +1 for period

         // act
         var paragraph = Lorem.ParagraphByChars();

         // assert
         Assert.AreEqual(defaultChars, paragraph.Length);
         var checkCount = 0;
         foreach(var word in paragraph.ToWordList()) {
            checkCount++;
            Assert.IsFalse(_supplementalWordList.Contains(word));
         }
         Assert.Greater(checkCount, 10);
      }

      [Test]
      public void ParagraphByChars_With_NonDefault_Chars() {
         // arrange

         // act
         var paragraph = Lorem.ParagraphByChars(42);

         // assert
         Assert.AreEqual(42 + 1, paragraph.Length);
      }

      [Test]
      public void ParagraphByChars_Zero_Returns_Empty_String() {
         // arrange

         // act
         var paragraph = Lorem.ParagraphByChars(0);

         // assert
         Assert.AreEqual(string.Empty, paragraph);
      }

      [Test]
      public void ParagraphByChars_Does_Not_End_With_Space_Dot() {
         // arrange
         RandomNumber.ResetSeed(42);
         var maxWordLength = 0;
         _wordList.All(x => { maxWordLength = Math.Max(maxWordLength, x.Length); return true; });
         _supplementalWordList.All(x => { maxWordLength = Math.Max(maxWordLength, x.Length); return true; });

         // act
         var startValue = 50;
         var charCount = startValue;
         while(charCount++ <= startValue + maxWordLength) {
            var paragraph = Lorem.ParagraphByChars(43);

         // assert
            Assert.IsFalse(paragraph.EndsWith(" ."));
         }
      }

      [Test]
      public void ParagraphByChars_Invalid_Chars_Value() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.ParagraphByChars(-1));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: chars", ex.Message);
      }

      [Test]
      public void ParagraphByChars_Without_Supplementary_List() {
         // arrange

         // act
         var paragraph = Lorem.ParagraphByChars(supplemental: false);

         // assert
         var checkCount = 0;
         foreach(var word in paragraph.ToWordList()) {
            checkCount++;
            if(paragraph.ToLower().EndsWith(word + ".")) {
               // don't check last word as it might be truncated or padded
               return;
            }
            // but check all others
            Assert.IsTrue(_wordList.Contains(word));
         }
         Assert.AreEqual(checkCount, paragraph.ToWordList());
      }

      [Test]
      public void ParagraphByChars_With_Supplementary_List() {
         // arrange

         // act
         var paragraph = Lorem.ParagraphByChars(supplemental: true);

         // assert
         foreach(var word in paragraph.ToWordList()) {
            if(!_jointWords.Contains(word)
               && _supplementalWordList.Contains(word)) {
               return;
            }
         }
         Assert.Fail("ParagraphByChars() does not consider supplementary list.");
      }

      [Test]
      public void Question_With_Default_Values() {
         // arrange

         // act
         var question = Lorem.Question();

         // assert
         Assert.AreEqual(4, question.Split(' ').Count());
         Assert.IsTrue(question.EndsWith("?"));
      }

      [Test]
      public void Question_With_Zero_Length_Returns_Empty_String() {
         // arrange

         // act
         var question = Lorem.Question(0);

         // assert
         Assert.AreEqual(string.Empty, question);
      }

      [Test]
      public void Question_Without_Supplementary_List() {
         // arrange
         const int wordCount = 50;

         // act
         var question = Lorem.Question(wordCount);

         // assert
         var checkCount = 0;
         foreach(var word in question.ToWordList()) {
            checkCount++;
            Assert.IsTrue(_wordList.Contains(word));
         }
         Assert.AreEqual(wordCount, checkCount);
      }

      [Test]
      public void Question_With_Supplementary_List() {
         // arrange
         const int wordCount = 50;

         // act
         var question = Lorem.Question(wordCount, supplemental: true);

         // assert
         var checkCount = 0;
         foreach (var word in question.ToWordList()) {
            checkCount++;
            if(!_jointWords.Contains(word)
               && _supplementalWordList.Contains(word)) {
               return;
            }
         }
         Assert.AreEqual(wordCount, checkCount);
         Assert.Fail("Question() does not consider supplementary words.");
      }

      [Test]
      public void Question_With_Invalid_WordCount() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Question(wordCount: -1));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: wordCount", ex.Message);
      }

      [Test]
      public void Question_With_Invalid_RandomWordsToAdd() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Question(wordCount: -1));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: wordCount", ex.Message);
      }

      /// <summary>
      /// Words in the standard list. Duplicates removed.
      /// </summary>
      private readonly string[] _wordList;

      /// <summary>
      /// Words in the supplementary list. Duplicates are removed.
      /// </summary>
      private readonly string[] _supplementalWordList;
      
      /// <summary>
      /// Words that are in both, the base list and the supplementary list.
      /// </summary>
      private readonly string[] _jointWords;
   }
}
