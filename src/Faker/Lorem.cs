using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
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
      /// Get a random collection of Lorem words or a random collection that includes words from a supplementary list of Lorem-like words.
      /// </summary>
      /// <param name="wordCount">Number of words required</param>
      /// <param name="supplemental">If true, include words from supplementary list of Lorem-like words.</param>
      /// <returns></returns>
      public static IEnumerable<string> Words(int wordCount = 3, bool supplemental = false) {
         if (wordCount < 1) {
            throw new ArgumentOutOfRangeException(nameof(wordCount), "Must be greater than zero.");
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
      /// Returns a random multibyte character. Example: 😀
      /// </summary>
      /// <returns></returns>
      public static char Multibyte() {
         return En.Multibyte();
      }

      /// <summary>
      /// Generates a capitalised sentence of random words.
      /// </summary>
      /// <param name="minWordCount">Minimum number of words required</param>
      /// <returns></returns>
      public static string Sentence(int minWordCount = 4) {
         if (minWordCount <= 0) {
            throw new ArgumentException($"'{nameof(minWordCount)}' must be greater than zero", nameof(minWordCount));
         }

         return string.Join(" ", Words(minWordCount + RandomNumber.Next(6)).ToArray()).Capitalise() + ".";
      }

      public static IEnumerable<string> Sentences(int sentenceCount) {
         if (sentenceCount <= 0) {
            throw new ArgumentException($"'{nameof(sentenceCount)}' must be greater than zero", nameof(sentenceCount));
         }

         return sentenceCount.Times(x => Sentence());
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
