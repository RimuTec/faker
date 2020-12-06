using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Extensions
{
   internal static class StringExtensions
   {
      private static readonly string[] _alphabet = "a b c d e f g h i j k l m n o p q r s t u v w x y z".Split(' ');

      /// <summary>
      /// Get a string with every occurence of '#' replaced with a random number.
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static string Numerify(this string s)
      {
         return Regex.Replace(s, "#", new MatchEvaluator(_ => RandomNumber.Next(0, 9).ToString()), RegexOptions.Compiled);
      }

      /// <summary>
      /// Get a string with every '?' replaced with a random character from the alphabet.
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static string Letterify(this string s)
      {
         return Regex.Replace(s, @"\?", new MatchEvaluator(_ => _alphabet.Sample()), RegexOptions.Compiled);
      }

      /// <summary>
      /// Gets a string with every '?' replaced with a random character from the alphabet and 
      /// with occurence of '#' replaced with a random number.
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static string Bothify(this string s)
      {
         return s.Letterify().Numerify();
      }

      /// <summary>
      /// Removes all whitespaces from the string.
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static string AlphanumericOnly(this string s)
      {
         return Regex.Replace(s, @"\W", string.Empty);
      }

      /// <summary>
      /// Capitalise the first letter of the given string.
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static string Capitalise(this string s)
      {
         return Regex.Replace(s, "^[a-z]", new MatchEvaluator(x => x.Value.ToUpperInvariant()));
      }

      /// <summary>
      /// Romanizes cyrillics, fixes umlauts and makes the string lower case.
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static string Prepare(this string s)
      {
         var result = s.RomanizeCyrillicString();
         result = result.FixUmlauts();
         result = Regex.Replace(result, @"\W", string.Empty, RegexOptions.Compiled).ToLower();
         return result;
      }

      /// <summary>
      /// Selects a random character from the string and returns it as a string.
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static string Sample(this string s)
      {
         int stringLength = s.Length;
         if (stringLength > 0)
         {
            return s[RandomNumber.Next(0, stringLength)].ToString();
         }
         return string.Empty;
      }

      /// <summary>
      /// Applies a regex pattern and returns a list of matches.
      /// </summary>
      /// <param name="s"></param>
      /// <param name="pattern"></param>
      /// <returns></returns>
      internal static IList<string> Scan(this string s, string pattern)
      {
         var matchList = new List<string>();
         var matches = Regex.Matches(s, pattern, RegexOptions.Compiled);
         for (var i = 0; i < matches.Count; i++)
         {
            matchList.Add(matches[i].Value);
         }
         return matchList;
      }

      /// <summary>
      /// Converts a string like "foo_bar" to "FooBar"
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      internal static string ToPascalCasing(this string s)
      {
         var parts = Regex.Split(s, $"[_]", RegexOptions.Compiled).Select(x => x.Capitalise());
         return string.Join(string.Empty, parts);
      }

      /// <summary>
      /// Converts a string like "FooBar" to "foo_bar"
      /// </summary>
      /// <param name="input"></param>
      /// <returns></returns>
      internal static string FromPascalCasing(this string input)
      {
         // Implementation based on code from https://stackoverflow.com/a/51310790/411428
         if (string.IsNullOrEmpty(input))
            return input;

         var sb = new StringBuilder();
         // start with the first character -- consistent camelcase and pascal case
         sb.Append(char.ToUpper(input[0]));

         // march through the rest of it
         for (var i = 1; i < input.Length; i++)
         {
            // any time we hit an uppercase OR number, it's a new word
            if (char.IsUpper(input[i]) || char.IsDigit(input[i]))
               sb.Append('_');
            // add regularly
            sb.Append(input[i]);
         }

         return sb.ToString().ToLower();
      }

      private static string RomanizeCyrillicString(this string s)
      {
         // To be implemented.
         return s;
      }

      private static string FixUmlauts(this string s)
      {
         var result = Regex.Replace(s, @"[äöüß]", match =>
         {
            switch (match.Value.ToLower())
            {
               case "ä":
                  return "ae";
               case "ö":
                  return "oe";
               case "ü":
                  return "ue";
               case "ß":
                  return "ss";
               default:
                  return match.Value;
            }
         });
         return s;
      }

      /// <summary>
      /// Given a regulare expression, attempt to generate a string that would match it. This is a rather
      /// simple implementation, so don't be shocked if it blows up on you in a spectacular fashion. It does
      /// not handle ., *, unbound ranges such as {1,}, extensions such as (?=), character classes, some
      /// abbreviations for characters classes, and nested parantheses. I told you it was simple. :) It's
      /// also probably dog-slow, so you shouldn't use it. It will take a reg ex like this:
      /// "^[A-PR-UWYZ0-9][A-HK-Y0-9][AEHMNPRTVXY0-9]?[ABEHMNPRVWXY0-9]? {1,2}[0-9][ABD-HJLN-UW-Z]{2}$"
      /// and generate a string like this: "U3V  3TP"
      /// </summary>
      /// <param name="reg"></param>
      /// <returns></returns>
      public static string Regexify(this string reg)
      {
         // Ditch the anchors
         reg = Regex.Replace(reg, @"{^\/?\^?}", "", RegexOptions.Compiled);
         reg = Regex.Replace(reg, @"{\$?\/?$}", "", RegexOptions.Compiled);

         // All {2} become {2,2}
         reg = Regex.Replace(reg, @"\{(\d+)\}", @"{$1,$1}", RegexOptions.Compiled);

         // All ? become {0,1}
         reg = Regex.Replace(reg, @"\?", @"{0,1}", RegexOptions.Compiled);

         // [12]{1,2} becomes [12] or [12][12]
         reg = Regex.Replace(reg, @"(\[[^\]]+\])\{(\d+),(\d+)\}", match =>
         {
            throw new NotImplementedException();
            // var toRepeat = match.Groups[1].Value;
            // var lowerBoundary = match.Groups[2].Value;
            // var uppderBoundary = match.Groups[3].Value;
            // return $"";
         }, RegexOptions.Compiled);

         // (12|34){1,2} becomes (12|34) or (12|34)(12|34)
         reg = Regex.Replace(reg, @"(\([^\)]+\))\{(\d+),(\d+)\}", match =>
         {
            throw new NotImplementedException();
         }, RegexOptions.Compiled);

         // A{1,2} becomes A or AA or \d{3} becomes \d\d\d
         reg = Regex.Replace(reg,
            @"(\\?.)\{(\d+),(\d+)\}",
            match =>
            {
               var toRepeat = match.Groups[1].Value;
               var lowerBoundary = int.Parse(match.Groups[2].Value);
               var upperBoundary = int.Parse(match.Groups[3].Value);
               int[] intRange = new Range2<int>(lowerBoundary, upperBoundary).AsArray();
               var result = string.Join("", intRange.Sample().Times(x => toRepeat));
               return result;
            }, RegexOptions.Compiled);

         // (this|that) becomes 'this' or 'that'
         reg = Regex.Replace(reg, @"\((.*?)\)", _ =>
         {
            throw new NotImplementedException();
         }, RegexOptions.Compiled);

         // All A-Z inside of [] become C (or X, or whatever)
         reg = Regex.Replace(reg, @"\[([^\]]+)\]", match =>
         {
            var result = Regex.Replace(match.Value, @"(\w\-\w)", range =>
            {
               var parts = Regex.Split(range.Value, @"-");
               var charRange = new Range2<char>(parts[0][0], parts[1][0]);
               return $"{charRange.Sample()}";
            });
            return result;
         }, RegexOptions.Compiled);

         // All [ABC] become B (or A or C)
         reg = Regex.Replace(reg, @"\[([^\]]+)\]", match =>
         {
            var charRange = match.Groups[1].Value.ToCharArray();
            return $"{charRange.Sample()}";
         }, RegexOptions.Compiled);

         reg = Regex.Replace(reg, Regex.Escape(@"\d"), match => $"{Numbers.Sample()}", RegexOptions.Compiled);
         reg = Regex.Replace(reg, Regex.Escape(@"\w"), match => $"{Letters.Sample().ToString()}", RegexOptions.Compiled);
         return reg;
      }

      private static readonly int[] Numbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

      // private static readonly char[] ULetters = new char[] {
      //    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
      // };

      private static readonly char[] Letters = new char[] {
         'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
         'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
      };
   }
}
