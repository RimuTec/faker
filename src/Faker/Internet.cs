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
         _freeEmail = _internet.FreeEmail;
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
      /// Generates an email address, optionally with a specific and/or separator. Example: "eliza@mann.net"
      /// </summary>
      /// <param name="name">Name to use for the email address. Default value is null in which case a random name will be used.</param>
      /// <param name="separators">String with separators. Default is "._". Separator is used between first and last name.</param>
      /// <returns></returns>
      public static string Email(string name = null, string separators = null) {
         return string.Join("@", UserName(name, separators), DomainName());
      }

      /// <summary>
      /// Generates an email address at a free email provider, optionally with a specific name. 
      /// Examples: FreeEmail() => "freddy@gmail.com", FreeEmail("Nancy") => "nancy@yahoo.com"
      /// </summary>
      /// <param name="name">Name to use. Default is null in which case a random name will be used.</param>
      /// <returns></returns>
      public static string FreeEmail(string name = null) {
         if (string.IsNullOrEmpty(name)) {
            name = null;
         }
         return string.Join("@", UserName(name), _internet.FreeEmail.Sample());
      }

      /// <summary>
      /// Generates a user name. Examples: "alexie", "johnson-nancy"
      /// </summary>
      /// <param name="name">Name to be used instead of a random one.</param>
      /// <param name="separators">Separators to consider. May not appear in result. Default are '.' and '_'.</param>
      /// <returns></returns>
      public static string UserName(string name = null, string separators = null) {
         string result;
         if (separators == null) {
            separators = "._"; // if space is sampled, it will be removed by Prepare()
         }
         var separator = separators.Sample();
         if (name != null) {
            IEnumerable<string> parts = name.Scan(@"\w+").Shuffle();
            result = string.Join(separator, parts).ToLower();
         }
         else {
            var candidates = new List<string> {
               Name.FirstName().Prepare(),
               $"{Name.FirstName().Prepare()}{separator}{Name.LastName().Prepare()}"
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
         if(minLength <= 0) {
            throw new ArgumentOutOfRangeException(nameof(minLength), "Must be greater than zero.");
         }
         var tries = 0;
         string result = null;
         do {
            result = UserName(null, "._");
            tries++;
         } while (result?.Length < minLength && tries < 7);
         if(minLength > 0) {
            result = string.Join("", (minLength / result.Length + 1).Times(x => result));
         }
         return result;
      }

      /// <summary>
      /// Generates a user name with minimum and maximum length. Example: UserName(5, 8) may return ""melany""
      /// </summary>
      /// <param name="minLength">Minimum length of the user name.</param>
      /// <param name="maxLength">Maximum length of the user name.</param>
      /// <returns></returns>
      public static string UserName(int minLength, int maxLength = int.MaxValue) {
         if(maxLength < minLength) {
            throw new ArgumentOutOfRangeException($"{nameof(maxLength)} must be equal to or greater than {nameof(minLength)}.", (Exception) null);
         }
         var tries = 0;
         string result = null;
         do {
            result = UserName(minLength);
            tries++;
         } while (result.Length > maxLength && tries < 7);
         return result.Substring(0, Math.Min(maxLength, result.Length));
      }

      internal static string[] _freeEmail;

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
