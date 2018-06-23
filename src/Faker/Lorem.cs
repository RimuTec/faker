using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;

namespace RimuTec.Faker {
   public static class Lorem {
      static Lorem() {
         const string yamlFileName = "RimuTec.Faker.locales.en.lorem.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _lorem = locale.en.faker.lorem;
      }

      /// <summary>
      /// Generates a random word. Example: "repellendus".
      /// </summary>
      /// <returns></returns>
      public static string Word() {
         return _lorem.Words.Random();
      }

      /// <summary>
      /// Generates a set of random Lorem words optionally considering words from a supplementary list of Lorem-like words.
      /// </summary>
      /// <param name="wordCount">Number of words required. Zero is a valid value.</param>
      /// <param name="supplemental">If true, include words from supplementary list of Lorem-like words.</param>
      /// <exception cref="ArgumentOutOfRangeException">If 'wordCount' is less than zero.</exception>
      /// <returns></returns>
      public static IEnumerable<string> Words(int wordCount = 3, bool supplemental = false) {
         if (wordCount < 0) {
            throw new ArgumentOutOfRangeException(nameof(wordCount), "Must be equal to or greater than zero.");
         }
         if(supplemental) {
            var combined = wordCount.Times(x => _lorem.Words.Random()).Concat(wordCount.Times(x => _lorem.Supplemental.Random()));
            return combined.Shuffle().Take(wordCount);
         }
         else {
            return wordCount.Times(x => _lorem.Words.Random());
         }
      }

      /// <summary>
      /// Returns a string with a single random multibyte character. Example: 😀
      /// </summary>
      /// <returns></returns>
      public static string Multibyte() {
         return new String(new char[] { En.Multibyte() });
      }

      /// <summary>
      /// Returns a string with a single random character from [0...9a...z]. Example: "y"
      /// </summary>
      /// <returns></returns>
      public static string Character() {
         return Characters(1);
      }

      /// <summary>
      /// Returns a string with 'charCount' random characters from [0...9a...z]. Example: Characters(11) returns "pprf5wrj85f".
      /// </summary>
      /// <param name="charCount">Number of characters. Default is 255. Zero is a valid valud and return an empty string.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">If 'charCount' is less than zero.</exception>
      /// <example><code>Lorem.Characters(11);</code> returns <code>"pprf5wrj85f"</code></example>
      public static string Characters(int charCount = 255) {
         if(charCount < 0) {
            throw new ArgumentOutOfRangeException(nameof(charCount), "Must be equal to or greater than zero.");
         }
         return string.Join(string.Empty, charCount.Times(x => _characters.Random()));
      }

      /// <summary>
      /// Generates a capitalised sentence of random words. To specify an exact word count for a 
      /// sentence, set wordCount to the number you want and randomWordsToAdd equal to 0. By 
      /// default (i.e. a call without any parameter values), sentences will have a random number 
      /// of words within the range (4..10).
      /// </summary>
      /// <param name="wordCount">Minimum number of words in the sentence. Default is 4.</param>
      /// <param name="supplemental">Flag to indicate whether to consider words from a supplementary list of Lorem-like words. Default is false.</param>
      /// <param name="randomWordsToAdd">The 'randomWordsToAdd' argument increases the sentence's word 
      /// count by a random value within the range (0..randomWordsToAdd). Default value is 6.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">If either 'wordCount' or 'randomWordsToAdd' is less than zero.</exception>
      public static string Sentence(int wordCount = 4, bool supplemental = false, int randomWordsToAdd = 6) {
         if (wordCount < 0) {
            throw new ArgumentOutOfRangeException(nameof(wordCount), "Must be equal to or greater than zero.");
         }
         if (randomWordsToAdd < 0) {
            throw new ArgumentOutOfRangeException(nameof(randomWordsToAdd), "Must be equal to or greater than zero.");
         }
         string sentence = string.Join(" ", Words(wordCount + RandomNumber.Next(randomWordsToAdd), supplemental).ToArray()).Capitalise();
         if(sentence.Length > 0) {
            sentence += ".";
         }
         return sentence;
      }

      /// <summary>
      /// Generates a set of random sentences, optionally considering words from a supplementary list of 
      /// Lorem-like words. Example: Sentences() returns array similar to "["Vero earum commodi soluta.", 
      /// "Quaerat fuga cumque et vero eveniet omnis ut.", "Cumque sit dolor ut est consequuntur."]"
      /// </summary>
      /// <param name="sentenceCount">Number of sentences. Zero is a valid value.</param>
      /// <param name="supplemental">Flag to indicate whether to consider words from a supplementary list of Lorem-like words. Default is false.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">If 'sentenceCount' is less than zero.</exception>
      public static IEnumerable<string> Sentences(int sentenceCount, bool supplemental = false) {
         if (sentenceCount < 0) {
            throw new ArgumentOutOfRangeException(nameof(sentenceCount), "Must be equal to or greater than zero.");
         }
         return sentenceCount.Times(x => Sentence(3, supplemental));
      }

      public static string Paragraph(int minSentenceCount = 3) {
         if (minSentenceCount <= 0) {
            throw new ArgumentException($"'{nameof(minSentenceCount)}' must be greater than zero", nameof(minSentenceCount));
         }

         return string.Join(" ", Sentences(minSentenceCount + RandomNumber.Next(3)).ToArray());
      }

      public static IEnumerable<string> Paragraphs(int paragraphCount) {
         if (paragraphCount <= 0) {
            throw new ArgumentException($"'{nameof(paragraphCount)}' must be greater than zero", nameof(paragraphCount));
         }
         return paragraphCount.Times(x => Paragraph());
      }

      internal static string[] _WordList => _lorem.Words;
      internal static string[] _SupplementalWordList => _lorem.Supplemental;

      private static readonly char[] _characters = "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();

      private static lorem _lorem;

#pragma warning disable IDE1006 // Naming Styles
      // Helper classes for reading the yaml file. Note that the class names are
      // intentionally lower case.

      internal class locale {
         public en en { get; set; }
      }

      internal class en {
         public faker faker { get; set; }
      }

      internal class faker {
         public lorem lorem { get; set; }
      }

      internal class lorem {
         [YamlMember(Alias = "words", ApplyNamingConventions = false)]
         public string[] Words { get; set; }

         [YamlMember(Alias = "supplemental", ApplyNamingConventions = false)]
         public string[] Supplemental { get; set; }

         [YamlMember(Alias = "punctuation", ApplyNamingConventions = false)]
         public punctuation Punctuation { get; set; }
      }

      internal class punctuation {
         [YamlMember(Alias = "space", ApplyNamingConventions = false)]
         public string Space { get; set; }

         [YamlMember(Alias = "period", ApplyNamingConventions = false)]
         public string Period { get; set; }

         [YamlMember(Alias = "question_mark", ApplyNamingConventions = false)]
         public string QuestionMark { get; set; }
      }

#pragma warning restore IDE1006 // Naming Styles
   }
}
