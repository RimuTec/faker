using System.IO;
using System.Reflection;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RimuTec.Faker.Helper {
   internal static class YamlLoader {
      internal static T Read<T>(string yamlFileName) {
         T locale;
         var executingAssembly = Assembly.GetExecutingAssembly();
         var resourceNames = executingAssembly.GetManifestResourceNames();
         using (var resourceStream = executingAssembly.GetManifestResourceStream(yamlFileName)) {
            using (var textReader = new StreamReader(resourceStream)) {
               var deserializer = new DeserializerBuilder()
                  .WithNamingConvention(new CamelCaseNamingConvention())
                  .Build()
                  ;
               locale = deserializer.Deserialize<T>(textReader.ReadToEnd());
            }
         }
         return locale;
      }
   }
}
