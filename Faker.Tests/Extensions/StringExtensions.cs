using System.Collections.Generic;

namespace RimuTec.Faker.Tests.Extensions
{
   internal static class StringExtensions
   {
      public static string RemovePeriods(this string source)
      {
         return source.Replace(Lorem.PunctuationPeriod(), string.Empty).Replace(".", string.Empty);
      }

      public static string RemoveQuestionMarks(this string source)
      {
         return source.Replace(Lorem.PunctuationQuestionMark(), string.Empty).Replace("?", string.Empty);
      }

      public static IEnumerable<string> ToWordList(this string source)
      {
         var seps = new List<char>();
         seps.AddRange(Lorem.PunctuationSpace().ToCharArray());
         seps.Add(' ');
         return source.RemovePeriods().RemoveQuestionMarks().ToLower().Split(seps.ToArray());
      }
   }
}
