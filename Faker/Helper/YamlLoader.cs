using RimuTec.Faker.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RimuTec.Faker.Helper
{
   internal static class YamlLoader
   {
      internal static T Read<T>(string yamlFileName, params IYamlTypeConverter[] converters)
      {
         T locale;
         var executingAssembly = Assembly.GetExecutingAssembly();
         using (var resourceStream = executingAssembly.GetManifestResourceStream(yamlFileName))
         {
            using (var textReader = new StreamReader(resourceStream))
            {
               DeserializerBuilder deserializerBuilder = new DeserializerBuilder();
               foreach (var converter in converters)
               {
                  deserializerBuilder = deserializerBuilder.WithTypeConverter(converter);
               }
               var deserializer = deserializerBuilder
                  .WithNamingConvention(new CamelCaseNamingConvention())
                  .Build()
                  ;
               locale = deserializer.Deserialize<T>(textReader.ReadToEnd());
            }
         }
         return locale;
      }

      internal static StreamReader OpenText(string yamlFileName)
      {
         var executingAssembly = Assembly.GetExecutingAssembly();
         var resourceStream = executingAssembly.GetManifestResourceStream(yamlFileName);
         return new StreamReader(resourceStream);
      }

      internal static string Fetch(string locator)
      {
         var localeName = Config.Locale;
         if (Config.Locale == "en")
         {
            localeName = $"{localeName}.{locator.Split('.')[0]}";
         }

         // if locale hasn't been loaded yet, now is a good time
         YamlNode fakerNode;
         if (!_dictionary.ContainsKey(localeName))
         {
            using (var reader = YamlLoader.OpenText($"RimuTec.Faker.locales.{localeName}.yml"))
            {
               var stream = new YamlStream();
               stream.Load(reader);
               YamlNode rootNode = stream.Documents[0].RootNode;
               var localeNode = rootNode[Config.Locale];
               fakerNode = localeNode["faker"];
               _dictionary.Add(localeName, fakerNode);
            }
         }

         // at this point the locale is in the dictionary
         fakerNode = _dictionary[localeName];
         var locatorParts = locator.Split('.');
         return Fetch(fakerNode[locatorParts[0]], locatorParts.Skip(1).ToArray());
      }

      private static string Fetch(YamlNode yamlNode, string[] locatorParts)
      {
         if (locatorParts.Length > 0)
         {
            return Fetch(yamlNode[locatorParts[0]], locatorParts.Skip(1).ToArray());
         }
         if (yamlNode is YamlSequenceNode sequenceNode)
         {
            IEnumerable<string> enumerable = sequenceNode.Children.Select(c => c.ToString());
            var arr = enumerable.ToArray();
            return enumerable.Sample();
         }
         return string.Empty;
      }

      internal static Dictionary<string, YamlNode> _dictionary = new Dictionary<string, YamlNode>();
   }
}
