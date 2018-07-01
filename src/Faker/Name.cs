using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace RimuTec.Faker {
   /// <summary>
   /// Generators for names for humans.
   /// </summary>
   public static class Name {
      static Name() {
         const string yamlFileName = "RimuTec.Faker.locales.en.name.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _name = locale.en.faker.name;
      }

      /// <summary>
      /// Generates a random name with first name, last name and potentially a prefix and/or a suffix. The name will not 
      /// include a middle name. Example: "Tyshawn Johns Sr.". Note: This is the equivalent to Ruby's 'Faker::Name.name'.
      /// </summary>
      /// <returns></returns>
      public static string FullName() {
         var result = _name.NamePatterns.Sample().Trim();
         return result
            .Replace("#{prefix}", Prefix())
            .Replace("#{first_name}", FirstName())
            .Replace("#{last_name}", LastName())
            .Replace("#{suffix}", Suffix())
            ;
      }

      /// <summary>
      /// Generates a random name including first name, middle name and potentially a prefix and/or suffix. Example:
      /// "Aditya Elton Douglas".
      /// </summary>
      /// <returns></returns>
      public static string NameWithMiddle() {
         var result = _name.NameWithMiddlePatterns.Sample().Trim();
         return result
            .Replace("#{prefix}", Prefix())
            .Replace("#{first_name}", FirstName())
            .Replace("#{middle_name}", MiddleName())
            .Replace("#{last_name}", LastName())
            .Replace("#{suffix}", Suffix())
            ;
      }

      /// <summary>
      /// Generates a random first name. Example: "Kaci"
      /// </summary>
      /// <returns>A string containing a first name</returns>
      public static string FirstName() {
         return _name.FirstName.Sample().Trim();
      }

      /// <summary>
      /// Generates a random middle name. Example: "Abraham"
      /// </summary>
      /// <returns></returns>
      public static string MiddleName() {
         return _name.MiddleName.Sample().Trim();
      }

      /// <summary>
      /// Generates a random last name. Example: "Ernser"
      /// </summary>
      /// <returns>A string containing a last name</returns>
      public static string LastName() {
         return _name.LastName.Sample().Trim();
      }

      /// <summary>
      /// Generates a random prefix. Example: "Mr."
      /// </summary>
      /// <returns></returns>
      public static string Prefix() {
         return _name.Prefix.Sample().Trim();
      }

      /// <summary>
      /// Generates a random suffix. Example: "IV"
      /// </summary>
      /// <returns></returns>
      public static string Suffix() {
         return _name.Suffix.Sample().Trim();
      }

      /// <summary>
      /// Generates name initials. Example: NJM.
      /// </summary>
      /// <param name="characterCount">Number of characters in initials.</param>
      /// <returns></returns>
      public static string Initials(int characterCount = 3) {
         if(!(characterCount > 0)) {
            throw new ArgumentOutOfRangeException(nameof(characterCount), "Must be greater than 0.");
         }
         return string.Concat(characterCount.Times(x => _alphabetUpper.Sample()));
      }

      private static readonly string[] _alphabet = "a b c d e f g h i j k l m n o p q r s t u v w x y z".Split(' ');
      private static readonly string[] _alphabetUpper = "a b c d e f g h i j k l m n o p q r s t u v w x y z".ToUpper().Split(' ');

      private static name _name;

#pragma warning disable IDE1006 // Naming Styles
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
#pragma warning restore IDE1006 // Naming Styles
   }
}
