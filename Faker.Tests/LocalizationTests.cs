using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class LocalizationTests
   {
      [Test]
      public void German_City()
      {
         var germanSuffixes = new string[] { "stadt", "dorf", "land", "scheid", "burg", "berg", "heim", "hagen", "feld", "brunn", "grün" };
         Config.Locale = "de";
         var city = Address.City();
         Assert.IsTrue(germanSuffixes.Any(x => city.EndsWith(x)), $"{nameof(city)} was '{city}'");
         Config.Locale = "en";
      }
   }
}
