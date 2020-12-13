using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using YamlDotNet.RepresentationModel;

namespace RimuTec.Faker.Tests
{
   public abstract class FixtureBase
   {
      protected static int RegexMatchesCount(string input, string pattern)
      {
         return Regex.Matches(input, pattern, RegexOptions.Compiled).Count;
      }

      protected static List<string> Fetch(string locator)
      {
         var key = Config.Locale;
         if (Config.Locale == "en")
         {
            key = $"{Config.Locale}.{locator.Split('.')[0]}";
         }
         key = key.ToLower();
         if (Dictionary.ContainsKey(key))
         {
            try
            {
               var yamlNode = Dictionary[key];
               var locatorParts = locator.Split('.');
               return Fetch(yamlNode[locatorParts[0].ToLowerInvariant()], locatorParts.Skip(1).ToArray());
            }
            catch
            {
               // Fall back to locale "en"
               var locatorParts = locator.Split('.');
               string fallbackKey = $"en.{locatorParts[0].ToLowerInvariant()}";
               var yamlNode = Dictionary[fallbackKey];
               return Fetch(yamlNode[locatorParts[0].ToLowerInvariant()], locatorParts.Skip(1).ToArray());
            }
         }
         throw new Exception($"Entry for locale {Config.Locale} not found.");
      }

      protected static List<string> Fetch(YamlNode yamlNode, string[] locatorParts)
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

      protected static Dictionary<string, YamlNode> Dictionary => Library._dictionary;
   }

   public static class DefaultFixtureData
   {
      public static IEnumerable FixtureParams
      {
         get
         {
            yield return new TestFixtureData("bg");
            yield return new TestFixtureData("ca");
            yield return new TestFixtureData("ca-CAT");
            yield return new TestFixtureData("da-DK");
            yield return new TestFixtureData("de");
            yield return new TestFixtureData("de-AT");
            yield return new TestFixtureData("de-CH");
            yield return new TestFixtureData("ee");
            yield return new TestFixtureData("en");
            yield return new TestFixtureData("en-AU");
            yield return new TestFixtureData("en-au-ocker");
            yield return new TestFixtureData("en-BORK");
            yield return new TestFixtureData("en-CA");
            yield return new TestFixtureData("en-GB");
            yield return new TestFixtureData("en-IND");
            yield return new TestFixtureData("en-MS");
            yield return new TestFixtureData("en-NEP");
            yield return new TestFixtureData("en-NG");
            yield return new TestFixtureData("en-NZ");
            yield return new TestFixtureData("en-PAK");
            yield return new TestFixtureData("en-SG");
            yield return new TestFixtureData("en-UG");
            yield return new TestFixtureData("en-US");
            yield return new TestFixtureData("en-ZA");
            yield return new TestFixtureData("es");
            yield return new TestFixtureData("es-MX");
            yield return new TestFixtureData("fa");
            yield return new TestFixtureData("fi-FI");
            yield return new TestFixtureData("fr");
            yield return new TestFixtureData("fr-CA");
            yield return new TestFixtureData("fr-CH");
            yield return new TestFixtureData("he");
            yield return new TestFixtureData("id");
            yield return new TestFixtureData("it");
            yield return new TestFixtureData("ja");
            yield return new TestFixtureData("ko");
            yield return new TestFixtureData("lv");
            yield return new TestFixtureData("nb-NO");
            yield return new TestFixtureData("nl");

            // Not testing locale 'no' since the file no.yml has an incorrect format at the time of writing.
            // Note that we won't fix the content as we take the file 'as-is' from https://github.com/faker-ruby/faker
            //yield return new TestFixtureData("no", null);

            yield return new TestFixtureData("pl");
            yield return new TestFixtureData("pt");
            yield return new TestFixtureData("pt-BR");
            yield return new TestFixtureData("ru");
            yield return new TestFixtureData("sk");
            yield return new TestFixtureData("sv");
            yield return new TestFixtureData("tr");
            yield return new TestFixtureData("uk");
            yield return new TestFixtureData("vi");
            yield return new TestFixtureData("zh-CN");
            yield return new TestFixtureData("zh-TW");
         }
      }
   }
}
