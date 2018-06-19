using RimuTec.Faker.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RimuTec.Faker {
   public static class Lorem {
      static Lorem() {
         var executingAssembly = Assembly.GetExecutingAssembly();
         var resourceNames = executingAssembly.GetManifestResourceNames();
         using (var resourceStream = executingAssembly.GetManifestResourceStream("RimuTec.Faker.locales.en.lorem.yml")) {
            using (var textReader = new StreamReader(resourceStream)) {
               var deserializer = new DeserializerBuilder()
                  .WithNamingConvention(new CamelCaseNamingConvention())
                  .Build()
                  ;
               _locale = deserializer.Deserialize<locale>(textReader.ReadToEnd());
            }
         }
      }

      /// <summary>
      /// Get a random collection of words.
      /// </summary>
      /// <param name="count">Number of words required</param>
      /// <returns></returns>
      public static IEnumerable<string> Words(int count) {
         if (count <= 0) {
            throw new ArgumentException($"'{nameof(count)}' must be greater than zero", nameof(count));
         }

         return count.Times(x => _locale.en.faker.lorem.Words.Random());
      }

      /// <summary>
      /// Get the first word of the random word collection. Useful for unit tests.
      /// </summary>
      /// <returns></returns>
      public static string GetFirstWord() {
         return _locale.en.faker.lorem.Words.First();
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

      private static locale _locale;

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
         public string[] Seniority { get; set; }

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
