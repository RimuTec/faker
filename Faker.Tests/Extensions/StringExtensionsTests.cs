using NUnit.Framework;
using RimuTec.Faker.Extensions;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests.Extensions
{
   [TestFixture]
   public class StringExtensionsTests
   {
      [Test]
      public void ToWordList_HappyDays()
      {
         // arrange
         const string sentence = "Must be equal to or greater than zero.";

         // act
         var wordList = string.Join("||", sentence.ToWordList());

         // assert
         Assert.AreEqual("must||be||equal||to||or||greater||than||zero", wordList);
      }

      [Test]
      public void ToWordList_For_Question()
      {
         // arrange
         const string sentence = "What is the answer to all questions?";

         // act
         var wordList = string.Join("||", sentence.ToWordList());

         // assert
         Assert.AreEqual("what||is||the||answer||to||all||questions", wordList);
      }

      [Test]
      public void Regexify_Digits()
      {
         // arrange
         const string template = @"[0-8]\d{2}-\d{2}-\d{4}";

         // act
         var result = template.Regexify();

         // assert
         Assert.AreEqual(9, Regex.Matches(result, @"\d").Count);
         Assert.AreEqual(2, Regex.Matches(result, "-").Count);
         Assert.IsFalse(result.Contains("#"));
      }

      [Test]
      public void Regexify_WithRegex()
      {
         const string expectedPattern = "^[A-CEGHJ-NPR-TVXY][0-9][A-CEJ-NPR-TV-Z] ?[0-9][A-CEJ-NPR-TV-Z][0-9]$";
         // en-CA. If more is need parameterize test
         const string providedPattern = "/[A-CEGHJ-NPR-TVXY][0-9][A-CEJ-NPR-TV-Z] ?[0-9][A-CEJ-NPR-TV-Z][0-9]/";
         var result = providedPattern.Regexify();
         Assert.AreEqual(1, Regex.Matches(result, expectedPattern).Count,
            $"Incorrect value is: '{result}'. Regex used: '{expectedPattern}'");
      }

      [Test]
      public void Regexify_WithRegex_en_GB()
      {
         const string expectedPattern = "^[A-PR-UWYZ]([A-HK-Y][0-9][ABEHMNPRVWXY0-9]?|[0-9][ABCDEFGHJKPSTUW0-9]?) [0-9][ABD-HJLNP-UW-Z]{2}$";
         const string providedPattern = "/[A-PR-UWYZ]([A-HK-Y][0-9][ABEHMNPRVWXY0-9]?|[0-9][ABCDEFGHJKPSTUW0-9]?) [0-9][ABD-HJLNP-UW-Z]{2}/";
         var result = providedPattern.Regexify();
         Assert.AreEqual(1, Regex.Matches(result, expectedPattern).Count,
            $"Incorrect value is: '{result}'. Regex used: '{expectedPattern}'");
      }

      [Test]
      public void ExpandOr()
      {
         const string expectedPattern = "^(this|that)$";
         const string input = "(this|that)";
         var result = input.ExpandOr();
         Assert.AreEqual(1, Regex.Matches(result, expectedPattern).Count,
            $"Incorrect value is: '{result}'. Regex used: '{expectedPattern}'");
      }

      [Test]
      public void ExpandRanges()
      {
         const string expectedPattern = @"^\[abc\]$|^\[abc\]\[abc\]$";
         const string input = "[abc]{2,2}";
         var result = input.ExpandRanges();
         Assert.AreEqual(1, Regex.Matches(result, expectedPattern).Count,
            $"Incorrect value is: '{result}'. Regex used: '{expectedPattern}'");
      }

      [Test]
      public void EnsureMixCase()
      {
         const string example = "6a7r5e3f";
         var result = Internet.EnsureMixCase(example);
         Assert.GreaterOrEqual(Regex.Matches(result, "[A-Z]").Count, 1,
            $"Invalid value '{result}'"
         );
      }
   }
}
