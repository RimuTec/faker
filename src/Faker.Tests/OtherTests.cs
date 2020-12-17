using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using RimuTec.Faker.Extensions;

namespace RimuTec.Faker.Tests.OtherTests
{
   public class OtherTests
   {
      [Test]
      public void EmploymentType_Twice_NotEqual()
      {
         const string locale = "zh-TW";
         RandomNumber.ResetSeed(42);
         Config.Locale = locale;
         var employmentType1 = Job.EmploymentType();
         var employmentType2 = Job.EmploymentType();
         Assert.AreNotEqual(employmentType1, employmentType2,
            $"Locale '{locale}'. employmentType1 '{employmentType1}'. employmentType2 '{employmentType2}'."
         );
      }

      [Test]
      public void SsnValid()
      {
         var ssn = IdNumber.SsnValid();
         Assert.IsFalse(Regex.Match(ssn, "000-[0-9]{2}-[0-9]{4}|[0-9]{3}-00-[0-9]{4}|[0-9]{3}-[0-9]{2}-0000|666-[0-9]{2}-[0-9]{4}|9[0-9]{2}-[0-9]{2}-[0-9]{4}").Success);
      }

      [Test]
      public void Split()
      {
         var cultureToRestore = Thread.CurrentThread.CurrentCulture;
         Thread.CurrentThread.CurrentCulture = new CultureInfo("fa");
         Config.Locale = "fa";

         const string foo = "abc def";
         const string firstNames = "گل آرا‌";

         var fooAsChars = foo.ToCharArray();
         var firstNamesAsChars = firstNames.ToCharArray();

         var firstNamesAsArray = firstNames.Split(' ', '-', '\'').Select(s => s.Trim('\u200C').ToLower()).ToArray();
         Assert.AreEqual(2, firstNamesAsArray.Length);

         var firstFirstName = firstNamesAsArray[0];
         var secondFirstName = firstNamesAsArray[1];

         var firstFirstNameAsChars = firstFirstName.ToCharArray();
         var secondFirstNameAsChars = secondFirstName.ToCharArray();

         string pattern = $"^({firstFirstName}[._]{{1}}{secondFirstName}|{secondFirstName}[._]{{1}}{firstFirstName})";

         IEnumerable<string> parts = firstNames.Scan(@"\w+").Shuffle();
         var userName = string.Join("._".Sample(), parts).ToLower();

         Assert.AreEqual(1, Regex.Matches(userName, pattern).Count,
            $"userName is '{userName}'. Pattern is '{pattern}'"
         );

         Thread.CurrentThread.CurrentCulture = cultureToRestore;
      }
   }
}
