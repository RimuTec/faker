using System.IO;
using System.Reflection;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RimuTec.Faker.Helper
{
   internal static class YamlLoader
   {
      internal static T Read<T>(string yamlFileName, params IYamlTypeConverter[] converters)
      {
         T locale;
         var executingAssembly = Assembly.GetExecutingAssembly();
         var resourceNames = executingAssembly.GetManifestResourceNames();
         using (var resourceStream = executingAssembly.GetManifestResourceStream(yamlFileName))
         {
            using (var textReader = new StreamReader(resourceStream))
            {
               DeserializerBuilder deserializerBuilder = new DeserializerBuilder();
               foreach (var converter in converters)
               {
                  deserializerBuilder = deserializerBuilder.WithTypeConverter(converter);
               }
               var deserializer = deserializerBuilder
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
