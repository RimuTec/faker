using System.Collections.Generic;

namespace RimuTec.Faker.Tests.Extensions
{
    internal static class StringExtensions
    {
        public static string RemovePeriods(this string source)
        {
            return source.Replace(".", string.Empty);
        }

        public static string RemoveQuestionMarks(this string source)
        {
            return source.Replace("?", string.Empty);
        }

        public static IEnumerable<string> ToWordList(this string source)
        {
            return source.RemovePeriods().RemoveQuestionMarks().ToLower().Split(' ');
        }
    }
}
