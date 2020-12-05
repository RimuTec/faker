using RimuTec.Faker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RimuTec.Faker
{
   /// <summary>
   /// Equivalent to Faker::Lorem in Ruby's Faker gem. In some cases their documentation appears to use different
   /// default values than what the code appears to use. In those cases we went for what the code indicates.
   /// </summary>
   public class Lorem : GeneratorBase<Lorem>
   {
      private Lorem() { }
      
      /// <summary>
      /// Generates a random word. Example: "repellendus".
      /// </summary>
      /// <returns></returns>
      public static string Word()
      {
         return Fetch("lorem.words");
      }

      /// <summary>
      /// Generates a set of random Lorem words optionally considering words from a supplementary list of Lorem-like words.
      /// </summary>
      /// <param name="wordCount">Number of words required. Zero is a valid value. Default value is 3.</param>
      /// <param name="supplemental">If true, include words from supplementary list of Lorem-like words. Default value is 'false'.</param>
      /// <exception cref="ArgumentOutOfRangeException">If 'wordCount' is less than zero.</exception>
      /// <returns></returns>
      public static IEnumerable<string> Words(int wordCount = 3, bool supplemental = false)
      {
         if (wordCount < 0)
         {
            throw new ArgumentOutOfRangeException(nameof(wordCount), "Must be equal to or greater than zero.");
         }
         var result = new List<string>();
         if (supplemental)
         {
            var combined = wordCount.Times(x => Word()).Concat(wordCount.Times(x => Fetch("lorem.supplemental")));
            result.AddRange(combined.Shuffle().Take(wordCount));
         }
         else
         {
            result.AddRange(wordCount.Times(x => Word()));
         }
         return result;
      }

      /// <summary>
      /// Returns a string with a single random multibyte character. Example: 😀
      /// </summary>
      /// <returns></returns>
      public static string Multibyte()
      {
         return new String(new char[] { En.Multibyte() });
      }

      /// <summary>
      /// Returns a string with a single random character from [0...9a...z]. Example: "y"
      /// </summary>
      /// <returns></returns>
      public static string Character()
      {
         return Characters(1);
      }

      /// <summary>
      /// Returns a string with 'charCount' random characters from [0...9a...z]. Example: Characters(11) returns "pprf5wrj85f".
      /// </summary>
      /// <param name="charCount">Number of characters. Default is 255. Zero is a valid valud and returns an empty string.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">If 'charCount' is less than zero.</exception>
      /// <example><code>Lorem.Characters(11);</code> returns <code>"pprf5wrj85f"</code></example>
      public static string Characters(int charCount = 255)
      {
         if (charCount < 0)
         {
            throw new ArgumentOutOfRangeException(nameof(charCount), "Must be equal to or greater than zero.");
         }
         return string.Join(string.Empty, charCount.Times(x => _characters.Sample()));
      }

      /// <summary>
      /// Generates a capitalised sentence of random words. To specify an exact word count for a 
      /// sentence, set wordCount to the number you want and randomWordsToAdd equal to 0. By 
      /// default (i.e. a call without any parameter values), sentences will have 4 words.
      /// </summary>
      /// <param name="wordCount">Minimum number of words in the sentence. Default is 4.</param>
      /// <param name="supplemental">Flag to indicate whether to consider words from a supplementary list of Lorem-like words. Default is false.</param>
      /// <param name="randomWordsToAdd">The 'randomWordsToAdd' argument increases the sentence's word 
      /// count by a random value within the range (0..randomWordsToAdd). Default value is 0.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">If either 'wordCount' or 'randomWordsToAdd' is less than zero.</exception>
      public static string Sentence(int wordCount = 4, bool supplemental = false, int randomWordsToAdd = 0)
      {
         if (wordCount < 0)
         {
            throw new ArgumentOutOfRangeException(nameof(wordCount), "Must be equal to or greater than zero.");
         }
         if (randomWordsToAdd < 0)
         {
            throw new ArgumentOutOfRangeException(nameof(randomWordsToAdd), "Must be equal to or greater than zero.");
         }
         string sentence = string.Join(Fetch("lorem.punctuation.space"), Words(wordCount + RandomNumber.Next(randomWordsToAdd), supplemental).ToArray()).Capitalise();
         if (sentence.Length > 0)
         {
            sentence += Fetch("lorem.punctuation.period");
         }
         return sentence;
      }

      /// <summary>
      /// Generates a set of random sentences, optionally considering words from a supplementary list of 
      /// Lorem-like words. Example: Sentences() returns array similar to "["Vero earum commodi soluta.", 
      /// "Quaerat fuga cumque et vero eveniet omnis ut.", "Cumque sit dolor ut est consequuntur."]"
      /// </summary>
      /// <param name="sentenceCount">Number of sentences. Zero is a valid value. Default value is 3.</param>
      /// <param name="supplemental">Flag to indicate whether to consider words from a supplementary list of Lorem-like words. Default is false.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">If 'sentenceCount' is less than zero.</exception>
      public static IEnumerable<string> Sentences(int sentenceCount = 3, bool supplemental = false)
      {
         if (sentenceCount < 0)
         {
            throw new ArgumentOutOfRangeException(nameof(sentenceCount), "Must be equal to or greater than zero.");
         }
         return sentenceCount.Times(x => Sentence(3, supplemental));
      }

      /// <summary>
      /// Generates a random paragraph, optionally considering words from a supplementary list of
      /// Lorem-like words. Example: Paragraph() returns a sentence similar to "Neque dicta enim quasi. 
      /// Qui corrupti est quisquam. Facere animi quod aut. Qui nulla consequuntur consectetur sapiente."
      /// </summary>
      /// <param name="sentenceCount">Number of sentences. Zero is a valid value. Default value is 3.</param>
      /// <param name="supplemental">Flag to indicate whether to consider words from a supplementary list of Lorem-like words. Default is false.</param>
      /// <param name="randomSentencesToAdd">Number of random sentences to add. Default value 0.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">If 'sentenceCount' or 'randomSentencesToAdd' is less than zero.</exception>
      public static string Paragraph(int sentenceCount = 3, bool supplemental = false, int randomSentencesToAdd = 0)
      {
         if (sentenceCount < 0)
         {
            throw new ArgumentOutOfRangeException(nameof(sentenceCount), "Must be equal to or greater than zero.");
         }
         if (randomSentencesToAdd < 0)
         {
            throw new ArgumentOutOfRangeException(nameof(randomSentencesToAdd), "Must be equal to or greater than zero.");
         }

         return string.Join(Fetch("lorem.punctuation.space"), Sentences(sentenceCount + RandomNumber.Next(randomSentencesToAdd), supplemental).ToArray());
      }

      /// <summary>
      /// Generates a collection of random paragraphs, optionally considering words from a supplementary
      /// list of Lorem-like words. Example: Paragraphs() returns something like: ["Dolores quis quia ad quo voluptates. 
      /// Maxime delectus totam numquam. Necessitatibus vel atque qui dolore.", "Id neque nemo. Dolores iusto facere est ad. 
      /// Accusamus ipsa dolor ut.", "Et officiis ut hic. Sunt asperiores minus distinctio debitis ipsa dolor. Minima eos deleniti."]
      /// </summary>
      /// <param name="paragraphCount">Number of paragraphs. Zero is a valid valie. Default value is 3.</param>
      /// <param name="supplemental">Flag to indicate whether to consider words from a supplementary list of Lorem-like words. Default is false.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">IF 'paragraphCount' is less than zero.</exception>
      public static IEnumerable<string> Paragraphs(int paragraphCount = 3, bool supplemental = false)
      {
         if (paragraphCount < 0)
         {
            throw new ArgumentOutOfRangeException(nameof(paragraphCount), "Must be equal to or greater than zero.");
         }
         return paragraphCount.Times(x => Paragraph(supplemental: supplemental));
      }

      /// <summary>
      /// Returns a paragraph with a specified amount of characters, optionally considering words
      /// from a supplementary list of Lorem-like words. If needed the last word may be truncated. 
      /// Example: ParagraphByChars() returns something similar to "Truffaut stumptown trust fund 8-bit 
      /// messenger bag portland. Meh kombucha selvage swag biodiesel. Lomo kinfolk jean shorts 
      /// asymmetrical diy. Wayfarers portland twee stumptown. Wes anderson biodiesel retro 90's pabst. 
      /// Diy echo 90's mixtape semiotics. Cornho."
      /// </summary>
      /// <param name="chars">Number of characters in the paragraph. Zero is a valid value. Default value is 256.</param>
      /// <param name="supplemental">Flag to indicate whether to consider words from a supplementary list of Lorem-like words. Default is false.</param>
      /// <returns></returns>
      /// <remarks>If the sentence would add in " ." then the space is replaced by a random letter. In other words that
      /// random letter is appended to the last word.</remarks>
      public static string ParagraphByChars(int chars = 256, bool supplemental = false)
      {
         if (chars < 0)
         {
            throw new ArgumentOutOfRangeException(nameof(chars), "Must be equal to or greater than zero.");
         }
         var paragraph = Paragraph(3, supplemental);
         while (paragraph.Length < chars)
         {
            paragraph += Fetch("lorem.punctuation.space") + Paragraph(3, supplemental);
         }
         paragraph = paragraph.Substring(0, chars);
         if (paragraph.EndsWith(Fetch("lorem.punctuation.space")))
         {
            // pad with random letter if paragraph would end in " ." otherwise
            paragraph = paragraph.Trim() + _characters[RandomNumber.Next(10, _characters.Length)];
         }
         if (!string.IsNullOrWhiteSpace(paragraph))
         {
            paragraph += Fetch("lorem.punctuation.period");
         }
         return paragraph;
      }

      /// <summary>
      /// Generates a question with random words, optionally considering words from a supplementary
      /// list of Lorem-like words. Example: Question() returns something similar to "Aliquid culpa 
      /// aut ipsam unde ullam labore?"
      /// </summary>
      /// <param name="wordCount">Number of words in the question. Zero is a valid value. Default value is 4</param>
      /// <param name="supplemental">Flag to indicate whether to consider words from a supplementary list of Lorem-like words. Default is false.</param>
      /// <param name="randomWordsToAdd">The 'randomWordsToAdd' argument increases the sentence's word 
      /// count by a random value within the range (0..randomWordsToAdd). Default value is 0.</param>
      /// <returns></returns>
      public static string Question(int wordCount = 4, bool supplemental = false, int randomWordsToAdd = 0)
      {
         string question = Sentence(wordCount, supplemental, randomWordsToAdd)
            .Trim(Fetch("lorem.punctuation.period").ToCharArray());
         if (!string.IsNullOrWhiteSpace(question))
         {
            question = question + Fetch("lorem.punctuation.question_mark");
         }
         return question;
      }

      /// <summary>
      /// Generates questions with random words, optionally considering words from a supplementary
      /// list of Lorem-like words. Example: Questions(3) returns something similar to ["Necessitatibus 
      /// deserunt animi?", "At hic dolores autem consequatur ut?", "Aliquam velit ex adipisci voluptatem 
      /// placeat?"]
      /// </summary>
      /// <param name="questionCount">Number of questions. Zero is a valid value. Default value is 3.</param>
      /// <param name="supplemental">Flag to indicate whether to consider words from a supplementary list of Lorem-like words. Default is false.</param>
      /// <returns></returns>
      public static IEnumerable<string> Questions(int questionCount = 3, bool supplemental = false)
      {
         if (questionCount < 0)
         {
            throw new ArgumentOutOfRangeException(nameof(questionCount), "Must be equal to or greater than zero.");
         }
         return questionCount.Times(x => Question(supplemental: supplemental));
      }

      private static readonly char[] _characters = "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();
   }
}
