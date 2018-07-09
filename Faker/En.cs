using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using YamlDotNet.Serialization;

namespace RimuTec.Faker {
   internal static class En {
      // Resources used by this class from https://github.com/stympy/faker/blob/master/lib/locales/en.yml

      static En() {
         const string yamlFileName = "RimuTec.Faker.locales.en.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _en = locale.en;
      }

      public static char Multibyte() {
         return _en.multibyte.Sample();
      }

      private static en _en;

#pragma warning disable IDE1006 // Naming Styles
      // Helper classes for reading the yaml file. Note that the class names are
      // intentionally lower case.

      internal class locale {
         public en en { get; set; }
      }

      internal class en {
         public time time { get; set; }

         [YamlMember(Alias = "multibyte", ApplyNamingConventions = false)]
         public char[] multibyte { get; set; }

         public faker faker { get; set; }
      }

      internal class time {
         public formats formats { get; set; }
         public string am { get; set; }
         public string pm { get; set; }
      }


      internal class faker {
         [YamlMember(Alias = "separator", ApplyNamingConventions = false)]
         public string separator { get; set; }
      }

      internal class formats {
         [YamlMember(Alias = "us", ApplyNamingConventions = false)]
         public string us { get; set; }
      }

#pragma warning restore IDE1006 // Naming Styles
   }
}
