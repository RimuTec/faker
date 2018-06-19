using RimuTec.Faker.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RimuTec.Faker {
   public static class PhoneNumber {
      static PhoneNumber() {
         var executingAssembly = Assembly.GetExecutingAssembly();
         var resourceNames = executingAssembly.GetManifestResourceNames();
         using (var resourceStream = executingAssembly.GetManifestResourceStream("RimuTec.Faker.locales.en.phone_number.yml")) {
            using (var textReader = new StreamReader(resourceStream)) {
               var deserializer = new DeserializerBuilder()
                  .WithNamingConvention(new CamelCaseNamingConvention())
                  .Build()
                  ;
               _locale = deserializer.Deserialize<locale>(textReader.ReadToEnd());
            }
         }
      }

      public static string CellPhone() {
         var numberTemplate = _locale.en.faker.CellPhone.Formats.Random();
         return numberTemplate.Numerify();
      }

      private static locale _locale;

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
         [YamlMember(Alias = "phone_number", ApplyNamingConventions = false)]
         public phone_number PhoneNumber { get; set; }

         [YamlMember(Alias = "cell_phone", ApplyNamingConventions = false)]
         public cell_phone CellPhone { get; set; }
      }

      internal class phone_number {
         [YamlMember(Alias = "formats", ApplyNamingConventions = false)]
         public string[] Formats { get; set; }
      }

      internal class cell_phone {
         [YamlMember(Alias = "formats", ApplyNamingConventions = false)]
         public string[] Formats { get; set; }
      }

#pragma warning restore IDE1006 // Naming Styles
   }
}
