using NUnit.Framework;
using RimuTec.Faker.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(DefaultFixtureData), nameof(DefaultFixtureData.FixtureParams))]
   public class LoremTests : FixtureBase
   {
      public LoremTests(string locale)
      {
         Locale = locale;
      }

      [SetUp]
      public void SetUp()
      {
         Config.Locale = Locale;
      }

      private string Locale { get; }

      [OneTimeSetUp]
      public void FixtureSetUp()
      {
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
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'charCount')", ex.Message);
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
         Assert.False(word.Length == 0);
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
            Assert.True(WordList.Contains(word),
               $"Locale '{Locale}'. Word missing from word list '{word}'"
            );
            Assert.False(SupplementalWordList.Contains(word));
         }
      }

      [Test]
      public void Words_With_Invalid_WordCount()
      {
         const int invalidWordCount = -1;
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Words(invalidWordCount));
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'wordCount')", ex.Message);
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
         var words = Lorem.Words(WordList.Length, supplemental: false);
         foreach (var word in words)
         {
            Assert.IsTrue(WordList.Contains(word),
               $"Locale '{Locale}'. Word missing from word list '{word}'"
            );
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
         Assert.IsTrue(sentence.EndsWith(Lorem.PunctuationPeriod()));
         Assert.AreEqual(0, Regex.Match(sentence, "^[A-Z]").Index); // first letter to be upper case
         var words = sentence.Split(Lorem.PunctuationSpace());
         Assert.GreaterOrEqual(words.Length, 4);
         Assert.GreaterOrEqual(sentence.Count(c => c.ToString() == Lorem.PunctuationSpace()), 3);
      }

      [Test]
      public void Sentence_Non_Default_Value()
      {
         const int minimumWordCount = 42;
         var sentence = Lorem.Sentence(minimumWordCount);
         var words = sentence.Split(Lorem.PunctuationSpace());
         Assert.GreaterOrEqual(words.Length, minimumWordCount);
      }

      [Test]
      public void Sentence_With_Exact_WordCount()
      {
         var localeSpecificSpace = Lorem.PunctuationSpace();
         var randomWordCount = RandomNumber.Next(1, 42);
         var sentence = Lorem.Sentence(randomWordCount, randomWordsToAdd: 0);
         var words = sentence.Split(localeSpecificSpace.ToCharArray());
         Assert.AreEqual(randomWordCount, words.Length,
            $"Locale {Locale}. randomWordCount is '{randomWordCount}' Sentence is '{sentence}'"
         );
      }

      [Test]
      public void Sentence_With_Words_From_Supplementary_List()
      {
         RandomNumber.ResetSeed(42);
         const int wordCount = 42;
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
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'wordCount')", ex.Message);
      }

      [Test]
      public void Sentence_With_Invalid_RandomWordsToAdd()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Sentence(7, randomWordsToAdd: -1));
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'randomWordsToAdd')", ex.Message);
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
         Assert.AreEqual(sentenceCount, sentences.Count(x => x.EndsWith(Lorem.PunctuationPeriod())),
            $"Locale {Locale}"
         );
         Assert.AreEqual(sentenceCount, sentences.Count(x => x.Contains(Lorem.PunctuationSpace())));
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
               if (SupplementalWordList.Contains(word))
               {
                  return;
               }
               checkCount++;
            }
         }
         Assert.Greater(checkCount, 0);
         Assert.Fail($"Locale {Locale}. Sentences() does not make use of supplementary words.");
      }

      [Test]
      public void Sentences_With_Invalid_SentenceCount()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Sentences(-1));
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'sentenceCount')", ex.Message);
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
         Assert.AreEqual(3, paragraph.Count(c => c.ToString() == Lorem.PunctuationPeriod()));
         Assert.AreEqual(8, paragraph.Count(c => c.ToString() == Lorem.PunctuationSpace()));
      }

      [Test]
      public void Paragraph_NonDefault_Value()
      {
         const int sentenceCount = 42;
         var paragraph = Lorem.Paragraph(sentenceCount);
         Assert.GreaterOrEqual(paragraph.Count(c => c.ToString() == Lorem.PunctuationPeriod()), sentenceCount);
         var wordListLower = WordList.Select(w => w.ToLower());
         foreach (var word in paragraph.ToWordList())
         {
            if(paragraph.ToLower().Contains(word + Lorem.PunctuationPeriod()))
            {
               // Last word in sentence might be truncated or padded.
               continue;
            }
            Assert.IsTrue(wordListLower.Contains(word.ToLower()),
               $"Locale is '{Locale}'. Missing word is '{word}'. Paragraph is '{paragraph}'"
            );
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
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'sentenceCount')", ex.Message);
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
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'randomSentencesToAdd')", ex.Message);
      }

      [Test]
      public void Paragraphs_HappyDays()
      {
         const int paragraphCount = 7;
         var paragraphs = Lorem.Paragraphs(paragraphCount);
         Assert.AreEqual(paragraphCount, paragraphs.Count());
         Assert.AreEqual(paragraphCount, paragraphs.Count(x => x.EndsWith(Lorem.PunctuationPeriod())));
         Assert.AreEqual(paragraphCount, paragraphs.Count(x => x.Contains(Lorem.PunctuationSpace())));
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
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'paragraphCount')", ex.Message);
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
         //var supplementalInLower = SupplementalWordList.Select(w => w.ToLower());
         IEnumerable<string> wordList = paragraph.ToWordList();
         foreach (var word in wordList.Take(wordList.Count() - 1)) // last word may be truncated or padded
         {
            checkCount++;
            Assert.IsTrue(wordList.Contains(word),
               $"Locale '{Locale}'. Missing word is '{word}'. Paragraph is '{paragraph}'."
            );
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
         _ = WordList.All(x => { maxWordLength = Math.Max(maxWordLength, x.Length); return true; });
         _ = SupplementalWordList.All(x => { maxWordLength = Math.Max(maxWordLength, x.Length); return true; });
         const int startValue = 50;
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
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'chars')", ex.Message);
      }

      [Test]
      public void ParagraphByChars_Without_Supplementary_List()
      {
         var paragraph = Lorem.ParagraphByChars(supplemental: false);
         var checkCount = 0;
         var wordListAsLower = WordList.Select(w => w.ToLower());
         foreach (var word in paragraph.ToWordList())
         {
            if (paragraph.ToLower().EndsWith(word + Lorem.PunctuationPeriod()))
            {
               // don't check last word as it might be truncated or padded
               return;
            }
            // but check all others
            checkCount++;
            Assert.IsTrue(wordListAsLower.Contains(word),
               $"Locale {Locale}. word is '{word}'. paraggraph is '{paragraph}'"
            );
         }
         Assert.AreEqual(checkCount, paragraph.ToWordList().Count());
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
         Assert.AreEqual(4, question.Split(Lorem.PunctuationSpace()).Length);
         Assert.IsTrue(question.EndsWith(Lorem.PunctuationQuestionMark()));
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
            Assert.IsTrue(WordList.Contains(word),
               $"Locale '{Locale}'. Missing word is '{word}'"
            );
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
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'wordCount')", ex.Message);
      }

      [Test]
      public void Question_With_Invalid_RandomWordsToAdd()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Question(wordCount: -1));
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'wordCount')", ex.Message);
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
         var wordListLower = WordList.Select(w => w.ToLower());
         foreach (var question in questions)
         {
            foreach (var word in question.ToWordList())
            {
               checkCount++;
               Assert.IsTrue(wordListLower.Contains(word.ToLower()),
                  $"Locale '{Locale}'. Missing word is {word}. Question is '{question}'"
               );
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
      }

      [Test]
      public void Questions_With_Invalid_QuestionCount()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Questions(-1));
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'questionCount')", ex.Message);
      }

      /// <summary>
      /// Words in the standard list. Duplicates removed.
      /// </summary>
      public string[] WordList
      {
         get
         {
            if (_wordList == null)
            {
               var temp = new List<string>();
               foreach(var word in Lorem.FetchList("lorem.words"))
               {
                  temp.AddRange(word.Split(',').Select(w => w.Trim('.', ' ')));
               }
               _wordList = temp.ToArray();
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
               var temp = new List<string>();
               foreach(var word in Lorem.FetchList("lorem.supplemental"))
               {
                  temp.AddRange(word.Split(',').Select(w => w.Trim('.', ' ')));
               }
               _supplementalWordList = temp.ToArray();
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
