using RimuTec.Faker.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RimuTec.Faker {
   public static class Name {
      static Name() {
         var executingAssembly = Assembly.GetExecutingAssembly();
         var resourceNames = executingAssembly.GetManifestResourceNames();
         using (var resourceStream = executingAssembly.GetManifestResourceStream("RimuTec.Faker.locales.en.name.yml")) {
            using (var textReader = new StreamReader(resourceStream)) {
               var deserializer = new DeserializerBuilder()
                  .WithNamingConvention(new CamelCaseNamingConvention())
                  .Build()
                  ;
               _locale = deserializer.Deserialize<locale>(textReader.ReadToEnd());
            }
         }
      }

      /// <summary>
      /// Generates a random first name
      /// </summary>
      /// <returns>A string containing a first name</returns>
      public static string FirstName() {
         return _locale.en.faker.name.FirstName.Random().Trim();
      }

      /// <summary>
      /// Generates a random last name
      /// </summary>
      /// <returns>A string containing a last name</returns>
      public static string LastName() {
         return _locale.en.faker.name.LastName.Random().Trim();
      }

      private static locale _locale;
   }

   // Helper classes for reading the yaml file. Note that the class names are
   // intentionally lower case.

   internal class locale {
      public en en { get; set; }
   }

   internal class en {
      public faker faker { get; set; }
   }

   internal class faker {
      public name name { get; set; }
   }

   internal class name {
      [YamlMember(Alias = "first_name", ApplyNamingConventions = false)]
      public string[] FirstName { get; set; }

      [YamlMember(Alias = "middle_name", ApplyNamingConventions = false)]
      public string[] MiddleName { get; set; }

      [YamlMember(Alias = "last_name", ApplyNamingConventions = false)]
      public string[] LastName { get; set; }

      [YamlMember(Alias = "prefix", ApplyNamingConventions = false)]
      public string[] Prefix { get; set; }

      [YamlMember(Alias = "suffix", ApplyNamingConventions = false)]
      public string[] Suffix { get; set; }

      [YamlMember(Alias = "name", ApplyNamingConventions = false)]
      public List<string> NamePatterns { get; set; }

      [YamlMember(Alias = "name_with_middle", ApplyNamingConventions = false)]
      public List<string> NameWithMiddlePatterns { get; set; }
   }
}
