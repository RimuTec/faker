using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests {
   public abstract class FixtureBase {
      protected static int RegexMatchesCount(string input, string pattern) {
         return Regex.Matches(input, pattern, RegexOptions.Compiled).Count;
      }
   }
}
