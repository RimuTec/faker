using System.Linq;
using NUnit.Framework;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class GeneratorBaseTests
   {
      [Test]
      public void Parse_DuplicateVariable()
      {
         Config.Locale = "en";
         const string template = "#{Name.last_name}, #{Name.last_name} and #{Name.last_name}";
         var result = GeneratorBase<Company>.Parse(template);
         var parts = result.Split(',', ' ').Where(x => x != string.Empty).Distinct().ToArray();
         Assert.GreaterOrEqual(parts.Length, 3);
      }
   }
}
