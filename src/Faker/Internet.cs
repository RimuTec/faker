using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
      public static string Email(string name = null) {
         return string.Join("@", UserName(name), DomainName());
      }

      /// <summary>
      /// Generates a user name. Examples: "alexie", "johnson-nancy"
      /// </summary>
      /// <param name="name">Name to be used instead of a random one.</param>
      /// <param name="separators">Separators to consider. May not appear in result. Default are '.' and '_'.</param>
      /// <returns></returns>
      public static string UserName(string name = null, char[] separators = null) {
         string result;
         if (separators == null) {
            separators = "._".ToCharArray(); // if space is sampled, it will be removed by Prepare()
         }
         if (name != null) {
            IEnumerable<string> parts = name.Scan(@"\w+").Shuffle();
            char separator = separators.Sample();
            result = string.Join(separator.ToString(), parts).ToLower();
         }
         else {
            var candidates = new List<string> {
               Name.FirstName().Prepare(),
               $"{Name.FirstName()}{separators.Sample()}{Name.LastName()}".Prepare()
            };
            result = candidates.Sample();
         }
         return result;
      }

      /// <summary>
      /// Generates a user name with minimum length. Example: UserName(8) may return "bernhard_schiller"
      /// </summary>
      /// <param name="minLength">Minimal length of the user name.</param>
      /// <returns></returns>
      public static string UserName(int minLength) {
         if(minLength > 1000000) {
            throw new ArgumentOutOfRangeException(nameof(minLength), "Must be equal to or less than 10^6.");
         }

         var separators = "._".ToCharArray();
         var tries = 0;
         string result = null;
         do {
            result = UserName(null, separators);
            tries++;
         } while (result?.Length < minLength && tries < 7);
         if(minLength > 0) {
            result = string.Join("", (minLength / result.Length + 1).Times(x => result));
         }
         return result;
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
