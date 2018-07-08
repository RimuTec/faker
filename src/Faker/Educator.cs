using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;

namespace RimuTec.Faker {
   /// <summary>
   /// Generators for education related data, e.g. university, secondary school, course and campus.
   /// </summary>
   public static class Educator {
      static Educator() {
         const string yamlFileName = "RimuTec.Faker.locales.en.educator.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _educator = locale.en.faker.educator;
      }

      /// <summary>
      /// Generates a name for a tertiary education provider. Example: "Mallowtown Technical College"
      /// </summary>
      /// <returns></returns>
      public static string University() {
         return $"{_educator.Name.Sample()} {_educator.Tertiary.Type.Sample()}";
      }

      /// <summary>
      /// Generates a name for a secondary school. Example: "Iceborough Secondary College"
      /// </summary>
      /// <returns></returns>
      public static string SecondarySchool() {
         return $"{_educator.Name.Sample()} {_educator.Secondary.Sample()}";
      }

      private static educator _educator;

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
         public educator educator { get; set; }
      }

      internal class educator {
         [YamlMember(Alias = "name", ApplyNamingConventions = false)]
         public string[] Name { get; set; }

         [YamlMember(Alias = "secondary", ApplyNamingConventions = false)]
         public string[] Secondary { get; set; }

         [YamlMember(Alias = "tertiary", ApplyNamingConventions = false)]
         public tertiary Tertiary { get; set; }
      }

      internal class tertiary {
         [YamlMember(Alias = "type", ApplyNamingConventions = false)]
         public string[] Type { get; set; }

         [YamlMember(Alias = "course", ApplyNamingConventions = false)]
         public course Course { get; set; }
      }

      internal class course {
         [YamlMember(Alias = "subject", ApplyNamingConventions = false)]
         public string[] Subject { get; set; }

         [YamlMember(Alias = "type", ApplyNamingConventions = false)]
         public string[] Type { get; set; }
      }
#pragma warning restore IDE1006 // Naming Styles
   }
}
