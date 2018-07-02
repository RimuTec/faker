using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace RimuTec.Faker {
   /// <summary>
   /// Generators for internet related data, e.g. email addresses, domain names, IP addresses, MAC addresses,
   /// passwords, etc.
   /// </summary>
   public static class Internet {
      static Internet() {
         const string yamlFileName = "RimuTec.Faker.locales.en.internet.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _internet = locale.en.faker.internet;
      }

      /// <summary>
      /// Gets a random domain name. Example: "effertz.info"
      /// </summary>
      /// <returns></returns>
      public static string DomainName() {
         return $"{DomainWord()}.{DomainSuffix()}";
      }

      /// <summary>
      /// Gets a random but valid domain suffix. Example: "info"
      /// </summary>
      /// <returns></returns>
      public static string DomainSuffix() {
         return _internet.DomainSuffix.Sample();
      }

      /// <summary>
      /// Generates a word that can be used in a domain name. Example: "haleyziemann"
      /// </summary>
      /// <returns></returns>
      public static string DomainWord() {
         return Company.Name().Split(' ').First().Prepare();
      }

      /// <summary>
      /// Generates an email address. Example: "eliza@mann.net"
      /// </summary>
      /// <returns></returns>
      public static string Email() {
         return string.Join("@", UserName(), DomainName());
      }

      /// <summary>
      /// Generates a user name. Examples: "alexie", "johnson-nancy"
      /// </summary>
      /// <returns></returns>
      public static string UserName() {
         var separators = "._";
         var candidates = new List<string> {
            Name.FirstName().Prepare(),
            $"{Name.FirstName().Prepare()}{separators.Random()}{Name.LastName().Prepare()}"
         };
         return candidates.Sample();
      }

      private static internet _internet;

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
         public internet internet { get; set; }
      }

      internal class internet {
         [YamlMember(Alias = "free_email", ApplyNamingConventions = false)]
         public string[] FreeEmail { get; set; }

         [YamlMember(Alias = "domain_suffix", ApplyNamingConventions = false)]
         public string[] DomainSuffix { get; set; }

         [YamlMember(Alias = "user_agent", ApplyNamingConventions = false)]
         public Dictionary<string,List<string>> UserAgent { get; set; }
      }
#pragma warning restore IDE1006 // Naming Styles
   }
}
