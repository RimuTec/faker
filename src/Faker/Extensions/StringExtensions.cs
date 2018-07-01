using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Extensions {
   internal static class StringExtensions {
      private static readonly string[] _alphabet = "a b c d e f g h i j k l m n o p q r s t u v w x y z".Split(' ');

      /// <summary>
      /// Get a string with every occurence of '#' replaced with a random number.
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static string Numerify(this string s) {
         return Regex.Replace(s, "#", new MatchEvaluator((m) => RandomNumber.Next(0, 9).ToString()), RegexOptions.Compiled);
      }

      /// <summary>
      /// Get a string with every '?' replaced with a random character from the alphabet.
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static string Letterify(this string s) {
         return Regex.Replace(s, @"\?", new MatchEvaluator((m) => _alphabet.Random()), RegexOptions.Compiled);
      }

      /// <summary>
      /// Gets a string with every '?' replaced with a random character from the alphabet and 
      /// with occurence of '#' replaced with a random number.
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static string Bothify(this string s) {
         return s.Letterify().Numerify();
      }

      /// <summary>
      /// Removes all whitespaces from the string.
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static string AlphanumericOnly(this string s) {
         return Regex.Replace(s, @"\W", string.Empty);
      }

      /// <summary>
      /// Capitalise the first letter of the given string.
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static string Capitalise(this string s) {
         return Regex.Replace(s, "^[a-z]", new MatchEvaluator(x => x.Value.ToUpperInvariant()));
      }

      /// <summary>
      /// Returns a random character from the string.
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public static string Random(this string s) {
         return s.ToCharArray().Random().ToString();
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
      public static string Regexify(this string reg) {
         // Ditch the anchors
         reg = Regex.Replace(reg, @"{^\/?\^?}", "", RegexOptions.Compiled); 
         reg = Regex.Replace(reg, @"{\$?\/?$}", "", RegexOptions.Compiled);
         
         // All {2} become {2,2}
         reg = Regex.Replace(reg, @"\{(\d+)\}", @"{$1,$1}", RegexOptions.Compiled);
         
         // All ? become {0,1}
         reg = Regex.Replace(reg, @"\?", @"{0,1}", RegexOptions.Compiled);

         // [12]{1,2} becomes [12] or [12][12]
         reg = Regex.Replace(reg, @"(\[[^\]]+\])\{(\d+),(\d+)\}", match => {
            throw new NotImplementedException();
            var toRepeat = match.Groups[1].Value;
            var lowerBoundary = match.Groups[2].Value;
            var uppderBoundary = match.Groups[3].Value;
            return $"";
         }, RegexOptions.Compiled);

         // (12|34){1,2} becomes (12|34) or (12|34)(12|34)
         reg = Regex.Replace(reg, @"(\([^\)]+\))\{(\d+),(\d+)\}", match => throw new NotImplementedException(), RegexOptions.Compiled);

         // A{1,2} becomes A or AA or \d{3} becomes \d\d\d
         reg = Regex.Replace(reg,
            @"(\\?.)\{(\d+),(\d+)\}",
            match => {
               var toRepeat = match.Groups[1].Value;
               var lowerBoundary = match.Groups[2].Value;
               var upperBoundary = match.Groups[3].Value;
               int[] intRange = new Range(lowerBoundary, upperBoundary).AsIntArray();
               var result = string.Join("", intRange.Random().Times(x => toRepeat));
               return result;
            }, RegexOptions.Compiled);

         // A{1,2} becomes A or AA or \d{3} becomes \d\d\d
         reg = Regex.Replace(reg, @"(\\?.)\{(\d+),(\d+)\}", match => {
            throw new NotImplementedException();
         }, RegexOptions.Compiled);

         // (this|that) becomes 'this' or 'that'
         reg = Regex.Replace(reg, @"\((.*?)\)", match => {
            throw new NotImplementedException();
         }, RegexOptions.Compiled);

         // All A-Z inside of [] become C (or X, or whatever)
         reg = Regex.Replace(reg, @"\[([^\]]+)\]", match => {
            string result = Regex.Replace(match.Value, @"(\w\-\w)", range => {
               var charRange = new Range(range.Value).ToArray();
               return $"{charRange.Random()}";
            });
            return result;
         }, RegexOptions.Compiled); 

         // All [ABC] become B (or A or C)
         reg = Regex.Replace(reg, @"\[([^\]]+)\]", match => {
            var charRange = match.Groups[1].Value.ToCharArray();
            return $"{charRange.Random()}";
         }, RegexOptions.Compiled);

         reg = Regex.Replace(reg, Regex.Escape(@"\d"), match => $"{Numbers.Random()}", RegexOptions.Compiled);
         reg = Regex.Replace(reg, Regex.Escape(@"\w"), match => $"{Letters.Random().ToString()}", RegexOptions.Compiled);
         return reg;
      }

      private static readonly int[] Numbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

      private static readonly char[] ULetters = new char[] {
         'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
      };

      private static readonly char[] Letters = new char[] {
         'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
         'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
      };
   }
}
