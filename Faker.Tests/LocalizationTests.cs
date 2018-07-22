using NUnit.Framework;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class LocalizationTests
   {
      [TearDown]
      public void TearDown()
      {
         Config.Locale = "en";
      }

      [Test]
      public void German_City()
      {
         var germanSuffixes = new string[] { "stadt", "dorf", "land", "scheid", "burg", "berg", "heim", "hagen", "feld", "brunn", "grün" };
         Config.Locale = "de";
         var city = Address.City();
         Assert.IsTrue(germanSuffixes.Any(x => city.EndsWith(x)), $"{nameof(city)} was '{city}'");
         Config.Locale = "en";
      }

      [Test]
      public void German_FirstName()
      {
         Config.Locale = "de";
         var firstName = Name.FirstName();
         var firstNames = Fetch("name.first_name");
         Assert.Greater(firstNames.Count(), 0);
         Assert.IsTrue(firstNames.Contains(firstName));
      }

      [Test]
      public void German_LastName()
      {
         Config.Locale = "de";
         var lastName = Name.LastName();
         var lastNames = Fetch("name.last_name");
         Assert.Greater(lastNames.Count(), 0);
         Assert.IsTrue(lastNames.Contains(lastName));
      }

      private static List<string> Fetch(string locator)
      {
         if (_dictionary.ContainsKey(Config.Locale))
         {
            var yamlNode = _dictionary[Config.Locale];
            var fakerNode = yamlNode;
            var locatorParts = locator.Split('.');
            return Fetch(yamlNode[locatorParts[0]], locatorParts.Skip(1).ToArray());
         }
         throw new Exception($"Entry for locale {Config.Locale} not found.");
      }

      private static List<string> Fetch(YamlNode yamlNode, string[] locatorParts)
      {
         if (locatorParts.Length > 0)
         {
            return Fetch(yamlNode[locatorParts[0]], locatorParts.Skip(1).ToArray());
         }
         if (yamlNode is YamlSequenceNode sequenceNode)
         {
            IEnumerable<string> enumerable = sequenceNode.Children.Select(c => c.ToString());
            var arr = enumerable.ToArray();
            return enumerable.ToList();
         }
         return new List<string>();
      }

      private static Dictionary<string, YamlNode> _dictionary => YamlLoader._dictionary;
   }
}
