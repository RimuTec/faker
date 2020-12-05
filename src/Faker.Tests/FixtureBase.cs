using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            if (_dictionary.ContainsKey(key))
            {
                try
                {
                    var yamlNode = _dictionary[key];
                    var locatorParts = locator.Split('.');
                    return Fetch(yamlNode[locatorParts[0].ToLowerInvariant()], locatorParts.Skip(1).ToArray());
                }
                catch
                {
                    // Fall back to locale "en"
                    var locatorParts = locator.Split('.');
                    string fallbackKey = $"en.{locatorParts[0].ToLowerInvariant()}";
                    var yamlNode = _dictionary[fallbackKey];
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

        protected static Dictionary<string, YamlNode> _dictionary => Library._dictionary;
    }
}
