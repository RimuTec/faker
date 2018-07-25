using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using YamlDotNet.RepresentationModel;

namespace RimuTec.Faker
{
   /// <summary>
   /// Base class for all fake data generators.
   /// </summary>
   public class GeneratorBase
   {
      internal GeneratorBase() { }

      /// <summary>
      /// Parses a template that may contains tokens like "#{address.full_address}" and
      /// either invokes generator methods or load content from yaml files as replacement
      /// for the token
      /// </summary>
      /// <param name="template"></param>
      /// <returns></returns>
      protected internal static string Parse(string template)
      {
         var clazz = new StackTrace().GetFrame(1).GetMethod().DeclaringType;
         var matches = Regex.Matches(template, @"#{([a-zA-Z._]{1,})}");
         for (var i = 0; i < matches.Count; i++)
         {
            string placeHolder = matches[i].Value;
            var token = matches[i].Groups[1].Value;
            if (!token.Contains("."))
            {
               // Prepend class name before fetching
               token = $"{clazz.Name.ToLower()}.{token}";
            }

            var className = token.Split('.')[0].ToPascalCasing();
            var method = token.Split('.')[1].ToPascalCasing();

            string replacement = null;
            var type = typeof(YamlLoader).Assembly.GetTypes().FirstOrDefault(t => t.Name == className);
            if (type != null)
            {
               var methodInfo = type.GetMethod(method, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
               if (methodInfo != null)
               {
                  // invoke statics method, if needed with default parameter values
                  // Ref: https://stackoverflow.com/a/9916197/411428
                  var paramCount = methodInfo.GetParameters().Count();
                  object[] parameters = Enumerable.Repeat(Type.Missing, paramCount).ToArray();
                  replacement = methodInfo.Invoke(null, parameters).ToString();
               }
            }
            if (string.IsNullOrWhiteSpace(replacement))
            {
               replacement = Fetch(token);
            }

            template = template.Replace(placeHolder, replacement);
         }
         return template;
      }

      protected internal static string Fetch(string locator)
      {
         var localeName = Config.Locale;

         // if locale hasn't been loaded yet, now is a good time
         LoadLocale(localeName, locator);

         // at this point the locale is in the dictionary
         YamlNode fakerNode;
         var key = localeName;
         try
         {
            fakerNode = _dictionary[key];
            var locatorParts = locator.Split('.');
            return Fetch(fakerNode[locatorParts[0]], locatorParts.Skip(1).ToArray());
         }
         catch
         {
            // fall back to locale "en"
            LoadLocale("en", locator);
            key = $"en.{locator.Split('.')[0]}";
            fakerNode = _dictionary[key];
            var locatorParts = locator.Split('.');
            return Fetch(fakerNode[locatorParts[0]], locatorParts.Skip(1).ToArray());
         }
      }

      private static void LoadLocale(string localeName, string locator)
      {
         var key = localeName;
         if (localeName == "en")
         {
            key = $"{localeName}.{locator.Split('.')[0]}";
         }

         if (!_dictionary.ContainsKey(key))
         {
            YamlNode fakerNode;
            var executingAssembly = Assembly.GetExecutingAssembly();
            string embeddedResourceFileName = $"RimuTec.Faker.locales.{key}.yml";
            if (executingAssembly.GetManifestResourceNames().Contains(embeddedResourceFileName))
            {
               using (var reader = YamlLoader.OpenText(embeddedResourceFileName))
               {
                  var stream = new YamlStream();
                  stream.Load(reader);
                  YamlNode rootNode = stream.Documents[0].RootNode;
                  var localeNode = rootNode[localeName];
                  fakerNode = localeNode["faker"];
                  _dictionary.Add(key, fakerNode);
               }
            }
            else
            {
               var assemblyLocation = new FileInfo(executingAssembly.Location);
               var fileName = Path.Combine(assemblyLocation.DirectoryName, $"{key}.yml");
               if (File.Exists(fileName))
               {
                  var yamlContent = File.ReadAllText(fileName);
                  using (var reader = new StringReader(yamlContent))
                  {
                     var stream = new YamlStream();
                     stream.Load(reader);
                     YamlNode rootNode = stream.Documents[0].RootNode;
                     var localeNode = rootNode[localeName];
                     fakerNode = localeNode["faker"];
                     _dictionary.Add(key, fakerNode);
                  }
               }
            }
         }
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
         else if (yamlNode is YamlScalarNode scalarNode)
         {
            return scalarNode.Value;
         }
         return string.Empty;
      }

      internal static Dictionary<string, YamlNode> _dictionary = new Dictionary<string, YamlNode>();
   }
}
