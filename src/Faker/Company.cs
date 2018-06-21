using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System.Collections.Generic;
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
         public List<string[]> BS { get; set; }

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
