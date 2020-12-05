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
   public class GeneratorBase<T> where T : class
   {
      internal GeneratorBase() { }

      /// <summary>
      /// Parses a template that may contain tokens like "#{address.full_address}" and
      /// either invokes generator methods or load content from yaml files as replacement
      /// for the token
      /// </summary>
      /// <param name="template"></param>
      /// <returns></returns>
      internal static string Parse(string template)
      {
         var clazz = new StackFrame(1).GetMethod().DeclaringType; // https://stackoverflow.com/a/171974/411428
         var matches = Regex.Matches(template, @"#{([a-zA-Z._]{1,})}");
         for (var i = 0; i < matches.Count; i++)
         {
            string placeHolder = matches[i].Value;
            var token = matches[i].Groups[1].Value;
            if (!token.Contains("."))
            {
               // Prepend class name before fetching
               token = $"{typeof(T).Name.ToLower()}.{token}";
            }

            var className = token.Split('.')[0].ToPascalCasing();
            var method = token.Split('.')[1].ToPascalCasing();

            string replacement = null;
            var type = typeof(YamlLoader).Assembly.GetTypes().FirstOrDefault(t => string.Compare(t.Name, className, true) == 0);
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

      internal static string Fetch(string locator)
      {
         var localeName = Config.Locale;

         // if locale hasn't been loaded yet, now is a good time
         LoadLocale(localeName);

         // at this point the locale is in the dictionary
         YamlNode fakerNode;
         var key = localeName.ToLower();
         try
         {
            fakerNode = Library._dictionary[key];
            var locatorParts = locator.Split('.');
            return Fetch(fakerNode[locatorParts[0]], locatorParts.Skip(1).ToArray());
         }
         catch
         {
            // fall back to locale "en"
            LoadLocale("en");
            var fileName = typeof(T).Name.FromPascalCasing();
            fileName = $"en.{fileName}";

            key = fileName.ToPascalCasing().ToLower();

            fakerNode = Library._dictionary[key];
            var locatorParts = locator.Split('.');
            return Fetch(fakerNode[locatorParts[0]], locatorParts.Skip(1).ToArray());
         }
      }

      private static void LoadLocale(string localeName)
      {
         var fileName = localeName;
         if (localeName == "en")
         {
            fileName = typeof(T).Name.FromPascalCasing();
            fileName = $"{localeName}.{fileName}";
         }
         var key = fileName.ToPascalCasing().ToLower();

         if (!Library._dictionary.ContainsKey(key))
         {
            YamlNode fakerNode;
            var executingAssembly = Assembly.GetExecutingAssembly();
            string embeddedResourceFileName = $"RimuTec.Faker.locales.{fileName}.yml";
            if (executingAssembly.GetManifestResourceNames().Contains(embeddedResourceFileName))
            {
               using (var reader = YamlLoader.OpenText(embeddedResourceFileName))
               {
                  var stream = new YamlStream();
                  stream.Load(reader);
                  YamlNode rootNode = stream.Documents[0].RootNode;
                  var localeNode = rootNode[localeName];
                  fakerNode = localeNode["faker"];
                  Library._dictionary.Add(key, fakerNode);
               }
            }
            else
            {
               var assemblyLocation = new FileInfo(executingAssembly.Location);
               var fileNamePath = Path.Combine(assemblyLocation.DirectoryName, $"{fileName}.yml");
               if (File.Exists(fileNamePath))
               {
                  var yamlContent = File.ReadAllText(fileNamePath);
                  using (var reader = new StringReader(yamlContent))
                  {
                     var stream = new YamlStream();
                     stream.Load(reader);
                     YamlNode rootNode = stream.Documents[0].RootNode;
                     var localeNode = rootNode[localeName];
                     fakerNode = localeNode["faker"];
                     Library._dictionary.Add(key, fakerNode);
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

      internal static List<KeyValuePair<string,string[]>> Translate(string locator) {
         var localeName = Config.Locale;

         // if locale hasn't been loaded yet, now is a good time
         LoadLocale(localeName);

         YamlNode fakerNode;
         var key = localeName;
         try
         {
            fakerNode = Library._dictionary[key];
            var locatorParts = locator.Split('.');
            return Translate(fakerNode[locatorParts[0]], locatorParts.Skip(1).ToArray());
         }
         catch
         {
            // fall back to locale "en"
            LoadLocale("en");
            key = $"en.{locator.Split('.')[0]}";
            fakerNode = Library._dictionary[key];
            var locatorParts = locator.Split('.');
            return Translate(fakerNode[locatorParts[0]], locatorParts.Skip(1).ToArray());
         }
      }

      private static List<KeyValuePair<string, string[]>> Translate(YamlNode yamlNode, string[] locatorParts)
      {
         if (locatorParts.Length > 0)
         {
            return Translate(yamlNode[locatorParts[0]], locatorParts.Skip(1).ToArray());
         }
         if(yamlNode is YamlSequenceNode sequenceNode)
         {
            var result = new List<KeyValuePair<string, string[]>>();
            var count = 0;
            foreach(var child in sequenceNode.Children)
            {
               if(child is YamlSequenceNode childNode)
               {
                  IEnumerable<string> enumerable = childNode.Children.Select(c => c.ToString());
                  var arr = enumerable.ToArray();
                  result.Add(new KeyValuePair<string, string[]>($"{count++}", arr));
               }
            }
            return result;
         }
         else if(yamlNode is YamlMappingNode mappingNode)
         {
            var result = new List<KeyValuePair<string, string[]>>();
            foreach(var pair in mappingNode)
            {
               if(pair.Value is YamlSequenceNode sequenceNodeInner)
               {
                  IEnumerable<string> enumerable = sequenceNodeInner.Children.Select(c => c.ToString());
                  var arr = enumerable.ToArray();
                  result.Add(new KeyValuePair<string, string[]>(pair.Key.ToString(), arr));
               }
            }
            return result;
         }
         return new List<KeyValuePair<string, string[]>>();
      }
   }
}
