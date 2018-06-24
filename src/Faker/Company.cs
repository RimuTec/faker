using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;

namespace RimuTec.Faker {
   public static class Company {
      // Resources used by this class from https://github.com/stympy/faker/blob/master/lib/locales/en/company.yml
      // [Manfred, 21jun2018]

      static Company() {
         const string yamlFileName = "RimuTec.Faker.locales.en.company.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _company = locale.en.faker.company;
      }

      /// <summary>
      /// Get a random company name. Example: "Hirthe-Ritchie"
      /// </summary>
      /// <returns></returns>
      public static string Name() {
         var nameTemplate = _company.Name.Random();
         const string placeholder = "#{Name.last_name}";
         var place = nameTemplate.IndexOf(placeholder);
         while (place >= 0){
            nameTemplate = nameTemplate.Remove(place, placeholder.Length).Insert(place, Faker.Name.LastName());
            place = nameTemplate.IndexOf(placeholder);
         }
         nameTemplate = nameTemplate.Replace("#{suffix}", _company.Suffix.Random());
         return nameTemplate;
      }

      /// <summary>
      /// Get a random company name suffix. Example: "Group"
      /// </summary>
      /// <returns></returns>
      public static string Suffix() {
         return _company.Suffix.Random();
      }

      /// <summary>
      /// Get a random industry. Example: "Information Services"
      /// </summary>
      /// <returns></returns>
      public static string Industry() {
         return _company.Industry.Random();
      }

      /// <summary>
      /// Generate a buzzword-laden catch phrase. Example: "Business-focused coherent parallelism"
      /// </summary>
      /// <returns></returns>
      public static string CatchPhrase() {
         var words = new List<string>();
         _company.Buzzwords.ForEach(x => words.Add(x.Random()));
         return string.Join(" ", words);
      }

      /// <summary>
      /// Get a random buzzword. Example: "Business-focused"
      /// </summary>
      /// <returns></returns>
      public static string Buzzword() {
         var foo = _company.Buzzwords.Aggregate(new List<string>(), (list, a) => {
            list.AddRange(a.ToList());
            return list;
         });
         return foo.Random().ToLower();
      }

      /// <summary>
      /// When a straight answer won't do, BS to the rescue! Example: "empower one-to-one web-readiness"
      /// </summary>
      /// <returns></returns>
      /// <example>"empower one-to-one web-readiness"</example>
      public static string Bs() {
         var sb = new StringBuilder();
         _company.Bs.ForEach(x => sb.Append(x.Random() + " "));
         return sb.ToString().Trim();
      }

      /// <summary>
      /// Returns a URL to a random fake company logo in PNG format.
      /// </summary>
      /// <returns></returns>
      public static string Logo() {
         // Selection of fake but convincing logos for real-looking test data:
         // https://github.com/pigment/fake-logos
         // Available under a Creative Commons (CC) "Attribution-ShareAlike 4.0 International (CC BY-SA 4.0) 
         // license. License https://creativecommons.org/licenses/by-sa/4.0/
         return $"https://pigment.github.io/fake-logos/logos/medium/color/#{RandomNumber.Next(13)}.png";
      }

      /// <summary>
      /// Get a random company type. Example: "Privately Held"
      /// </summary>
      /// <returns></returns>
      public static string Type() {
         return _company.Type.Random();
      }

      /// <summary>
      /// Generate US employee identification number. Example: "34-8488813"
      /// </summary>
      /// <returns></returns>
      public static string Ein() {
         return "##-#######".Numerify();
      }

      /// <summary>
      /// Generate "Data Universal Numbering System". Example: "08-341-3736"
      /// </summary>
      /// <returns></returns>
      public static string DunsNumber() {
         return "##-###-####".Numerify();
      }

      /// <summary>
      /// Get a random profession. E.g. "Firefighter"
      /// </summary>
      /// <returns></returns>
      public static string Profession() {
         return _company.Profession.Random();
      }

      /// <summary>
      /// Generates a random Swedish organization number. See more here https://sv.wikipedia.org/wiki/Organisationsnummer
      /// </summary>
      /// <returns></returns>
      public static string SwedishOrganizationNumber() {
         // Valid leading digit: 1, 2, 3, 5, 6, 7, 8, 9
         // Valid third digit: >=2
         // Last digit is a control digit
         var validLeadingDigits = new List<int> { 1, 2, 3, 5, 6, 7, 8, 9 };
         var digits = new List<int> {
            validLeadingDigits.Random(),
            RandomNumber.Next(10),
            RandomNumber.Next(2, 10)
         };
         digits.AddRange(6.Times(x => RandomNumber.Next(10)));
         digits.Add(digits.CheckDigit());
         return digits.Aggregate(new StringBuilder(), (sb, d) => sb.Append($"{d}")).ToString();
      }

      private static company _company;

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
         public company company { get; set; }
      }

      internal class company {
         [YamlMember(Alias = "suffix", ApplyNamingConventions = false)]
         public List<string> Suffix { get; set; }

         [YamlMember(Alias = "buzzwords", ApplyNamingConventions = false)]
         public List<string[]> Buzzwords { get; set; }

         [YamlMember(Alias = "bs", ApplyNamingConventions = false)]
         public List<string[]> Bs { get; set; }

         [YamlMember(Alias = "name", ApplyNamingConventions = false)]
         public List<string> Name { get; set; }

         [YamlMember(Alias = "industry", ApplyNamingConventions = false)]
         public string[] Industry { get; set; }

         [YamlMember(Alias = "profession", ApplyNamingConventions = false)]
         public string[] Profession { get; set; }

         [YamlMember(Alias = "type", ApplyNamingConventions = false)]
         public string[] Type { get; set; }
      }
#pragma warning restore IDE1006 // Naming Styles
   }
}
