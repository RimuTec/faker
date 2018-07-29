using NUnit.Framework;
using RimuTec.Faker.Tests.Extensions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class LoremTests : FixtureBase
   {
      [OneTimeSetUp]
      public void FixtureSetUp()
      {
      }

      [SetUp]
      public void SetUp()
      {
         RandomNumber.ResetSeed(42);
      }

      [Test]
      public void Character_HappyDays()
      {
         var character = Lorem.Character();
         Assert.AreEqual(1, character.Length);
      }

      [Test]
      public void Characters_With_Default_Value()
      {
         var characters = Lorem.Characters();
         Assert.AreEqual(255, characters.Length);
         Assert.AreEqual(0, characters.Count(c => c == ' '));
      }

      [Test]
      public void Characters_With_Random_CharCount()
      {
         var charCount = RandomNumber.Next(42, 84);
         var characters = Lorem.Characters(charCount);
         Assert.AreEqual(charCount, characters.Length);
         Assert.AreEqual(0, characters.Count(c => c == ' '));
      }

      [Test]
      public void Characters_With_Invalid_CharCount()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Characters(-1));
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: charCount", ex.Message);
      }

      [Test]
      public void Characters_Returns_Empty_String_When_CharCount_Zero()
      {
         var characters = Lorem.Characters(0);
         Assert.AreEqual(string.Empty, characters);
      }

      [Test]
      public void Word_HappyDays()
      {
         var word = Lorem.Word();
         Assert.AreEqual("libero", word);
      }

      [Test]
      public void Word_Twice_NotEqual()
      {
         var word1 = Lorem.Word();
         var word2 = Lorem.Word();
         Assert.AreNotEqual(word1, word2);
      }

      [Test]
      public void Words_HappyDays()
      {
         var words = Lorem.Words();
         Assert.AreEqual(3, words.Count());
         Assert.AreEqual("libero qui consequuntur", string.Join(" ", words));
         Assert.AreEqual(0, words.Count(x => string.IsNullOrWhiteSpace(x)));
      }

      [Test]
      public void Words_With_WordCount()
      {
         const int wordCount = 42;
         var words = Lorem.Words(wordCount);
         Assert.AreEqual(wordCount, words.Count());
         Assert.AreEqual(0, words.Count(x => string.IsNullOrWhiteSpace(x)));
         foreach (var word in words)
         {
            if (JointWords.Contains(word))
            {
               continue;
            }
            Assert.True(WordList.Contains(word));
            Assert.False(SupplementalWordList.Contains(word));
         }
      }

      [Test]
      public void Words_With_Invalid_WordCount()
      {
         var invalidWordCount = -1;
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Words(invalidWordCount));
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: wordCount", ex.Message);
      }

      [Test]
      public void Words_With_WordCount_Zero_Returns_Empty_Enumerable()
      {
         var words = Lorem.Words(0);
         Assert.AreEqual(0, words.Count());
      }

      [Test]
      public void Words_With_Supplemental()
      {
         const int defaultWordCount = 3;
         var words = Lorem.Words(supplemental: true);
         Assert.AreEqual(defaultWordCount, words.Count());
         Assert.AreEqual("consequuntur argentum canonicus", string.Join(" ", words));
      }

      [Test]
      public void Words_Uses_Words_From_Supplementary_List()
      {
         var words = Lorem.Words(42, supplemental: true);
         foreach (var word in words)
         {
            if (SupplementalWordList.Contains(word)
               || !JointWords.Contains(word))
            {
               return;
            }
         }
         Assert.Fail("Words() does not consider supplementary words.");
      }

      [Test]
      public void Words_Uses_Words_From_Basic_List_Only()
      {
         var words = Lorem.Words(WordList.Count(), supplemental: false);
         foreach (var word in words)
         {
            Assert.IsTrue(WordList.Contains(word));
         }
      }

      [Test]
      public void Words_Twice_Are_Different()
      {
         var words1 = Lorem.Words();
         var words2 = Lorem.Words();
         Assert.AreNotEqual(words1, words2);
      }

      [Test]
      public void Words_WithSupplementalWords_Twice_Are_Different()
      {
         var words1 = Lorem.Words(supplemental: true);
         var words2 = Lorem.Words(supplemental: true);
         Assert.AreNotEqual(words1, words2);
      }

      [Test]
      public void Multibyte_HappyDays()
      {
         var multibyte = Lorem.Multibyte();
         Assert.AreEqual(1, multibyte.Length);
         var withoutNonAscii = Regex.Replace(multibyte, @"[^\u0000-\u007F]", string.Empty);
         Assert.AreEqual(0, withoutNonAscii.Length);
      }

      [Test]
      public void Sentence_HappyDays()
      {
         var sentence = Lorem.Sentence(/* 4 is default */);
         Assert.IsTrue(sentence.EndsWith("."));
         Assert.AreEqual(0, Regex.Match(sentence, "^[A-Z]").Index); // first letter to be upper case
         var matches = Regex.Matches(sentence, "[a-z]+");
         Assert.GreaterOrEqual(matches.Count, 4);
         Assert.GreaterOrEqual(sentence.Count(c => c == ' '), 3);
      }

      [Test]
      public void Sentence_Non_Default_Value()
      {
         const int minimumWordCount = 42;
         var sentence = Lorem.Sentence(minimumWordCount);
         var matches = Regex.Matches(sentence, "[a-zA-Z]+");
         Assert.GreaterOrEqual(matches.Count, minimumWordCount);
      }

      [Test]
      public void Sentence_With_Exact_WordCount()
      {
         var randomWordCount = RandomNumber.Next(42);
         var sentence = Lorem.Sentence(randomWordCount, randomWordsToAdd: 0);
         var matches = Regex.Matches(sentence, "[a-zA-Z]+");
         Assert.AreEqual(randomWordCount, matches.Count);
      }

      [Test]
      public void Sentence_With_Words_From_Supplementary_List()
      {
         RandomNumber.ResetSeed(42);
         var wordCount = 42;
         var sentence = Lorem.Sentence(wordCount, supplemental: true).ToLower();
         var checkCount = 0;
         foreach (var word in sentence.ToWordList())
         {
            if (!JointWords.Contains(word)
               && SupplementalWordList.Contains(word))
            {
               return;
            }
            checkCount++;
         }
         Assert.Greater(checkCount, 0);
         Assert.Fail("Sentence() does not consider supplementary words.");
      }

      [Test]
      public void Sentence_With_Invalid_WordCount()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Sentence(-1));
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: wordCount", ex.Message);
      }

      [Test]
      public void Sentence_With_Invalid_RandomWordsToAdd()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Sentence(7, randomWordsToAdd: -1));
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: randomWordsToAdd", ex.Message);
      }

      [Test]
      public void Sentence_With_Exact_WordCount_Of_Zero_Is_Empty_String()
      {
         var sentence = Lorem.Sentence(0, randomWordsToAdd: 0);
         Assert.AreEqual(string.Empty, sentence);
      }

      [Test]
      public void Sentences_HappyDays()
      {
         const int sentenceCount = 7;
         var sentences = Lorem.Sentences(sentenceCount);
         Assert.AreEqual(sentenceCount, sentences.Count());
         Assert.AreEqual(sentenceCount, sentences.Count(x => x.EndsWith(".")));
         Assert.AreEqual(sentenceCount, sentences.Count(x => x.Contains(" ")));
      }

      [Test]
      public void Sentences_With_Default_Value()
      {
         const int defaultValue = 3;
         var sentences = Lorem.Sentences();
         Assert.AreEqual(defaultValue, sentences.Count());
      }

      [Test]
      public void Sentences_With_Words_From_Supplementary_List()
      {
         var sentences = Lorem.Sentences(42, true);
         var checkCount = 0;
         foreach (var sentence in sentences)
         {
            foreach (var word in sentence.ToWordList())
            {
               if (SupplementalWordList.Contains(word)
                  && !JointWords.Contains(word))
               {
                  return;
               }
               checkCount++;
            }
         }
         Assert.Greater(checkCount, 0);
         Assert.Fail("Sentences() does not make use of supplementary list.");
      }

      [Test]
      public void Sentences_With_Invalid_SentenceCount()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Sentences(-1));
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: sentenceCount", ex.Message);
      }

      [Test]
      public void Sentences_Zero_Is_Valid_For_SentenceCount()
      {
         var sentences = Lorem.Sentences(0);
         Assert.AreEqual(0, sentences.Count());
      }

      [Test]
      public void Paragraph_HappyDays()
      {
         var paragraph = Lorem.Paragraph(/* 3 is default */);
         Assert.AreEqual(3, paragraph.Count(c => c == '.'));
         Assert.AreEqual(8, paragraph.Count(c => c == ' '));
      }

      [Test]
      public void Paragraph_NonDefault_Value()
      {
         const int sentenceCount = 42;
         var paragraph = Lorem.Paragraph(sentenceCount);
         Assert.GreaterOrEqual(paragraph.Count(c => c == '.'), sentenceCount);
         foreach (var word in paragraph.ToWordList())
         {
            if (SupplementalWordList.Contains(word))
            {
               continue;
            }
            Assert.IsTrue(WordList.Contains(word));
            Assert.IsFalse(SupplementalWordList.Contains(word));
         }
      }

      [Test]
      public void Paragraph_SentenceCount_Zero_Returns_Empty_Paragraph()
      {
         var paragraph = Lorem.Paragraph(0);
         Assert.AreEqual(string.Empty, paragraph);
      }

      [Test]
      public void Paragraph_Invalid_SentenceCount()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Paragraph(-1));
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: sentenceCount", ex.Message);
      }

      [Test]
      public void Paragraph_Use_Supplementary_List()
      {
         var paragraph = Lorem.Paragraph(supplemental: true);
         var checkCount = 0;
         foreach (var word in paragraph.ToWordList())
         {
            checkCount++;
            if (!JointWords.Contains(word)
               && SupplementalWordList.Contains(word))
            {
               return;
            }
         }
         Assert.AreEqual(checkCount, paragraph.ToWordList().Count());
         Assert.Fail("Paragraph() does not consider supplementary list.");
      }

      [Test]
      public void Paragraph_With_Invalid_RandomSentencesToAdd()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Paragraph(randomSentencesToAdd: -1));
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: randomSentencesToAdd", ex.Message);
      }

      [Test]
      public void Paragraphs_HappyDays()
      {
         const int paragraphCount = 7;
         var paragraphs = Lorem.Paragraphs(paragraphCount);
         Assert.AreEqual(paragraphCount, paragraphs.Count());
         Assert.AreEqual(paragraphCount, paragraphs.Count(x => x.EndsWith(".")));
         Assert.AreEqual(paragraphCount, paragraphs.Count(x => x.Contains(" ")));
      }

      [Test]
      public void Paragraphs_With_Zero()
      {
         var paragraph = Lorem.Paragraphs(0);
         Assert.AreEqual(0, paragraph.Count());
      }

      [Test]
      public void Paragraphs_With_DefaultValues()
      {
         var paragraphs = Lorem.Paragraphs();
         Assert.AreEqual(3, paragraphs.Count());
      }

      [Test]
      public void Paragraphs_With_Invalid_ParagraphCount()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Paragraphs(-1));
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: paragraphCount", ex.Message);
      }

      [Test]
      public void Paragraphs_With_Supplemental()
      {
         var paragraphs = Lorem.Paragraphs(supplemental: true);
         var checkCount = 0;
         foreach (var paragraph in paragraphs)
         {
            foreach (var word in paragraph.ToWordList())
            {
               checkCount++;
               if (!JointWords.Contains(word)
                  && SupplementalWordList.Contains(word))
               {
                  return;
               }
            }
         }
         Assert.Greater(checkCount, paragraphs.Count());
         Assert.Fail("Paragraphs() doesn't consider supplementary list.");
      }

      [Test]
      public void Paragraphs_Without_Supplemental()
      {
         var paragraphs = Lorem.Paragraphs(supplemental: false);
         var checkCount = 0;
         foreach (var paragraph in paragraphs)
         {
            foreach (var word in paragraph.ToWordList())
            {
               checkCount++;
               if (!JointWords.Contains(word)
                  && SupplementalWordList.Contains(word))
               {
                  Assert.Fail("Paragraphs() shouldn't consider supplementary list.");
               }
            }
         }
         Assert.Greater(checkCount, paragraphs.Count());
      }

      [Test]
      public void ParagraphByChars_Default_Values()
      {
         const int defaultChars = 256 + 1; // +1 for period
         var paragraph = Lorem.ParagraphByChars();
         Assert.AreEqual(defaultChars, paragraph.Length);
         var checkCount = 0;
         foreach (var word in paragraph.ToWordList())
         {
            checkCount++;
            Assert.IsFalse(SupplementalWordList.Contains(word));
         }
         Assert.Greater(checkCount, 10);
      }

      [Test]
      public void ParagraphByChars_With_NonDefault_Chars()
      {
         var paragraph = Lorem.ParagraphByChars(42);
         Assert.AreEqual(42 + 1, paragraph.Length);
      }

      [Test]
      public void ParagraphByChars_Zero_Returns_Empty_String()
      {
         var paragraph = Lorem.ParagraphByChars(0);
         Assert.AreEqual(string.Empty, paragraph);
      }

      [Test]
      public void ParagraphByChars_Does_Not_End_With_Space_Dot()
      {
         RandomNumber.ResetSeed(42);
         var maxWordLength = 0;
         WordList.All(x => { maxWordLength = Math.Max(maxWordLength, x.Length); return true; });
         SupplementalWordList.All(x => { maxWordLength = Math.Max(maxWordLength, x.Length); return true; });
         var startValue = 50;
         var charCount = startValue;
         while (charCount++ <= startValue + maxWordLength)
         {
            var paragraph = Lorem.ParagraphByChars(43);
            Assert.IsFalse(paragraph.EndsWith(" ."));
         }
      }

      [Test]
      public void ParagraphByChars_Invalid_Chars_Value()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.ParagraphByChars(-1));
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: chars", ex.Message);
      }

      [Test]
      public void ParagraphByChars_Without_Supplementary_List()
      {
         var paragraph = Lorem.ParagraphByChars(supplemental: false);
         var checkCount = 0;
         foreach (var word in paragraph.ToWordList())
         {
            checkCount++;
            if (paragraph.ToLower().EndsWith(word + "."))
            {
               // don't check last word as it might be truncated or padded
               return;
            }
            // but check all others
            Assert.IsTrue(WordList.Contains(word));
         }
         Assert.AreEqual(checkCount, paragraph.ToWordList());
      }

      [Test]
      public void ParagraphByChars_With_Supplementary_List()
      {
         var paragraph = Lorem.ParagraphByChars(supplemental: true);
         foreach (var word in paragraph.ToWordList())
         {
            if (!JointWords.Contains(word)
               && SupplementalWordList.Contains(word))
            {
               return;
            }
         }
         Assert.Fail("ParagraphByChars() does not consider supplementary list.");
      }

      [Test]
      public void Question_With_Default_Values()
      {
         var question = Lorem.Question();
         Assert.AreEqual(4, question.Split(' ').Count());
         Assert.IsTrue(question.EndsWith("?"));
      }

      [Test]
      public void Question_With_Zero_Length_Returns_Empty_String()
      {
         var question = Lorem.Question(0);
         Assert.AreEqual(string.Empty, question);
      }

      [Test]
      public void Question_Without_Supplementary_List()
      {
         const int wordCount = 50;
         var question = Lorem.Question(wordCount);
         var checkCount = 0;
         foreach (var word in question.ToWordList())
         {
            checkCount++;
            Assert.IsTrue(WordList.Contains(word));
         }
         Assert.AreEqual(wordCount, checkCount);
      }

      [Test]
      public void Question_With_Supplementary_List()
      {
         const int wordCount = 50;
         var question = Lorem.Question(wordCount, supplemental: true);
         var checkCount = 0;
         foreach (var word in question.ToWordList())
         {
            checkCount++;
            if (!JointWords.Contains(word)
               && SupplementalWordList.Contains(word))
            {
               return;
            }
         }
         Assert.AreEqual(wordCount, checkCount);
         Assert.Fail("Question() does not consider supplementary words.");
      }

      [Test]
      public void Question_With_Invalid_WordCount()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Question(wordCount: -1));
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: wordCount", ex.Message);
      }

      [Test]
      public void Question_With_Invalid_RandomWordsToAdd()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Question(wordCount: -1));
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: wordCount", ex.Message);
      }

      [Test]
      public void Questions_With_Default_Values()
      {
         var questions = Lorem.Questions();
         Assert.AreEqual(3, questions.Count());
      }

      [Test]
      public void Questions_No_Supplementary_Words()
      {
         var questions = Lorem.Questions(supplemental: false);
         var checkCount = 0;
         foreach (var question in questions)
         {
            foreach (var word in question.ToWordList())
            {
               checkCount++;
               Assert.IsTrue(WordList.Contains(word));
            }
         }
      }

      [Test]
      public void Questions_With_Words_From_Supplementary_List()
      {
         var questions = Lorem.Questions(supplemental: true);
         var supplementalWordCount = 0;
         foreach (var question in questions)
         {
            foreach (var word in question.ToWordList())
            {
               if (!JointWords.Contains(word)
                  && SupplementalWordList.Contains(word))
               {
                  supplementalWordCount++;
               }
            }
         }
         Assert.Greater(supplementalWordCount, 0);
      }

      [Test]
      public void Questions_With_QuestionCount()
      {
         const int questionCount = 7;
         var questions = Lorem.Questions(questionCount);
         Assert.AreEqual(questionCount, questions.Count());
      }

      [Test]
      public void Questions_With_MinimumValue()
      {
         var questions = Lorem.Questions(0);
         Assert.AreEqual(0, questions.Count());
         ;
      }

      [Test]
      public void Questions_With_Invalid_QuestionCount()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Questions(-1));
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: questionCount", ex.Message);
      }

      /// <summary>
      /// Words in the standard list. Duplicates removed.
      /// </summary>
      public string[] WordList { get {
            if(_wordList == null)
            {
               var wordList = Fetch("lorem.words");
               _wordList = wordList.Distinct().ToArray();
            }
            return _wordList;
         }
      }


      /// <summary>
      /// Words in the supplementary list. Duplicates are removed.
      /// </summary>
      public string[] SupplementalWordList
      {
         get
         {
            if (_supplementalWordList == null)
            {
               var supplementaryList = Fetch("lorem.supplemental");
               _supplementalWordList = supplementaryList.Distinct().ToArray();
            }
            return _supplementalWordList;
         }
      }

      /// <summary>
      /// Words that are in both, the base list and the supplementary list.
      /// </summary>
      public string[] JointWords { get {
            if (_jointWords == null)
            {
               var intersection = WordList.Intersect(SupplementalWordList);
               _jointWords = intersection.Distinct().ToArray();
            }
            return _jointWords;
         }
      }

      private string[] _supplementalWordList;
      private string[] _wordList;
      private string[] _jointWords;
   }
}
